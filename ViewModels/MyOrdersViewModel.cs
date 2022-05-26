using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using Autosalon.Base;
using Autosalon.Models;

namespace Autosalon.ViewModels;

public class MyOrdersViewModel : ViewModelBase
{
    private ObservableCollection<Order> _orderList;
    private ObservableCollection<Automobile> _myRequestsList;

    public ObservableCollection<Order> OrderList
    {
        get => _orderList;
        set => Set(ref _orderList, value);
    }

    public ObservableCollection<Automobile> MyRequestsList
    {
        get => _myRequestsList;
        set => Set(ref _myRequestsList, value);
    }

    public MyOrdersViewModel()
    {
        var ids = AutosalonContext.GetContext().Requests.Where(r => r.UserId == CurrentUser.getInstanceCustomer().Id)
            .AsEnumerable().Select(r => r.AutomobileId).ToList();
        OrderList = new ObservableCollection<Order>(AutosalonContext.GetContext().Orders
            .Where(x => x.CustomerId == CurrentUser.getInstanceCustomer()!.Id));

        MyRequestsList =
            new ObservableCollection<Automobile>(AutosalonContext.GetContext().Automobiles
                .Where(x => ids.Contains(x.Id)).ToList());
                
    }
}