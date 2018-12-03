using System;
using System.Collections.Generic;

namespace MatrixLogic
{
    /// <summary>
    /// Represents a class for work with diagonal matrix and implrmrnts <see cref="Matrix{T}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of elements in matrix.
    /// </typeparam>
    public class DiagonalMatrix<T> : Matrix<T>
    {
        private T[] elements;

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
        public DiagonalMatrix(T[,] elements) : base(elements) { }

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
        public DiagonalMatrix(T[,] elements, IComparer<T> comparer): base(elements,comparer) { }

        /// <summary>
        /// Constructor with a specific dimension.
        /// </summary>
        /// <param name="dimension">
        /// The dimension of matrix.
        /// </param>
        /// <exception cref="ArgumentException">Theh <paramref name="dimension"/>is less than one.</exception>
        public DiagonalMatrix(int dimension) : base(dimension)
        {
            elements = new T[dimension];
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
            this.elements = new T[dimension];
            MatrixOrder = dimension;

            for (int i = 0; i < dimension; i++)
            {
                this.elements[i] = elements[i, i];
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
        /// The items of input array outside the main diagonal have value is not default T.
        /// </exception>
        protected override void VerifyMatrixElements(T[,] elements)
        {
            if (!IsAllowedForSquareMatrix(elements))
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a diagonal matrix. Array must have equal count of columns and rows.");
            }
            //TODO подумать. а может это не ответсвенность вообще этого класса (как с binary search проверка на отсортированный массив). 
            //и если клиент сам неверные данные положил - сам себе буратино. 
            if (!IsAllowedForDioganalMatrix(elements))
            {
                throw new ArgumentException($"The elements of {nameof(elements)} are not match for a diagonal matrix.");
            }
        }

        /// <summary>
        /// Implements <see cref="Matrix{T}.GetValue(int, int)"/> according rules of diagonal matrix.
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
            if (rowIndex != columnIndex)
            {
                return default(T);
            }

            return elements[rowIndex];            
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
        /// <exception cref="ArgumentException">
        /// When try change value outside the main diagonal.
        /// </exception>
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
