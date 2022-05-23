using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Autosalon.Resources.UserControls;

namespace Autosalon.ViewModels.AdminViewModels;

public class OrdersViewModel : ViewModelBase
{
    private ObservableCollection<Order> _orderList;
    private Order _selectedOrder;

    public Order SelectedOrder
    {
        get => _selectedOrder;
        set => Set(ref _selectedOrder, value);
    }

    public ObservableCollection<Order> OrderList
    {
        get => _orderList;
        set => Set(ref _orderList, value);
    }

    public OrdersViewModel()
    {
        ConfirmCommand = new RelayCommand(OnConfirmExecute, CanConfirmExecuted);
        CancelCommand = new RelayCommand(OnCancelExecute, CanCancelExecuted);
        DeleteCommand = new RelayCommand(OnDeleteExecute, CanDeleteExecuted);
        
        OrderList = new ObservableCollection<Order>(AutosalonContext.GetContext().Orders
            .Where(x => x.ManagerId == CurrentUser.getInstanceManager()!.Id));
    }

    #region Commands

    public ICommand ConfirmCommand { get; }
    public bool CanConfirmExecuted(object o) => true;

    private void OnConfirmExecute(object o)
    {
        var confirmation = new CustomMessageBox("Do you want to confirm this order?", MessageType.Confirmation,
            MessageButtons.YesNo);
        if (confirmation.ShowDialog() == true)
        {
            using (var db = AutosalonContext.GetContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == SelectedOrder.Id);
                if (order != null) order.Status = Status.Confirmed.ToString();
                db.SaveChanges();
            }

            OrderList = new ObservableCollection<Order>(AutosalonContext.GetContext().Orders);
        }
    }
    
    
    public ICommand CancelCommand { get; }
    public bool CanCancelExecuted(object o) => true;
    private void OnCancelExecute(object o)
    {
        var confirmation = new CustomMessageBox("Do you want to cancel this order?", MessageType.Confirmation,
            MessageButtons.YesNo);
        if(confirmation.ShowDialog() == true)
        {
            using (var db = AutosalonContext.GetContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == SelectedOrder.Id);
                if (order != null) order.Status = Status.Canceled.ToString();
                db.SaveChanges();
            }

            OrderList = new ObservableCollection<Order>(AutosalonContext.GetContext().Orders);
        }
        
    }
    
    public ICommand DeleteCommand { get; }
    
    private bool CanDeleteExecuted(object o) => true;
    private void OnDeleteExecute(object o)
    {
        var confirmation = new CustomMessageBox("Do you want to delete this order?", MessageType.Confirmation,
            MessageButtons.YesNo);
        if (confirmation.ShowDialog() == true)
        {
            using (var db = AutosalonContext.GetContext())
            {
                var order = db.Orders.FirstOrDefault(x => x.Id == SelectedOrder.Id);
                if (order != null) db.Orders.Remove(order);
                db.SaveChanges();
            }

            OrderList = new ObservableCollection<Order>(AutosalonContext.GetContext().Orders);
        }
    }

    #endregion
}