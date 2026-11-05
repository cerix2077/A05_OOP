using Microsoft.Win32;
using System.IO;
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

namespace A05_Snow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // this string holds the origin state of the content within the FileTextBox,
        //  it is empty on a blank page and will be set to the data in an opened file or text data successfully saved
        private string _comparatorString = string.Empty; 

        public MainWindow()
        {
            InitializeComponent();
        }

        public void NewFile(object sender, RoutedEventArgs e)
        {
            if(FileTextBox.Text != _comparatorString)
            {
                //prompt user for yes/no/cancel with focus set onto yes for enter press selection
            }
        }

        public void OpenFile(object sender, RoutedEventArgs e)
        {
        }

        public void SaveFile(object sender, RoutedEventArgs e)
        {
            // check if a file exists to save too, otherwise use SaveFileAs()
            if(false)// file was found to save too
            {

            }
            else
            {
                SaveFileAs(sender, e);
            }
        }

        public void SaveFileAs(object sender, RoutedEventArgs e)
        {
            Stream writeStream;
            SaveFileDialog sv = new SaveFileDialog();
            if(sv.ShowDialog() == true) // returns true if user selects ok 
            {
                if((writeStream = sv.OpenFile()) != null) //check that stream opens file
                {
                    // code to write stream to file, this should overwrite and never append
                    writeStream.Close(); //close stream
                }
            }
        }

        public void ExitFunc(object sender, RoutedEventArgs e)
        {
        }

        public void WrapOn(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.Wrap;
        }

        public void WrapOff(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.NoWrap;
        }

        public void ShowAboutPage(object sender, RoutedEventArgs e)
        {

        }

    }
}