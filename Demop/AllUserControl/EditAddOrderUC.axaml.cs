using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Demop.Entities;
using Demop.Models;

namespace Demop.AllUserControl;

public partial class EditAddOrderUC : UserControl
{
    public Order _order { get; set; } = new Order();
    public List<Address> addresses { get; set; } = new List<Address>();
    public List<Status> statuses { get; set; } = new List<Status>();

    public EditAddOrderUC()
    {
        InitializeComponent();
        _order = new Order();
        LoadLists();
        DataContext = this;
    }

    public EditAddOrderUC(Order order)
    {
        InitializeComponent();
        _order = order;
        LoadLists();
        DataContext = this;
    }

    private void LoadLists()
    {
        addresses = Context.Connect.Addresses.ToList();
        statuses = Context.Connect.Statuses.ToList();
    }

    public void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(ArticleTextB.Text, out var article))
        {
            ErrorText.Text = "Артикул должен быть числом";
            return;
        }

        var selectedAddress = AddressesComboB.SelectedItem as Address;
        var selectedStatus = StatusComboB.SelectedItem as Status;

        if (selectedAddress == null || selectedStatus == null)
        {
            ErrorText.Text = "Заполните адрес и статус";
            return;
        }

        _order.Article = article;
        _order.DateOrder = DateStart.Text;
        _order.DateDelivery = DateEnd.Text;
        _order.Status = selectedStatus.IdStatus;
        _order.Address = selectedAddress.IdAddress;

        if (_order.IdOrder == 0)
        {
            Context.Connect.Orders.Add(_order);
        }

        Context.Connect.SaveChanges();
        App.MainWindow.MainContentControl.Content = new OrderUC();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MainWindow.MainContentControl.Content = new OrderUC();
    }
}
