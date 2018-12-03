using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public class SquareMatrix<T> : Matrix<T>
    {
        public SquareMatrix(T[,] elements) : base(elements) { }

        public SquareMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        
        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (elements.GetLength(0) != elements.GetLength(1))
            {
                throw new ArgumentException($"The {nameof(elements)} is not a square matrix.");
            } 
        }

        protected override void SetValue(int rowIndex, int columnIndex, T value)
        {            
            elements[rowIndex, columnIndex] = value;

            ChangeValueInMatrix(rowIndex, columnIndex, $"was changed to a new value {value}");
        }
    }
}
