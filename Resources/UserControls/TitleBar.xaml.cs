using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Autosalon.Resources.UserControls;

public partial class TitleBar : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string),
            typeof(TitleBar), new PropertyMetadata(string.Empty));

    public new static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register("Width", typeof(double),
            typeof(TitleBar), new PropertyMetadata(double.NaN));

    public new static readonly DependencyProperty HeightProperty =
        DependencyProperty.Register("Height", typeof(double),
            typeof(TitleBar), new PropertyMetadata(double.NaN));

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(ImageSource),
            typeof(TitleBar), new PropertyMetadata(null));

    public string Title
    {
        get => (string) GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public new double Width
    {
        get => (double) GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    public new double Height
    {
        get => (double) GetValue(HeightProperty);
        set => SetValue(HeightProperty, value);
    }

    public ImageSource Icon
    {
        get => (ImageSource) GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public TitleBar()
    {
        InitializeComponent();
    }

    private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this);
        if (window != null)
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    private void MaximizeButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this);
        if (window == null)
        {
            return;
        }

        if (window.WindowState == WindowState.Maximized)
        {
            window.WindowState = WindowState.Normal;
            MaximizeButton.Content = FindResource("MaximizeIconImg");
        }
        else
        {
            window.WindowState = WindowState.Maximized;
            MaximizeButton.Content = FindResource("RestoreIconImg");
        }
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        var window = Window.GetWindow(this);
        window?.Close();
    }
}