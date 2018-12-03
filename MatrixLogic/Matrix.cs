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
        private int matrixOrder;

        public event EventHandler<MatrixArgs> ChengeValue = delegate { };

        public int MatrixOrder
        {
            get => matrixOrder;
            internal protected set => matrixOrder = value;
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

        protected abstract void VerifyMatrixElements(T[,] elements);

        protected abstract void InitializeMatrix(T[,] elements);

        protected abstract void SetValue(int rowIndex, int columnIndex, T value);

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
