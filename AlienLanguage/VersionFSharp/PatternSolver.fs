module AlienLanguage.PatternSolver

open AlienLanguage.Trie
open System.Text.RegularExpressions

type PatternSolver(words : seq<string>, patternLengthIn : int, patterns : List<string>) =
    
    let trie = new Trie()
    let patternLengthIn = patternLengthIn

    let parcePattern(pattern : string): List<seq<char>> =
        let input = Regex.Matches(pattern, "([(][a-z]*[)])|[a-z]") |> Seq.cast<Match> |> Seq.map (fun x -> x.Value)
        input |> Seq.map (fun x -> x.ToCharArray() |> Seq.filter (fun x -> x <> '(' && x <> ')')) |> Seq.filter (fun x -> Seq.length x > 0) |> List.ofSeq
    
    let rec processWordRecursive(m : List<seq<char>>, subWord: List<char>): int = 
        let mutable result = 0
        
        for letter in m.Head do
            if m.IsEmpty = false then 
                let p = (List.ofSeq subWord) @ [letter]
                result <- result + match trie.subContain(p) with
                                    | true -> if m.Tail.IsEmpty then 1 else processWordRecursive(m.Tail, p)
                                    | false -> 0
        result

    let findWords(p : List<seq<char>>) : int = 
        match Seq.length p > 0 with
            | true -> processWordRecursive(p, [])
            | _ -> 0

    let explode (s:string) = 
        [for c in s -> c]

    do Seq.iter (fun x -> x |> explode |> trie.addWord) words

    let answer = List.map (fun x -> x |> parcePattern |> findWords ) patterns

    member this.Answer : List<int> = answer