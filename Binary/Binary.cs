using System;
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
        int X
        {
            get;
            set;
        }

        int[] binaryNumber = new int[16];

        public static implicit operator Binary(int value)
        {
            //Console.WriteLine($"{value});
            //Boolean isNegative = false;
            //string binaryNumber = Convert.ToString(value, 2);
            //Console.WriteLine(binaryNumber);
            //if(value < 0)
            //{
            //    isNegative = true;
            //}
            //int sum = 0;
            //int counter = 0;
            ////isNegative ? 16 : binaryNumber.Length - 1;
            //for (int i = binaryNumber.Length - 1; i >= 0; i--)
            //{
            //    sum = sum + (int.Parse(binaryNumber[i].ToString()) * Convert.ToInt16(Math.Pow(2, counter)));
            //    counter++;
            //}
            //Console.WriteLine($"sum is: {sum}");
            return new Binary()
            {
                X = value
            };
        }
        
        public override string ToString()
        {
            binaryNumber = Convert.ToString(X, 2).Select(c => c - '0').ToArray();
            int missingBits = 16 - binaryNumber.Length;
            string temp = "";
            int space = 0;
            for (int i = 0; i < missingBits; i++)
            {
                if (i != 0 && i % 4 == 0)
                {
                    temp += ' ';
                    space++;
                }
                temp += '0';
            }
            for (int i = 0; i < binaryNumber.Length; i++)
            {
                if ((temp.Length - space ) % 4 == 0)
                {
                    temp += " ";
                    space++;
                }
                temp += binaryNumber[i];
            }
            return temp.ToString();
        }

        public static Binary operator << (Binary number, int place)
        {
            return number;
        }

        #region(Fields)
        #endregion
        #region(Properties)
        #endregion
        #region(Index operator)
        #endregion
        #region(Implicit Convertors: int to Binary, Binary to int)
        #endregion
        #region(Methods: ToDecimal, ToString)
        #endregion
        #region(Shift Opertors: Shift to left by n (<<), Shift to right by n (>>))
        #endregion
        #region(Binary Operators: Ones' complement, Negation)
        #endregion
        #region(Binary Arithmatic Opertors: +, -, *, /)
        #endregion
        #region(Logical Operators: ==, !=, <, >, <=, >=)
        #endregion
    }
}
