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

            private int[] integerArguments;
            private double xMin, yMin, xStep, yStep;
            private int imageWidth, imageHeight;
            private List<ComplexNumber> roots;
            private string outputPath;
            private Bitmap outputImages;

            private static readonly Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange,
                Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            public void Initialization(string[] arguments)
            {
                integerArguments = ParseIntegerArguments(arguments);
                double[] doubleArguments = ParseDoubleArguments(arguments);

                imageWidth = integerArguments[0];
                imageHeight = integerArguments[1];

                xMin = doubleArguments[0];
                double xMax = doubleArguments[1];
                yMin = doubleArguments[2];
                double yMax = doubleArguments[3];

                xStep = (xMax - xMin) / imageWidth;
                yStep = (yMax - yMin) / imageHeight;

                outputPath = arguments[6];

                outputImages = new Bitmap(imageWidth, imageHeight);
                roots = new List<ComplexNumber>();
            }

            private int[] ParseIntegerArguments(string[] args)
            {
                int[] integerArguments = new int[2];
                for (int i = 0; i < integerArguments.Length; i++)
                {
                    integerArguments[i] = int.Parse(args[i]);
                }

                return integerArguments;
            }

            private double[] ParseDoubleArguments(string[] args)
            {
                double[] doubleArguments = new double[4];
                for (int i = 0; i < doubleArguments.Length; i++)
                {
                    doubleArguments[i] = double.Parse(args[i + 2]);
                }

                return doubleArguments;
            }

            public Polynomial CreatePolynomial()
            {
                Polynomial polynomial = new Polynomial();
                polynomial.Coefficients.Add(new ComplexNumber() { Re = 1 });
                polynomial.Coefficients.Add(ComplexNumber.Zero);
                polynomial.Coefficients.Add(ComplexNumber.Zero);
                polynomial.Coefficients.Add(new ComplexNumber() { Re = 1 });

                return polynomial;
            }

            public void EvaluatePolynomial(Polynomial polynomial, Polynomial derivationPolynomial)
            {
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
                            var diff = polynomial.SolvePolynomial(currentPoint).
                                Divide(derivationPolynomial.SolvePolynomial(currentPoint));

                            currentPoint = currentPoint.Subtract(diff);

                            if (IsAchievedAccuracy(diff))
                            {
                                k--;
                            }
                        }

                        int rootIdentifier = FindRoots(roots, currentPoint);
                        SetPixelsColors(j, i, rootIdentifier);
                    }
                }

                SaveImage();
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
                    if (IsKnowTheFoundRoot(roots[i], currentPoint))
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

            private bool IsKnowTheFoundRoot(ComplexNumber root, ComplexNumber currentPoint)
            {
                return Math.Pow(currentPoint.Re - root.Re, 2) + Math.Pow(currentPoint.Im - root.Im, 2) <= ACCURACY_OF_ROOT_COMPARISON;
            }

            private void SetPixelsColors(int x, int y, int rootIdentifier)
            {
                Color pixelColor = colors[rootIdentifier % colors.Length];
                outputImages.SetPixel(x, y, pixelColor);
            }

            private void SaveImage()
            {
                outputImages.Save(outputPath ?? "../../../out.png");
            }
        }
    }
}
