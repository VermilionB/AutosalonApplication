using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Autosalon.Pages;
using Autosalon.Pages.AdminPages;
using Autosalon.Resources.UserControls;
using Autosalon.ViewModels.AdminViewModels;

namespace Autosalon.ViewModels;

public class AuthorizationViewModel : ViewModelBase
{
    private LoginViewModel _loginViewModel;

    private string _email;
    private string _password;

    public string Email
    {
        get => _email;
        set => Set(ref _email, value);
    }
    
    public string Password
    {
        get => _password;
        set => Set(ref _password, value);
    }

    public AuthorizationViewModel(LoginViewModel loginViewModel)
    {
        RegistrationWindow = new RegistrationWindow();
        _loginViewModel = loginViewModel;
        ToRegisterWindowCommand = new RelayCommand(OnToRegisterWindowExecute, CanToRegisterWindowExecuted);
        UserSignInCommand = new RelayCommand(OnUserSignIn, CanUserSignIn);
    }
    
    #region Navigation
    public ICommand ToRegisterWindowCommand { get; }
    #endregion
    
    #region AdminSignIn
    public ICommand UserSignInCommand { get; }
    private bool CanUserSignIn(object o) => true;
    private void OnUserSignIn(object o)
    {
        try
        {
            using (var db = new AutosalonContext())
            {
                var userAuthId = db.UserAuths.ToList().FirstOrDefault(u => u.Email == Email && u.Password == Encryption.Encrypt(Password))?.Id;
                if (userAuthId == null)
                {
                    throw new Exception("Incorrect data input");
                }
                var customer = db.Customers.ToList().FirstOrDefault(u => u.AuthId == userAuthId);
                var manager = db.Managers.ToList().FirstOrDefault(u => u.AuthId == userAuthId);
                Navigator navigator = new Navigator();

                if (customer != null)
                {
                    CurrentUser.setInstanceCustomer(customer);
                    CustomerWindow customerWindow = new CustomerWindow();
                    if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
                    customerWindow.DataContext = new CustomerViewModel();
                    customerWindow.Show();
                }
                else
                {
                    if (manager != null)
                    {
                        
                        CurrentUser.setInstanceManager(manager);
                        AdminWindow adminWindow = new AdminWindow();
                        if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
                        adminWindow.DataContext = new AdminViewModel();
                        adminWindow.Show();   
                        return;
                    }
                    
                    throw new Exception("User was not found");

                }
            }
        }
        catch(Exception ex)
        {
            var exception = new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
        }
           
    }

    #endregion

    #region  ToRegisterWindow
    
    private RegistrationWindow _registrationWindow;

    public RegistrationWindow RegistrationWindow
    {
        get => _registrationWindow;
        set => Set(ref _registrationWindow, value);
    }
     private bool CanToRegisterWindowExecuted(object o) => true;

     private void OnToRegisterWindowExecute(object o)
     {
         RegistrationWindow.DataContext = new RegistrationViewModel(_loginViewModel);
         _loginViewModel.CurrentWindow = RegistrationWindow;
     }

    #endregion
}