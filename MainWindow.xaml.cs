using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HoggerBencher;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.SizeToContent = SizeToContent.WidthAndHeight;
    }

    private void TestChooserBtn_Click(object sender, RoutedEventArgs e)
    {

        if (stringVsStringBuilderRadioBtn.IsChecked == true)
            startSelectedTest(1);

        if (arrayListDictionaryRadioBtn.IsChecked == true)
            startSelectedTest(2);

        if (strangeTxtRadioBtn.IsChecked == true)
            startSelectedTest(3);
    }

    private void startSelectedTest(int testNo)
    {
        switch (testNo)
        {
            case 1:
                var stringVsStringBuilderTest = new StringVsStringBuilder();
                stringVsStringBuilderTest.ShowDialog();
                break;
            case 2:
                var arrayVsListVsDictionary = new ArrayVsListVsDictionary();
                arrayVsListVsDictionary.ShowDialog();
                break;
            case 3:
                var strangeText = new StrangeText();
                strangeText.ShowDialog();
                break;

        }
    }
}