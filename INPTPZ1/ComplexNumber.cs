﻿using System;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double Re { get; set; }
            public double Im { get; set; }

            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber complexNumber = obj as ComplexNumber;
                    return complexNumber.Re == Re && complexNumber.Im == Im;
                }
                return base.Equals(obj);
            }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                Re = 0,
                Im = 0
            };

            public double GetAbs()
            {
                return Math.Sqrt(Re * Re + Im * Im);
            }

            public double GetAngleInDegrees()
            {
                return Math.Atan(Im / Re);
            }

            public ComplexNumber Add(ComplexNumber second)
            {
                ComplexNumber first = this;

                return new ComplexNumber()
                {
                    Re = first.Re + second.Re,
                    Im = first.Im + second.Im
                };
            }

            public ComplexNumber Subtract(ComplexNumber subtrahend)
            {
                ComplexNumber minued = this;
                return new ComplexNumber()
                {
                    Re = minued.Re - subtrahend.Re,
                    Im = minued.Im - subtrahend.Im
                };
            }

            public ComplexNumber Multiply(ComplexNumber multiplicand)
            {
                ComplexNumber multiplier = this;

                return new ComplexNumber()
                {
                    Re = multiplier.Re * multiplicand.Re - multiplier.Im * multiplicand.Im,
                    Im = multiplier.Re * multiplicand.Im + multiplier.Im * multiplicand.Re
                };
            }
            
            public ComplexNumber Divide(ComplexNumber divisor)
            {
                var numerator = Multiply(new ComplexNumber() { Re = divisor.Re, Im = -divisor.Im });
                var denominator = divisor.Re * divisor.Re + divisor.Im * divisor.Im;

                return new ComplexNumber()
                {
                    Re = numerator.Re / denominator,
                    Im = numerator.Im / denominator
                };
            }

            public override string ToString()
            {
                return $"({Re} + {Im}i)";
            }
        }
    }
}
