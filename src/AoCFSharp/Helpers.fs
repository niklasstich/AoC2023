module AoCFSharp.Helpers

let splitLines (input: string) = input.Split '\n'

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