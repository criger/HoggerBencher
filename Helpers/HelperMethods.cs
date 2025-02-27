using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
