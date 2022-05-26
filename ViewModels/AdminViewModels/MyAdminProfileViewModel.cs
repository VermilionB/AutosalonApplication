using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Models;
using Autosalon.Pages;

namespace Autosalon.ViewModels.AdminViewModels;

public class MyAdminProfileViewModel : ViewModelBase
{
    private AdminViewModel _managerViewModel;
    private Manager _manager;
    private string _email;
    public Manager Manager
    {
        get => _manager; 
        set => Set(ref _manager, value);
    }

    public string Email
    {
        get => _email;
        set => Set(ref _email, value);
    }

    public string? Name
    {
        get => Manager.Name;
    }
    
    public string? Surname
    {
        get => Manager.Surname;
    }
    
    public string? AmountOfSaledCars
    {
        get => Manager.AmountOfSaledCars.ToString();
    }
    public MyAdminProfileViewModel(AdminViewModel managerViewModel)
    {
        _managerViewModel = managerViewModel;
        Manager = AutosalonContext.GetContext().Managers.FirstOrDefault(c => c.Id == CurrentUser.getInstanceManager().Id);
        Email = AutosalonContext.GetContext().UserAuths.FirstOrDefault(u => u.Id == Manager.AuthId).Email;
        LogOutCommand = new RelayCommand(OnLogOutExecute, CanLogOutExecuted);
    }
    public ICommand LogOutCommand { get; }
    private bool CanLogOutExecuted(object o) => true;

    private void OnLogOutExecute(object o)
    {
        CurrentUser.ClearInstance();
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.DataContext = new LoginViewModel();

        if (Application.Current.MainWindow != null) Application.Current.MainWindow = loginWindow;
        loginWindow.Show();
        foreach (Window item in Application.Current.Windows)
        {
            if (item.DataContext == _managerViewModel) item.Close();
        }   
        
    }
}