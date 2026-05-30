using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Demop.Entities;
using Demop.Models;
using Microsoft.EntityFrameworkCore;

namespace Demop.AllUserControl;

public partial class MainUC : UserControl
{
    private const int GuestRole = 0;
    private const int AdminRole = 1;
    private const int ManagerRole = 2;

    public List<Product> ProductList { get; set; } = new List<Product>();
    public List<Suplier> Suppliers { get; set; } = new List<Suplier>();
    private static int userRole = GuestRole;
    private Product? selectProduct;
    private string supplierPar = string.Empty;
    private string searchPar = string.Empty;
    private int sort = -1;

    public MainUC(int role)
    {
        userRole = role;
        InitializeComponent();
        LoadData();
        VisibleUI();
    }

    public MainUC()
    {
        InitializeComponent();
        LoadData();
        VisibleUI();
    }

    private void LoadData()
    {
        ProductList = Context.Connect.Products.Include(c => c.ManufacturerNavigation)
            .Include(c => c.NameProductNavigation)
            .Include(c => c.SuplierNavigation)
            .ToList();

        Suppliers = Context.Connect.Supliers.ToList();
        Suppliers.Insert(0, new Suplier { IdSuplier = 0, Suplier1 = "Все поставщики" });
    }

    private void VisibleUI()
    {
        if (userRole == AdminRole || userRole == ManagerRole)
        {
            SortPanel.IsVisible = true;
            OrdersButton.IsVisible = true;
        }

        if (userRole == AdminRole)
        {
            ButtonPanel.IsVisible = true;
        }
    }

    public void Control_OnLoaded(object? sender, RoutedEventArgs e)
    {
        DataContext = this;
    }

    private void ProductListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        selectProduct = ProdustListB.SelectedItem as Product;
    }

    private void AddButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new EditAddProductUC();
    }

    private void EditButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (selectProduct == null)
        {
            Errors.Text = "Выберите товар для редактирования";
            return;
        }

        App.MainWindow.MainContentControl.Content = new EditAddProductUC(selectProduct);
    }

    private void DeleteButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (selectProduct == null)
        {
            Errors.Text = "Выберите товар для удаления";
            return;
        }

        if (Context.Connect.Ordersproducts.Any(c => c.Product == selectProduct.IdProduct))
        {
            Errors.Text = "Данный товар имеется в списке заказов. Невозможно удалить";
            return;
        }

        Context.Connect.Products.Remove(selectProduct);
        Context.Connect.SaveChanges();
        App.MainWindow.MainContentControl.Content = new MainUC(userRole);
    }

    private void Search(string supplier, string search, int selectedSort = -1)
    {
        supplierPar = supplier ?? string.Empty;
        searchPar = search ?? string.Empty;

        var query = Context.Connect.Products.Include(c => c.ManufacturerNavigation)
            .Include(c => c.NameProductNavigation)
            .Include(c => c.SuplierNavigation)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(supplierPar))
        {
            query = query.Where(c => c.SuplierNavigation != null && c.SuplierNavigation.Suplier1 != null && c.SuplierNavigation.Suplier1.Contains(supplierPar));
        }

        if (!string.IsNullOrWhiteSpace(searchPar))
        {
            query = query.Where(c =>
                (c.NameProductNavigation != null && c.NameProductNavigation.NameProduct != null && c.NameProductNavigation.NameProduct.Contains(searchPar)) ||
                (c.Category != null && c.Category.Contains(searchPar)) ||
                (c.Description != null && c.Description.Contains(searchPar)) ||
                (c.ManufacturerNavigation != null && c.ManufacturerNavigation.Manufacturer1 != null && c.ManufacturerNavigation.Manufacturer1.Contains(searchPar)) ||
                (c.SuplierNavigation != null && c.SuplierNavigation.Suplier1 != null && c.SuplierNavigation.Suplier1.Contains(searchPar)) ||
                (c.ProductUnit != null && c.ProductUnit.Contains(searchPar)));
        }

        ProductList = query.ToList();

        if (selectedSort == 1)
        {
            ProductList = ProductList.OrderBy(c => c.Count).ToList();
        }

        if (selectedSort == 0)
        {
            ProductList = ProductList.OrderByDescending(c => c.Count).ToList();
        }

        ProdustListB.ItemsSource = ProductList;
    }

    private void SuppliersCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var supplier = SuppliersCBox.SelectedItem as Suplier;
        supplierPar = supplier == null || supplier.IdSuplier == 0 ? string.Empty : supplier.Suplier1 ?? string.Empty;
        Search(supplierPar, searchPar, sort);
    }

    private void SearchTextB_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        searchPar = SearchTextB.Text ?? string.Empty;
        Search(supplierPar, searchPar, sort);
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

        Search(supplierPar, searchPar, sort);
    }

    private void ResetButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new MainUC(userRole);
    }

    private void OrderButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new OrderUC(userRole);
    }
}
