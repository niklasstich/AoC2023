module AoCFSharp.Lib.IntervalTree

open AoCFSharp.Lib.Tree

module IntervalTree =

    type 'a IntervalTreeNode = { lower: 'a; upper: 'a; max: 'a }

    let intersects' head (bl, bu) = max head.lower bl <= min head.upper bu

    let rec insert tree interval =
        let lo, hi = interval

        if lo > hi then
            insert tree (hi, lo)
        else
            match tree with
            | Empty -> Node({ lower = lo; upper = hi; max = hi }, Empty, Empty)
            | Node(head, l, r) ->
                if head.lower = lo then
                    Node(
                        { lower = lo
                          upper = max head.upper hi
                          max = max head.max hi },
                        l,
                        r
                    )
                elif lo < head.lower then
                    Node(head, insert l interval, r)
                else
                    Node({ head with max = max head.max hi }, l, insert r interval)

    let rec intersects tree interval =
        match tree with
        | Empty -> false
        | Node(head, l, r) ->
            match (l, interval) with
            | _ when intersects' head interval -> true
            | Empty, _ -> intersects r interval
            | Node(headInner, _, _), (lo, _) when headInner.max < lo -> intersects r interval
            | _ -> intersects l interval

    type IntervalTree<'a when 'a: comparison>(inner: 'a IntervalTreeNode tree) =
        member this.head = Tree.head inner
        member this.intersects interval = intersects inner interval

        member this.insert interval : IntervalTree<'a> = IntervalTree(insert inner interval)

        static member empty = IntervalTree(Empty)
