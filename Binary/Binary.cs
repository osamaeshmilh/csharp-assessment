using System;
using System.Text.RegularExpressions;

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

        Int16 X;
        Int16[] binaryArray = {0,0,0,0, 0,0,0,0, 0,0,0,0, 0,0,0,0};
        string binaryNumber = "";
        public static implicit operator Binary(Int16 value)
        {
            //Console.WriteLine($"{value});
            Boolean isNegative = false;
            string binaryNumber = Convert.ToString(value, 2);
            Console.WriteLine(binaryNumber);
            if(value < 0)
            {
                isNegative = true;
            }
            int sum = 0;
            int counter = 0;
            //isNegative ? 16 : binaryNumber.Length - 1;
            for (int i = binaryNumber.Length - 1; i >= 0; i--)
            {
                sum = sum + (int.Parse(binaryNumber[i].ToString()) * Convert.ToInt16(Math.Pow(2, counter)));
                counter++;
            }
            Console.WriteLine($"sum is: {sum}");
            return new Binary()
            {
                X = value
            };
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
