module AlienLanguage.Trie

type Node = 
        { Letter : char;
          isLeaf : bool;
          mutable NextValues : List<Node> }

type Trie() = 
    let nodes = Array.zeroCreate<Option<Node>>(26)

    let rec recursCheck(nextValues : List<Node>, tail : List<char>, subContain : bool): bool = 
        let nextLetter = tail.Head
        let elem = nextValues |> List.tryFind (fun x -> x.Letter = nextLetter)
        match elem with
        | None -> false
        | Some y -> if (y.isLeaf || subContain) && tail.Length = 1 then true else recursCheck(y.NextValues, tail.Tail, subContain)

    let checkNode(node : Node, tail : List<char>, subContain : bool) : bool = 
        let letter = tail.Head
        if node.Letter = letter then 
            match tail.Tail.IsEmpty with
                | true -> true
                | _ -> recursCheck(node.NextValues, tail.Tail, subContain)
        else false
    
    let nodeIndex(letter : char) = (int)letter - (int)'a'

    let containCore(word : List<char>, subContain : bool) : bool = 
        if word.IsEmpty then false else
        match nodes.[nodeIndex(word.Head)] with
        | None -> false
        | Some y -> checkNode(y, word, subContain)

    let rec addNextLetter(nodePrev : Node, tail : List<char>) : Unit = 
        if tail.IsEmpty = false then
            let nextLetter = tail.Head
            let elem = nodePrev.NextValues |> List.tryFind (fun x -> x.Letter = nextLetter)
            if elem.IsSome && tail.IsEmpty = false then addNextLetter(elem.Value, tail.Tail)
            else 
                let node = { Letter = nextLetter; isLeaf = tail.Length = 1; NextValues = [] }
                nodePrev.NextValues <- node :: nodePrev.NextValues
                if (tail.IsEmpty = false) then addNextLetter(node, tail.Tail)

    member this.contain(word : List<char>) : bool = containCore(word, false)

    member this.subContain(word : List<char>) : bool = containCore(word, true)
    
    member this.addWord(word : List<char>)  = 
        let currentNode = nodes.[nodeIndex(word.Head)]
        if currentNode.IsNone then
            let node = Some { Letter = word.Head; isLeaf = false; NextValues = []}
            nodes.[nodeIndex(word.Head)] <- node
            addNextLetter(node.Value, word.Tail)
        else 
            addNextLetter(currentNode.Value, word.Tail)
