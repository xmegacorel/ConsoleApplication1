module UnitTest

open Xunit
open VersionCSharp
open AlienLanguage.Trie
open System.Text.RegularExpressions

type Tests() =

    let explode (s:string) =
        [for c in s -> c]
    
    [<Fact>]
    let TestTrie() = 
        let trie = new Trie()
        let flag = "abc" |> explode |> trie.contain
        Assert.False flag

        "abc" |> explode |> trie.addWord
        let flag = "abc" |> explode |> trie.contain
        Assert.True flag

        "albatros" |> explode |> trie.addWord
        let flag = "albatros" |> explode |> trie.contain
        Assert.True flag

        "fns" |> explode |> trie.addWord
        let flag = "fns" |> explode |> trie.contain
        Assert.True flag

    [<Fact>]
    let TestAlgoCSharpVersion () = 
        let dictWords = new ResizeArray<string>();
        dictWords.AddRange([ "abc"; "bca"; "dac"; "dbc"; "cba"])

        let patterns = new ResizeArray<string>();
        patterns.AddRange( ["(ab)(bc)(ca)"; "abc"; "(abc)(abc)(abc)"; "(zyx)bc"])

        let solver = new PatternSolver(dictWords, 3, patterns)
        
        Assert.Equal(solver.Answers.Count, patterns.Count);
        Assert.Equal(solver.Answers.Item(0), 2);
        Assert.Equal(solver.Answers.Item(1), 1);
        Assert.Equal(solver.Answers.Item(2), 3);
        Assert.Equal(solver.Answers.Item(3), 0);

    [<Fact>]
    let testAlfoFSharpVersion ()=
        
        let dictWords = Seq.ofList [ "abc"; "bca"; "dac"; "dbc"; "cba"]

        let patterns = ["(ab)(bc)(ca)"; "abc"; "(abc)(abc)(abc)"; "(zyx)bc"]

        let solver = new AlienLanguage.PatternSolver.PatternSolver(dictWords, 3, patterns)
        
        Assert.Equal(List.length solver.Answer, List.length patterns);
        Assert.Equal(solver.Answer.Item(0), 2);
        Assert.Equal(solver.Answer.Item(1), 1);
        Assert.Equal(solver.Answer.Item(2), 3);
        Assert.Equal(solver.Answer.Item(3), 0);

    [<Fact>]
    let testAA() = 
        let input = Regex.Matches("a(ba)c", "([(][a-z]*[)])|[a-z]") |> Seq.cast<Match> |> Seq.map (fun x -> x.Value)
        let input12  = input |> Seq.map (fun x -> x.ToCharArray() |> Seq.filter (fun x -> x <> '(' && x <> ')')) |> Seq.filter (fun x -> Seq.length x > 0) |> List.ofSeq

        Assert.Equal(1, 1)