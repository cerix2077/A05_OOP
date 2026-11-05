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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void NewFile(object sender, RoutedEventArgs e)
        {
        }

        public void OpenFile(object sender, RoutedEventArgs e)
        {
        }

        public void SaveFile(object sender, RoutedEventArgs e)
        {
        }

        public void SaveFileAs(object sender, RoutedEventArgs e)
        {
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