using System.Collections.Generic;
using System.Linq;

namespace VersionCSharp
{
    public class Trie
    {
        private Node[] _nodes = new Node[26];


        public bool Contain(string word)
        {
            Node prev = null;
            var wordLength = word.Length;
            for (int i = 0; i < wordLength; i++)
            {
                var letter = word[i];
                if (i == 0)
                {
                    var current = _nodes[letter - 'a'];
                    if (current == null)
                    {
                        return false;
                    }
                    prev = current;
                }
                else
                {
                    var currentNode = prev.NextValues.SingleOrDefault(t => t.Letter == letter);
                    if (currentNode == null)
                    {
                        return false;
                    }
                    if (i == wordLength - 1)
                    {
                        return currentNode.IsLeaf;
                    }
                    prev = currentNode;
                }
            }

            return false;
        }


        public void AddWordToTrie(string word)
        {
            var wordLength = word.Length;
            Node prevNode = null;

            for (int i = 0; i < wordLength; i++)
            {
                var letter = word[i];
                if (i == 0) // this is first letter, we need to
                {
                    prevNode = SetFirstNode(letter);
                }
                else
                {
                    var currentNode = prevNode.NextValues.SingleOrDefault(t => t.Letter == letter);
                    if (currentNode == null)
                    {
                        currentNode = new Node() { Letter = letter };
                        prevNode.NextValues.Add(currentNode);
                        if (i == wordLength - 1)
                        {
                            currentNode.IsLeaf = true;
                        }
                        prevNode = currentNode;
                    }
                    else
                    {
                        prevNode = currentNode;
                    }
                }
            }
        }

        private Node SetFirstNode(char letter)
        {
            var node = _nodes[letter - 'a'];
            if (node == null)
            {
                node = new Node() { Letter = letter };
                _nodes[letter - 'a'] = node;
            }
            return node;
        }

        public bool SubContain(string word)
        {
            Node prev = null;
            var wordLength = word.Length;
            for (int i = 0; i < wordLength; i++)
            {
                var letter = word[i];
                if (i == 0)
                {
                    var current = _nodes[letter - 'a'];
                    if (current == null)
                    {
                        return false;
                    }
                    prev = current;
                }
                else
                {
                    var currentNode = prev.NextValues.SingleOrDefault(t => t.Letter == letter);
                    if (currentNode == null)
                    {
                        return false;
                    }
                    prev = currentNode;
                }
            }

            return true;
        }

        class Node
        {
            public Node()
            {
                NextValues = new List<Node>();
                IsLeaf = false;
            }

            public List<Node> NextValues { get; set; }
            public bool IsLeaf { get; set; }
            public char Letter { get; set; }
        }

        
    }
}