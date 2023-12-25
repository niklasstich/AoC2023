module AoCFSharp.Helpers

open System

let splitLines (input: string) = input.Split Environment.NewLine

let crossProduct xs ys =
    seq {
        for x in xs do
            for y in ys -> x, y
    }

let crossProductArray (xs: 'a array) (ys: 'b array) =
    crossProduct xs ys |> Seq.toArray
    
let countDigitsInNumber n =
    let rec digitsInNumber' n acc =
        match n with
        | _ when n < 10 -> acc
        | _ -> digitsInNumber' (n/10) (acc+1)
    digitsInNumber' n 1
    
let flip f x y = f y x
let (-*-) = flip
let curry f a b =
    f (a, b)
let uncurry f (a, b) =
    f a b
let toHexString (c: int) = c.ToString("X")

let rec gcd x y = if y = 0 then abs x else gcd y (x % y)

let lcm x y = x * y / (gcd x y)
let rec gcd64 (x:int64) (y:int64) = if y = 0 then abs x else gcd64 y (x % y)

let lcm64 (x:int64) (y:int64) = x * y / (gcd64 x y)
