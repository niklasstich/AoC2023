module AoCFSharp.Days.Day04

open System.Text.RegularExpressions
open AoCFSharp
open Helpers

type SplitLine = string * string

let throwAwayCardInfo (s:string) = s.Split([|':'|])[1]
let splitOnPipe (s:string) =
    let splits = s.Split([|'|'|])
    (splits.[0], splits.[1])
    
let findInts (left, right) =
    let regex =
        let regexString = @"(\d+)"
        Regex(regexString)
    let leftMatches = regex.Matches left |> Seq.map (fun m -> m.Value) |> Seq.map int
    let rightMatches = regex.Matches right |> Seq.map (fun m -> m.Value) |> Seq.map int
    (leftMatches, rightMatches)

let getNumberOfWins (left, right) =
    crossProduct left right |> Seq.filter (fun (l,r) -> l = r) |> Seq.length
let getPoints wins = wins - 1 |> pown 2

type State = int * int array

let part2Folder ((i,a): State) r =
    for j in (i + 1) .. (i + r) do
        a[j] <- a[j] + a[i]
    (i + 1, a)

let example =
    [|"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53";
    "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19";
    "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1";
    "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83";
    "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36";
    "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"|]

let part1 (input: string array) =
    input |> Array.map (throwAwayCardInfo >> splitOnPipe >> findInts >> getNumberOfWins >> getPoints) |> Array.sum |> string
    
let part2 (input: string array) =
    let rowsWithWins = input |> Array.map (throwAwayCardInfo >> splitOnPipe >> findInts >> getNumberOfWins)
    let state = (0, [|for _ in 1 .. rowsWithWins.Length -> 1|])
    rowsWithWins |> Array.fold part2Folder state |> snd |> Array.sum |> string
    
let solution1 = 25571
let solution2 = 8805731