using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Demop.Entities;
using Demop.Models;
using Microsoft.EntityFrameworkCore;

namespace Demop.AllUserControl;

public partial class OrderUC : UserControl
{
    public List<Order> orderList{get; set;} = new List<Order>();
    public OrderUC()
    {
        InitializeComponent();
        orderList = Context.Connect.Orders.Include(c=> c.AddressNavigation)
                                            .Include(c=> c.IdUserNavigation).ToList();
        EventUI();               
    }

    private void EventUI(){
        OrderUserControl.Loaded += Control_OnLoaded;//Подписка на событие

        AddButton.Click += AddButton_OnClick;
        OrderListB.SelectionChanged += ProdustListB_OnSelectionChanged;
        RemoveButton.Click += DeleteButton_onClick;
        EditButton.Click+=EditButton_OnClick;
    }

    public void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        DataContext = this;
    }

      public void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new EditAddOrderUC();
    }

    Order selectProduct;
     public void ProdustListB_OnSelectionChanged(object? sender, RoutedEventArgs e)
    {
       selectProduct =(Order)OrderListB.SelectedItem;
       //selectProduct = OrderListB.SelectedItem as Order;
    }

    public void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
      App.MainWindow.MainContentControl.Content = new EditAddOrderUC(selectProduct);
    }

    public void DeleteButton_onClick(object? sender, RoutedEventArgs e)
    {
      
            Context.Connect.Orders.Remove(selectProduct);
            Context.Connect.SaveChanges();
           

         App.MainWindow.MainContentControl.Content = new OrderUC();
    }
}