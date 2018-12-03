using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public class SymmetricMatrix<T> : Matrix<T>
    {
        public SymmetricMatrix(T[,] elements) : base(elements) { }

        public SymmetricMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        protected override void VerifyMatrixElements(T[,] elements)
        {
            int firstDemensionLength = elements.GetLength(0);
            int secondDemensionLength = elements.GetLength(1);

            if (firstDemensionLength != secondDemensionLength)
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a summetric matrix. Array must have equal count of columns and rows.");
            }

            for (int i = 0; i < firstDemensionLength; i++)
            {
                for (int j = 0; j < secondDemensionLength; j++)
                {
                    if (comparer.Compare(elements[i, j], elements[j, i]) != 0)
                    {
                        throw new ArgumentNullException($"The elements of {nameof(elements)} are not match for a symmetric matrix.");
                    }
                }
            }
        }

        protected override void SetValue(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex == columnIndex)
            {
                elements[rowIndex, columnIndex] = value;
            }
            else
            {
                elements[rowIndex, columnIndex] = value;
                elements[columnIndex, rowIndex] = value;
            }
        }
    }
}
