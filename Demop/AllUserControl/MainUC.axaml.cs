using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.OpenGL;
using Demop.Entities;
using Demop.Models;
using Microsoft.EntityFrameworkCore;

namespace Demop.AllUserControl;

public partial class MainUC : UserControl
{
    public List<Product> ProductList{get;set;}=new List<Product>();
    public List<Manufacturer> manufacturer1{get; set;} = new List<Manufacturer>();
    static int user;
    public MainUC(int userRoles)
    {
        InitializeComponent();
         ProductList = Context.Connect.Products.Include(c => c.ManufacturerNavigation)
                                      .Include(c => c.NameProductNavigation)
                                      .Include(c => c.Orders)
                                      .Include(c => c.SuplierNavigation).ToList();
         manufacturer1=Context.Connect.Manufacturers.ToList(); 
         user = userRoles;
         VisibleUI();                            
    }

    public MainUC()
    {
        InitializeComponent();
         ProductList = Context.Connect.Products.Include(c => c.ManufacturerNavigation)
                                      .Include(c => c.NameProductNavigation)
                                      .Include(c => c.Orders)
                                      .Include(c => c.SuplierNavigation).ToList();
        
        manufacturer1=Context.Connect.Manufacturers.ToList();
        if(user==1 || user == 2)
        {
            VisibleUI();
        }
    }

    private void VisibleUI()
    {
        if (user == 1)
        {
            SortPanel.IsVisible=true;
            ButtonPanel.IsVisible=true;
        }
        if (user == 2)
        {
            SortPanel.IsVisible=true;
        }
    }

    public void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        DataContext = this;
    }

    Product selectProduct;

        private void ProductListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        selectProduct = (Product)ProdustListB.SelectedItem;
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new EditAddProductUC();
    }

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new EditAddProductUC(selectProduct);
    }

    private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Context.Connect.Ordersproducts.FirstOrDefault(c => c.ProductNavigation.IdProduct == selectProduct.IdProduct) != null)
        {
            Errors.Text="Данный товар имеется в списке заказов. Невозможно удалить";
        }
    else
    {
        Context.Connect.Products.Remove(selectProduct);
        Context.Connect.SaveChanges();
    }

    App.MainWindow.MainContentControl.Content = new MainUC();
    }

    private string manufacturerPar;
    private string searchPar;
    private int sort=0;

     void Search(string manufacturer, string search, int sort = -1)
    {
    ProductList = Context.Connect.Products.Include(c => c.ManufacturerNavigation).Where
        (c => c.ManufacturerNavigation.Manufacturer1.Contains(manufacturerPar) && c.NameProductNavigation.NameProduct.Contains(searchPar)).ToList();

    if (sort == 1)
    {
        ProductList = ProductList.OrderBy(c => c.Count).ToList();
    }

    if (sort == 0)
    {
        ProductList = ProductList.OrderByDescending(c => c.Count).ToList();
    }

    ProdustListB.ItemsSource=ProductList;
    }

     private void ManufacturersCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
    manufacturerPar = (ManufacturersCBox.SelectedItem as Manufacturer)?.Manufacturer1 ?? string.Empty;
    Search(manufacturerPar, searchPar);
    }

    private void SearchTextB_OnTextChanged(object? sender, RoutedEventArgs e)
    {
    searchPar = SearchTextB.Text;
    Search(manufacturerPar, searchPar);
    }

    private void SortButton_OnChecked(object? sender, RoutedEventArgs e)
    {
    if (SortUpButton.IsChecked == true)
    {
        sort = 1;
    }

    if (SortDownButton.IsChecked == true)
    {
        sort = 0;
    }
    Search(manufacturerPar, searchPar, sort);
    }

    private void ResetButton_OnClick(object? sender, RoutedEventArgs e)
    {
       App.MainWindow.MainContentControl.Content = new MainUC(); 
    }

    private void OrderButton_OnClick(object? sender, RoutedEventArgs e)
    {
       App.MainWindow.MainContentControl.Content = new OrderUC(); 
    }
}