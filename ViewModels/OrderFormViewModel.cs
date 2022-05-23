using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Autosalon.Resources.UserControls;
using Autosalon.ViewModels.AdminViewModels;

namespace Autosalon.ViewModels;

public class OrderFormViewModel : ViewModelBase
{
    private CarPageViewModel _carPageViewModel;
    private Automobile _selectedAutomobile;
    private ObservableCollection<Manager> _managerList;
    private Manager _selectedManager;

    public OrderFormViewModel(CarPageViewModel carPageViewModel)
    {
        _carPageViewModel = carPageViewModel;
        SelectedAutomobile = carPageViewModel.SelectedAutomobile;
        ManagerList = new ObservableCollection<Manager>(AutosalonContext.GetContext().Managers.ToList());
        
        ConfirmOrderCommand = new RelayCommand(OnConfirmOrderExecute, CanConfirmOrderExecuted);
    }
    
    public Automobile SelectedAutomobile
    {
        get => _selectedAutomobile;
        set => Set(ref _selectedAutomobile, value);
    }
    public ObservableCollection<Manager> ManagerList
    {
        get => _managerList;
        set => Set(ref _managerList, value);
    }
    
    public Manager SelectedManager
    {
        get => _selectedManager;
        set => Set(ref _selectedManager, value);
    }

    public string ReleaseDate
    {
        get => SelectedAutomobile.ReleaseDate.ToString("dd.MM.yyyy");
    }
    
    #region ConfirmPrder
    public ICommand ConfirmOrderCommand { get; }
    private bool CanConfirmOrderExecuted(object o) => true;
    
    private void OnConfirmOrderExecute(object o)
    {
        try
        {
            if (SelectedManager == null)
            {
                throw new Exception("Select your manager");
            }
            using (var context = new AutosalonContext())
            {
                var order = new Order();
                order.Id = Guid.NewGuid();
                order.CustomerId = CurrentUser.getInstanceCustomer()!.Id;
                order.ManagerId = SelectedManager.Id;
                order.AutomobileId = SelectedAutomobile.Id;
                order.Date = DateTime.Now;
                order.TotalPrice = ((int) SelectedAutomobile.Price)!;
                order.Status = Status.InProcessing.ToString();
                context.Orders.Add(order);
                context.SaveChanges();
            }
            
        }
        catch (Exception e)
        {
            var exception = new CustomMessageBox(e.Message, MessageType.Error, MessageButtons.Ok);
        }
        
    }
    
    #endregion
}