using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public class DiagonalMatrix<T> : Matrix<T>
    {
        public DiagonalMatrix(T[,] elements) : base(elements) { }

        public DiagonalMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        //TODO подумать. или вынести отдельно. Но тогдав пользователю нужно еще указывать конкретный верификатор. 
        //или можно сделать этот по умолчанию и еще один конструктор c IValidation 
        // или определить интерфейсы IDiogonal, ISquareMatrix і реаліз іх, поміімо Matrix
        protected override void VerifyMatrixElements(T[,] elements)
        {
            int firstDemensionLength = elements.GetLength(0);
            int secondDemensionLength = elements.GetLength(1);

            if (firstDemensionLength != secondDemensionLength)
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a diagonal matrix. Array must have equal count of columns and rows.");
            }

            for (int i = 0; i < firstDemensionLength; i++)
            {
                for (int j = 0;  j < secondDemensionLength; j++)
                {
                    if ((i != j) && comparer.Compare(elements[i, j], default(T)) != 0)
                    {
                        throw new ArgumentNullException($"The elements of {nameof(elements)} are not match for a diagonal matrix.");
                    }
                }
            }
        }

        protected override void SetValue(int rowIndex, int columnIndex, T value)
        {
            if (rowIndex != columnIndex)
            {
                throw new ArgumentException($"You can not change value outside the main diagonal.");
            }
            else
            {
                elements[rowIndex, columnIndex] = value;

                ChangeValueInMatrix(rowIndex, columnIndex, $"was changed to a new value {value}");
            }
        }
    }
}
