using System.Collections.Generic;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class Polynomial
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public Polynomial Derive()
            {
                Polynomial polynomial = new Polynomial();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    polynomial.Coefficients.Add(
                        Coefficients[i].Multiply(new ComplexNumber() { Re = i }));
                }

                return polynomial;
            }

            public ComplexNumber SolvePolynomial(ComplexNumber complexNumber)
            {
                ComplexNumber outputComplexNumber = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber tempComplexNumber = complexNumber;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            tempComplexNumber = tempComplexNumber.Multiply(complexNumber);

                        coefficient = coefficient.Multiply(tempComplexNumber);
                    }

                    outputComplexNumber = outputComplexNumber.Add(coefficient);
                }

                return outputComplexNumber;
            }

            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    result += Coefficients[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            result += "x";
                        }
                    }
                    
                    result += i == Coefficients.Count - 1 ? "" : " + ";
                }
                return result;
            }
        }
    }
}
