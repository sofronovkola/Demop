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

public partial class MainUC : UserControl
{
    public List<Product> ProductList{get;set;}=new List<Product>();
    public MainUC()
    {
        InitializeComponent();
         ProductList = Context.Connect.Products.Include(c => c.ManufacturerNavigation)
                                      .Include(c => c.NameProductNavigation)
                                      .Include(c => c.Orders)
                                      .Include(c => c.SuplierNavigation).ToList();
    }


    public void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        DataContext = this;
    }

    Product selectProduct;

        private void ProductListBox_OnSelectionChanged(object? sender, RoutedEventArgs e)
    {
        selectProduct = (Product)ProdustListB.SelectedItem;
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        //App.MainWindow.MainContentControl.Content = new EditAddProductUC();
    }

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        //App.MainWindow.MainContentControl.Content = new EditAddProductUC(selectProduct);
    }

    private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Context.Connect.Ordersproducts.FirstOrDefault(c => c.ProductNavigation.IdProduct == selectProduct.IdProduct) != null)
    {
    }
    else
    {
        Context.Connect.Products.Remove(selectProduct);
        Context.Connect.SaveChanges();
    }

    App.MainWindow.MainContentControl.Content = new MainUC();
    }
}