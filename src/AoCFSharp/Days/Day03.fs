module AoCFSharp.Days.Day03

open System.Text.RegularExpressions
open AoCFSharp
open Helpers

type SymbolPosition = int * int * char
type NumberPosition = int * int * int

let findSymbols ((lineIdx: int, line: string)) : SymbolPosition array =
    let isSymbol c =
        match c with
        | '.' -> false
        | _ when c >= '0' && c <= '9' -> false
        | _ -> true

    line
    |> Seq.mapi (fun i c -> (i, c))
    |> Seq.filter (fun (_, c) -> isSymbol c)
    |> Seq.map (fun (i, c) -> (i, lineIdx, c))
    |> Seq.toArray


let findNumbers ((lineIdx: int, line: string)) : NumberPosition array =
    let regexString = @"(\d+)"
    let regex = Regex regexString

    let matchMapper (m: Match) =
        (m.Index, lineIdx, (int m.Groups[1].Value))

    let matchCollection = line |> regex.Matches
    matchCollection |> Seq.map matchMapper |> Seq.toArray

let areNeighbors ((ax: int, ay: int), (bx: int, by: int, n)) =
    let digits = countDigitsInNumber n
    let xDif = bx - ax

    if xDif < 0 then
        abs xDif <= digits && abs (ay - by) <= 1
    else
        abs xDif <= 1 && abs (ay - by) <= 1

let findNumbersNextToSymbols symbols numbers =
    crossProduct symbols numbers |> Seq.filter areNeighbors

let findGearRatios (symbols: SymbolPosition array) (numbers: NumberPosition array) =
    let numsNextToGears (numbers: NumberPosition array) (symbol: SymbolPosition) =
        crossProductArray [|symbol|] numbers |> Array.map (fun ((ax,ay,_),(bx,by,n))->((ax,ay),(bx,by,n))) |> Array.filter areNeighbors |> Array.map snd
    let addTuples (a: NumberPosition array) =
        let _,_,n1 = a[0]
        let _,_,n2 = a[1]
        n1 * n2
        
    symbols |> Array.map (numsNextToGears numbers) |> Array.filter (fun a -> Array.length a = 2) |> Array.map addTuples

let example =
    @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."

let part1 (input: string array) =
    let numbersNextToSymbols =
        input
        |> Array.mapi (fun i line -> (i, line))
        |> Array.collect findSymbols
        |> Array.map (fun (x, y, c) -> (x, y))
        |> findNumbersNextToSymbols
        <| (input |> Array.mapi (fun i line -> (i, line)) |> Array.collect findNumbers)

    numbersNextToSymbols
    |> Seq.map (fun ((_, _), (_, _, n)) -> n)
    |> Seq.sum
    |> string



let part2 (input: string array) =
    let numbers =
        (input |> Array.mapi (fun i line -> (i, line)) |> Array.collect findNumbers)

    (input
    |> Array.mapi (fun i line -> (i, line))
    |> Array.collect findSymbols
    |> Array.where (fun (_, _, c) -> c = '*')
    |> findGearRatios
    <| numbers) |> Array.sum |> string

let solution1 = 559667


let debug = example |> splitLines |> part2
