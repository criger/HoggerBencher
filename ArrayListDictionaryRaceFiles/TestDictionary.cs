using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArrayListDictionaryRaceFiles
{
    public class TestDictionary
    {
        public static async Task writeToDict(int numObjects, bool writeString, bool writeInt, bool writeRandomValues)
        {
            if (writeInt)
            {
                await writeIntDict(numObjects, writeRandomValues);
            }
            if (writeString)
            {
                await writeStringDict(numObjects, writeRandomValues);
            }
        }

        public static async Task writeThenOverWriteDict(int objectsToWrite, bool writeInt, bool writeString)
        {
            if (writeInt)
            {
                await writeThenOverWriteIntDict(objectsToWrite);

            }
            if (writeString)
            {
                await writeThenOverWriteStringDict(objectsToWrite);
            }
        }

        private static async Task writeThenOverWriteStringDict(int objectsToWrite)
        {
            var stringDict = new Dictionary<int, string>();

            for (int i = 0; i < objectsToWrite; i++)
            {
                stringDict.Add(i, HelperMethods.getAlphaNumericString(5));
            }

            for (int i = 0; i < objectsToWrite; i++) // 1 mill kr spørsmål: ville det vært bedre å bruke stringArr.length() istedenfor numObjects?
            {
                stringDict.Add(i, HelperMethods.getAlphaNumericString(5));
            }

        }

        private static async Task writeThenOverWriteIntDict(int objectsToWrite)
        {
            var intDict = new Dictionary<int, int>();

            Random rand = new Random();
            for (int i = 0; i < objectsToWrite; i++)
            {
                intDict.Add(i, new Random().Next(1000)); // random number in range 0 to 999
            }

            for (int i = 0; i < objectsToWrite; i++)
            {
                intDict.Add(i, new Random().Next(1000)); // random number in range 0 to 999
            }
        }


        public static async Task writeStringDict(int numObjects, bool writeRandomValues)
        {
            var stringDict = new Dictionary<int, string>();


            if (writeRandomValues)
            {
                for (int i = 0; i < numObjects; i++)
                {
                    stringDict.Add(i, HelperMethods.getAlphaNumericString(5));
                }
            }
            else
            {
                for (int i = 0; i < numObjects; i++)
                {
                    stringDict.Add(i, "");
                }
            }
        }

        public static async Task writeIntDict(int numObjects, bool writeRandomValues)
        {
            var intDict = new Dictionary<int, int>();

            if (writeRandomValues)
            {
                Random rand = new Random();
                for (int i = 0; i < numObjects; i++)
                {
                    intDict.Add(i, new Random().Next(1000)); // random number in range 0 to 999
                }
            }
            else
            {
                for (int i = 0; i < numObjects; i++)
                {
                    intDict.Add(i, i);
                }
            }

        }
    }
}
