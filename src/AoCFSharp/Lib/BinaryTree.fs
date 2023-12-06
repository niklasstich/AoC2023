module AoCFSharp.Lib.BinaryTree

open AoCFSharp.Lib.Tree

module BinaryTree =

    type BinaryTree<'a when 'a: comparison>(inner: 'a tree) =

        member this.head = Tree.head inner
        member this.contains = Tree.contains inner
        member this.insert = Tree.insert inner
        static member empty = BinaryTree<'a>(Empty)
