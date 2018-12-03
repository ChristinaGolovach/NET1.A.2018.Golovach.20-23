using System;
using System.Collections.Generic;

namespace MatrixLogic
{
    /// <summary>
    /// Represents a class for work with square matrix and implrmrnts <see cref="Matrix{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in matrix.
    /// </typeparam>
    public class SquareMatrix<T> : Matrix<T>
    {
        private T[,] elements;

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
        public SquareMatrix(T[,] elements) : base(elements) { }

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
        public SquareMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        /// <summary>
        /// Constructor with a specific dimension.
        /// </summary>
        /// <param name="dimension">
        /// The dimension of matrix.
        /// </param>
        /// <exception cref="ArgumentException">Theh <paramref name="dimension"/>is less than one.</exception>
        public SquareMatrix(int dimension) : base(dimension)
        {
            elements = new T[dimension, dimension];
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

            MatrixOrder = dimension;

            this.elements = new T[dimension, dimension];

            Array.Copy(elements, 0, this.elements, 0, elements.Length);
        }

        /// <summary>
        /// Implements the method <see cref="Matrix{T}.VerifyMatrixElements(T[,])"/> 
        /// </summary>
        /// <param name="elements">
        /// Array for the checking.
        /// </param>
        /// <exception cref="ArgumentException">
        /// The dimension of the input array is not square.
        /// </exception>
        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (!IsAllowedForSquareMatrix(elements))
            {
                throw new ArgumentException($"The {nameof(elements)} is not a square matrix.");
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
            return elements[rowIndex, columnIndex];
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
            elements[rowIndex, columnIndex] = value;

            ChangeValueInMatrix(rowIndex, columnIndex, $"was changed to a new value {value}");
        }
    }
}
