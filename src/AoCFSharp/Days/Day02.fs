module AoCFSharp.Days.Day02

open System.Text.RegularExpressions
open AoCFSharp
open Helpers


type LineMax =
    { redMax: int
      greenMax: int
      blueMax: int }

let groupRegex =
    let regexString = @"(\d*) (red|green|blue)"
    Regex(regexString)

let findMaximums (line: string) : LineMax =
    let folder (max: LineMax) (m: Match) =
        let value = int m.Groups[1].Value |> int

        match m.Groups[2].Value with
        | "red" ->
            if (max.redMax < value) then
                { max with redMax = value }
            else
                max
        | "green" ->
            if (max.greenMax < value) then
                { max with greenMax = value }
            else
                max
        | "blue" ->
            if (max.blueMax < value) then
                { max with blueMax = value }
            else
                max
        | _ -> failwith "impossible"

    let seed =
        { redMax = 0
          greenMax = 0
          blueMax = 0 }

    groupRegex.Matches(line) |> Seq.cast |> Seq.fold folder seed

let maxAllowed =
    { redMax = 12
      greenMax = 13
      blueMax = 14 }

let isAllowed (_, linemax) =
    linemax.redMax <= maxAllowed.redMax
    && linemax.greenMax <= maxAllowed.greenMax
    && linemax.blueMax <= maxAllowed.blueMax

let part1 input =
    input
    |> Array.map findMaximums
    |> Array.mapi (fun i x -> (i + 1, x))
    |> Array.where isAllowed
    |> Array.map fst
    |> Array.sum
    |> string

let part2 input =
    input
    |> Array.map (findMaximums >> (fun x -> x.blueMax * x.greenMax * x.redMax))
    |> Array.sum
    |> string
