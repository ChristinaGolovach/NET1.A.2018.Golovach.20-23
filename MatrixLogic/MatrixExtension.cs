using System;
using Microsoft.CSharp.RuntimeBinder;

namespace MatrixLogic
{
    public static class MatrixExtension
    {
        public static Matrix<T> Add<T>(this Matrix<T> matrix, Matrix<T> otherMatrix)
        {
            CheckMatrixOrder(matrix, otherMatrix);

            Matrix<T> result = null;

            try
            {
                result = Add<T>((dynamic)matrix, (dynamic)otherMatrix, matrix.MatrixOrder);
            }
            catch (RuntimeBinderException exception)
            {
                throw;
            }
            return result;
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> matrix, SquareMatrix<T> otherMatrix, int matrixOrder)
        {
            SquareMatrix<T> resultMatrix = new SquareMatrix<T>(matrixOrder);

            SumElementsOfFullMatrix(matrix, otherMatrix, resultMatrix, matrixOrder);

            return resultMatrix;
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> matrix, DiagonalMatrix<T> otherMatrix, int matrixOrder)
        {
            SquareMatrix<T> resultMatrix = new SquareMatrix<T>(matrixOrder);

            SumElementsOfFullAndDiagonal(matrix, otherMatrix, resultMatrix, matrixOrder);

            return resultMatrix;
        }

        private static SquareMatrix<T> Add<T>(DiagonalMatrix<T> otherMatrix, SquareMatrix<T> matrix, int matrixOrder)
            => Add<T>(matrix, otherMatrix, matrixOrder);


        private static SquareMatrix<T> Add<T>(SquareMatrix<T> matrix, SymmetricMatrix<T> otherMatrix, int matrixOrder)
        {
            SquareMatrix<T> resultMatrix = new SquareMatrix<T>(matrixOrder);

            SumElementsOfFullMatrix(matrix, otherMatrix, resultMatrix, matrixOrder);

            return resultMatrix;
        }

        private static SquareMatrix<T> Add<T>(SymmetricMatrix<T> otherMatrix, SquareMatrix<T> matrix, int matrixOrder)
            => Add<T>(matrix, otherMatrix, matrixOrder);

        private static DiagonalMatrix<T> Add<T>(DiagonalMatrix<T> matrix, DiagonalMatrix<T> otherMatrix, int matrixOrder)
        {
            DiagonalMatrix<T> resultMatrix = new DiagonalMatrix<T>(matrixOrder);

            SumElementOfDiagonals(matrix, otherMatrix, resultMatrix, matrixOrder);

            return resultMatrix;
        }

        private static void CheckMatrixOrder<T>(Matrix<T> matrix, Matrix<T> otherMatrix)
        {
            if (matrix.MatrixOrder != otherMatrix.MatrixOrder)
            {
                throw new ArgumentException($"Dimensions of {nameof(matrix)} and {nameof(otherMatrix)} are not equal.");
            }
        }

        private static void SumElementsOfFullMatrix<T>(Matrix<T> matrix, Matrix<T> otherMatrix, Matrix<T> resultMatrix, int matrixOrder)
        {
            dynamic firstMatrix = (dynamic)matrix;
            dynamic secondMatrix = (dynamic)otherMatrix;

            for (int i = 0; i < matrixOrder; i++)
            {
                for (int j = 0; j < matrixOrder; j++)
                {
                    resultMatrix[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
                }
            }
        }

        private static void SumElementsOfFullAndDiagonal<T>(Matrix<T> matrix, Matrix<T> otherMatrix, Matrix<T> resultMatrix, int matrixOrder)
        {
            dynamic firstMatrix = (dynamic)matrix;
            dynamic secondMatrix = (dynamic)otherMatrix;

            for (int i = 0; i < matrixOrder; i++)
            {
                for (int j = 0; j < matrixOrder; j++)
                {
                    if (i == j)
                    {
                        resultMatrix[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
                    }
                    else
                    {
                        resultMatrix[i, j] = firstMatrix[i, j];
                    }
                }
            }
        }

        private static void SumElementOfDiagonals<T>(Matrix<T> matrix, Matrix<T> otherMatrix, Matrix<T> resultMatrix, int matrixOrder)
        {
            dynamic firstMatrix = (dynamic)matrix;
            dynamic secondMatrix = (dynamic)otherMatrix;

            for (int i = 0; i < matrixOrder; i++)
            {
                resultMatrix[i, i] = firstMatrix[i, i] + secondMatrix[i, i];
            }
        }
    }   
}
