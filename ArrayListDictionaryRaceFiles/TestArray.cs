using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArrayListDictionaryRaceFiles
{
    public class TestArrays
    {
        public static async Task writeToArray(int numObjects, bool writeString, bool writeInt, bool writeRandomValues)
        {
            if (writeInt)
            {
                await writeIntArray(numObjects, writeRandomValues);
            }
            if (writeString)
            {
                writeStringArray(numObjects, writeRandomValues);
            }
        }

        public static CompletableFuture<Void> writeThenOverWriteArray(int objectsToWrite, boolean writeInt, boolean writeString)
        {
            if (writeInt)
            {
                writeThenOverWriteIntArray(objectsToWrite);

            }
            if (writeString)
            {
                writeThenOverWriteStringArray(objectsToWrite);
            }

            return CompletableFuture.completedFuture(null);
        }


        private static async Task writeIntArray(int numObjects, bool feedRandomInts)
        {
            int[] intArr = new int[numObjects];

            if (feedRandomInts)
            {
                Random rand = new Random();
                for (int i = 0; i < numObjects; i++)
                {
                    intArr[i] = rand.Next(1000); // random number in range 0 to 999
                }
            }
            else
            {
                for (int i = 0; i < numObjects; i++)
                {
                    intArr[i] = i;
                }
            }
        }

        private static async Task writeStringArray(int numObjects, bool feedRandomText)
        {
            string[] stringArr = new string[numObjects];

            if (feedRandomText)
            {
                for (int i = 0; i < numObjects; i++)
                {
                    stringArr[i] = getAlphaNumericString(25);
                }
            }
            else
            {
                for (int i = 0; i < numObjects; i++)
                {
                    stringArr[i] = i + "";
                }
            }
        }

        static CompletableFuture<Void> writeThenOverWriteStringArray(int numObjects)
        {
            String[] stringArr = new String[numObjects];

            for (int i = 0; i < numObjects; i++)
            {
                stringArr[i] = getAlphaNumericString(25);
            }

            for (int i = 0; i < numObjects; i++) // 1 mill kr spørsmål: ville det vært bedre å bruke stringArr.length() istedenfor numObjects?
            {
                stringArr[i] = getAlphaNumericString(25);
            }
            return CompletableFuture.completedFuture(null);
        }

        static CompletableFuture<Void> writeThenOverWriteIntArray(int numObjects)
        {
            int[] intArr = new int[numObjects];

            Random rand = new Random();
            for (int i = 0; i < numObjects; i++)
            {
                intArr[i] = rand.nextInt(1000); // random number in range 0 to 999
            }

            for (int i = 0; i < numObjects; i++)
            {
                intArr[i] = rand.nextInt(1000); // random number in range 0 to 999
            }
            return CompletableFuture.completedFuture(null);
        }
    }
}
