using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Demop.Entities;
using Demop.Models;
using Microsoft.EntityFrameworkCore;

namespace Demop.AllUserControl;

public partial class OrderUC : UserControl
{
    private const int AdminRole = 1;

    public List<Order> orderList { get; set; } = new List<Order>();
    private static int userRole;
    private Order? selectOrder;

    public OrderUC(int role)
    {
        userRole = role;
        InitializeComponent();
        LoadOrders();
        EventUI();
    }

    public OrderUC()
    {
        InitializeComponent();
        LoadOrders();
        EventUI();
    }

    private void LoadOrders()
    {
        orderList = Context.Connect.Orders.Include(c => c.AddressNavigation)
            .Include(c => c.StatusNavigation)
            .Include(c => c.ArticleNavigation)
            .Include(c => c.IdUserNavigation)
            .ToList();
    }

    private void EventUI()
    {
        OrderUserControl.Loaded += Control_OnLoaded;
        AddButton.Click += AddButton_OnClick;
        OrderListB.SelectionChanged += OrderListB_OnSelectionChanged;
        RemoveButton.Click += DeleteButton_onClick;
        EditButton.Click += EditButton_OnClick;
        ButtonPanel.IsVisible = userRole == AdminRole;
    }

    public void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        DataContext = this;
    }

    public void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new EditAddOrderUC();
    }

    public void OrderListB_OnSelectionChanged(object? sender, RoutedEventArgs e)
    {
        selectOrder = OrderListB.SelectedItem as Order;
    }

    public void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (selectOrder == null)
        {
            ErrorDelete.Text = "Выберите заказ для редактирования";
            return;
        }

        App.MainWindow.MainContentControl.Content = new EditAddOrderUC(selectOrder);
    }

    public void DeleteButton_onClick(object? sender, RoutedEventArgs e)
    {
        if (selectOrder == null)
        {
            ErrorDelete.Text = "Выберите заказ для удаления";
            return;
        }

        Context.Connect.Orders.Remove(selectOrder);
        Context.Connect.SaveChanges();
        App.MainWindow.MainContentControl.Content = new OrderUC(userRole);
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new MainUC(userRole);
    }
}
