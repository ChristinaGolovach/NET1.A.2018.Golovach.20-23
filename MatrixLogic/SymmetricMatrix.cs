using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public class SymmetricMatrix<T> : Matrix<T>
    {
        private T[][] elements;
        public SymmetricMatrix(T[,] elements) : base(elements) { }

        public SymmetricMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        protected override void InitializeMatrix(T[,] elements)
        {
            int dimension = elements.GetLength(0);
            this.elements = new T[dimension][];
            MatrixOrder = dimension;

            for (int i = 0; i < dimension; i++)
            {
                this.elements[i] = new T[i + 1];

                for (int j = 0; j <= i; j++)
                {
                    this.elements[i][j] = elements[i, j];
                }
            }
        }

        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (!IsAllowedForSquareMatrix(elements))
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a summetric matrix. Array must have equal count of columns and rows.");
            }
            if (!IsAllowedForSymmetricMatrix(elements))
            {
                throw new ArgumentNullException($"The elements of {nameof(elements)} are not match for a symmetric matrix.");
            }
        }

        protected override T GetValue(int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        protected override void SetValue(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex == columnIndex)
            {
                elements[rowIndex][columnIndex] = value;
            }
            else
            {
                if (columnIndex >= elements[rowIndex].Length)
                {
                    elements[columnIndex][rowIndex] = value;
                }
                else
                {
                    elements[rowIndex][columnIndex] = value;
                }
            }

            ChangeValueInMatrix(rowIndex, columnIndex, $"was changed to a new value {value} and in row index {columnIndex}  column index {rowIndex} too.");
        }

        private bool IsAllowedForSymmetricMatrix(T[,] elements)
        {
            int firstDemensionLength = elements.GetLength(0);
            int secondDemensionLength = elements.GetLength(1);

            bool isAllowed = true;

            for (int i = 0; i < firstDemensionLength && isAllowed; i++)
            {
                for (int j = 0; j < secondDemensionLength && isAllowed; j++)
                {
                    if (comparer.Compare(elements[i, j], elements[j, i]) != 0)
                    {
                        
                        isAllowed = false;
                    }
                }
            }

            return isAllowed;
        }
    }
}
