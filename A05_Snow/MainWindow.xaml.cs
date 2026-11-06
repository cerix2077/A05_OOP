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
        private string _workingFile = string.Empty;

        public string comparatorString
        {
            get { return _comparatorString; }
            set { _comparatorString = value; }
        }

        public string workingFile
        {
            get { return _workingFile; }
            set {  _workingFile = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void NewFile(object sender, RoutedEventArgs e)
        {
            if(FileTextBox.Text != comparatorString)
            {
                //prompt user for yes/no/cancel with focus set onto yes for enter press selection
            }
        }

        public void OpenFile(object sender, RoutedEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)
            {
                //prompt user for yes/no/cancel with focus set onto yes for enter press selection
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if(ofd.ShowDialog() == true)
                {
                    FileTextBox.Text = File.ReadAllText(ofd.FileName);
                    comparatorString = FileTextBox.Text; // this is the current 
                }
            }
        }

        public void SaveFile(object sender, RoutedEventArgs e)
        {
            // check if a file exists to save too, otherwise use SaveFileAs()
            if(workingFile != null)// file was found to save too
            {
                File.WriteAllText(workingFile, FileTextBox.Text);
            }
            else
            {
                SaveFileAs(sender, e);
            }
        }

        public void SaveFileAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if(sfd.ShowDialog() == true) // returns true if user selects ok 
            {
                workingFile = sfd.FileName;
                File.WriteAllText(workingFile, FileTextBox.Text);
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