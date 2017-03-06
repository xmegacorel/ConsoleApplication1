namespace FSharpVersion

type Vector = seq<int>
    
type MinScalarVectorProduct(first: Vector, second: Vector, dimension : int) = 
    
    let _dimention = dimension
    
    let buildNewVector(vector : Vector, i : int, j : int) : Vector =
        seq { for k in 0 .. Seq.length(vector) - 1 do
                if k = i then yield Seq.item(j) vector else
                    if k = j then yield Seq.item(i) vector else 
                        yield Seq.item(k) vector
        }

    let permutate(vector : Vector) : seq<Vector> = 
        seq { for i in 0 .. _dimention - 1 do
                
                for j in i .. 1 do
                    let newVector = buildNewVector(vector, j - 1, j)
                    yield newVector
                
                for k in i .. _dimention - 2 do
                    let newVector = buildNewVector(vector, i, k + 1)
                    yield newVector
            }

    let scalarProduct(first : Vector, second : Vector): int = 
        Seq.map2 (fun x y -> x * y ) first second |> Seq.sum 

    let getMinScalarProduct firstVectors secondVectors  =

        let first = Array.ofSeq (firstVectors)
        let second = Array.ofSeq (secondVectors)
        
        let length = Array.length(first)

        let mutable min = 2147483647

        for i in 0 .. length - 1 do
            for j in 0 .. length - 1 do
                if i <> j then
                    let currentScalar = scalarProduct(first.[i], second.[j]);
                    if currentScalar < min then min <- currentScalar

        min
        
    let answer = permutate(first), permutate(second) |> getMinScalarProduct