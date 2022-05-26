using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Autosalon.Base;
using Autosalon.Commands;
using Autosalon.Infrastructure;
using Autosalon.Models;
using Autosalon.Resources.UserControls;
using static System.String;

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
    
    
    public RegistrationViewModel(LoginViewModel loginViewModel)
    {
        _loginViewModel = loginViewModel;
        RegisterCommand = new RelayCommand(OnRegisterExecute, CanRegisterExecuted);
        ToLoginCommand = new RelayCommand(OnToLoginExecute, CanToLoginExecuted);
    }
    public ICommand RegisterCommand { get; set; }
    private bool CanRegisterExecuted(object o) => true;
    private void OnRegisterExecute(object obj)
    {
        try
        {
            if (IsNullOrEmpty(Email))
            {
                throw new Exception("Enter email");
            }
            if (IsNullOrEmpty(Password))
            {
                throw new Exception("Enter password");
            }
            if(IsNullOrEmpty(Surname) || IsNullOrEmpty(Name))
            {
                throw new Exception("Name and surname cannot be empty");
            }
            
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
                    
                    if(db.UserAuths.Any(x => x.Email == Email))
                    {
                        var emailEx = new CustomMessageBox("User with this email already exists", MessageType.Error, MessageButtons.Ok).ShowDialog();
                        return;
                    }
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
    
    public ICommand ToLoginCommand { get; }
    private bool CanToLoginExecuted(object o) => true;
    private void OnToLoginExecute(object obj)
    {
        _loginViewModel.CurrentWindow = new AuthorizationWindow();
        _loginViewModel.CurrentWindow.DataContext = new AuthorizationViewModel(_loginViewModel);
    }
    
}