using StringVsStringBuilderRaceFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HoggerBencher
{
    /// <summary>
    /// Interaction logic for StringVsStringBuilder.xaml
    /// </summary>
    public partial class StringVsStringBuilder : Window
    {
        public StringVsStringBuilder()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "NOTE:";
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "For security performance reasons, the String test is limited to only 100 000 loops.";
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "If you type a value bigger than 100 000, it will default to 100 000.";
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "BUT.... the StringBuilder test will be run with the value you typed :-)";
        }

        private async void StartStringVsStringBuilderTestBtn_Click(object sender, RoutedEventArgs e)
        {

            var gcMemoryInfo = GC.GetGCMemoryInfo();
            var installedMemoryBytes = gcMemoryInfo.TotalAvailableMemoryBytes;  
            var usedMemoryBytes = GC.GetTotalMemory(true); 
            var availableMemoryBytes = installedMemoryBytes - usedMemoryBytes;

            var bytesToAllocate = availableMemoryBytes / 4;

            if (GC.TryStartNoGCRegion(bytesToAllocate, true))
            {
                try
                {
                    var loops = Int32.Parse(numOfLoopsInput.Text);
                    var numLoopsString = loops > 100000 ? 100000 : loops;

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    long beforeStringTest = Process.GetCurrentProcess().WorkingSet64;
                    _ = await TestString.TestMemory(numLoopsString, null);
                    long afterStringTest = Process.GetCurrentProcess().WorkingSet64;
                    stopwatch.Stop();
                    var stringTestTotalTime = stopwatch.Elapsed.TotalMilliseconds;

                    stopwatch.Restart();
                    long beforeSBTest = Process.GetCurrentProcess().WorkingSet64;
                    _ = await TestStringBuilder.TestMemory(loops, null);
                    long afterSBTest = Process.GetCurrentProcess().WorkingSet64;
                    stopwatch.Stop();
                    var stringBuilderTestTotalTime = stopwatch.Elapsed.TotalMilliseconds;


                    long stringTestUsedMem = afterStringTest - beforeStringTest;
                    long sbTestUsedMem = afterSBTest - beforeSBTest;

                    stringVsStringBuilderResult.Text = "String test wrote 'ABCD' " + numLoopsString + " times with a newline after each iteration." + System.Environment.NewLine
                                                     + "Further on, the String test consumed " + stringTestUsedMem + " bytes (" + stringTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(stringTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!)";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "The test ran in " + stringTestTotalTime + " ms";

                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;

                    stringVsStringBuilderResult.Text += "StringBuilder test wrote 'ABCD' " + loops + " times with a newline after each iteration." + System.Environment.NewLine
                                                     + "Further on, the StringBuilder test consumed " + sbTestUsedMem + " bytes (" + sbTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(sbTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!)";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "The test ran in " + stringBuilderTestTotalTime + " ms";

                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "Be aware that the numbers may not be 100% accurate.";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "Prior to running the test, Windows Garbage Collection is instructed to 'save' a fourth of available memory.";
                    stringVsStringBuilderResult.Text += "This allocates to " + bytesToAllocate + " bytes of the memory from being collected and released.";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "However, it is NOT a guarantee that Garbace Collection did NOT run.. (thanks Microsoft...!)";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "Therefore, it is advised to run the test some more times in order to get a complete overview of how much memory has been used and see the BIG difference between String and StringBuilder!";
                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Please type an integer number in the textbox...");
                }

                finally
                {
                    if (GCSettings.LatencyMode == GCLatencyMode.NoGCRegion)
                        GC.EndNoGCRegion();
                }

            }

        }
    }
}
