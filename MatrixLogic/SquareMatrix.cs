using System;
using System.Collections.Generic;

namespace MatrixLogic
{
    public class SquareMatrix<T> : Matrix<T>
    {
        private T[,] elements;

        public SquareMatrix(T[,] elements) : base(elements) { }

        public SquareMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        public SquareMatrix(int dimension) : base(dimension)
        {
            elements = new T[dimension, dimension];
        }

        protected override void InitializeMatrix(T[,] elements)
        {
            int dimension = elements.GetLength(0);

            MatrixOrder = dimension;

            this.elements = new T[dimension, dimension];

            Array.Copy(elements, 0, this.elements, 0, elements.Length);
        }

        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (!IsAllowedForSquareMatrix(elements))
            {
                throw new ArgumentException($"The {nameof(elements)} is not a square matrix.");
            } 
        }

        protected override T GetValue(int rowIndex, int columnIndex)
        {
            return elements[rowIndex, columnIndex];
        }

        protected override void SetValue(int rowIndex, int columnIndex, T value)
        {            
            elements[rowIndex, columnIndex] = value;

            ChangeValueInMatrix(rowIndex, columnIndex, $"was changed to a new value {value}");
        }
    }
}
