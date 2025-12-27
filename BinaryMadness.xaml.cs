using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BinaryMadness.xaml
    /// </summary>
    public partial class BinaryMadness : Window
    {
        public BinaryMadness()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void findNumberBtn_Click(object sender, RoutedEventArgs e)
        {
            // inputs are validated on the fly upon typing.
            long howManyNumbers = Convert.ToInt64(howManyNumbersInArrayInput.Text.Length > 0 ? howManyNumbersInArrayInput.Text : 10000000);
            long numberToFind = Convert.ToInt64(numberToFindInput.Text.Length > 0 ? numberToFindInput.Text : 100000);



        }

        private void ValidateInput_OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
