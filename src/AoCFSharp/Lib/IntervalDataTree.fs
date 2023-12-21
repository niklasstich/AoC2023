module AoCFSharp.Lib.IntervalDataTree

open Tree
open Shared

module IntervalDataTree =
    type IntervalTreeNode<'a, 'b> =
        { lower: 'a
          upper: 'a
          max: 'a
          data: 'b }

    let rec insert tree interval data =
        let lo, hi = interval

        if lo > hi then
            insert tree (hi, lo) data
        else
            match tree with
            | Empty ->
                Node(
                    { lower = lo
                      upper = hi
                      max = hi
                      data = data },
                    Empty,
                    Empty
                )
            | Node(head, l, r) ->
                if head.lower = lo then
                    Node(
                        { lower = lo
                          upper = max head.upper hi
                          max = max head.max hi
                          data = head.data },
                        l,
                        r
                    )
                elif lo < head.lower then
                    Node(head, insert l interval data, r)
                else
                    Node({ head with max = max head.max hi }, l, insert r interval data)

    let rec intersects tree interval =
        match tree with
        | Empty -> false
        | Node(head, l, r) ->
            match (l, interval) with
            | _ when intersects' (head.lower, head.upper) interval -> true
            | Empty, _ -> intersects r interval
            | Node(headInner, _, _), (lo, _) when headInner.max < lo -> intersects r interval
            | _ -> intersects l interval
    
    let rec getIntersection tree interval =
        match tree with
        | Empty -> None
        | Node(head, l, r) ->
            match (l, interval) with
            | _ when intersects' (head.lower, head.upper) interval -> Some((intersection (head.lower, head.upper) interval), head.data)
            | Empty, _ -> getIntersection r interval
            | Node(headInner, _, _), (lo, _) when headInner.max < lo -> getIntersection r interval
            | _ -> getIntersection l interval


    type IntervalDataTree<'a, 'b when 'a: comparison>(inner: IntervalTreeNode<'a, 'b> tree) =
        member this.head = Tree.head inner
        member this.intersects interval = intersects inner interval
        member this.getIntersection interval = getIntersection inner interval

        member this.insert interval data : IntervalDataTree<'a, 'b> =
            IntervalDataTree(insert inner interval data)

        static member empty = IntervalDataTree<'a, 'b>(Empty)
