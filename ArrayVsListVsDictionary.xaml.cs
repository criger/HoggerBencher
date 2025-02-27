using ArrayListDictionaryRaceFiles;
using StringVsStringBuilderRaceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HoggerBencher
{
    /// <summary>
    /// Interaction logic for ArrayVsListVsDictionary.xaml
    /// </summary>
    public partial class ArrayVsListVsDictionary : Window
    {
        public ArrayVsListVsDictionary()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private async void startArrayListDictTestBtn_Click(object sender, RoutedEventArgs e)
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
                    int amountValuesToInsert = howManyValuesToInsertInput.Text.Length > 1 ? Convert.ToInt32(howManyValuesToInsertInput.Text) : 10000;


                    if (writeStringsRadioBtn.IsChecked == true)
                    {
                        await PerformStringTest(amountValuesToInsert, bytesToAllocate);
                    }

                    if (writeIntsRadioBtn.IsChecked == true)
                    {
                        await PerformIntTest(amountValuesToInsert, bytesToAllocate);
                    }

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

        private async Task PerformStringTest(int amountValuesToInsert, long bytesToAllocate)
        {
            var stopwatch = new Stopwatch();

            //write random strings to an array
            stopwatch.Start();
            long beforeWriteStringToArray = Process.GetCurrentProcess().WorkingSet64;
            await TestArrays.WriteToArray(amountValuesToInsert, true, false, true);
            long afterWriteStringToArray = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var writeStringToArrayTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random strings to an array and then overwrite all values
            stopwatch.Restart();
            long beforeOverwriteStringToArray = Process.GetCurrentProcess().WorkingSet64;
            await TestArrays.WriteThenOverWriteArray(amountValuesToInsert, false, true);
            long afterOverwriteStringToArray = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var overWriteStringToArrayTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random strings to a List
            stopwatch.Restart();
            long beforeWriteStringToList = Process.GetCurrentProcess().WorkingSet64;
            await TestList.WriteToList(amountValuesToInsert, true, false, true);
            long afterWriteStringToList = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var writeStringToListTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random strings to a List, then overwrite all values with random strings
            stopwatch.Restart();
            long beforeOverwriteStringToList = Process.GetCurrentProcess().WorkingSet64;
            await TestList.WriteThenOverWriteList(amountValuesToInsert, false, true);
            long afterOverwriteStringToList = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var overWriteStringToListTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random strings to a Dictionary
            stopwatch.Restart();
            long beforeWriteStringToDict = Process.GetCurrentProcess().WorkingSet64;
            await TestDictionary.WriteStringDict(amountValuesToInsert, true);
            long afterWriteStringToDict = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var writeStringToDictTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random strings to a Dictionary, then overwrite all values with random strings
            stopwatch.Restart();
            long beforeOverwriteStringToDict = Process.GetCurrentProcess().WorkingSet64;
            await TestDictionary.WriteThenOverWriteDict(amountValuesToInsert, false, true);
            long afterOverwriteStringToDict = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var overWriteStringToDictTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            long writeStringsToArrayTestUsedMem = afterWriteStringToArray - beforeWriteStringToArray;
            long overWriteStringsToArrayTestUsedMem = afterOverwriteStringToArray - beforeOverwriteStringToArray;

            long writeStringsToListTestUsedMem = afterWriteStringToList - beforeWriteStringToList;
            long overWriteStringsToListTestUsedMem = afterOverwriteStringToList - beforeOverwriteStringToList;

            long writeStringsToDictTestUsedMem = afterWriteStringToDict - beforeWriteStringToDict;
            long overWriteStringsToDictTestUsedMem = afterOverwriteStringToDict - beforeOverwriteStringToDict;

            arrayVsListVsDictionaryResult.Text = "Array test 1:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random strings with 25 characters, " + amountValuesToInsert + " times into an array.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + writeStringsToArrayTestUsedMem + " bytes (" + writeStringsToArrayTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(writeStringsToArrayTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + writeStringToArrayTotalTime + " ms";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Array test 2:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random strings with 25 characters, " + amountValuesToInsert + " times into an array, then overwrote all of the values with random strings of 25 characters.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + overWriteStringsToArrayTestUsedMem + " bytes (" + overWriteStringsToArrayTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(overWriteStringsToArrayTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + overWriteStringToArrayTotalTime + " ms";

            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "*********************************************************";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;

            arrayVsListVsDictionaryResult.Text += "List test 1:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random strings with 25 characters, " + amountValuesToInsert + " times into a List<string> object.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + writeStringsToListTestUsedMem + " bytes (" + writeStringsToListTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(writeStringsToListTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + writeStringToListTotalTime + " ms";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "List test 2:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random strings with 25 characters, " + amountValuesToInsert + " times into List<string> object, then overwrote all of the values with random strings of 25 characters.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + overWriteStringsToListTestUsedMem + " bytes (" + overWriteStringsToListTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(overWriteStringsToListTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + overWriteStringToListTotalTime + " ms";

            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "*********************************************************";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;

            arrayVsListVsDictionaryResult.Text += "Dictionary test 1:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random strings with 25 characters, " + amountValuesToInsert + " times into a Dictionary<int, string> object.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + writeStringsToDictTestUsedMem + " bytes (" + writeStringsToDictTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(writeStringsToDictTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + writeStringToDictTotalTime + " ms";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Dictionary test 2:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random strings with 25 characters, " + amountValuesToInsert + " times into Dictionary<int, string> object, then overwrote all of the values with random strings of 25 characters.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + overWriteStringsToDictTestUsedMem + " bytes (" + overWriteStringsToDictTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(overWriteStringsToDictTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + overWriteStringToDictTotalTime + " ms";

            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Be aware that the numbers may not be 100% accurate.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Prior to running the test, Windows Garbage Collection is instructed to 'save' a fourth of available memory.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This allocates to " + bytesToAllocate + " bytes of the memory from being collected and released.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "However, it is NOT a guarantee that Garbace Collection did NOT run.. (thanks Microsoft...!)";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Therefore, it is advised to run the test some more times in order to get a complete overview of how much memory has been used and see the BIG difference between arrays, Lists and Dictionary!";

        }

        private async Task PerformIntTest(int amountValuesToInsert, long bytesToAllocate)
        {
            var stopwatch = new Stopwatch();

            //write random ints to an array
            stopwatch.Start();
            long beforeWriteIntToArray = Process.GetCurrentProcess().WorkingSet64;
            await TestArrays.WriteToArray(amountValuesToInsert, false, true, true);
            long afterWriteIntToArray = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var writeIntsToArrayTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random ints to an array and then overwrite all values
            stopwatch.Restart();
            long beforeOverwriteIntToArray = Process.GetCurrentProcess().WorkingSet64;
            await TestArrays.WriteThenOverWriteArray(amountValuesToInsert, true, false);
            long afterOverwriteIntToArray = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var overWriteIntsToArrayTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random ints to a List
            stopwatch.Restart();
            long beforeWriteIntToList = Process.GetCurrentProcess().WorkingSet64;
            await TestList.WriteToList(amountValuesToInsert,false, true, true);
            long afterWriteIntToList = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var writeIntsToListTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random ints to a List, then overwrite all values with random ints
            stopwatch.Restart();
            long beforeOverwriteIntToList = Process.GetCurrentProcess().WorkingSet64;
            await TestList.WriteThenOverWriteList(amountValuesToInsert, true, false);
            long afterOverwriteIntToList = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var overWriteIntsToListTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random ints to a Dictionary
            stopwatch.Restart();
            long beforeWriteIntToDict = Process.GetCurrentProcess().WorkingSet64;
            await TestDictionary.WriteIntDict(amountValuesToInsert, true);
            long afterWriteIntToDict = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var writeIntsToDictTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            //write random ints to a Dictionary, then overwrite all values with random ints
            stopwatch.Restart();
            long beforeOverwriteIntToDict = Process.GetCurrentProcess().WorkingSet64;
            await TestDictionary.WriteThenOverWriteDict(amountValuesToInsert, true, false);
            long afterOverwriteIntToDict = Process.GetCurrentProcess().WorkingSet64;
            stopwatch.Stop();
            var overWriteIntsToDictTotalTime = stopwatch.Elapsed.TotalMilliseconds;

            long writeIntsToArrayTestUsedMem = afterWriteIntToArray - beforeWriteIntToArray;
            long overWriteIntsToArrayTestUsedMem = afterOverwriteIntToArray - beforeOverwriteIntToArray;

            long writeIntsToListTestUsedMem = afterWriteIntToList - beforeWriteIntToList;
            long overWriteIntsToListTestUsedMem = afterOverwriteIntToList - beforeOverwriteIntToList;

            long writeIntsToDictTestUsedMem = afterWriteIntToDict - beforeWriteIntToDict;
            long overWriteIntsToDictTestUsedMem = afterOverwriteIntToDict - beforeOverwriteIntToDict;

            arrayVsListVsDictionaryResult.Text = "Array test 1:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random numbers between 0 and 1000, " + amountValuesToInsert + " times into an array.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + writeIntsToArrayTestUsedMem + " bytes (" + writeIntsToArrayTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(writeIntsToArrayTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + writeIntsToArrayTotalTime + " ms";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Array test 2:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random numbers between 0 and 1000, " + amountValuesToInsert + " times into an array, then overwrote all of the values with random numbers between 0 and 1000.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + overWriteIntsToArrayTestUsedMem + " bytes (" + overWriteIntsToArrayTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(overWriteIntsToArrayTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + overWriteIntsToArrayTotalTime + " ms";

            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "*********************************************************";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;

            arrayVsListVsDictionaryResult.Text += "List test 1:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random numbers between 0 and 1000, " + amountValuesToInsert + " times into a List<int> object.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + writeIntsToListTestUsedMem + " bytes (" + writeIntsToListTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(writeIntsToListTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + writeIntsToListTotalTime + " ms";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "List test 2:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random numbers between 0 and 1000, " + amountValuesToInsert + " times into a List<int> object, then overwrote all of the values with random numbers between 0 and 1000.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + overWriteIntsToListTestUsedMem + " bytes (" + overWriteIntsToListTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(overWriteIntsToListTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + overWriteIntsToListTotalTime + " ms";

            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "*********************************************************";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;

            arrayVsListVsDictionaryResult.Text += "Dictionary test 1:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random numbers between 0 and 1000, " + amountValuesToInsert + " times into a Dictionary<int, int> object.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + writeIntsToDictTestUsedMem + " bytes (" + writeIntsToDictTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(writeIntsToDictTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + writeIntsToDictTotalTime + " ms";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Dictionary test 2:";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Wrote random numbers between 0 and 1000, " + amountValuesToInsert + " times into a Dictionary<int, int> object, then overwrote all of the values with random numbers between 0 and 1000.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This consumed " + overWriteIntsToDictTestUsedMem + " bytes (" + overWriteIntsToDictTestUsedMem / 1000 + " KB or " + Convert.ToDecimal(overWriteIntsToDictTestUsedMem) / Convert.ToDecimal(1000000) + " MB...!) from the RAM";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "The test ran in " + overWriteIntsToDictTotalTime + " ms";

            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Be aware that the numbers may not be 100% accurate.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Prior to running the test, Windows Garbage Collection is instructed to 'save' a fourth of available memory.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "This allocates to " + bytesToAllocate + " bytes of the memory from being collected and released.";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "However, it is NOT a guarantee that Garbace Collection did NOT run.. (thanks Microsoft...!)";
            arrayVsListVsDictionaryResult.Text += System.Environment.NewLine;
            arrayVsListVsDictionaryResult.Text += "Therefore, it is advised to run the test some more times in order to get a complete overview of how much memory has been used and see the BIG difference between arrays, Lists and Dictionary!";

        }
    }
}
