using System;
using System.Collections.Generic;
using System.Linq;
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
using mshtml;

namespace WpfApplication1
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

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //HTMLDocument head = (web.Document as HTMLDocument);
            //head.getElementsByTagName("head")
            //HtmlElement scriptEl = webBrowser1.Document.CreateElement("script");
            //IHTMLScriptElement element = (IHTMLScriptElement)scriptEl.DomElement;
            //element.text = "function sayHello() { alert('hello') }";
            //head.AppendChild(scriptEl);
            //webBrowser1.Document.InvokeScript("sayHello");
        }
    }
}
