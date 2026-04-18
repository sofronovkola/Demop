using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace Demop.Entities;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? Article { get; set; }

    public int? NameProduct { get; set; }

    public string? ProductUnit { get; set; }

    public int? Price { get; set; }

    public int? Suplier { get; set; }

    public int? Manufacturer { get; set; }

    public string? Category { get; set; }

    public int? Discount { get; set; }

    public int? Count { get; set; }

    public string? Description { get; set; }

    public string? Photo { get; set; }

    public virtual Manufacturer? ManufacturerNavigation { get; set; }

    public virtual Namesproduct? NameProductNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Ordersproduct> Ordersproducts { get; set; } = new List<Ordersproduct>();

    public virtual Suplier? SuplierNavigation { get; set; }


    string put=@"C:\Users\kolya\AvaloniaProjects\Demop\Demop\bin\Debug\net10.0\import";
    public Bitmap? ImagePath
    {
        get
        {
            var ImagePath = @"C:\Users\kolya\AvaloniaProjects\Demop\Demop\bin\Debug\net10.0\import\picture.png";

            if (!string.IsNullOrWhiteSpace(Photo))
            {
                ImagePath=Path.Combine(put, Photo);
            } 

            return new Bitmap(ImagePath);
        }
    }

    public bool HasDiscount => Discount.GetValueOrDefault() > 0;

    public bool HasNoDiscount => !HasDiscount;

    public string OriginalPriceText => $"{Price.GetValueOrDefault()}";

    public string FinalPriceText
    {
        get
        {
            var price = Price.GetValueOrDefault();
            var discount = Discount.GetValueOrDefault();
            var finalPrice = price * (1 - discount / 100.0);
            return $"{Math.Round(finalPrice, 2)}";
        }
    }

    public IBrush RowBackground
    {
        get
        {
            if (Count.GetValueOrDefault() <= 0)
            {
                return Brushes.LightBlue;
            }

            if (Discount.GetValueOrDefault() > 15)
            {
                return new SolidColorBrush(Color.Parse("#2E8B57"));
            }

            return Brushes.Transparent;
        }
    }
}
