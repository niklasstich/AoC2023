module AoCFSharp.Days.Day07

open AoCFSharp
open Helpers

let cardOrder = "23456789TJQKA"
let newCardOrder = "J23456789TQKA"

let getTypesPartOne s =
    s
    |> Seq.countBy id
    |> Seq.map snd
    |> Seq.sortDescending
    |> Seq.map string
    |> String.concat ""

let getTypesPartTwo (s: string) =
    let counts = s |> Seq.countBy id |> Seq.sortByDescending snd |> Seq.toArray

    let jokerCount =
        match counts |> Array.tryFind (fun (c, _) -> c = 'J') with
        | Some c -> snd c
        | None -> 0

    match jokerCount with
    | 5 -> "5"
    | _ ->
        let counts =
            counts |> Array.filter (fun (c, _) -> c <> 'J') |> Array.sortByDescending snd
        counts[0] <- (fst counts[0], snd counts[0] + jokerCount)
        counts |> Array.map (snd >> string) |> String.concat ""

let parseLine getTypesFunc (line: string) =
    line.Split([| ' ' |]) |> (fun a -> (a[0], int (a[1]), getTypesFunc a[0]))

let getCardRanks (orderStr: string) (cards: string, _, _) =
    cards |> Seq.map (orderStr.IndexOf >> toHexString) |> String.concat ""

let part1 (input: string array) =
    input
    |> Array.map (parseLine getTypesPartOne)
    |> Array.toSeq
    |> Seq.sortBy (getCardRanks cardOrder)
    |> Seq.sortBy (fun (_, _, t) -> t)
    |> Seq.mapi (fun i (_, bet, _) -> (i + 1) * bet)
    |> Seq.sum
    |> string

let part2 (input: string array) =
    input
    |> Array.map (parseLine getTypesPartTwo)
    |> Array.toSeq
    |> Seq.sortBy (getCardRanks newCardOrder)
    |> Seq.sortBy (fun (_, _, t) -> t)
    |> Seq.mapi (fun i (_, bet, _) -> (i + 1) * bet)
    |> Seq.sum
    |> string
