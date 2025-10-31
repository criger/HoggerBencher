using System.Windows;

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

            strangeSpacesTxtBlock.Text = "Two of these three following lines looks identical, but looks may deceive..! But at least one of them should be obvious :-)";
            strangeSpacesTxtBlock.Text += Environment.NewLine;
            strangeSpacesTxtBlock.Text += Environment.NewLine;

            var threeSpaces = PopulateThreeWordsList();

            threeSpaces.ForEach(x => strangeSpacesTxtBlock.Text += x + Environment.NewLine);
            strangeSpacesTxtBlock.Text += Environment.NewLine;


            this.SizeToContent = SizeToContent.WidthAndHeight;
        }



        private List<string> PopulateThreeWordsList()
        {
            var spacesList = new List<string>();

            spacesList.Add("Three words");
            spacesList.Add("Three" + (char)9 + "words");
            spacesList.Add("Three words");

            return spacesList;
        }


        private void revealHintBtn_Click(object sender, RoutedEventArgs e)
        {
            var threeWordsList = PopulateThreeWordsList();
            
            char[] charArray1 = threeWordsList[0].ToCharArray();
            char[] charArray2 = threeWordsList[1].ToCharArray();
            char[] charArray3 = threeWordsList[2].ToCharArray();

            strangeSpacesTxtBlock.Text += "HINT";

            strangeSpacesTxtBlock.Text += Environment.NewLine;
            strangeSpacesTxtBlock.Text += "ASCII values for line 1:";
            strangeSpacesTxtBlock.Text += Environment.NewLine;

            foreach (var item in charArray1)
            {
                strangeSpacesTxtBlock.Text += (int)item + " ";
            }

            strangeSpacesTxtBlock.Text += Environment.NewLine;
            strangeSpacesTxtBlock.Text += Environment.NewLine;
            strangeSpacesTxtBlock.Text += "ASCII values for line 2:";
            strangeSpacesTxtBlock.Text += Environment.NewLine;

            foreach (var item in charArray2)
            {
                strangeSpacesTxtBlock.Text += (int)item + " ";
            }

            strangeSpacesTxtBlock.Text += Environment.NewLine;
            strangeSpacesTxtBlock.Text += Environment.NewLine;
            strangeSpacesTxtBlock.Text += "ASCII values for line 3:";
            strangeSpacesTxtBlock.Text += Environment.NewLine;

            foreach (var item in charArray3)
            {
                strangeSpacesTxtBlock.Text += (int)item + " ";
            }


        }
    }


}
