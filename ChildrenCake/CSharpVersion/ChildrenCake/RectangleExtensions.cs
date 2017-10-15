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

        internal static Rectangle Rotate(this Rectangle rectangle, int angle)
        {
            var oldLeft = rectangle.LeftMost;
            var left = new MatrixCoord()
            {
                Column = (int)(oldLeft.Column * System.Math.Cos(angle) - oldLeft.Row * System.Math.Sin(angle)),
                Row = (int)(oldLeft.Column * System.Math.Sin(angle) + oldLeft.Row * System.Math.Cos(angle))
            };

            var oldRight = rectangle.RightMost;
            var right = new MatrixCoord()
            {
                Column = (int)(oldRight.Column * System.Math.Cos(angle) - oldRight.Row * System.Math.Sin(angle)),
                Row = (int)(oldRight.Column * System.Math.Sin(angle) + oldRight.Row * System.Math.Cos(angle))
            };

            return new Rectangle(left, right);
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


        internal static Rectangle Normalize(this Rectangle rect)
        {
            int leftColumn = rect.LeftMost.Column > rect.RightMost.Column ? rect.RightMost.Column : rect.LeftMost.Column;
            int leftRow = rect.LeftMost.Row > rect.RightMost.Row ? rect.RightMost.Row : rect.LeftMost.Row;

            int rightColumn = rect.LeftMost.Column > rect.RightMost.Column ? rect.LeftMost.Column : rect.RightMost.Column;
            int rightRow = rect.LeftMost.Row > rect.RightMost.Row ? rect.LeftMost.Row : rect.RightMost.Row;


            return new Rectangle(
                new MatrixCoord() { Row = leftRow, Column = leftColumn },
                new MatrixCoord() { Row = rightRow, Column = rightColumn });
        }
    }
}
