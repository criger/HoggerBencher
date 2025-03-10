﻿using Helpers;

namespace ArrayListDictionaryRaceFiles
{
    public class TestList
    {
        public static async Task WriteToList(int numObjects, bool writeString, bool writeInt, bool writeRandomValues)
        {
            if (writeInt)
            {
                await WriteIntList(numObjects, writeRandomValues);
            }
            if (writeString)
            {
                await WriteStringList(numObjects, writeRandomValues);
            }

        }

        public static async Task WriteThenOverWriteList(int objectsToWrite, bool writeInt, bool writeString)
        {
            if (writeInt)
            {
                await WriteThenOverWriteIntList(objectsToWrite);

            }
            if (writeString)
            {
                await WriteThenOverWriteStringList(objectsToWrite);
            }

        }

        private static async Task WriteThenOverWriteStringList(int objectsToWrite)
        {
            await Task.Run(() =>
            {
                var stringList = new List<string>();

                for (int i = 0; i < objectsToWrite; i++)
                {
                    stringList.Add(HelperMethods.GetAlphaNumericString(5));
                }

                for (int i = 0; i < objectsToWrite; i++) // 1 mill kr spørsmål: ville det vært bedre å bruke stringArr.length() istedenfor numObjects?
                {
                    stringList[i] = HelperMethods.GetAlphaNumericString(5);
                }
            });
        }

        private static async Task WriteThenOverWriteIntList(int objectsToWrite)
        {
            await Task.Run(() =>
            {
                var intList = new List<int>();

                for (int i = 0; i < objectsToWrite; i++)
                {
                    intList.Add(new Random().Next(1000)); // random number in range 0 to 999
                }

                for (int i = 0; i < objectsToWrite; i++)
                {
                    intList[i] = new Random().Next(1000);
                }

            });
        }


        private static async Task WriteStringList(int numObjects, bool writeRandomValues)
        {
            await Task.Run(() =>
            {
                var stringList = new List<string>();


                if (writeRandomValues)
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        stringList.Add(HelperMethods.GetAlphaNumericString(5));
                    }
                }
                else
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        stringList.Add(i + "");
                    }
                }

            });

        }

        private static async Task WriteIntList(int numObjects, bool writeRandomValues)
        {
            await Task.Run(() =>
            {
                var intList = new List<int>();

                if (writeRandomValues)
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        intList.Add(new Random().Next(1000)); // random number in range 0 to 999
                    }
                }
                else
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        intList.Add(i);
                    }
                }
            });

        }
    }
}
