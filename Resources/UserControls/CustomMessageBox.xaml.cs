using System.Windows;

namespace Autosalon.Resources.UserControls;

public partial class CustomMessageBox : Window
{
    public CustomMessageBox(string message, MessageType type, MessageButtons buttons)
    {
        InitializeComponent();
        TxtMessage.Text = message;
        switch (type)
        {
            case MessageType.Info:
            {
                TxtTitle.Text = "Information";
                InfoIcon.Visibility = Visibility.Visible;
                break;
            }

            case MessageType.Confirmation:
            {
                TxtTitle.Text = "Confirmation";
                ConfirmationIcon.Visibility = Visibility.Visible;
                break;
            }

            case MessageType.Success:
            {
                TxtTitle.Text = "Successfully";
                SuccessIcon.Visibility = Visibility.Visible;
                break;
            }
            case MessageType.Warning:
            {
                TxtTitle.Text = "Warning";
                ErrorIcon.Visibility = Visibility.Visible;
                break;
            }
            case MessageType.Error:
            {
                TxtTitle.Text = "Error";
                ErrorIcon.Visibility = Visibility.Visible;
                break;
            }
        }

        switch (buttons)
        {
            case MessageButtons.OkCancel:
                BtnYes.Visibility = Visibility.Collapsed;
                BtnNo.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.YesNo:
                BtnOk.Visibility = Visibility.Collapsed;
                BtnCancel.Visibility = Visibility.Collapsed;
                break;
            case MessageButtons.Ok:
                BtnOk.Visibility = Visibility.Visible;
                BtnCancel.Visibility = Visibility.Collapsed;
                BtnYes.Visibility = Visibility.Collapsed;
                BtnNo.Visibility = Visibility.Collapsed;
                break;
        }
    }

    private void btnYes_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void btnOk_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = true;
        this.Close();
    }

    private void btnNo_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        this.DialogResult = false;
        this.Close();
    }
}

public enum MessageType
{
    Info,
    Confirmation,
    Success,
    Warning,
    Error,
}

public enum MessageButtons
{
    OkCancel,
    YesNo,
    Ok,
}