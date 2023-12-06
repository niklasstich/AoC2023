module AoCFSharpTests

open AoCFSharp.Lib.IntervalTree.IntervalTree
open NUnit.Framework

module IntervalTreeTests =

    [<SetUp>]
    let Setup () =
        ()

        
    [<Test>]
    let ``When empty, nothing intersects`` () =
        let tree = IntervalTree<int>.empty
        Assert.AreEqual (tree.intersects (1,3), false)
        Assert.AreEqual (tree.intersects (2,4), false)
        Assert.AreEqual (tree.intersects (2,3), false)
        
    [<Test>]
    let ``Insert (1,2), after that (1,3) intersects`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1,2)
        Assert.AreEqual (tree.intersects (1,3), true)
        
    [<Test>]
    let ``When (1,2) and (3,4) inserted, (1,3), (2,4), (2,3) and (0,5) intersect`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1,2)
        tree <- tree.insert (3,4)
        Assert.AreEqual (tree.intersects (1,3), true)
        Assert.AreEqual (tree.intersects (2,4), true)
        Assert.AreEqual (tree.intersects (2,3), true)
        Assert.AreEqual (tree.intersects (0,5), true)
    
    [<Test>]    
    let ``When (1,2) and (3,4) inserted, (0,0) and (5,5) do not intersect`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1,2)
        tree <- tree.insert (3,4)
        Assert.AreEqual (tree.intersects (0,0), false)
        Assert.AreEqual (tree.intersects (5,5), false)   
        
    [<Test>]
    let ``Insert (1,2) then (1,3), after that (3,4) intersects`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1,2)
        Assert.AreEqual (tree.intersects (3,4), false)
        tree <- tree.insert (1,3)
        Assert.AreEqual (tree.intersects (3,4), true)
       
       
    [<Test>]
    let ``Insert (1,2) then (0,1), after that (0,0) intersects``() =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1,2)
        Assert.AreEqual (tree.intersects (0,0), false)
        tree <- tree.insert (0,1)
        Assert.AreEqual (tree.intersects (0,0), true)
       
       
    [<Test>]
    let ``Insert (1,2) then (3,5), after that head has max=5`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1,2)
        tree <- tree.insert (3,5)
        Assert.AreEqual (tree.head.max, 5)