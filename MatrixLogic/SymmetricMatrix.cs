using System;
using System.Collections.Generic;

namespace MatrixLogic
{
    /// <summary>
    /// Represents a class for work with symmetric matrix and implrmrnts <see cref="Matrix{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in matrix.
    /// </typeparam>
    public class SymmetricMatrix<T> : Matrix<T>
    {
        private T[][] elements;

        /// <summary>
        /// Constructor with two-dimension array.
        /// </summary>
        /// <param name="elements">
        /// Two-dimension array for initialization of matrix.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="elements"/> is null.
        /// The of elements in matrix does not implement IComparable and IComparable<T> interfaces.
        /// </exception> 
        public SymmetricMatrix(T[,] elements) : base(elements) { }

        /// <summary>
        ///  Constructor with two-dimension array and specific comparer.
        /// </summary>
        /// <param name="elements">
        /// Two-dimension array for initialization of matrix.
        /// </param>
        /// <param name="comparer">\
        /// Type that implement <see cref="IComparer{T}"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="comparer"/> is null.
        /// <paramref name="elements"/> is null.
        /// </exception>
        public SymmetricMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        /// <summary>
        /// Constructor with a specific dimension.
        /// </summary>
        /// <param name="dimension">
        /// The dimension of matrix.
        /// </param>
        /// <exception cref="ArgumentException">Theh <paramref name="dimension"/>is less than one.</exception>
        public SymmetricMatrix(int dimension) : base(dimension)
        {
            elements = new T[dimension][];

            for (int i = 0; i < dimension; i++)
            {
                elements[i] = new T[i + 1];
            }
        }

        /// <summary>
        /// Implements the method <see cref="Matrix{T}.InitializeMatrix(T[,])"/> 
        /// </summary>
        /// <param name="elements">
        /// Array for initialize.
        /// </param>
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

        /// <summary>
        /// Implements the method <see cref="Matrix{T}.VerifyMatrixElements(T[,])"/> 
        /// </summary>
        /// <param name="elements">
        /// Array for the checking.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The dimension of the input array is not square.
        /// Items of input array are not symmetric relative to the main diagonal.
        /// </exception>
        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (!IsAllowedForSquareMatrix(elements))
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a summetric matrix. Array must have equal count of columns and rows.");
            }
            if (!IsAllowedForSymmetricMatrix(elements))
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a symmetric matrix.");
            }
        }

        /// <summary>
        /// Implements <see cref="Matrix{T}.GetValue(int, int)"/>.
        /// </summary>
        /// <param name="rowIndex">
        /// The index of row.
        /// </param>
        /// <param name="columnIndex">
        /// The index of column. 
        /// </param>
        /// <returns>
        /// An element of matrix.
        /// </returns>
        protected override T GetValue(int rowIndex, int columnIndex)
        {
            if (columnIndex >= elements[rowIndex].Length)
            {
                return elements[columnIndex][rowIndex];
            }
            else
            {
                return elements[rowIndex][columnIndex];
            }
        }

        /// <summary>
        /// Implements <see cref="Matrix{T}.SetValue(int, int, T)"/>
        /// </summary>
        /// <param name="rowIndex">
        /// The index of row.
        /// </param>
        /// <param name="columnIndex">
        /// The index of column.
        /// </param>
        /// <param name="value">
        /// A new value.
        /// </param>
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
