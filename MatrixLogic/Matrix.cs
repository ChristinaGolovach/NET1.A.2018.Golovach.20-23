using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLogic
{
    public abstract class Matrix<T> 
    {
        private const int firstDemension = 0;
        private const int secondDemension = 1;

        internal protected IComparer<T> comparer;
        internal protected T[,] elements;
        internal protected int matrixOrder;

        public event EventHandler<MatrixArgs> ChengeValue = delegate { };

        //TODO подумать. порядок ведь наверное только у квад матриц. 
        //тогда это св-во нужно не здесь, иначе этот класс не очень будет исп-ть для других видов матриц (прям, вектор).
        // или (но как-то не очень) в описаніі класса указать что он базовый только для квадратных и название класса поменять.
        public int MatrixOrder
        {
            get => matrixOrder;
        }

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

            VerifyMatrixElements(elements);

            InitializeMatrix(elements);

            comparer = Comparer<T>.Default;            
        }

        public Matrix(T[,] elements, IComparer<T> comparer)
        {            
            if (elements == null)
            {
                throw new ArgumentNullException($"The {nameof(elements)} can not be null.");
            }

            this.comparer = comparer ?? throw new ArgumentNullException($"The {nameof(comparer)} can not be null.");

            VerifyMatrixElements(elements);

            InitializeMatrix(elements);
            
        }

        public T this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckIndexesRange(rowIndex, columnIndex);
                return elements[rowIndex, columnIndex];
            }

            set
            {
                CheckIndexesRange(rowIndex, columnIndex);
                SetValue(rowIndex, columnIndex, value);
            }
        }

        protected abstract void VerifyMatrixElements(T[,] elements);

        protected abstract void SetValue(int rowIndex, int columnIndex, T value);

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
            MatrixArgs matrixArgs = new Matrix<T>.MatrixArgs(eventMessage);
            OnChangeValue(matrixArgs);
        }

        private void InitializeMatrix(T[,] elements)
        {
            int rowCount = elements.GetLength(firstDemension);
            int columnCount = elements.GetLength(secondDemension);

            this.elements = new T[rowCount, columnCount];

            //TODO подумать. опять же,если этот класс использовать базовым для прямоугольной матрицы,
            //только у квадратных матріц
            // matrixOrder = length;

            Array.Copy(elements, 0, this.elements, 0, elements.Length);            
        }

        private void CheckIndexesRange(int rowIndex, int columnIndex)
        {
            int rowCount = elements.GetLength(firstDemension);
            int columnCount = elements.GetLength(secondDemension);

            if (rowIndex < 0 || rowIndex >= rowCount)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(rowIndex)} is out of range.");
            }

            if (columnIndex < 0 || columnIndex >= columnCount)
            {
                throw new ArgumentOutOfRangeException($"The {nameof(columnIndex)} is out of range.");
            }
        }

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
}
