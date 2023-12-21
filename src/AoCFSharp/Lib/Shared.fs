module AoCFSharp.Lib.Shared

open System

// module Shared =
let intersects' (lower, upper) (bl, bu) = max lower bl <= min upper bu
    
let intersection (lower: 'a, upper: 'a) (bl: 'a, bu: 'a): 'a * 'a =
    let left: 'a = max lower bl
    let right: 'a = (min upper bu)
    (left, right)
