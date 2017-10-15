using System.Collections.Generic;
using System.Linq;

namespace ChildrenCake
{
    public class Matrix<T>
    {
        private readonly int _row;
        private readonly int _column;
        private readonly List<T> _elements;
        private readonly T _defaultValue = default(T);

        public Matrix(int row, int column)
        {
            _row = row;
            _column = column;
            _elements = new List<T>(_row * _column);
            _elements.InsertRange(0, Enumerable.Repeat<T>(_defaultValue, _row * _column));
        }

        public bool IsEmpty(int row, int column) => GetElementAtRowColumn(row, column).Equals(_defaultValue);

        public void SetElementAtRowColumn(int row, int column, T value)
        {
            int index = GetFlatIndex(row, column);
            _elements[index] = value;
        }

        private T GetElementAtRowColumn(int row, int column)
        {
            int index = GetFlatIndex(row, column);
            return _elements[index];
        }

        public int Row => _row;
        public int Column => _column;

        public int GetFlatIndex(int row, int column) => (row - 1) * _column + column - 1;

        public T this[int row, int column] => _elements[GetFlatIndex(row, column)];

        internal MatrixCoord GetPairByFlatIndex(int index)
        {
            var row = (index % _column == 0) ? index / _column : index / _column + 1;
            var column = index - (row - 1) * _column;

            return new MatrixCoord() { Row = row, Column = column };
        }

        
    }

    public class MatrixCoord
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public override string ToString()
        {
            return $"{nameof(Column)} - {Column}, {nameof(Row)} - {Row}";
        }
    }

    public class Rectangle
    {
        private MatrixCoord _leftTop;
        private MatrixCoord _rightBotom;

        public Rectangle(MatrixCoord leftTop, MatrixCoord rightBotom)
        {
            _leftTop = leftTop;
            _rightBotom = rightBotom;
        }

        public MatrixCoord LeftMost => _leftTop;
        public MatrixCoord RightMost => _rightBotom;
    }
}