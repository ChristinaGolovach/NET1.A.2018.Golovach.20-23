using System;
using System.Collections.Generic;

namespace MatrixLogic
{
    /// <summary>
    /// Represents a base abstract class for a square matrices.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in matrix.
    /// </typeparam>
    public abstract class Matrix<T> 
    {
        private const int firstDemension = 0;
        private const int secondDemension = 1;

        internal protected IComparer<T> comparer;
        private int matrixOrder;

        /// <summary>
        /// Event when the matrix's element is changed. 
        /// </summary>
        public event EventHandler<MatrixArgs> ChengeValue = delegate { };

        /// <summary>
        /// Return dimension of matrix.
        /// </summary>
        public int MatrixOrder
        {
            get => matrixOrder;
            internal protected set => matrixOrder = value;
        }

        /// <summary>
        /// Constructor with a specific dimension.
        /// </summary>
        /// <param name="dimension">
        /// The dimension of matrix.
        /// </param>
        /// <exception cref="ArgumentException">Theh <paramref name="dimension"/>is less than one.</exception>
        protected Matrix(int dimension)
        {
            if (dimension <= 0)
            {
                throw new ArgumentException($"The {nameof(dimension)} can not be less than one.");
            }

            MatrixOrder = dimension;
        }

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
        protected Matrix(T[,] elements)
        {   
            if (elements == null)
            {
                throw new ArgumentNullException($"The {nameof(elements)} can not be null.");
            }            
            if (!typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentNullException($"The {typeof(T)} must immplement IComparable<{typeof(T)}> interface.");
            }
            if (!typeof(IComparable).IsAssignableFrom(typeof(T)))
            {
                throw new ArgumentNullException($"The {typeof(T)} must immplement IComparable interface.");
            }
            comparer = Comparer<T>.Default;

            VerifyMatrixElements(elements);

            InitializeMatrix(elements);                        
        }

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
        protected Matrix(T[,] elements, IComparer<T> comparer)
        {            
            if (elements == null)
            {
                throw new ArgumentNullException($"The {nameof(elements)} can not be null.");
            }

            this.comparer = comparer ?? throw new ArgumentNullException($"The {nameof(comparer)} can not be null.");

            VerifyMatrixElements(elements);

            InitializeMatrix(elements);            
        }

        /// <summary>
        /// Indexer for get and set value in matrix.
        /// </summary>
        /// <param name="rowIndex">
        /// Index of row.
        /// </param>
        /// <param name="columnIndex">
        /// Index of column.
        /// </param>
        /// <returns>
        /// Elements of matrix by indexes.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rowIndex"/> is less than zero or large then <see cref="Matrix{T}"/>.
        /// <paramref name="columnIndex"/> is less than zero or large then <see cref="Matrix{T}"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When set value that does not match to logic of derived class.
        /// </exception>
        public T this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckIndexesRange(rowIndex, columnIndex);
                return GetValue(rowIndex, columnIndex);
            }
            set
            {
                CheckIndexesRange(rowIndex, columnIndex);
                SetValue(rowIndex, columnIndex, value);
            }
        }

        /// <summary>
        /// Checks the elements of the input array for compliance with the logic of the selected matrix.
        /// </summary>
        /// <param name="elements">
        /// Array for the checking.
        /// </param>
        protected abstract void VerifyMatrixElements(T[,] elements);

        /// <summary>
        /// Initializes the internal storage of items in the input array.
        /// </summary>
        /// <param name="elements">
        /// Array for initialize.
        /// </param>
        protected abstract void InitializeMatrix(T[,] elements);

        /// <summary>
        /// Sets a new value in the matrix for the given indexes.
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
        protected abstract void SetValue(int rowIndex, int columnIndex, T value);

        /// <summary>
        /// Return an element from the matrix for the given indexes.
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
        protected abstract T GetValue(int rowIndex, int columnIndex);

        protected void OnChangeValue(MatrixArgs matrixArgs)
        {
            if (matrixArgs == null)
            {
                throw new ArgumentNullException($"The {nameof(matrixArgs)} can not be null.");
            }
           
            ChengeValue.Invoke(this, matrixArgs);
        }

        protected void ChangeValueInMatrix(int rowIndex, int columnIndex, string message)
        {
            string eventMessage = $"The value in row index {rowIndex} and {columnIndex}  - {message}.";
            MatrixArgs matrixArgs = new MatrixArgs(eventMessage);
            OnChangeValue(matrixArgs);
        }

        protected bool IsAllowedForSquareMatrix(T[,] elements)
        {
            return elements.GetLength(firstDemension) == elements.GetLength(secondDemension);     
        }    

        private void CheckIndexesRange(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= MatrixOrder)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(rowIndex)} is out of range.");
            }

            if (columnIndex < 0 || columnIndex >= MatrixOrder)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(columnIndex)} is out of range.");
            }
        }        
    }

    /// <summary>
    /// Represent a data class for <see cref="Matrix{T}.ChengeValue"/> event.
    /// </summary>
    public class MatrixArgs : EventArgs
    {
        private string message;

        public string Message { get => message; }

        public MatrixArgs(string message)
        {
            this.message = message;
        }
    }
}
