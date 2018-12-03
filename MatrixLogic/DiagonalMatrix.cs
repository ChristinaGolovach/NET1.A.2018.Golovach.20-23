using System;
using System.Collections.Generic;

namespace MatrixLogic
{
    public class DiagonalMatrix<T> : Matrix<T>
    {
        private T[] elements;

        public DiagonalMatrix(T[,] elements) : base(elements) { }

        public DiagonalMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        public DiagonalMatrix(int dimension) : base(dimension)
        {
            elements = new T[dimension];
        }

        protected override void InitializeMatrix(T[,] elements)
        {
            int dimension = elements.GetLength(0);            
            this.elements = new T[dimension];
            MatrixOrder = dimension;

            for (int i = 0; i < dimension; i++)
            {
                this.elements[i] = elements[i, i];
            }
        }

        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (!IsAllowedForSquareMatrix(elements))
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a diagonal matrix. Array must have equal count of columns and rows.");
            }
            //TODO подумать. а может это не ответсвенность вообўе этого класса как с binary search проверка на отсортированный массив. 
            //и если клиент сам неверные данные положил - сам себе буратино. 
            if (!IsAllowedForDioganalMatrix(elements))
            {
                throw new ArgumentNullException($"The elements of {nameof(elements)} are not match for a diagonal matrix.");
            }
        }

        protected override T GetValue(int rowIndex, int columnIndex)
        {
            if (rowIndex != columnIndex)
            {
                return default(T);
            }

            return elements[rowIndex];            
        }

        protected override void SetValue(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex != columnIndex)
            {
                throw new ArgumentException($"You can not change value outside the main diagonal.");
            }
            else
            {
                elements[rowIndex] = value;

                ChangeValueInMatrix(rowIndex, columnIndex, $"was changed to a new value {value}");
            }
        }

        //TODO можно вынести в базовый класс и сделать чтбы делегат(предикат) принимал, т.к. с симетрич совпадает кроме внутреннего правила
        private bool IsAllowedForDioganalMatrix(T[,] elements)
        {
            int firstDemensionLength = elements.GetLength(0);
            int secondDemensionLength = elements.GetLength(1);

            bool isAllowed = true;

            for (int i = 0; i < firstDemensionLength && isAllowed; i++)
            {
                for (int j = 0; j < secondDemensionLength && isAllowed; j++)
                {
                    if ((i != j) && comparer.Compare(elements[i, j], default(T)) != 0)
                    {                        
                        isAllowed = false;
                    }
                }
            }

            return isAllowed;
        }
    }
}
