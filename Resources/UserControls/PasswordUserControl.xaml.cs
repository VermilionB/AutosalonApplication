using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Autosalon.Resources.UserControls;

public partial class PasswordUserControl : UserControl
{
    public PasswordUserControl()
    {
        InitializeComponent();
    }
    private bool _isPasswordChanging;

    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(PasswordUserControl),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

    private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordUserControl passwordBox)
        {
            passwordBox.UpdatePassword();
        }
    }

    public string Password
    {
        get { return (string)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _isPasswordChanging = true;
        Password = PasswordBox.Password;
        _isPasswordChanging = false;
    }

    private void UpdatePassword()
    {
        if (!_isPasswordChanging)
        {
            PasswordBox.Password = Password;
        }
    }
}