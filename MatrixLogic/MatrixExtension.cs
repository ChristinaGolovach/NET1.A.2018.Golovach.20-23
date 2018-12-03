using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public static class MatrixExtension
    {
        //TODO подумать. или сделать visitor
        public static dynamic Add<T>(this Matrix<T> matrix, Matrix<T> otherMatrix)
            => Add<T>((dynamic)matrix, (dynamic)otherMatrix);

        //TODO подумать. в рез-те сложеня может выйти симметричная/диогональная - делать потом проверку элементов и фабрику? 
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
    }
}
