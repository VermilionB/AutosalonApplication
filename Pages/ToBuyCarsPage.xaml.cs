using System;
using System.Windows.Controls;
using System.Windows.Input;


namespace Autosalon.Pages;

public partial class ToBuyCarsPage : Page
{
    public ToBuyCarsPage()
    {
        InitializeComponent();
    }

    private void Preview_TextInput(object sender, TextCompositionEventArgs e)
    {
        if (!Char.IsDigit(e.Text, 0))
            e.Handled = true;
        
    }
}