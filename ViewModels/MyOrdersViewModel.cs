using System.Collections.ObjectModel;
using System.Linq;
using Autosalon.Base;
using Autosalon.Models;

namespace Autosalon.ViewModels;

public class MyOrdersViewModel : ViewModelBase
{
    private ObservableCollection<Order> _orderList;

    public ObservableCollection<Order> OrderList
    {
        get => _orderList;
        set => Set(ref _orderList, value);
    }

    public MyOrdersViewModel()
    {
        OrderList = new ObservableCollection<Order>(AutosalonContext.GetContext().Orders
            .Where(x => x.CustomerId == CurrentUser.getInstanceCustomer()!.Id));
    }
}