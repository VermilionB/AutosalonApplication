using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Autosalon.Pages.AdminPages;

public partial class AllCarsPage : Page
{
    public AllCarsPage()
    {
        InitializeComponent();
    }
    
    private void Preview_TextInput(object sender, TextCompositionEventArgs e)
    {
        if (!Char.IsDigit(e.Text, 0))
            e.Handled = true;
        
    }
}