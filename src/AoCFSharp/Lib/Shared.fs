module AoCFSharp.Lib.Shared

// module Shared =
let intersects' (lower, upper) (bl, bu) = max lower bl <= min upper bu
let intersection (lower, upper) (bl, bu) = (max lower bl, min upper bu)
