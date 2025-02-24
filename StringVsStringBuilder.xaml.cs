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
        }

        private async void StartStringVsStringBuilderTestBtn_Click(object sender, RoutedEventArgs e)
        {

            var gcMemoryInfo = GC.GetGCMemoryInfo();
            var installedMemoryBytes = gcMemoryInfo.TotalAvailableMemoryBytes;  // / 1024;
            var usedMemoryBytes = GC.GetTotalMemory(true); // / 1024;
            var availableMemoryBytes = installedMemoryBytes - usedMemoryBytes;

            var bytesToAllocate = availableMemoryBytes / 4;

            if (GC.TryStartNoGCRegion(bytesToAllocate, true))
            {
                try
                {
                    var numLoops = Int32.Parse(numOfLoopsInput.Text);

                    long beforeStringTest = Process.GetCurrentProcess().WorkingSet64;
                    string stringTest = await TestString.TestMemory(numLoops, null);
                    long afterStringTest = Process.GetCurrentProcess().WorkingSet64;

                    long stringTestUsedMem = afterStringTest - beforeStringTest;

                    stringVsStringBuilderResult.Text = "String test wrote XXXX " + numLoops + " times with a newline after each iteration." + System.Environment.NewLine
                                                     + "Further on, the String test consumed " + stringTestUsedMem + " bytes (" + stringTestUsedMem / 1000 + " KB or " + stringTestUsedMem / 1000000 + " MB...!)";

                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "Be aware that the numbers may not be 100% accurate.";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "Prior to running the test, Windows Garbage Collection is instructed to 'save' " + bytesToAllocate + " bytes of the memory from being collected and released.";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "However, it is NOT a guarantee that Garbace Collection did NOT run.. (thanks Microsoft...!)";
                    stringVsStringBuilderResult.Text += System.Environment.NewLine;
                    stringVsStringBuilderResult.Text += "Therefore, it is advised to run the test some more times in order to get a complete overview of how much memory has been used.";


                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Please type a number in the textbox...");

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
