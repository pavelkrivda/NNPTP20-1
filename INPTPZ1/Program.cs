using INPTPZ1.Mathematics;
using System;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            PolynomialSolver polynomialSolver = new PolynomialSolver();
            polynomialSolver.Initialization(args);

            Polynomial polynomial = polynomialSolver.CreatePolynomial();
            Polynomial polynomialDerivation = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(polynomialDerivation);

            polynomialSolver.EvaluatePolynomial(polynomial, polynomialDerivation);
        }
    }
}
