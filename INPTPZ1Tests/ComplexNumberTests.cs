using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {
        private ComplexNumber firstComplexNumber = new ComplexNumber()
        {
            Re = 10,
            Im = 20
        };
        
        private ComplexNumber secondComplexNumber = new ComplexNumber()
        {
            Re = -1,
            Im = 2
        };

        [TestMethod()]
        public void EqualsTest_Pass()
        {
            ComplexNumber compareComlexNumber = new ComplexNumber()
            {
                Re = 10,
                Im = 20
            };

            bool actual = firstComplexNumber.Equals(compareComlexNumber);

            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void EqualsTest_Error()
        {
            ComplexNumber compareComlexNumber = new ComplexNumber()
            {
                Re = 1,
                Im = 70
            };

            bool actual = firstComplexNumber.Equals(compareComlexNumber);

            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void GetAbsTest()
        {
            double actual = secondComplexNumber.GetAbs();
            double shouldBe = 2.23606797749979;

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod()]
        public void GetAngleInDegreesTest()
        {
            double actual = secondComplexNumber.GetAngleInDegrees();
            double shouldBe = -1.1071487177940904;

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber actual = firstComplexNumber.Add(secondComplexNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Re = 9,
                Im = 22
            };

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod()]
        public void SubtractTest()
        {
            ComplexNumber actual = firstComplexNumber.Subtract(secondComplexNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Re = 11,
                Im = 18
            };

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod()]
        public void MultiplyTest()
        {
            ComplexNumber actual = firstComplexNumber.Multiply(secondComplexNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Re = -50,
                Im = 0
            };

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod()]
        public void DivideTest()
        {
            ComplexNumber actual = firstComplexNumber.Divide(secondComplexNumber);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Re = 6,
                Im = -8
            };

            Assert.AreEqual(shouldBe, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string actual = firstComplexNumber.ToString();
            string shouldBe = $"({ firstComplexNumber.Re } + { firstComplexNumber.Im }i)";
            Assert.AreEqual(actual, shouldBe);
        }
    }
}


