/*
 * FILE             : MainWindow.xaml.cs
 * PROJECT          : Assignment 5
 * PROGRAMMER       : Cameron Snow & Byron Balsom
 * FIRST VERSION    : 2026-11-05
 */

using Microsoft.Win32;
using System.ComponentModel;
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
    /// 

    /*
     * NAME     : MainWindow
     * PURPOSE  : Code behind for the SETpad text editor
     */
    public partial class MainWindow : Window
    {

        // full path to file being edited
        private string _workingFilePath = string.Empty;
        // an isolated filename of the file being edited
        private string _workingFileName = "Untitled";

        // Holds the last saved string to compare if changes were made
        private string _comparatorString = string.Empty; 
        // Bool to store if changes have been made
        private bool _changesMade = false;

        // Properties
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
        * METHOD       : ChangesMadePrompt
        * DESCRIPTION  :
        *   method to prompt user if the want to save the current textbox data before 
        *       performing the action in calling method
        * PARAMETERS   :
        *   object sender   : the object which raised the event
        * RETURNS      :
        *   MessageBoxResult result : an enum relating to the selected option
        */
        MessageBoxResult ChangesMadePrompt(object sender) 
        {
            //prompt user for yes/no/cancel
            MessageBoxResult result;
            string saveString = $"Do you want to Save changes to {workingFileName}?";
            result = MessageBox.Show(saveString, "SETpad",  MessageBoxButton.YesNoCancel);
            return result;
        }


        /*
        * METHOD       : NewFile
        * DESCRIPTION  :
        *   this method handles logic for opening a blank untitled document
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event 
        * RETURNS      :
        *   N/A
        */
        public void NewFile(object sender, RoutedEventArgs e)
        {
            if (changesMade)
            {
                MessageBoxResult choice;
                choice = ChangesMadePrompt(sender);
                if (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, e);
                    FileTextBox.Text = "";
                    workingFilePath = string.Empty;
                    workingFileName = "Untitled";
                    this.Title = "SETpad - Untitled";
                }
                else if (choice == MessageBoxResult.No)
                {
                    FileTextBox.Text = "";
                    workingFilePath = string.Empty;
                    workingFileName = "Untitled";
                    this.Title = "SETpad - Untitled";
                }
                // cancel and x button both drop through and do nothing
            }
        }


        /*
        * METHOD       : OpenFile
        * DESCRIPTION  :
        *   this methods handles opening an existing file
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void OpenFile(object sender, RoutedEventArgs e)
        {
            if (changesMade)
            {
                //prompt user for yes/no/cancel with focus set onto yes for enter press selection
                MessageBoxResult choice;
                choice = ChangesMadePrompt(sender);
                if (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, e);
                    OpenFileDialog ofd = new OpenFileDialog();
                    if (ofd.ShowDialog() == true)
                    {
                        FileTextBox.Text = File.ReadAllText(ofd.FileName);
                        comparatorString = FileTextBox.Text; // this is the current 
                        workingFilePath = ofd.FileName;
                        workingFileName = System.IO.Path.GetFileName(workingFilePath); // Path has ambiguity between IO.Path and Windows.Shapes.Path
                        this.Title = $"SETpad - {workingFilePath}";
                    }
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
                        this.Title = $"SETpad - {workingFilePath}";
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
                    workingFileName = System.IO.Path.GetFileName(workingFilePath); // Path has ambiguity between IO.Path and Windows.Shapes.Path
                    this.Title = $"SETpad - {workingFilePath}";
                }
            }
        }


        /*
        * METHOD       : SaveFile
        * DESCRIPTION  :
        *   this method handles saving to the current file or passes it of to SaveFileAs to make file if none exists
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void SaveFile(object sender, RoutedEventArgs e)
        {
            // check if a file exists to save too, otherwise use SaveFileAs()
            if(workingFilePath != String.Empty)// file was found to save too
            {
                File.WriteAllText(workingFilePath, FileTextBox.Text);
                comparatorString = FileTextBox.Text;
            }
            else
            {
                SaveFileAs(sender, e);
            }
        }


        /*
        * METHOD       : SaveFileAs
        * DESCRIPTION  :
        *   use a save file dialoug to save choose the filename to sve as
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
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
                comparatorString = FileTextBox.Text;
            }
        }


        /*
        * METHOD       : TextChangedEvent
        * DESCRIPTION  :
        *   this method handles both updating the status bar and checking if changes have been made to the 
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            StatusBar.Content = FileTextBox.Text.Length.ToString();
            changesMade = (FileTextBox.Text != comparatorString);
        }


        /*
        * METHOD       : ExitFunc
        * DESCRIPTION  :
        *   handles exiting using the menu option
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void ExitFunc(object sender, RoutedEventArgs e)
        {
            if (changesMade)
            {
                MessageBoxResult choice;
                choice = ChangesMadePrompt(sender);
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

        /*
        * METHOD       : CloseWindow
        * DESCRIPTION  :
        *   handles exiting using the close button
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   CancelEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void CloseWindow(object sender, CancelEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)
            {
                MessageBoxResult choice = ChangesMadePrompt(sender);
                if (choice == MessageBoxResult.Yes)
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
        * METHOD       : WrapOn
        * DESCRIPTION  :
        *   handles activating text wrapping
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void WrapOn(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.Wrap;
        }


        /*
        * METHOD       : WrapOff
        * DESCRIPTION  :
        *   handles deactivationg text wrapping
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void WrapOff(object sender, RoutedEventArgs e)
        {
            FileTextBox.TextWrapping = TextWrapping.NoWrap;
        }


        /*
        * METHOD       : ShowAboutPage
        * DESCRIPTION  :
        *   handles opening a modal about page
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void ShowAboutPage(object sender, RoutedEventArgs e) // this will be refactored to display a separate xaml page
        {
            AboutPage AboutWindow = new AboutPage();
            AboutWindow.Owner = this;
            AboutWindow.ShowDialog();
        }
    }
}