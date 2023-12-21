module AoCFSharp.Lib.Tree

type 'a tree =
    | Empty
    | Node of 'a * 'a tree * 'a tree

module Tree = 
    let inline head tree =
        match tree with 
        | Empty -> failwith "Empty tree"
        | Node(x, _, _) -> x

    [<TailCall>]
    let rec contains tree item =
        match tree with 
        | Empty -> false
        | Node(head, l, r) ->
            if head = item then true
            elif item < head then contains l item
            else contains r item

    [<TailCall>]
    let rec insert tree item =
        match tree with 
        | Empty -> Node(item, Empty, Empty)
        | Node(head, l, r) as node ->
            if head = item then node (* no change neccessary *)
            elif item < head then Node(head, insert l item, r) (* replace current node with new node where we insert value left*)
            else Node(head, l, insert r item) (* replace current node with new node where we insert value right*)
        
   