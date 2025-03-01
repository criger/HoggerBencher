using Helpers;

namespace ArrayListDictionaryRaceFiles
{
    public class TestDictionary
    {
        public static async Task WriteToDict(int numObjects, bool WriteString, bool WriteInt, bool WriteRandomValues)
        {
            if (WriteInt)
            {
                await WriteIntDict(numObjects, WriteRandomValues);
            }
            if (WriteString)
            {
                await WriteStringDict(numObjects, WriteRandomValues);
            }
        }

        public static async Task WriteThenOverWriteDict(int objectsToWrite, bool WriteInt, bool WriteString)
        {
            if (WriteInt)
            {
                await WriteThenOverWriteIntDict(objectsToWrite);

            }
            if (WriteString)
            {
                await WriteThenOverWriteStringDict(objectsToWrite);
            }
        }

        private static async Task WriteThenOverWriteStringDict(int objectsToWrite)
        {

            await Task.Run(() =>
            {
                var stringDict = new Dictionary<int, string>();

                for (int i = 0; i < objectsToWrite; i++)
                {
                    stringDict.Add(i, HelperMethods.GetAlphaNumericString(5));
                }

                for (int i = 0; i < objectsToWrite; i++) // 1 mill kr spørsmål: ville det vært bedre å bruke stringArr.length() istedenfor numObjects?
                {
                    stringDict[i] = HelperMethods.GetAlphaNumericString(5);
                }
            });
        }

        private static async Task WriteThenOverWriteIntDict(int objectsToWrite)
        {
            await Task.Run(() =>
            {
                var intDict = new Dictionary<int, int>();

                for (int i = 0; i < objectsToWrite; i++)
                {
                    intDict.Add(i, new Random().Next(1000)); // random number in range 0 to 999
                }

                for (int i = 0; i < objectsToWrite; i++)
                {
                    intDict[i] = new Random().Next(1000);
                }
            });
        }


        public static async Task WriteStringDict(int numObjects, bool WriteRandomValues)
        {
            await Task.Run(() =>
            {
                var stringDict = new Dictionary<int, string>();


                if (WriteRandomValues)
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        stringDict.Add(i, HelperMethods.GetAlphaNumericString(5));
                    }
                }
                else
                {
                    for (int i = 0; i < numObjects; i++)
                    {
                        stringDict.Add(i, "");
                    }
                }

            });
        }

        public static async Task WriteIntDict(int numObjects, bool WriteRandomValues)
        {
            await Task.Run(() =>
            {
                var intDict = new Dictionary<int, int>();

                if (WriteRandomValues)
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

            });
        }
    }
}
