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

            Random rand = new Random();

            for(long i = 0; i < arraySize; i++)
            {
                // feeding the array with random values up to the same value as arraySize.
                // note that duplicate values can occour.

                numbers[i] = rand.NextInt64(arraySize);
            }

            // sorts the array from smallest to biggest number
            Array.Sort(numbers);

            return numbers;
        }

        public static long GetNumberPosInArray(long valueToFind, long[] arrayToSearch)
        {
            return Array.BinarySearch(arrayToSearch, valueToFind);
        }


        public long GetPositionOfValue(long valueToFind, long[] arrayToSearchIn)
        {

            // TODO:
            // Method needs more work..!


            int numThreadsAvailable = Environment.ProcessorCount - 1; // we subtract one logical core from the amount, to not block the main thread completely

            long howManyArraysToCreate = arrayToSearchIn.LongLength / numThreadsAvailable; // we need to know how many arrays to create. Dividing it on number of threads available gives us a whole integer.
            long sizeOfEachArray = arrayToSearchIn.LongLength / howManyArraysToCreate; // calculates number of arrays to be created (minus the one remaining array)
            long sizeRestArray = arrayToSearchIn.LongLength % numThreadsAvailable; // notice the modulo operator. here we get how many elements must be in the remaining array.

            List<long[]> listOfArrays = new List<long[]>();
            long[] arrayToInsertToList = new long[sizeOfEachArray];

            long positionsToLoop = arrayToInsertToList.LongLength - sizeRestArray;

            for (long i = 0; i < positionsToLoop; i += sizeOfEachArray)
            {
                // notice "i" is increased by sizeOfEachArray variable

                Array.Copy(arrayToSearchIn, i, arrayToInsertToList, 0, sizeOfEachArray);

                listOfArrays.Add(arrayToInsertToList);
                Array.Clear(arrayToInsertToList);
            }

            long[] restArray = new long[sizeRestArray];
            Array.Copy(
                arrayToSearchIn, 
                arrayToSearchIn.LongLength - sizeRestArray, 
                restArray, 
                0, 
                sizeRestArray);

            listOfArrays.Add(restArray);


            return 0;
        }

        public static List<long[]> GetListOfArrays(long[] arrayToDivide)
        {

            //TODO
            // Get amount of cpu threads and divide arrayToDivide on X amounts of threads (minus main thread).
            // return a list of all the new and smaller arrays.




            return null;
        }


    }
}
