using NUnit.Framework;

namespace MatrixLogic.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        private static int[,] firstSquareArray;
        private static int[,] secondSquareArray;

        private static int[,] firstSymmetricArray;
        private static int[,] secondSymmetricArray;

        private static int[,] firstDiagonalArray;
        private static int[,] secondDiagonalArray;

        [SetUp]
        public void TestSetUp()
        {
            firstSquareArray = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            secondSquareArray = new int[3, 3] { { 1, -2, -3 }, { -4, -5, -6 }, { 7, 8, 9 } };

            firstSymmetricArray = new int[3, 3] { {1, 3, 0 }, {3, 2, 6 }, {0, 6, 5 } };
            secondSymmetricArray = new int[3, 3] { { 1, 3, 0 }, { 3, 2, 6 }, { 0, 6, 5 } };

            firstDiagonalArray = new int[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            secondDiagonalArray = new int[3, 3] { { 2, 0, 0 }, { 0, 2, 0 }, { 0, 0, 2 } };

        }

        [Test]
        public void Matrix_Create_SquareMatrix()
        {
            // Act
            Matrix<int> matrix = new SquareMatrix<int>(firstSquareArray);

            // Assert
            Assert.AreEqual(matrix[0, 0], 1);
            Assert.AreEqual(matrix[1, 0], 4);
        }

        [Test]
        public void Matrix_Sum_Two_SquareMatrix()
        {
            // Arrange
            Matrix<int> firstMatrix = new SquareMatrix<int>(firstSquareArray);
            Matrix<int> secondMatrix = new SquareMatrix<int>(secondSquareArray);

            // Act
            Matrix<int> result = firstMatrix.Add(secondMatrix);

            // Assert
            Assert.AreEqual(result[0, 0], 2);
            Assert.AreEqual(result[0, 1], 0);
            Assert.AreEqual(result[2, 2], 18);
        }

        [Test]
        public void Matrix_Sum_Square_And_SymmetricMatrix()
        {
            // Arrange
            Matrix<int> firstMatrix = new SquareMatrix<int>(firstSquareArray);
            Matrix<int> secondMatrix = new SymmetricMatrix<int>(firstSymmetricArray);

            // Act
            Matrix<int> result = firstMatrix.Add(secondMatrix);

            // Assert
            Assert.AreEqual(result[0, 0], 2);
            Assert.AreEqual(result[0, 1], 5);
            Assert.AreEqual(result[0, 2], 3);
        }

        [Test]
        public void Matrix_Sum_Square_And_DiagonalMatrix()
        {
            // Arrange
            Matrix<int> firstMatrix = new SquareMatrix<int>(firstSquareArray);
            Matrix<int> secondMatrix = new DiagonalMatrix<int>(firstDiagonalArray);

            // Act
            Matrix<int> result = firstMatrix.Add(secondMatrix);

            // Assert
            Assert.AreEqual(result[0, 0], 2);
            Assert.AreEqual(result[0, 1], 2);
            Assert.AreEqual(result[0, 2], 3);
        }
    }
}
