using System.Security.AccessControl;
using Avalonia.Controls;
using Demop.AllUserControl;

namespace Demop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        App.MainWindow=this;
        App.MainWindow.MainContentControl.Content=new AuthUC();
    }
}