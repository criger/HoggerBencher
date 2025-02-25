using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringVsStringBuilderRaceFiles
{
    public class TestString
    {
        public static Task<string> TestMemory(int loops, string value)
        {
            return Task.Run(() =>
            {
                string bigString = "";

                if (value == null)
                {
                    for (int i = 0; i < loops; i++)
                    {
                        bigString += "ABCD: " + i + System.Environment.NewLine;
                    }
                }
                else
                {
                    for (int i = 0; i < loops; i++)
                    {
                        bigString += i + " --> " + value + System.Environment.NewLine;
                    }
                }
                return bigString;
            });
        }
    }
}
