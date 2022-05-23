using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Autosalon.Pages.AdminPages;

public partial class AddCarPage : Page
{
    public AddCarPage()
    {
        InitializeComponent();
    }
    
    private void Price_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!Char.IsDigit(e.Text, 0))
            e.Handled = true;
    }
}