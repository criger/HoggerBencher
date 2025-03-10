﻿using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringVsStringBuilderRaceFiles
{
    public class TestStringBuilder
    {
        public async static Task<StringBuilder> TestMemory(int loops, string value)
        {
            return await Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();

                if (value == null)
                {
                    for (int i = 0; i < loops; i++)
                    {
                        sb.Append(HelperMethods.GetAlphaNumericString(25)).Append(System.Environment.NewLine);
                    }

                }
                else
                {
                    for (int i = 0; i < loops; i++)
                    {
                        sb.Append(i).Append(" --> ").Append(value).Append(System.Environment.NewLine);
                    }
                }
                return sb;
            });
        }
    }
}
