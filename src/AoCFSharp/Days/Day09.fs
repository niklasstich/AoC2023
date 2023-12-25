module AoCFSharp.Days.Day09

open AoCFSharp
open AoCFSharp.Lib
open Polynomial
open Helpers

let parseLine (line: string) = line.Split " " |> Array.map float

let interpolateAndCalc data i =
    match interpolate data with
    | Ok p -> p i |> round |> Ok
    | Error e -> Error e

let toOutput =
    Array.fold
        (fun acc x ->
            match x with
            | Ok x -> acc + x
            | Error e -> failwith e)
        0.0
    >> int64
    >> string

let part1 (input: string array) =
    input
    |> Array.map (
        parseLine
        >> Seq.mapi (fun i n -> (float i, n))
        >> (fun s -> (s, s |> Seq.length |> float))
        >> uncurry interpolateAndCalc
    )
    |> toOutput

let part2 (input: string array) =
    input
    |> Array.map (
        parseLine
        >> Seq.mapi (fun i n -> (float i, n))
        >> (fun s -> (s, -1.0))
        >> uncurry interpolateAndCalc
    )
    |> toOutput

let solution1 = 1972648895
let solution2 = 919