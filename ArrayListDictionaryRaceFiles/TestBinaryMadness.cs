using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayListDictionaryRaceFiles
{
    public class TestBinaryMadness
    {
        public static async Task SearchAndFindInArray(long valueToFind, long[] arrayToSearch)
        {
            long position = 0;

            await Task.Run(() =>
            {
                position = HelperMethods.GetNumberPosInArray(valueToFind, arrayToSearch);
            });
        }

        public static async Task<List<long[]>>  DivideArrayIntoSmallerArrays(long[] arrayToDivide)
        {
            List<long[]> listOfArrays = new List<long[]>();

            await Task.Run(() =>
            {
                listOfArrays = HelperMethods.GetListOfArrays(arrayToDivide);
            });

            return listOfArrays;
        }
    }
}
