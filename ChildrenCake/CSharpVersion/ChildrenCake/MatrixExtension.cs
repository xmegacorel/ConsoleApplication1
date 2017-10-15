using System.Collections.Generic;
using System.Text;

namespace ChildrenCake
{
    public static class MatrixExtension
    {
        public static string Print<T>(this Matrix<T> matrix)
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 1; row <= matrix.Row; row++)
            {
                sb.AppendLine(string.Join("-", GetRow(row)));
            }

            IEnumerable<T> GetRow(int row)
            {
                for (int i = 1; i <= matrix.Column; i++)
                {
                    yield return matrix[row, i];
                }
            }

            return sb.ToString();
        }
    }
}