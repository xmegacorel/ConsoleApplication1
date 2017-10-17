using C5;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChildrenCake
{
    public class MatrixSolver
    {
        private Matrix<char> _matrix;
        private Dictionary<int, char> _proportion = new Dictionary<int, char>();

        public MatrixSolver(int row, int column, IEnumerable<System.Collections.Generic.KeyValuePair<int, char>> letters)
        {
            if (row < 1)
            {
                throw new ArgumentException($"{nameof(row)} has incorrect value");
            }

            if (column < 1)
            {
                throw new ArgumentException($"{nameof(column)} has incorrect value");
            }

            if (letters.Count() == 0)
            {
                throw new ArgumentException("Please, specify letters");
            }

            _matrix = new Matrix<char>(row, column);
            ApplyValues(_matrix, letters.Reverse());

            Solve(letters);
        }

        private void ApplyValues(Matrix<char> matrix, IEnumerable<System.Collections.Generic.KeyValuePair<int, char>> letters)
        {
            foreach (var pair in letters)
            {
                var coord = matrix.GetPairByFlatIndex(pair.Key);
                matrix.SetElementAtRowColumn(coord.Row, coord.Column, pair.Value);
            }
        }

        private void Solve(IEnumerable<System.Collections.Generic.KeyValuePair<int, char>> letters)
        {
            foreach (var pair in letters)
            {
                var rect = GetMaxRectAtLetter(pair.Key, pair.Value);
                rect.MatchSome(FillLetterResult);
            }
        }

        private void FillLetterResult(Gas gas)
        {
            var rect = gas.Rect;
            for (int i = 0; i <= rect.RightMost.Column - rect.LeftMost.Column; i++)
            {
                for (int j = 0; j <= rect.RightMost.Row - rect.LeftMost.Row; j++)
                {
                    _matrix.SetElementAtRowColumn(rect.LeftMost.Row + j, rect.LeftMost.Column + i, gas.Value);
                }
            }
        }

        private Optional.Option<Gas> GetMaxRectAtLetter(int index, char letter)
        {
            var result = new IntervalHeap<Gas>(new GasComparer());
            int startDimention = 2;
            var startIndex = _matrix.GetPairByFlatIndex(index);
            while (true)
            {
                bool rectFound = false;
                for (int i = 1; i <= startDimention; i++)
                {

                    var coords = GenerateHorizMovement(i, startDimention, startIndex);
                    coords.ForEach(x => { if (TestRect(x, letter)) { result.Add(x.ToGas(letter)); rectFound = true; } });

                    if (startDimention != i) 
                    {
                        coords = GenerateVertMovement(i, startDimention, startIndex);
                        coords.ForEach(x => { if (TestRect(x, letter)) { result.Add(x.ToGas(letter)); rectFound = true; } });
                    }
                }

                if (!rectFound)
                {
                    break;
                }

                startDimention++;
            }

            return result.Count == 0 ? Optional.Option.None<Gas>() : Optional.Option.Some<Gas>(result.FindMax());
        }

        private List<Rectangle> GenerateHorizMovement(int current, int dimention, MatrixCoord start)
        {
            List<Rectangle> result = new List<Rectangle>();

            var startPoint = new Rectangle(start, new MatrixCoord() { Column = start.Column + dimention - 1, Row = start.Row + current - 1 });
            Rectangle prev = startPoint;
            for (int i = 1; i <= current * dimention; i++)
            {
                var row = ((i % dimention == 0) ? i / dimention : i / dimention + 1) - 1;
                var column = (i - (row) * dimention) - 1;

                prev = startPoint.Copy();
                prev = prev.Move(-column, -row);
                result.Add(prev);
            }

            return result;
        }

        private List<Rectangle> GenerateVertMovement(int current, int dimention, MatrixCoord start)
        {
            List<Rectangle> result = new List<Rectangle>();

            var startPoint = new Rectangle(start, new MatrixCoord() { Column = start.Column + current - 1, Row = start.Row + dimention - 1 });
            Rectangle prev = startPoint;
            for (int i = 1; i <= current * dimention; i++)
            {
                var row = ((i % current == 0) ? i / current : i / current + 1) - 1;
                var column = (i - (row) * current) - 1;

                prev = startPoint.Copy();
                prev = prev.Move(-column, -row);
                result.Add(prev);
            }

            return result;
        }

        private bool TestRect(Rectangle v, char letter)
        {
            if ((v.LeftMost.Column > _matrix.Column || v.LeftMost.Column <= 0) ||
                (v.LeftMost.Row > _matrix.Row || v.LeftMost.Row <= 0))
            {
                return false;
            }

            if ((v.RightMost.Column > _matrix.Column || v.RightMost.Column <= 0) ||
                (v.RightMost.Row > _matrix.Row || v.RightMost.Row <= 0))
            {
                return false;
            }

            for (int i = 0; i <= v.RightMost.Column - v.LeftMost.Column; i++)
            {
                for (int j = 0; j <= v.RightMost.Row - v.LeftMost.Row; j++)
                {
                    var row = v.LeftMost.Row + j;
                    var column = v.LeftMost.Column + i;
                    var value = _matrix[row, column];
                    if (value != default && value != letter)
                        return false;
                }
            }

            return true;
        }
    
        public string Result => _matrix.Print();

        public bool Succes => _matrix.Filled;

        public string Detail => _matrix.Detail;

        class RectVolume
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }

    internal class Gas
    {
        public Rectangle Rect { get; set; }
        public int Square { get; set; }
        public char Value { get; set; }
    }

    class GasComparer : IComparer<Gas>
    {
        public int Compare(Gas x, Gas y)
        {
            if (x.Square > y.Square)
                return 1;
            if (x.Square < y.Square)
                return -1;

            return 0;
        }
    }
}