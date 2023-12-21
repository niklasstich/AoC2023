module AoCFSharp.Lib.Shared

open System

// module Shared =
let intersects' (lower, upper) (bl, bu) = max lower bl <= min upper bu
    
let intersection (lower: 'a, upper: 'a) (bl: 'a, bu: 'a): 'a * 'a =
    let left: 'a = max lower bl
    let right: 'a = (min upper bu)
    (left, right)

(*
    Standard quadratic equation (Mitternachtsformel)
*)
let quadraticEq a b c =
    let root = sqrt (b * b - 4.0 * a * c)
    let denom = 2.0 * a
    let x1 = (-b + root) / denom
    let x2 = (-b - root) / denom
    (x1, x2)
