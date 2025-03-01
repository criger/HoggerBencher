using StringVsStringBuilderRaceFiles;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime;
using System.Windows;
using System.Windows.Threading;


namespace HoggerBencher
{
    /// <summary>
    /// Interaction logic for StringVsStringBuilder.xaml
    /// </summary>
    public partial class StringVsStringBuilder : Window
    {
        private BackgroundWorker worker;

        public StringVsStringBuilder()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;


            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "NOTE:";
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "For performance reasons, the String test is limited to only 20 000 000 loops.";
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "If you type a value bigger than 20 000 000, it will default to 20 000 000.";
            stringVsStringBuilderResult.Text += System.Environment.NewLine;
            stringVsStringBuilderResult.Text += "BUT.... the StringBuilder test will be run with the value you typed :-)";
        }

        private async void StartStringVsStringBuilderTestBtn_Click(object sender, RoutedEventArgs e)
        {

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            progressGrid.Visibility = Visibility.Visible;
            updateProgressBarStringVsStringBuilder(0);

        }

        private void worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {

        }

        private void worker_DoWork(object? sender, DoWorkEventArgs e)
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

                    int loops = 0;
                    numOfLoopsInput.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action
                    (
                        () =>
                        {
                            loops = numOfLoopsInput.Text.Length > 1 ? Convert.ToInt32(numOfLoopsInput.Text) : 10000;
                        }
                    ));

                    var numLoopsString = loops > 20000000 ? 20000000 : loops;

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    long beforeStringTest = Process.GetCurrentProcess().WorkingSet64;
                    _ = TestString.TestMemory(numLoopsString, null);
                    long afterStringTest = Process.GetCurrentProcess().WorkingSet64;
                    stopwatch.Stop();
                    var stringTestTotalTime = stopwatch.Elapsed.TotalMilliseconds;

                    updateProgressBarStringVsStringBuilder(50);

                    stopwatch.Restart();
                    long beforeSBTest = Process.GetCurrentProcess().WorkingSet64;
                    _ = TestStringBuilder.TestMemory(loops, null);
                    long afterSBTest = Process.GetCurrentProcess().WorkingSet64;
                    stopwatch.Stop();
                    var stringBuilderTestTotalTime = stopwatch.Elapsed.TotalMilliseconds;

                    updateProgressBarStringVsStringBuilder(100);

                    Thread.Sleep(1000);

                    long stringTestUsedMem = afterStringTest - beforeStringTest;
                    long sbTestUsedMem = afterSBTest - beforeSBTest;


                    stringVsStringBuilderResult.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action
                    (
                        () =>
                        {
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
                    ));

                    progressGrid.Dispatcher.Invoke(DispatcherPriority.Normal,
                    new Action
                    (
                        () =>
                        {
                            progressGrid.Visibility = Visibility.Collapsed;
                        }
                    ));


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
        public async void updateProgressBarStringVsStringBuilder(int value)
        {
            pbStringVsStringBuilder.Dispatcher.Invoke(DispatcherPriority.Normal,
            new Action
            (
                () =>
                {
                    pbStringVsStringBuilder.Value = value;
                }
            ));
        }

    }
}
