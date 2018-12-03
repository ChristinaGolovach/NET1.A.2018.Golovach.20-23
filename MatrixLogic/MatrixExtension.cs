using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public static class MatrixExtension
    {
        public static Matrix<T> Add<T>(this Matrix<T> matrix, Matrix<T> otherMatrix)
        {
            CheckMatrixOrder(matrix, otherMatrix);

            return Add<T>((dynamic)matrix, (dynamic)otherMatrix);
        }
             
        private static SquareMatrix<T> Add<T>(SquareMatrix<T> matrix, SquareMatrix<T> squareMatrix)
        {
            throw new NotImplementedException();
        }

        private static SquareMatrix<T> Add<T>(SquareMatrix<T> matrix, DiagonalMatrix<T> squareMatrix)
        {
            throw new NotImplementedException();
        }

        //TODO и т.д.
        //private static 
        
        private static void CheckMatrixOrder<T>(Matrix<T> matrix, Matrix<T> otherMatrix)
        {
            if (matrix.MatrixOrder != otherMatrix.MatrixOrder)
            {
                throw new ArgumentException($"Dimensions of {nameof(matrix)} and {nameof(otherMatrix)} are not equal.");
            }
        }
    }
}
