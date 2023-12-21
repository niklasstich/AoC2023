module AoCFSharp.Days.Day06

open System
open AoCFSharp.Lib
open Shared

let input' = [| (35L, 212L); (93L, 2060L); (73L, 1201L); (66L, 1044L) |]
let input'' = (35937366L, 212206012011044L)

let solveCountAsQuadraticEquation (t: int64, d: int64) =
    //solve x*(t-x)-d=0 and return distance of those points (rounded up because discrete time values)
    let x1, x2 = quadraticEq -1.0 (float t) (float (-d))
    let x1' = Math.Ceiling(x1)
    let x2' = Math.Ceiling(x2)
    int64(max x1' x2' - min x1' x2')
 
// naive linear solution   
// let mapTimeToDistances (i, d) =
//     (seq { for j in 0..i -> (j * (i - j)) }, d)
//     
// let countWinners (d, target) =
//     d |> Seq.where (fun d' -> d' > target) |> Seq.length

let part1 (input: string array) =
    input' |> Array.map solveCountAsQuadraticEquation |> Array.fold (*) 1L |> string
    
let part2 (input: string array) =
    input'' |> solveCountAsQuadraticEquation |> string

let solution1 = 114400