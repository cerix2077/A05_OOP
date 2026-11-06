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
        private string _workingFilePath = string.Empty;

        private string _workingFileName = "Untitled";

        public string comparatorString
        {
            get { return _comparatorString; }
            set { _comparatorString = value; }
        }

        public string workingFilePath
        {
            get { return _workingFilePath; }
            set {  _workingFilePath = value; }
        }

        public string workingFileName
        {
            get { return _workingFileName; }
            set { _workingFileName = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        MessageBoxResult UnsavedChangesWarning(object sender, RoutedEventArgs e)
        {
            //prompt user for yes/no/cancel
            MessageBoxResult result;
            string saveString = $"Do you want to Save changes to {workingFileName}?";
            result = MessageBox.Show(saveString, "SETpad",  MessageBoxButton.YesNoCancel);
            return result;
            //if (result == MessageBoxResult.Yes)
            //{
            //    SaveFile(sender, e);
            //}
            //else if (result == MessageBoxResult.No)
            //{

            //}
            //else if (result == MessageBoxResult.Cancel)
            //{

            //}
        }



        public void NewFile(object sender, RoutedEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)
            {
                MessageBoxResult choice;
                choice = UnsavedChangesWarning(sender, e);
                if (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, e);
                }
                else if (choice == MessageBoxResult.No)
                {
                    FileTextBox.Text = "";
                    workingFilePath = string.Empty;
                    workingFileName = "untitled";
                }
            }


        }

        public void OpenFile(object sender, RoutedEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)
            {
                //prompt user for yes/no/cancel with focus set onto yes for enter press selection
                MessageBoxResult choice;
                choice = UnsavedChangesWarning(sender, e);
                if (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, e);
                }
                else if (choice == MessageBoxResult.No)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() == true)
                    {
                        FileTextBox.Text = File.ReadAllText(ofd.FileName);
                        comparatorString = FileTextBox.Text; // this is the current 
                        workingFilePath = ofd.FileName;
                        workingFileName = System.IO.Path.GetFileName(workingFileName); // Path has ambiguity between IO.Path and Windows.Shapes.Path
                    }
                }
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    FileTextBox.Text = File.ReadAllText(ofd.FileName);
                    comparatorString = FileTextBox.Text; // this is the current 
                    workingFilePath = ofd.FileName;
                    workingFileName = System.IO.Path.GetFileName(workingFileName); // Path has ambiguity between IO.Path and Windows.Shapes.Path
                    this.Title = $"SETpad - {workingFilePath}";
                }
            }
        }

        public void SaveFile(object sender, RoutedEventArgs e)
        {
            // check if a file exists to save too, otherwise use SaveFileAs()
            if(workingFilePath != String.Empty)// file was found to save too
            {
                File.WriteAllText(workingFilePath, FileTextBox.Text);
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
                workingFilePath = sfd.FileName;
                workingFileName = System.IO.Path.GetFileName(workingFilePath);
                File.WriteAllText(workingFilePath, FileTextBox.Text);
                this.Title = workingFilePath;
            }
        }

        void UpdateCharCount(object sender, RoutedEventArgs e)
        {
            StatusBar.Content = FileTextBox.Text.Length.ToString();
        }

        public void ExitFunc(object sender, RoutedEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)
            {
                MessageBoxResult choice;
                choice = UnsavedChangesWarning(sender, e);
                if (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, e);
                }
                if (choice == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        public void WrapOn(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.Wrap;
        }

        public void WrapOff(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.NoWrap;
        }

        public void ShowAboutPage(object sender, RoutedEventArgs e) // this will be refactored to display a separate xaml page
        {
            MessageBoxResult result;
            result = MessageBox.Show("message box ocntent here", "Title Sring");
        }

    }
}