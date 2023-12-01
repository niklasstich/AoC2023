module AoCFSharp.Days.Day01

open AoCFSharp
open Helpers

let isDigit c = c >= '0' && c <= '9'

let filterStringToIntSeq s =
    s |> String.filter isDigit |> Seq.toList

let takeBothEnds (l: char list) =
    (int (List.head l - '0')) * 10 + int (List.last l - '0')

let digitMap =
    [| ("one", "o1ne")
       ("two", "t2wo")
       ("three", "thr3ee")
       ("four", "fo4ur")
       ("five", "fi5ve")
       ("six", "si6x")
       ("seven", "se7ven")
       ("eight", "eig8ht")
       ("nine", "ni9ne") |]
    |> Map.ofArray

let replaceWordsWithDigits (line: string) =
    let rp (s: string) (o: string) (n: string) = s.Replace(o, n)
    digitMap |> Map.fold rp line


let part1 (input: string) =
    input
    |> splitLines
    |> Array.map (filterStringToIntSeq >> takeBothEnds >> int)
    |> Array.sum
    |> string

let part2 (input: string) =
    input
    |> splitLines
    |> Array.map (replaceWordsWithDigits >> filterStringToIntSeq >> takeBothEnds >> int)
    |> Array.sum
    |> string


let solution1 = 53651
