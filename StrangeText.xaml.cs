using System;
using System.Collections.Generic;
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
    /// Interaction logic for StrangeText.xaml
    /// </summary>
    public partial class StrangeText : Window
    {
        public StrangeText()
        {
            InitializeComponent();

            strangeSpacesTxtBlock.Text = "Two these three following lines looks identical, but looks may deceive..! But at least one of them should be obvious :-)";
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;

            var threeSpaces = GetThreeWordsList();

            threeSpaces.ForEach(x => strangeSpacesTxtBlock.Text += x + System.Environment.NewLine);
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;


            this.SizeToContent = SizeToContent.WidthAndHeight;
        }



        private List<string> GetThreeWordsList()
        {
            var spacesList = new List<string>();

            spacesList.Add("Three words");
            spacesList.Add("Three" + (char)9 + "words");
            spacesList.Add("Three words");

            return spacesList;
        }


        private void revealHintBtn_Click(object sender, RoutedEventArgs e)
        {
            char[] charArray1 = GetThreeWordsList()[0].ToCharArray();
            char[] charArray2 = GetThreeWordsList()[1].ToCharArray();
            char[] charArray3 = GetThreeWordsList()[2].ToCharArray();

            strangeSpacesTxtBlock.Text += "HINT";

            strangeSpacesTxtBlock.Text += System.Environment.NewLine;
            strangeSpacesTxtBlock.Text += "ASCII values for line 1:";
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;

            foreach (var item in charArray1)
            {
                strangeSpacesTxtBlock.Text += (int)item + " ";
            }

            strangeSpacesTxtBlock.Text += System.Environment.NewLine;
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;
            strangeSpacesTxtBlock.Text += "ASCII values for line 2:";
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;

            foreach (var item in charArray2)
            {
                strangeSpacesTxtBlock.Text += (int)item + " ";
            }

            strangeSpacesTxtBlock.Text += System.Environment.NewLine;
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;
            strangeSpacesTxtBlock.Text += "ASCII values for line 3:";
            strangeSpacesTxtBlock.Text += System.Environment.NewLine;

            foreach (var item in charArray3)
            {
                strangeSpacesTxtBlock.Text += (int)item + " ";
            }


        }
    }


}
