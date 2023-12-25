module AoCFSharp.Lib.Polynomial

let interpolate data =
    let interpolate' data =
        let lagrangePolynomial xi x =
            data
            |> Seq.map fst
            |> Seq.except [ xi ]
            |> Seq.fold (fun acc xj -> acc * ((x - xj) / (xi - xj))) 1.0
        
        fun x ->
            data
            |> Seq.map (fun (xi, yi) -> lagrangePolynomial xi x * yi)
            |> Seq.sum

    if data |> Seq.distinct |> Seq.length <> Seq.length data then
        Error "duplicate x values are not allowed"
    else
        Ok(interpolate' data)