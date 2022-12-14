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

        //Constant for the integer array length of 16 to be used in the class
        const int ArrayLength = 16;
        //the binary array using 16 integer array to store 0 and 1 bits
        private int[] binaryNumberArray = new int[ArrayLength];

        #endregion

        #region(Properties)
        #endregion

        #region(Index operator)

        //index operator to access the int[] array in the class Ex: Binary binary[1];
        public int this[int index]
        {
            get => GetValue(index);
            set => SetValue(value, index);
        }

        public int GetValue(int index) => binaryNumberArray[index];
        public int SetValue(int value, int index) => binaryNumberArray[index] = value;
        #endregion

        #region(Implicit Convertors: int to Binary, Binary to int)
        //Binary implicit Convertors to convert and store int values in int array Ex: Binary binary = 23;
        public static implicit operator Binary(int intValue)
        {
            return new Binary(intValue);
        }

        //Binary implicit Convertors to store int array value Ex: Binary binary = new int[16];
        public static implicit operator Binary(int[] intArrayValue)
        {
            return new Binary(intArrayValue);
        }

        public Binary(int[] intArrayValue)
        {
            this.binaryNumberArray = intArrayValue;
        }

        public Binary(int intValue)
        {
            //for loop for 16 bit as identified in ArrayLength Const
            //to get decimal intValue binary implementation using AND operator with one shifted by bit location
            for (int i = 0; i < ArrayLength; i++)
                binaryNumberArray[ArrayLength - 1 - i] = (intValue & (1 << i)) != 0 ? 1 : 0;
        }

        #endregion

        #region(Methods: ToDecimal, ToString)

        public override string ToString()
        {
            string binaryString = "";
            //for loop through the binaryArray
            for (int i = 0; i < ArrayLength; i++)
            {
                //adding space after every four bits
                if (i != 0 && i % 4 == 0)
                    binaryString += " ";
                //Appending Bits
                binaryString += binaryNumberArray[i];
            }
            return binaryString;
        }


        public double ToDecimal()
        {
            Binary binary = binaryNumberArray;
            //checking if the binaryNumberArray starts with one if yes it means it is a negative number
            if (binaryNumberArray[0] == 1)
            {
                //taking the two's complement
                binary = -binary;
            }
            double sum = 0;
            int counter = 0;
            //for loop to take power of bits location if they were one's
            for (int i = ArrayLength - 1; i >= 0; i--)
            {
                sum = sum + (binary[i] * this.PowerOf(2, counter));
                counter++;
            }
            return binaryNumberArray[0] == 1 ? -sum : sum;

        }

        public int PowerOf(int number, int power)
        {
            int sum = 1;
            for(int i = 0; i < power; i++)
                sum = number * sum;
            return sum;
        }

        #endregion

        #region(Shift Opertors: Shift to left by n (<<), Shift to right by n (>>))

        public static Binary operator <<(Binary number, int place)
        {
            int newArrayLength = ArrayLength - place;
            int[] tempNumber = new int[newArrayLength];
            for (int i = place; i < ArrayLength; i++)
            {
                tempNumber[i - place] = number.binaryNumberArray[i];
            }
            for (int i = 0; i < ArrayLength; i++)
            {
                if (i >= tempNumber.Length)
                {
                    number.binaryNumberArray[i] = 0;
                }
                else
                {
                    number.binaryNumberArray[i] = tempNumber[i];
                }
            }
            return number;
        }

        public static Binary operator >>(Binary number, int place)
        {
            int newArrayLength = ArrayLength - place;
            int[] tempNumber = new int[newArrayLength];
            for (int i = 0; i < newArrayLength; i++)
            {
                tempNumber[i] = number.binaryNumberArray[i];
            }
            for (int i = 0; i < ArrayLength; i++)
            {
                if (i < place)
                {
                    number.binaryNumberArray[i] = 0;
                }
                else
                {
                    number.binaryNumberArray[i] = tempNumber[i - place];
                }
            }
            return number;
        }


        #endregion

        #region(Binary Operators: Ones' complement, Negation)

        public static Binary operator ~(Binary number)
        {
            Binary oneComplement = new Binary(0);
            for(int i = 0;  i < ArrayLength; i++)
            {
                oneComplement[i] = number.binaryNumberArray[i] == 1 ? 0 : 1;
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
            int[] sum = new int[ArrayLength];
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

        public static Binary operator /(Binary numberOneBinary, Binary numberTwoBinary)
        {
            Binary divisor = new Binary(new int[ArrayLength * 2]);
            Binary tempRemainder =  new Binary(new int[ArrayLength * 2]);//Initialized with dividend
            Binary tempQuotient = new Binary(new int[ArrayLength]);
            int[] remainder = new int[ArrayLength * 2];
            int[] quotient = new int[ArrayLength];

            for (int i = (ArrayLength * 2) - 1; i >= 0; i--)
            {
                divisor[i] = 0;
                tempRemainder[i] = 0;
            }
            for (int i = 0; i < ArrayLength; i++)
            {
                tempRemainder[i + ArrayLength] = numberOneBinary[i];
                tempQuotient[i] = 0;
                divisor[i] = numberTwoBinary[i];
            }

            for (int j = 0; j <= ArrayLength; j++)
            {
                tempRemainder = tempRemainder - divisor;
                //if(!tempRemainder[0])
                if (tempRemainder[0] == 0)
                {
                    //shift quotient left and set rightmost bit to 1
                    for (int i = 0; i < ArrayLength - 1; i++)
                    {
                        tempQuotient[i] = tempQuotient[i + 1];
                    }
                    tempQuotient[ArrayLength - 1] = 1;
                }
                else
                {
                    tempRemainder = tempRemainder + divisor;
                    //tempQuotient = shiftLeft(tempQuotient);
                    tempQuotient = tempQuotient << 1;
                }
                //divisor = shiftRight(divisor);
                divisor = divisor >> 1;
            }
            for (int i = ArrayLength - 1; i >= 0; i--)
            {
                remainder[i] = tempRemainder[i];
            }
            for (int i = ArrayLength - 1; i >= 0; i--)
            {
                quotient[i] = tempQuotient[i];
            }
            return quotient;

        }

        public static Binary operator *(Binary number1, Binary number2)
        {
            Binary multiply = 0b0000; // Default value 0
            Binary result = 0b0000; // Default value 0
            int counter = 0;
            //nested for loops to multiply all bits of both numbers
            for(int i = ArrayLength - 1;  i>= 0; i--)
            {
                for(int j = ArrayLength - 1; j>= 0; j-- )
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

        public static Binary operator %(Binary number1, Binary number2)
        {
            //TODO:: needs to be fixed to recive the second number as int binary array and return int binary result
            int k = (int)number2.ToDecimal();
            //this

            int n = ArrayLength;
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
            for(int i = 0; i< ArrayLength; i++)
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
            for(int i = 0; i < ArrayLength; i++)
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
