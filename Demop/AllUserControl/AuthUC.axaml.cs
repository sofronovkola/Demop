using System;
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
           App.MainWindow.userFIO.Text=loginUser.Fio.ToString(); //передача ФИО пользователя в MainWindow

            if (loginUser.RoleEmployee == 1)
            {
                App.MainWindow.MainContentControl.Content=new MainUC((Int32)loginUser.RoleEmployee);
            }
            else if (loginUser.RoleEmployee == 2)
            {
                App.MainWindow.MainContentControl.Content=new MainUC();
            }
             else if (loginUser.RoleEmployee == 3)
            {
                App.MainWindow.MainContentControl.Content=new MainUC();
            }
            else
            {
                App.MainWindow.MainContentControl.Content=new MainUC();
            }
        }
    }

}