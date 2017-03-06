using System;
using System.Collections.Generic;

namespace CSharpVersion
{
    public class MinScalarVectorProduct
    {
        private readonly int _dimension;

        public MinScalarVectorProduct(Vector firstVector, Vector secondVector, int dimension)
        {
            _dimension = dimension;

            if (firstVector == null)
                throw new ArgumentException("Не задан первые вектор");

            if (secondVector == null)
                throw new ArgumentException("Не задан второй вектор");

            var firstVectorPermutations = Permutate(firstVector);
            var secondVectorPermutations = Permutate(secondVector);
            var answer = GetMinScalarProduct(firstVectorPermutations, secondVectorPermutations);
            Answer = answer;
        }

        private List<Vector> Permutate(Vector vector)
        {
            var result = new List<Vector>();
            for (int i = 0; i < _dimension; i++)
            {
                int j = i;
                while (j > 0)
                {
                    var newVector = vector.Copy();
                    Swap(newVector, j - 1, j);
                    result.Add(newVector);
                    j--;
                }

                int k = i;
                while (k < _dimension - 1)
                {
                    var newVector = vector.Copy();
                    Swap(newVector, i, k + 1);
                    result.Add(newVector);
                    k++;
                }
            }

            return result;
        }

        private void Swap(Vector newVector, int i, int j)
        {
            var tmp = newVector[j];
            newVector[j] = newVector[i];
            newVector[i] = tmp;
        }

        private int GetMinScalarProduct(List<Vector> first, List<Vector> second)
        {
            var min = int.MaxValue;
            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count; j++)
                {
                    if (i == j)
                        continue;
                    var currentScalar = ScalarProduct(first[i], second[j]);
                    if (currentScalar < min)
                    {
                        min = currentScalar;
                    }
                }
            }

            return min;
        }

        private int ScalarProduct(Vector vector1, Vector vector2)
        {
            int sum = 0;
            for (int i = 0; i < _dimension; i++)
            {
                sum += vector1[i] * vector2[i];
            }

            return sum;
        }

        public int Answer { get; set; } = 0;
    }
}
