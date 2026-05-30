using Avalonia.Controls;
using Avalonia.Interactivity;
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

    private void LogoutButton_OnClick(object? sender, RoutedEventArgs e)
    {
        userFIO.Text = string.Empty;
        MainContentControl.Content = new AuthUC();
    }
}