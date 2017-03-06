using System;
using System.Collections.Generic;
using System.Linq;

namespace VersionCSharp
{
    public class PatternSolver
    {
        private readonly Trie _trie = new Trie();

        public PatternSolver(List<string> words, int lengthPatternWord, List<string> patterns)
        {
            if (words == null)
            {
                throw new ArgumentException("list of word is empty!");
            }

            if (patterns == null)
            {
                throw new ArgumentException("list of patterns is empty");
            }

            BuildTrie(words);
            Answers = new List<int>();
            for (int i = 0; i < patterns.Count; i++)
            {
                var parcedPattern = Parce(patterns[i], lengthPatternWord);
                var count = FindWords(parcedPattern);
                Answers.Add(count);
            }

        }

        private void BuildTrie(List<string> words)
        {
            foreach (var word in words)
            {
                _trie.AddWordToTrie(word);
            }
        }

       
        private List<char>[] Parce(string pattern, int length)
        {
            var result = new List<char>[length];
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                var currentLetter = pattern[index++];
                if (currentLetter != '(') // if just simple letter without choice
                {
                    result[i] = new List<char>() { currentLetter };
                }
                else
                {
                    var letters = new List<char>();
                    while (true)
                    {
                        currentLetter = pattern[index++];
                        if (currentLetter == ')')
                        {
                            break;
                        }
                        letters.Add(currentLetter);
                    }
                    result[i] = letters;
                }
            }
            return result;
        }

        private int FindWords(List<char>[] pattern)
        {
            var result = 0;
            if (pattern.Length == 0)
            {
                return result;
            }
            
            foreach (var t in pattern.First())
            {
                result += GetAllVariationInner(pattern, 0, t.ToString());
            }
            return result;
        }

        private int GetAllVariationInner(List<char>[] pattern, int index, string prefix)
        {
            int result = 0;
            index++;
            var lenPattern = pattern.Length - 1;
            foreach (var t in pattern[index])
            {
                var currentSubWord = prefix + t;
                var contain = _trie.SubContain(currentSubWord);
                if (contain)
                {
                    if (index == lenPattern)
                    {
                        return 1;
                    }
                    else
                    {
                        result += GetAllVariationInner(pattern, index, currentSubWord);
                    }
                }    
            }
            return result;
        }

        public IList<int> Answers { get; private set; }


        
    }
}