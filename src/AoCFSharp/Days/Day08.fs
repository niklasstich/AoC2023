module AoCFSharp.Days.Day08

open AoCFSharp
open Helpers

type Instruction =
    | Left
    | Right

let parseInstructions line =
    let parseChar =
        function
        | 'L' -> Left
        | 'R' -> Right
        | _ -> failwith "Invalid instruction"

    line |> Seq.map parseChar |> Seq.toArray

let parseMap (line: string) = (line[..2], (line[7..9], line[12..14]))

[<TailCall>]
let rec processInstructions instructions (map: Map<string, string * string>) i node eval =
    let mapEntry = map[node]
    let instructionLen = instructions |> Array.length

    let nextNode =
        match instructions[i % instructionLen] with
        | Left -> fst mapEntry
        | Right -> snd mapEntry

    if eval nextNode then
        i + 1
    else
        processInstructions instructions map (i + 1) nextNode eval

let rec part1eval = (=) "ZZZ"

let part2eval (nextNode: string) = nextNode.EndsWith "Z"

let part1 (input: string array) =
    let instructions = input |> Array.take 1 |> Array.collect parseInstructions
    let map = input |> Array.skip 2 |> Array.map parseMap |> Map
    processInstructions instructions map 0 "AAA" part1eval |> string

(*
For part two, we need to find out how many steps it takes so that ALL starting nodes end up at a node that ends with Z.
This means we can just find the length of the cycle for each node that ends with an A and then find the least common multiple of all of them.
This is possible because the input map makes loops.
*)
let part2 (input: string array) =
    let instructions = input |> Array.take 1 |> Array.collect parseInstructions
    let tuples = input |> Array.skip 2 |> Array.map parseMap
    let map = tuples |> Map

    let startingPoints =
        tuples |> Array.map fst |> Array.filter (fun k -> k.EndsWith "A")

    let loopLens =
        startingPoints
        |> Array.map ((fun node -> processInstructions instructions map 0 node part2eval) >> int64)

    let firstLoopLen = loopLens[0]
    loopLens |> Array.skip 1 |> Array.fold lcm64 firstLoopLen |> string

let solution1 = 24253
let solution2 = 12357789728873L
