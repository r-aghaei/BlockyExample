using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlockyExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowCodeButton.IsEnabled = false;
            RunCodeButton.IsEnabled = false;
            Browser.NavigateToString(System.IO.File.ReadAllText(GetFilePath("blockyHTML.html")));
        }

        private void RunCodeButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.InvokeScript("runCode", new object[] { });
        }

        private void ShowCodeButton_Click(object sender, RoutedEventArgs e)
        {
            var result = Browser.InvokeScript("showCode", new object[] { });
            MessageBox.Show(result.ToString());
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            ShowCodeButton.IsEnabled = true;
            RunCodeButton.IsEnabled = true;
            var toolboxXML = System.IO.File.ReadAllText(GetFilePath("blockyToolbox.xml"));
            var workspaceXML = System.IO.File.ReadAllText(GetFilePath("blockyWorkspace.xml"));
            //Initialize blocky using toolbox and workspace
            Browser.InvokeScript("init", new object[] { toolboxXML, workspaceXML });
        }

        private string GetFilePath(string file)
        {
            var directory = System.AppDomain.CurrentDomain.BaseDirectory;
            return System.IO.Path.Combine(directory, file);
        }
    }
}
