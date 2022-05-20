using System.Windows.Controls;
using Autosalon.Base;
using Autosalon.Resources.UserControls;

namespace Autosalon.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private UserControl _currentWindow;
    private UserControl _loginWindow;
    private UserControl _registrationWindow;
    
    public UserControl CurrentWindow
    {
        get => _currentWindow;
        set => Set(ref _currentWindow, value);
    }
    public LoginViewModel()
    {
        _loginWindow = new AuthorizationWindow();
        _loginWindow.DataContext = new AuthorizationViewModel(this);
        CurrentWindow = _loginWindow;
        
    }

    
}