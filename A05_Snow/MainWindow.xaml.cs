/*
 * FILE             : MainWindow.xaml.cs
 * PROJECT          : Assignment 5
 * PROGRAMMER       : Cameron Snow & Byron Balsom
 * FIRST VERSION    : 2026-10-10
 * DESCRIPTION:
 */

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

        // Holds the 
        private string _comparatorString = string.Empty; 
        private string _workingFilePath = string.Empty;
        private string _workingFileName = "Untitled";

        private bool _changesMade = false;


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

        public bool changesMade
        {
            get { return _changesMade; }
            set { _changesMade = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
        }


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        MessageBoxResult UnsavedChangesWarning(object sender, EventArgs e)
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


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        public void NewFile(object sender, RoutedEventArgs e)
        {
            if (changesMade)
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


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        public void OpenFile(object sender, RoutedEventArgs e)
        {
            if (changesMade)
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

        
        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
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


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
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


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            StatusBar.Content = FileTextBox.Text.Length.ToString();
            changesMade = (FileTextBox.Text != comparatorString);
        }


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        public void ExitFunc(object sender, RoutedEventArgs e)
        {
            if (changesMade)
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

        public void CloseWindow(object sender, CancelEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)
            {
                MessageBoxResult choice = UnsavedChangesWarning(sender, e);
                if  (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, null);
                    e.Cancel = false;
                }
                else if (choice == MessageBoxResult.No)
                {
                    e.Cancel = false;
                }
                else if (choice == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        
        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        public void WrapOn(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.Wrap;
        }

        
        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        public void WrapOff(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.NoWrap;
        }


        /*
        * METHOD       : 
        * DESCRIPTION  :
        *   
        * PARAMETERS   :
        *     : 
        * RETURNS      :
        *   
        */
        public void ShowAboutPage(object sender, RoutedEventArgs e) // this will be refactored to display a separate xaml page
        {
            //MessageBoxResult result;

            AboutPage AboutWindow = new AboutPage();
            AboutWindow.Owner = this;
            AboutWindow.ShowDialog();

            //result = MessageBox.Show("message box content here", "Title String");
        }

    }
}