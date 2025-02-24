using StringVsStringBuilderRaceFiles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

            try
            {
                var numLoops = Int32.Parse(numOfLoopsInput.Text);

                string stringTest = await TestString.TestMemory(numLoops, null);

                stringVsStringBuilderResult.Text = stringTest;

            }
            catch (FormatException fe)
            {
                MessageBox.Show("Please type a number in the textbox...");

            }

        }
    }
}
