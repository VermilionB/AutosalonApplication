using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;
using Autosalon.Pages;
using Autosalon.Resources.UserControls;
using Microsoft.EntityFrameworkCore.Storage;

namespace Autosalon.ViewModels;

public class MyProfileViewModel : ViewModelBase
{
    private CustomerViewModel _customerViewModel;
    private Customer _customer;
    private string _email;
    public Customer Customer
    {
        get => _customer; 
        set => Set(ref _customer, value);
    }

    public string Email
    {
        get => _email;
        set => Set(ref _email, value);
    }

    public string? Name
    {
        get => Customer.Name;
    }
    
    public string Surname
    {
        get => Customer.Surname;
    }
    public MyProfileViewModel(CustomerViewModel customerViewModel)
    {
        _customerViewModel = customerViewModel;
        Customer = AutosalonContext.GetContext().Customers.FirstOrDefault(c => c.Id == CurrentUser.getInstanceCustomer().Id);
        Email = AutosalonContext.GetContext().UserAuths.FirstOrDefault(u => u.Id == Customer.AuthId).Email;
        LogOutCommand = new RelayCommand(OnLogOutExecute, CanLogOutExecuted);
    }
    public ICommand LogOutCommand { get; }
    private bool CanLogOutExecuted(object o) => true;

    private void OnLogOutExecute(object o)
    {
        CurrentUser.setInstanceCustomer(null);
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.DataContext = new LoginViewModel();

        if (Application.Current.MainWindow != null) Application.Current.MainWindow = loginWindow;
        loginWindow.Show();
        foreach (Window item in Application.Current.Windows)
        {
            if (item.DataContext == _customerViewModel) item.Close();
        }   
        
    }
}