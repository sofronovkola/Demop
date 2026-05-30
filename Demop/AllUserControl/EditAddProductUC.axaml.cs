using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Demop.Entities;
using Demop.Models;

namespace Demop.AllUserControl;

public partial class EditAddProductUC : UserControl
{
    public Product _product{get;set;}=new Product();
    public List<Manufacturer> manufacturers{get; set;}
    public List<Suplier> suppliers{get;set;}
    public List<Namesproduct> namesproducts{get;set;}
    public EditAddProductUC()
    {
        InitializeComponent();
        _product=new Product();
        namesproducts=Context.Connect.Namesproducts.ToList();
        suppliers=Context.Connect.Supliers.ToList();
        manufacturers=Context.Connect.Manufacturers.ToList();
        DataContext=this;
    }

    public EditAddProductUC(Product product)
    {
        InitializeComponent();
        _product=product;
        namesproducts=Context.Connect.Namesproducts.ToList();
        suppliers=Context.Connect.Supliers.ToList();
        manufacturers=Context.Connect.Manufacturers.ToList();
        DataContext=this;
    }

    private void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_product.Price.GetValueOrDefault() < 0)
        {
            ErrorText.Text = "Цена не может быть отрицательной";
            return;
        }

        if (_product.Count.GetValueOrDefault() < 0)
        {
            ErrorText.Text = "Количество не может быть отрицательным";
            return;
        }

        if (_product.Discount.GetValueOrDefault() < 0 || _product.Discount.GetValueOrDefault() > 100)
        {
            ErrorText.Text = "Скидка должна быть от 0 до 100";
            return;
        }

        if (ManufacturerComboBox.SelectedItem is Manufacturer manufacturer)
        {
            _product.Manufacturer = manufacturer.IdManufacturer;
        }

        if (SupplierComboBox.SelectedItem is Suplier supplier)
        {
            _product.Suplier = supplier.IdSuplier;
        }

        if (CategoryComboBox.SelectedItem is Namesproduct namesproduct)
        {
            _product.NameProduct = namesproduct.IdNameproduct;
        }

        if (_product.Article == null)
        {
            _product.Article = GenerateUniqueArticul();
            Context.Connect.Products.Add(_product);
        }

        Context.Connect.SaveChanges();
        App.MainWindow.MainContentControl.Content = new MainUC();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new MainUC();
    }

    private string GenerateUniqueArticul()
    {
        const string letters="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var rand = new Random();
        
        string articul;
        bool exists;

        do
        {
            //Формируем строку A111A1
            var a1=letters[rand.Next(letters.Length)];
            var d1=rand.Next(0,10);
            var d2=rand.Next(0,10);
            var d3=rand.Next(0,10);
            var a2=letters[rand.Next(letters.Length)];
            var d4=rand.Next(0,10);

            articul=$"{a1}{d1}{d2}{d3}{a2}{d4}";

            //Проверяем, есть ли такой уже в базе
            exists=Context.Connect.Products.Any(p => p.Article==articul);
        }while(exists);

        return articul;
    }

}