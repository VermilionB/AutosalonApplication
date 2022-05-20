using System;
using System.Windows;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Autosalon.Resources.UserControls;
using Microsoft.AspNet.Identity;

namespace Autosalon.ViewModels;

public class RegistrationViewModel : ViewModelBase
{
    private LoginViewModel _loginViewModel;
    private string _name;
    private string _surname;
    private string _email;
    private string _password;
    
    public string Name
    {
        get => _name;
        set => Set(ref _name, value);
    }
    
    public string Surname
    {
        get => _surname;
        set => Set(ref _surname, value);
    }
    
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
    
    public ICommand RegisterCommand { get; set; }
    
    public RegistrationViewModel(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
        RegisterCommand = new RelayCommand(OnRegisterExecute, CanRegisterExecuted);
    }

    private bool CanRegisterExecuted(object o) => true;
    private void OnRegisterExecute(object obj)
    {
        try
        {   
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                AuthId = Guid.NewGuid(),
                Name = Name,
                Surname = Surname,

            };

            var auth = new UserAuth()
            {
                Id = customer.AuthId,
                Email = Email,
                Password = Encryption.Encrypt(Password)
            };

            if (Validation.CheckValid(customer) && Validation.CheckValid(auth))
            {
                using (var db = new AutosalonContext())
                {
                    db.Customers.Add(customer);
                    db.UserAuths.Add(auth);
                    db.SaveChanges();
                }
                var result = new CustomMessageBox("You successfully registered!", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _loginViewModel.CurrentWindow = new AuthorizationWindow();
                _loginViewModel.CurrentWindow.DataContext = new AuthorizationViewModel(_loginViewModel);
            }
            else
            {
                var result = new CustomMessageBox("Some fields are empty or invalid!", MessageType.Error, MessageButtons.OkCancel).ShowDialog();
            }
        }

        catch (Exception e)
        {
            var exception = new CustomMessageBox(e.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
        }
        
    }
    
}