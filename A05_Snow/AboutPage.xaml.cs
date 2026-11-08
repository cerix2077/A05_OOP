/*
 * FILE             : AboutPage.xaml.cs
 * PROJECT          : Assignment 5
 * PROGRAMMER       : Cameron Snow & Byron Balsom
 * FIRST VERSION    : 2026-11-06
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    /// 

    /*
     * NAME     : AboutPage
     * PURPOSE  : Code behind for the SETpad about page
     */
    public partial class AboutPage : Window
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        // NOTE 
        // this was not part of the assignment spec but i had some extra time and wanted to try out using hyperlinks
        //  used learn.microsoft to find most information as well as comments within class and method definitions in Visual Studio  
        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.documents.hyperlink?view=windowsdesktop-10.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.start?view=net-10.0
        // https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo?view=net-10.0


        /*
        * METHOD       : HyperlinkNavHandler
        * DESCRIPTION  :
        *   sends the passed uri to the OS shell to be opened by the systems/users default browser
        * PARAMETERS   :
        *   object sender               : the object which raised the event
        *   RequestNavigateEventArgs e  : hyperlink data
        * RETURNS      :
        *   N/A
        */
        public void HyperlinkNavHandler(object sender, RequestNavigateEventArgs e) // RequestNavigateEventArgs is a sublass of RoutedEventArgs than can handle hyperlink URIs
        {
            // pass the uri into a ProcessStartInfo object
            ProcessStartInfo uriHandler = new ProcessStartInfo(e.Uri.AbsoluteUri);
            
            // set UseShellExecute to pass object to external OS shell, using the system default application to handle the uri
            uriHandler.UseShellExecute = true;
            
            // start the process
            Process.Start(uriHandler);
            
            // mark that this event was handled
            e.Handled = true; 
        }
    }
}
