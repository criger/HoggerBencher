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
        public static async Task WriteToArray(int numObjects, bool writeString, bool writeInt, bool writeRandomValues)
        {
            if (writeInt)
            {
                await WriteIntArray(numObjects, writeRandomValues);
            }
            if (writeString)
            {
                await WriteStringArray(numObjects, writeRandomValues);
            }
        }

        public static async Task WriteThenOverWriteArray(int objectsToWrite, bool writeInt, bool writeString)
        {
            if (writeInt)
            {
               await WriteThenOverWriteIntArray(objectsToWrite);

            }
            if (writeString)
            {
                await WriteThenOverWriteStringArray(objectsToWrite);
            }
        }


        private static async Task WriteIntArray(int numObjects, bool feedRandomInts)
        {
            await Task.Run(() =>
            {
                int[] intArr = new int[numObjects];

                if (feedRandomInts)
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        intArr[i] = new Random().Next(1000); // random number in range 0 to 999
                    }
                }
                else
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        intArr[i] = i;
                    }
                }
            });
        }

        private static async Task WriteStringArray(int numObjects, bool feedRandomText)
        {
            await Task.Run(() =>
            {
                string[] stringArr = new string[numObjects];

                if (feedRandomText)
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        stringArr[i] = HelperMethods.GetAlphaNumericString(25);
                    }
                }
                else
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        stringArr[i] = i + "";
                    }
                }
            });
        }

        private static async Task WriteThenOverWriteStringArray(int numObjects)
        {

            await Task.Run(() =>
            {
                string[] stringArr = new string[numObjects];

                for (int i = 0; i < numObjects; i++)
                {
                    stringArr[i] = HelperMethods.GetAlphaNumericString(25);
                }

                for (int i = 0; i < numObjects; i++) // 1 mill kr spørsmål: ville det vært bedre å bruke stringArr.length() istedenfor numObjects?
                {
                    stringArr[i] = HelperMethods.GetAlphaNumericString(25);
                }

            });
        }

        private static async Task WriteThenOverWriteIntArray(int numObjects)
        {
            await Task.Run(() =>
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

            });
        }
    }
}
