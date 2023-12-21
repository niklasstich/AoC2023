module AoCFSharp.Days.Day05

open System.Text.RegularExpressions
open AoCFSharp
open AoCFSharp.Lib.IntervalDataTree
open AoCFSharp.Lib.IntervalDataTree.IntervalDataTree
open Helpers

type MapEntry =
    { start: int64
      ``end``: int64
      delta: int64 }

type OutputTuple = int64 * int64
type SeedRange = int64 * int64

let digitRegex =
    let regexString = @"(\d+)"
    Regex(regexString)

let parseSeedLine line =
    line |> digitRegex.Matches |> Seq.map ((fun m -> m.Value) >> int64)


let parseMapLine line =
    let matches =
        line
        |> digitRegex.Matches
        |> Seq.map ((fun m -> m.Value) >> int64)
        |> Seq.toArray

    { start = matches[1]
      ``end`` = matches[1] + matches[2] - 1L
      delta = matches[0] - matches[1] }

let parseMaps (lines: string array) =
    let mutable lines' = lines
    let mutable mapTrees = []

    let insertMapLineIntoTree (tree: IntervalDataTree<'a,'b>) (mapLine: MapEntry) =
        tree.insert (mapLine.start, mapLine.``end``) mapLine.delta

    while (lines'.Length > 0) do
        let block = lines' |> Array.takeWhile (fun l -> l <> "\r" && l <> "")
        lines' <- lines' |> Array.skip (min (block.Length + 1) lines'.Length)

        mapTrees <-
            (block
             |> Array.skip 1
             |> Array.map parseMapLine
             |> Array.fold insertMapLineIntoTree IntervalDataTree<int64,int64>.empty)
            :: mapTrees

    mapTrees |> List.rev

let processMaps (trees: IntervalDataTree<'a,'b> list) seed =
    // let unpackDelta me =
    //     match me with
    //     | Some me -> me.delta
    //     | None -> 0L
    //
    // let listfolder acc map =
    //     map |> Array.tryFind (fun me -> acc >= me.start && acc < (me.start + me.``end``))
    //         |> unpackDelta
    //         |> (+) acc
    //
    // let location =
    //     // maps |> List.map (fun map -> map |> Array.tryFind (fun me -> seed >= me.start && seed < (me.start + me.count)))
    //     //                      |> List.fold (fun acc me -> acc + (unpackDelta me)) seed
    //     maps |> List.fold listfolder seed
    let folder seed (tree: IntervalDataTree<'a,'b>) =
        match tree.getIntersection (seed, seed) with
        | None -> seed
        | Some((_, _), delta) -> seed + delta

    let location = trees |> List.fold folder seed
    location

let processMapsRanges (maps: IntervalDataTree<'a,'b> list) (seedrange: int64 * int64) =
    let folder (seedranges: (int64 * int64) list) (tree: IntervalDataTree<'a,'b>) =
        seedranges
        |> List.collect (fun (sl, su) ->
            match tree.getIntersection (sl, su) with
            | None -> [ (sl, su) ]
            | Some((ml, mu), delta) ->
                if (ml = sl && mu = su) then
                    [ (sl + delta), (su + delta) ]
                elif (ml = sl) then
                    [((sl+delta), (mu+delta));((mu+1L), su)]
                elif (mu = su) then
                    [su, ml; ml+1L+delta, mu+delta]
                else
                    [su, ml; ml+1L+delta, mu+delta; mu+1L, su]
            )

    let location = maps |> List.fold folder [ seedrange ]
    (seedrange,location)

let part1 (input: string array) =
    let seeds = parseSeedLine input[0]
    let rest = Array.skip 2 input
    let maps = parseMaps rest

    seeds |> Seq.map (processMaps maps) |> Seq.min |> string

let part2' (input: string array) =
    let seedRanges =
        parseSeedLine input[0]
        |> Seq.chunkBySize 2
        |> Seq.map (fun s -> (s[0], s[0] + s[1] - 1L))

    let rest = Array.skip 2 input
    let maps = parseMaps rest

    let enumerable = seedRanges |> Seq.map (processMapsRanges maps)
    enumerable |> Seq.map fst |> Seq.min |> string


let example =
    @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4"
let part2 (input: string array) =
    //example.Split '\n' |> part2'
    ()