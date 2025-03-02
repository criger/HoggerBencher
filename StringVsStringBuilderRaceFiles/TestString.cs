using Helpers;

namespace StringVsStringBuilderRaceFiles
{
    public class TestString
    {
        public async static Task<string> TestMemory(int loops, string value)
        {
            return await Task.Run(() =>
            {
                string bigString = "";

                if (value == null)
                {
                    for (int i = 0; i < loops; i++)
                    {
                        bigString += HelperMethods.GetAlphaNumericString(25) + System.Environment.NewLine;
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
