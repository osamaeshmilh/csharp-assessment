using System;
using System.Diagnostics.Metrics;
using System.Text;
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

        #region(Fields)

        private int[] binaryNumber = new int[16];
        bool is_negative = false;

        #endregion

        #region(Properties)
        #endregion

        #region(Index operator)
        public int this[int index]
        {
            get => GetValue(index);
            set => SetValue(value, index);
        }

        public int GetValue(int index) => binaryNumber[index];
        public int SetValue(int value, int index) => binaryNumber[index] = value;
        #endregion

        #region(Implicit Convertors: int to Binary, Binary to int)

        public static implicit operator Binary(int intValue)
        {
            return new Binary(intValue);
        }

        public static implicit operator Binary(int[] intArrayValue)
        {
            return new Binary(intArrayValue);
        }

        public Binary(int[] intArrayValue)
        {
            this.binaryNumber = intArrayValue;
        }

        public Binary(int intValue)
        {
            if (intValue < 0)
                is_negative = true;    
            for (int i = 0; i < 16; i++)
                binaryNumber[16 - 1 - i] = (intValue & (1 << i)) != 0 ? 1 : 0;
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
            Binary b = binaryNumber;
            Binary One = 0b0001;
            if (binaryNumber[0] == 1)
            {
                b.is_negative = true;
                b = ~b;
                b += One;
            }
            double sum = 0;
            int counter = 0;
            for (int i = b.binaryNumber.Length - 1; i >= 0; i--)
            {
                sum = sum + (b[i] * this.PowerOf(2, counter));
                counter++;
            }
            return binaryNumber[0] == 1 ? -sum : sum;

        }

        public int PowerOf(int number, int power)
        {
            int sum = 1;
            for(int i = 0; i < power; i++)
            {
                sum = number * sum;
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

        public static Binary operator ~(Binary number)
        {
            Binary oneComplement = new Binary(0);
            for(int i = 0;  i < number.binaryNumber.Length; i++)
            {
                oneComplement[i] = number.binaryNumber[i] == 1 ? 0 : 1;
            }
            return oneComplement;
        }

        public static Binary operator -(Binary number)
        {
            Binary b = number;
            Binary One = 0b0001;
            b = ~b;
            b += One;
            return b;
        }

        #endregion

        #region(Binary Arithmatic Opertors: +, -, *, /)

        public static Binary operator +(Binary number1, Binary number2)
        {
            int[] sum = new int[16];
            int carry = 0;
            int add = 0;
            for (int i = 15; i >= 0; i--)
            {
                add = number1[i] + number2[i] + carry;
                if (add == 3)
                {
                    sum[i] = 1;
                    carry = 1;
                }
                else if (add == 2)
                {
                    sum[i] = 0;
                    carry = 1;
                }
                else
                {
                    sum[i] = add;
                    carry = 0;
                }
            }
            return new Binary(sum);
        }

        public static Binary operator -(Binary number1, Binary number2)
        {
            Binary n1 = number1;
            Binary n2 = number2;
            n2= ~n2;
            n2 += 0b0001;
            return n1 + n2;
        }

        public static Binary operator /(Binary number1, Binary number2)
        {
            //Console.WriteLine("shiftAmount = " + number2.ToDecimal());
            //double shiftAmount = (number2.ToDecimal() / 2.2);
            //Console.WriteLine("shiftAmount = " + shiftAmount );

            //Binary b = (number1 >> (int)shiftAmount);
            //Console.WriteLine("Div = " + b.ToDecimal() + " bi = " + b.ToString());
            return 0b1010;
        }

        public static Binary operator *(Binary number1, Binary number2)
        {
            Binary multiply = 0b0000; // Default value 0
            Binary result = 0b0000; // Default value 0
            int counter = 0;
            for(int i = number2.binaryNumber.Length -1;  i>= 0; i--)
            {
                for(int j = number1.binaryNumber.Length -1; j>= 0; j-- )
                {
                    if (j - counter < 0)
                        break;
                    multiply[j-counter] = number2[i] * number1[j];
                }
                counter++;
                result+= multiply;
            }
            return result;
        }

        private void setIntArrayValue(int[] result)
        {
            this.binaryNumber = result;
        }

        public static Binary operator %(Binary number1, Binary number2)
        {
            //TODO:: needs to be fixed to recive the second number as int binary array and return int binary result
            int k = (int)number2.ToDecimal();
            //this

            int n = 16;
            int[] pwrTwo = new int[n];
            pwrTwo[0] = 1 % k;
            for (int i = 1; i < n; i++)
            {
                pwrTwo[i] = pwrTwo[i - 1] * (2 % k);
                pwrTwo[i] %= k;
            }

            // To store the result
            int res = 0;
            int x = 0, j = n - 1;
            while (x < n)
            {
                // If current bit is 1
                if (number1[x] == 1)
                {
                    // Add the current power of 2
                    res += (pwrTwo[x]);
                    res %= k;
                }
                x++;
                j--;
            }

            return new Binary(res);
        }

        #endregion

        #region(Logical Operators: ==, !=, <, >, <=, >=)

        public static bool operator == (Binary number1, Binary number2)
        {
            for(int i = 0; i< 16; i++)
            {
                if (number1[i] != number2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator != (Binary number1, Binary number2)
        {
            return !(number1 == number2);
        }

        public static bool operator > (Binary number1, Binary number2)
        {
            if (number1[0] != 1 && number2[0] == 1)
            {
                return true;
            }
            else if (number1[0] == 1 && number2[0] != 1)
            {
                return false;
            }
            int count = 0;
            for(int i = 0; i < 16; i++)
            {
                if(number1[i] > number2[i])
                {
                    count++;
                }
                else if(number1[i] < number2[i])
                {
                    count--;
                }
            }
            return count > 0 ? true : false;
        }

        public static bool operator < (Binary number1, Binary number2)
        {
            return !(number1 > number2);
        }

        public static bool operator >= (Binary number1, Binary number2)
        {
            bool equal = number1 == number2;
            bool greater = number1 > number2;
            return equal || greater;
        }

        public static bool operator <= (Binary number1, Binary number2)
        {
            bool equal = number1 == number2;
            bool lessThan = number1 < number2;
            return equal || lessThan;
        }

        #endregion
    }
}
