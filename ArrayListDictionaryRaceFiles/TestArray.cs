using Helpers;
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
                await writeStringArray(numObjects, writeRandomValues);
            }
        }

        public static async Task writeThenOverWriteArray(int objectsToWrite, bool writeInt, bool writeString)
        {
            if (writeInt)
            {
               await writeThenOverWriteIntArray(objectsToWrite);

            }
            if (writeString)
            {
                await writeThenOverWriteStringArray(objectsToWrite);
            }
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
                    stringArr[i] = HelperMethods.getAlphaNumericString(25);
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

        private static async Task writeThenOverWriteStringArray(int numObjects)
        {
            string[] stringArr = new string[numObjects];

            for (int i = 0; i < numObjects; i++)
            {
                stringArr[i] = HelperMethods.getAlphaNumericString(25);
            }

            for (int i = 0; i < numObjects; i++) // 1 mill kr spørsmål: ville det vært bedre å bruke stringArr.length() istedenfor numObjects?
            {
                stringArr[i] = HelperMethods.getAlphaNumericString(25);
            }
        }

        private static async Task writeThenOverWriteIntArray(int numObjects)
        {
            int[] intArr = new int[numObjects];

            for (int i = 0; i < numObjects; i++)
            {
                intArr[i] = new Random().Next(1000); // random number in range 0 to 999
            }

            for (int i = 0; i < numObjects; i++)
            {
                intArr[i] = new Random().Next(1000); // random number in range 0 to 999
            }
        }
    }
}
