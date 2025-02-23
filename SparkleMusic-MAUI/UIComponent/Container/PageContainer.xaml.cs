using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Compatibility;

namespace SparkleMusic_MAUI.UIComponent.Container;

public partial class PageContainer : ContentView
{
    public PageContainer()
    {
        InitializeComponent();
        BindingContext = this.Parent?.BindingContext;
        // Initialize();
        // TestLabel.Text = "Replaced content";
    }

    private static readonly BindableProperty ChildrenContentProperty = BindableProperty.Create(nameof(ChildrenContent), typeof(View), typeof(PageContainer), null);
    
    public View ChildrenContent
    {
        get => (View)GetValue(ChildrenContentProperty);
        set => SetValue(ChildrenContentProperty, value);
    }
    
}