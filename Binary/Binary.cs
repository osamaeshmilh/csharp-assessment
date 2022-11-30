using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace CP
{
    // Requirements, Assignment/Implementation Rules:
    //  You must work with 16 bits, hint: Use an int array of 16 as a field
    //  Binary operators: ~, <<, >>
    //  Binary, Arithmatic, and Logical operators: +, -, *, /, ==, !=, <, >, <=, >= algorithms
    //      must perform in binary. Algorthims that convert to denary, perform in denary, and convert
    //      back to binary carry 0 marks.

    class Binary
    {

        #region(Fields)

        private int[] binaryNumber = new int[16];

        #endregion

        #region(Properties)
        #endregion

        #region(Index operator)
        #endregion

        #region(Implicit Convertors: int to Binary, Binary to int)

        public static implicit operator Binary(int value)
        {
            return new Binary(value);
        }

        public Binary(int value)
        {
            int[] tempBinaryNumber = new int[0];
            string binaryNumberInStringFormat = Convert.ToString(value, 2);
            if (binaryNumberInStringFormat.Length > 16)
            {
                tempBinaryNumber = binaryNumberInStringFormat.Substring(16).Select(c => c - '0').ToArray();
            }
            else
            {
                tempBinaryNumber = binaryNumberInStringFormat.Select(c => c - '0').ToArray();
            }
            int missingBits = 16 - tempBinaryNumber.Length;
            int counter = 0;
            for (int i = 0; i < missingBits; i++)
            {
                binaryNumber[i] = 0;
            }
            for (int i = missingBits; i < 16; i++)
            {
                binaryNumber[i] = tempBinaryNumber[counter];
                counter++;
            }
        }

        #endregion

        #region(Methods: ToDecimal, ToString)

        public override string ToString()
        {
            string temp = "";
            for (int i = 0; i < binaryNumber.Length; i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    temp += " ";
                }
                temp += binaryNumber[i];
            }
            return temp;
        }


        public double ToDecimal()
        {
            double sum = 0;
            int counter = 0;
            for (int i = binaryNumber.Length - 1; i >= 0; i--)
            {
                sum = sum + (binaryNumber[i] * Math.Pow(2, counter));
                counter++;
            }
            return sum;

        }

        #endregion

        #region(Shift Opertors: Shift to left by n (<<), Shift to right by n (>>))

        public static Binary operator <<(Binary number, int place)
        {
            int newArrayLength = number.binaryNumber.Length - place;
            int[] tempNumber = new int[newArrayLength];
            for (int i = place; i < number.binaryNumber.Length; i++)
            {
                tempNumber[i - place] = number.binaryNumber[i];
            }
            for (int i = 0; i < 16; i++)
            {
                if (i >= tempNumber.Length)
                {
                    number.binaryNumber[i] = 0;
                }
                else
                {
                    number.binaryNumber[i] = tempNumber[i];
                }
            }
            return number;
        }

        public static Binary operator >>(Binary number, int place)
        {
            int newArrayLength = number.binaryNumber.Length - place;
            int[] tempNumber = new int[newArrayLength];
            for (int i = 0; i < newArrayLength; i++)
            {
                tempNumber[i] = number.binaryNumber[i];
            }
            for (int i = 0; i < 16; i++)
            {
                if (i < place)
                {
                    number.binaryNumber[i] = 0;
                }
                else
                {
                    number.binaryNumber[i] = tempNumber[i - place];
                }
            }
            return number;
        }


        #endregion

        #region(Binary Operators: Ones' complement, Negation)

        #endregion

        #region(Binary Arithmatic Opertors: +, -, *, /)

        public static Binary operator +(Binary number1, Binary number2)
        {
            int[] sum = new int[16];
            int carry = 0;
            int add = 0;
            for (int i = 15; i >= 0; i--)
            {
                add = number1.binaryNumber[i] + number2.binaryNumber[i] + carry;
                if (add == 3)
                {
                    sum[i] = 1;
                    carry = 1;
                }
                else if (add == 2)
                {
                    sum[i] = 0;
                    carry = 0;
                }
                else
                {
                    sum[i] = add;
                    carry = 0;
                }
            }
            //TODO: fix this return, Need to implement implicit conversion for Binary to Int and vice-versa
            return new Binary(0b1010);
        }

        public static Binary operator -(Binary number1, Binary number2)
        {
            return new Binary(0b1010);
        }

        public static Binary operator /(Binary number1, Binary number2)
        {
            return new Binary(0b1010);
        }

        public static Binary operator *(Binary number1, Binary number2)
        {
            return new Binary(0b1010);
        }

        public static Binary operator %(Binary number1, Binary number2)
        {
            return new Binary(0b1010);
        }

        #endregion

        #region(Logical Operators: ==, !=, <, >, <=, >=)

        public static bool operator == (Binary number1, Binary number2)
        {
            return true;
        }

        public static bool operator !=(Binary number1, Binary number2)
        {
            return false;
        }

        public static bool operator > (Binary number1, Binary number2)
        {
            return false;
        }

        public static bool operator < (Binary number1, Binary number2)
        {
            return false;
        }

        public static bool operator >= (Binary number1, Binary number2)
        {
            return false;
        }

        public static bool operator <= (Binary number1, Binary number2)
        {
            return false;
        }

        #endregion
    }
}
