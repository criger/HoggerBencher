using System.Text;

namespace Helpers
{
    public class HelperMethods
    {
        // function to generate a random string of length n
        public static String GetAlphaNumericString(int n)
        {

            // choose a Character random from this String
            string AlphaNumericString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                    + "0123456789"
                    + "abcdefghijklmnopqrstuvxyz";

            // create StringBuffer size of AlphaNumericString
            StringBuilder sb = new StringBuilder(n);

            for (int i = 0; i < n; i++)
            {

                // generate a random number between
                // 0 to AlphaNumericString variable length
                int index
                        = (int)(AlphaNumericString.Length
                        * new Random().NextDouble());

                // add Character one by one in end of sb
                sb.Append(AlphaNumericString[index]);
            }

            return sb.ToString();
        }
        public static String GetAlphaNumericStringBadMethod(int n)
        {

            // this method CAN be used in order to further wreak havoc on the memory consumption.


            // choose a Character random from this String
            string AlphaNumericString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                    + "0123456789"
                    + "abcdefghijklmnopqrstuvxyz";

            // create StringBuffer size of AlphaNumericString
            string myString = "";

            for (int i = 0; i < n; i++)
            {

                // generate a random number between
                // 0 to AlphaNumericString variable length
                int index
                        = (int)(AlphaNumericString.Length
                        * new Random().NextDouble());

                // add Character one by one in end of sb
                myString = myString + AlphaNumericString[index];
            }

            return myString;
        }

        public static long[] GetBigArraySorted(long arraySize)
        {
            long[] numbers = new long[arraySize];

            for(long i = 0; i < arraySize; i++)
            {
                // feeding the array with random values up to the same value as arraySize.
                // note that duplicate values can occour.

                numbers[i] = new Random().NextInt64(arraySize);
            }

            // sorts the array from smallest to biggest number
            Array.Sort(numbers);

            return numbers;
        }


    }
}
