using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace SparkleMusic_MAUI.UIComponent.Button;

public partial class IconButton : ContentView
{
    public IconButton()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Icon Property
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(string), typeof(IconButton), default(string));

    // Command Property
    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(IconButton), null);

    // Command Parameter Property (Optional)
    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(IconButton), null);
    
}