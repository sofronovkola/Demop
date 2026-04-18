using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Demop.Entities;
using Demop.Models;

namespace Demop.AllUserControl;

public partial class EditAddOrderUC : UserControl
{
     Order _order{get; set;} = new Order();
    public List<Address> addresses{get; set;}
    public EditAddOrderUC()
    {
        InitializeComponent();
        _order=new Order();
        addresses = Context.Connect.Addresses.ToList();
      
        AddressesComboB.SelectedItem=_order.AddressNavigation;
        DataContext=this;
        App.PrewiewUC = new OrderUC();
    }

     public EditAddOrderUC(Order order)
    {
        InitializeComponent();
        _order=order;
        addresses = Context.Connect.Addresses.ToList();
     
        DataContext=this;
        App.PrewiewUC = new OrderUC();
       
    }
    public void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        _order.Article= ArticleTextB.Text;

        _order.DateOrder = DateStart.Text;
        _order.DateDelivery = DateEnd.Text;

        _order.Status = StatusTxtB.Text;
        _order.AddressNavigation = (Address)AddressesComboB.SelectedItem;


        Context.Connect.SaveChanges();
        App.MainWindow.MainContentControl.Content = new OrderUC();
    }
}