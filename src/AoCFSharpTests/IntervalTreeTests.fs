module AoCFSharpTests.IntervalTreeTests

open AoCFSharp.Lib.IntervalTree.IntervalTree
open NUnit.Framework

module IntervalTreeTests =

    [<Test>]
    let ``When empty, nothing intersects`` () =
        let tree = IntervalTree<int>.empty
        Assert.That(tree.intersects (1, 3), Is.False)
        Assert.That(tree.intersects (2, 4), Is.False)
        Assert.That(tree.intersects (2, 3), Is.False)

    [<Test>]
    let ``Insert (1,2), after that (1,3) intersects`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1, 2)
        Assert.That(tree.intersects (1, 3), Is.True)

    [<Test>]
    let ``When (1,2) and (3,4) inserted, (1,3), (2,4), (2,3) and (0,5) intersect`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1, 2)
        tree <- tree.insert (3, 4)
        Assert.That(tree.intersects (1, 3), Is.True)
        Assert.That(tree.intersects (2, 4), Is.True)
        Assert.That(tree.intersects (2, 3), Is.True)
        Assert.That(tree.intersects (0, 5), Is.True)

    [<Test>]
    let ``When (1,2) and (3,4) inserted, (0,0) and (5,5) do not intersect`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1, 2)
        tree <- tree.insert (3, 4)
        Assert.That(tree.intersects (0, 0), Is.False)
        Assert.That(tree.intersects (5, 5), Is.False)

    [<Test>]
    let ``Insert (1,2) then (1,3), after that (3,4) intersects`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1, 2)
        Assert.That(tree.intersects (3, 4), Is.False)
        tree <- tree.insert (1, 3)
        Assert.That(tree.intersects (3, 4), Is.True)

    [<Test>]
    let ``Insert (1,2) then (0,1), after that (0,0) intersects`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1, 2)
        Assert.That(tree.intersects (0, 0), Is.False)
        tree <- tree.insert (0, 1)
        Assert.That(tree.intersects (0, 0), Is.True)

    [<Test>]
    let ``Insert (1,2) then (3,5), after that head has max=5`` () =
        let mutable tree = IntervalTree<int>.empty
        tree <- tree.insert (1, 2)
        tree <- tree.insert (3, 5)
        Assert.That(tree.head.max, Is.EqualTo(5))
