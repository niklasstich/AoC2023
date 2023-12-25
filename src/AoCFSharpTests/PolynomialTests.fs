module AoCFSharpTests.PolynomialTests

open System
open AoCFSharp.Lib
open NUnit.Framework
open Polynomial
open FsUnit

module InterpolateTests =

    let test data expectedValues =
        let res = interpolate data

        match res with
        | Error e -> e |> failwith e
        | Ok f ->
            expectedValues
            |> Seq.map (fst >> (fun x -> (x, Math.Round(f x))))
            |> should equalSeq expectedValues

    [<Test>]
    let ``Constant function f(x)=1`` () =
        let data = [| (0.0, 1.0); (1, 1); (2, 1) |] |> Array.toSeq
        let expectedValues = [| (0.0, 1.0); (1, 1); (2, 1); (3, 1); (4, 1) |] |> Array.toSeq
        test data expectedValues

    [<Test>]
    let ``Linear function f(x)=x+1`` () =
        let data = [| (0.0, 1.0); (1, 2); (2, 3) |] |> Array.toSeq
        let expectedValues = [| (0.0, 1.0); (1, 2); (2, 3); (3, 4); (4, 5) |] |> Array.toSeq
        test data expectedValues

    [<Test>]
    let ``Quadratic function f(x)=-4x^2+2x+1`` () =
        let data = [| (0.0, 1.0); (1, -1); (2, -11) |] |> Array.toSeq

        let expectedValues =
            [| (0.0, 1.0); (1, -1); (2, -11); (3, -29); (4, -55) |] |> Array.toSeq

        test data expectedValues

    [<Test>]
    let ``Cubic function f(x)=x^3-2x^2+3x-4`` () =
        let data = [| (-2.0, -26.0); (-1, -10); (0, -4); (1, -2) |] |> Array.toSeq

        let expectedValues =
            [| (-3.0, -58.0); (-2, -26); (-1, -10); (0, -4); (1, -2); (2, 2) |]
            |> Array.toSeq

        test data expectedValues

    [<Test>]
    let ``AoC Day 09 Example 1`` () =
        let data =
            [| 0; 3; 6; 9; 12; 15 |]
            |> Array.mapi (fun i x -> (float i, float x))
            |> Array.toSeq

        let expectedValues =
            [| (6.0, 18.0); (7, 21); (8, 24); (9, 27); (10, 30) |] |> Array.toSeq

        test data expectedValues

    [<Test>]
    let ``AoC Day 09 Example 2`` () =
        let data =
            [| 1; 3; 6; 10; 15; 21 |]
            |> Array.mapi (fun i x -> (float i, float x))
            |> Array.toSeq

        let expectedValues = [| (6.0, 28.0); (7.0, 36.0); (8.0, 45.0) |] |> Array.toSeq

        test data expectedValues

    [<Test>]
    let ``AoC Day 09 Example 3`` () =
        let data =
            [|10; 13; 16; 21; 30; 45|]
            |> Array.mapi (fun i x -> (float i, float x))
            |> Array.toSeq
            
        let expectedValues = [| (6.0, 68.0); (7.0, 101); (8.0, 146) |] |> Array.toSeq
        
        test data expectedValues