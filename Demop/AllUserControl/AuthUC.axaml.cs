using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Demop.Entities;
using Demop.Models;

namespace Demop.AllUserControl;

public partial class AuthUC : UserControl
{
    public AuthUC()
    {
        InitializeComponent();
    }

    private void LoginBtn_Click(object? sender, RoutedEventArgs e)
    {
     User loginUser = Context.Connect.Users.FirstOrDefault(c => c.Login == LoginTb.Text && c.Password == PasswordTb.Text);
     if (loginUser != null)
        {
           App.MainWindow!.MainContentControl.Content = new MainUC();
        }
    }

}