using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.UIComponent.Container;

[ContentProperty(nameof(BodyContent))]
public partial class AppPageContainer : ContentView
{
    public AppPageContainer()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty BodyContentProperty = BindableProperty.Create(
        nameof(BodyContent),
        typeof(View),
        typeof(AppPageContainer),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            // Optionally, handle changes here if needed
        });

    public View BodyContent
    {
        get => (View)GetValue(BodyContentProperty);
        set => SetValue(BodyContentProperty, value);
    }
}