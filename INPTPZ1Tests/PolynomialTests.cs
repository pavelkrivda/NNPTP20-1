using INPTPZ1.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPTPZ1Tests
{
    [TestClass()]
    public class PolynomialTests
    {
        [TestMethod()]
        public void DeriveTest()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Re = 5 });
            polynomial.Coefficients.Add(new ComplexNumber() { Re = 10 });

            Polynomial actual = polynomial.Derive();
            ComplexNumber shouldBe = new ComplexNumber() { Re = 10 };

            Assert.AreEqual(shouldBe, actual.Coefficients[0]);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Re = 5 });

            string actual = polynomial.ToString();
            string shouldBe = "(5 + 0i)";

            Assert.AreEqual(shouldBe, actual);
        }
    }
}
