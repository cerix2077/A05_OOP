/*
 * FILE             : MainWindow.xaml.cs
 * PROJECT          : Assignment 5
 * PROGRAMMER       : Cameron Snow & Byron Balsom
 * FIRST VERSION    : 2026-10-10
 * DESCRIPTION:
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
        * METHOD       : ChangesMadePrompt
        * DESCRIPTION  :
        *   method to prompt user if the want to save the current textbox data before 
        *       performing the action in calling method
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   MessageBoxResult result : an enum relating to the selected option
        */
        MessageBoxResult ChangesMadePrompt(object sender, EventArgs e)
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
        * METHOD       : NewFile
        * DESCRIPTION  : Creates a new file and clears the textbox.
        *   
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
                choice = ChangesMadePrompt(sender, e);
                if (choice == MessageBoxResult.Yes)
                {
                    SaveFile(sender, e);
                    FileTextBox.Text = "";
                    workingFilePath = string.Empty; 
                    workingFileName = "Untitled";  
                }
                else if (choice == MessageBoxResult.No)
                {
                    FileTextBox.Text = "";  
                    workingFilePath = string.Empty;
                    workingFileName = "Untitled";
                }
                // cancel and x button both drop through and do nothing
            }
        }


        /*
        * METHOD       : OpenFile
        * DESCRIPTION  : Allows the user to select a file to open and displays the data in the textbox.
        *   
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
                choice = ChangesMadePrompt(sender, e);
                if (choice == MessageBoxResult.Yes) // if user selects yes, save the file and then open a new file
                {
                    SaveFile(sender, e);
                }
                else if (choice == MessageBoxResult.No) // if user selects no, open a new file without saving
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
        * METHOD       : SaveFile
        * DESCRIPTION  : Saves the current data to a file.
        *   
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
            }
            else
            {
                SaveFileAs(sender, e);
            }
        }


        /*
        * METHOD       : SaveFileAs
        * DESCRIPTION  : Allows the user to select a file to save the current data to.
        *   
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
            }
        }


        /*
        * METHOD       : TextChangedEvent
        * DESCRIPTION  : Detects when the text in the textbox has been changed,
        *              : updates the status bar with an active character count.
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void TextChangedEvent(object sender, RoutedEventArgs e)
        {
            StatusBar.Content = FileTextBox.Text.Length.ToString(); // update the status bar with the current character count
            changesMade = (FileTextBox.Text != comparatorString);   // update changesMade to the current text, to determine if changes have been made since the last save
        }


        /*
        * METHOD       : ExitFunc
        * DESCRIPTION  : Exits the application
        *   
        * PARAMETERS   :
        *   object sender           : the object which raised the event
        *   RoutedEventArgs e       : state info and data of an event
        * RETURNS      :
        *   N/A
        */
        public void ExitFunc(object sender, RoutedEventArgs e)
        {
            if (changesMade)    // check if changes have been made to the text, if so it will prompt the user to save changes before exiting
            {
                MessageBoxResult choice;
                choice = ChangesMadePrompt(sender, e);
                if (choice == MessageBoxResult.Yes) // if user selects yes, save the file and then exit
                {
                    SaveFile(sender, e);
                }
                if (choice == MessageBoxResult.No)  // if user selects no, exit without saving
                {
                    this.Close();
                }
            }
            else // if no changes have been made, exit without prompting the user to save changes
            {
                this.Close();
            }
        }



        /*
        * METHOD       : CloseWindow
        * DESCRIPTION  : When the user attempts to close the window, 
        *              : this method will prompt the user to save changes if any have been made.
        * PARAMETERS   : object sender      : the object which raised the event
        *              : CancelEventArgs e  : 
        * RETURNS      : N/A
        */
        public void CloseWindow(object sender, CancelEventArgs e)
        {
            if (FileTextBox.Text != comparatorString)   // check if changes have been made to the text, if so it will prompt the user to save changes before exiting
            {
                MessageBoxResult choice = ChangesMadePrompt(sender, e);
                if (choice == MessageBoxResult.Yes)     // if the user selects yes, save the file and then exit
                {
                    SaveFile(sender, null);
                    e.Cancel = false;
                }
                else if (choice == MessageBoxResult.No) // if the user selects no, exit without saving
                {
                    e.Cancel = false;
                }
                else if (choice == MessageBoxResult.Cancel) // if the user selects cancel, the program continues to run and the window does not close
                {
                    e.Cancel = true;
                }
            }
        }


        /*
        * METHOD       : WrapOn
        * DESCRIPTION  : Enables text wrapping
        *   
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
        * DESCRIPTION  : Disables text wrapping
        *   
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
        * DESCRIPTION  : Displays the about page
        *   
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