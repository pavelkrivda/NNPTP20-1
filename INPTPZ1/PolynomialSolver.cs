using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class PolynomialSolver
        {
            private const double PRECISION = 0.5;
            private const byte ITERATIONS_COUNT = 30;
            private const double ACCURACY_OF_ROOT_COMPARISON = 0.01;

            private double xMin, yMin, xStep, yStep;
            private int imageWidth, imageHeight;
            private List<ComplexNumber> roots;
            private string outputPath;
            private FractalImage outputImage;


            public void Initialization(string[] arguments)
            {
                if (arguments.Length != 7)
                {
                    throw new ArgumentException("Musí být zadáno 7 vstupních parametrů!");
                }

                var parseArguemnts = ParseInputArguemnts(arguments);
                var integerArguments = parseArguemnts.Item1;
                var doubleArguments = parseArguemnts.Item2;

                imageWidth = integerArguments[0];
                imageHeight = integerArguments[1];
                xMin = doubleArguments[0];
                double xMax = doubleArguments[1];
                yMin = doubleArguments[2];
                double yMax = doubleArguments[3];

                xStep = (xMax - xMin) / imageWidth;
                yStep = (yMax - yMin) / imageHeight;

                outputPath = arguments[6];

                outputImage = new FractalImage(imageWidth, imageHeight);
                roots = new List<ComplexNumber>();
            }

            private static Tuple<int[], double[]> ParseInputArguemnts(string[] arguments)
            {
                var integerArguments = new int[2];
                for (int i = 0; i < integerArguments.Length; i++)
                {
                    if (!int.TryParse(arguments[i], out integerArguments[i]))
                    {
                        throw new ArgumentException("Neplatné vstupní argumenty!");
                    }
                }

                var doubleArguments = new double[4];
                for (int i = 0; i < doubleArguments.Length; i++)
                {
                    if (!double.TryParse(arguments[i + 2], out doubleArguments[i]))
                    {
                        throw new ArgumentException("Neplatné vstupní argumenty!");
                    }
                }

                return new Tuple<int[], double[]>(integerArguments, doubleArguments);
            }

            public Polynomial CreatePolynomial()
            {
                Polynomial polynomial = new Polynomial();
                polynomial.Coefficients.Add(new ComplexNumber() {Re = 1});
                polynomial.Coefficients.Add(ComplexNumber.Zero);
                polynomial.Coefficients.Add(ComplexNumber.Zero);
                polynomial.Coefficients.Add(new ComplexNumber() {Re = 1});

                return polynomial;
            }

            public void EvaluatePolynomial(Polynomial polynomial, Polynomial derivationPolynomial)
            {
                int rootIdentifier;
                for (int i = 0; i < imageWidth; i++)
                {
                    for (int j = 0; j < imageHeight; j++)
                    {
                        double xCoordinate = xMin + i * xStep;
                        double yCoordinate = yMin + j * yStep;

                        ComplexNumber currentPoint = new ComplexNumber()
                        {
                            Re = xCoordinate,
                            Im = yCoordinate
                        };

                        for (int k = 0; k < ITERATIONS_COUNT; k++)
                        {
                            var diff = polynomial.SolvePolynomial(currentPoint)
                                .Divide(derivationPolynomial.SolvePolynomial(currentPoint));

                            currentPoint = currentPoint.Subtract(diff);

                            if (IsAchievedAccuracy(diff))
                            {
                                k--;
                            }
                        }

                        rootIdentifier = FindRoots(roots, currentPoint);
                        outputImage.SetPixelsColors(j, i, rootIdentifier);
                    }
                }

                outputImage.SaveImage(outputPath);
            }

            private bool IsAchievedAccuracy(ComplexNumber diff)
            {
                return Math.Pow(diff.Re, 2) + Math.Pow(diff.Im, 2) >= PRECISION;
            }

            private int FindRoots(List<ComplexNumber> roots, ComplexNumber currentPoint)
            {
                var known = false;
                var rootIdentifier = 0;
                for (int i = 0; i < roots.Count; i++)
                {
                    if (RootEquals(roots[i], currentPoint))
                    {
                        known = true;
                        rootIdentifier = i;
                    }
                }

                if (!known)
                {
                    roots.Add(currentPoint);
                    rootIdentifier = roots.Count;
                }

                return rootIdentifier;
            }

            private bool RootEquals(ComplexNumber root, ComplexNumber currentPoint)
            {
                return Math.Pow(currentPoint.Re - root.Re, 2) + Math.Pow(currentPoint.Im - root.Im, 2) <=
                       ACCURACY_OF_ROOT_COMPARISON;
            }
        }
    }
}
