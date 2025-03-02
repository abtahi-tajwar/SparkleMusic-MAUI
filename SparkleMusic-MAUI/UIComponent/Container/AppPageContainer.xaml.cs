using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.UIComponent.Container;

public partial class AppPageContainer : ContentView
{
    public AppPageContainer()
    {
        InitializeComponent();
        // Populate children
    }
    
    private static readonly BindableProperty BodyContentProperty = BindableProperty.Create(
        nameof(BodyContent), 
        typeof(View), 
        typeof(AppPageContainer), 
        null,
        propertyChanged: OnBodyContentPropertyChanged
        );

    public View BodyContent
    {
        get => (View)GetValue(BodyContentProperty); 
        set => SetValue(BodyContentProperty, value);
    }

    private static void OnBodyContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var container = (AppPageContainer)bindable;
        container.UpdateGridContent();

    }
    private void UpdateGridContent()
    {
        BodyContentContainer.Children.Clear(); // Clear existing content
        if (BodyContent != null)
        {
            BodyContentContainer.Children.Add(BodyContent);
        }
    }
}