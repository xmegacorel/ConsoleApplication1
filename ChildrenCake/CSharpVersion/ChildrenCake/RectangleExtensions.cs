namespace ChildrenCake
{
    public static class RectangleExtensions
    {
        internal static Gas ToGas(this Rectangle rectangle, char value)
        {
            return new Gas()
            {
                Rect = rectangle,
                Square = (rectangle.RightMost.Column - rectangle.LeftMost.Column + 1) * (rectangle.RightMost.Row - rectangle.LeftMost.Row + 1),
                Value = value
            };
        }

        internal static Rectangle Move(this Rectangle rect, int column, int row)
        {
            var left = new MatrixCoord() { Column = rect.LeftMost.Column += column, Row = rect.LeftMost.Row += row };
            var rigth = new MatrixCoord() { Column = rect.RightMost.Column += column, Row = rect.RightMost.Row += row };
            var newRect = new Rectangle(left, rigth);

            return newRect;
        }

        internal static Rectangle Copy(this Rectangle rect)
        {
            var left = new MatrixCoord() { Column = rect.LeftMost.Column, Row = rect.LeftMost.Row };
            var rigth = new MatrixCoord() { Column = rect.RightMost.Column, Row = rect.RightMost.Row };
            var newRect = new Rectangle(left, rigth);

            return newRect;
        }
    }
}
