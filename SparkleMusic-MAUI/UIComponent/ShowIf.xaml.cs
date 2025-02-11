using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.UIComponent;

public partial class ShowIf : ContentView
{
    public ShowIf()
    {
        InitializeComponent();
    }
    
    // Bindables
    public View True
    {
        get => (View)GetValue(TrueProperty);
        set => SetValue(TrueProperty, value);
    }

    public static readonly BindableProperty TrueProperty =
        BindableProperty.Create (nameof(True), typeof(View), typeof(ShowIf), null, propertyChanged: OnPropertyChange);

    public View False
    {
        get => (View)GetValue(FalseProperty);
        set => SetValue(FalseProperty, value);
    }

    public static readonly BindableProperty FalseProperty =
        BindableProperty.Create (nameof(False), typeof(View), typeof(ShowIf), null, propertyChanged: OnPropertyChange);
    
    public bool Condition
    {
        get => (bool)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly BindableProperty ConditionProperty =
        BindableProperty.Create (nameof(False), typeof(bool), typeof(ShowIf), true, propertyChanged: OnPropertyChange);

    private static void OnPropertyChange(BindableObject bindable, object oldvalue, object newvalue)
    {
        var ShowIf = (ShowIf)bindable;
        ShowIf.HandleRender();
    }
    public void HandleRender()
    {
        if (Condition)
        {
            Content = True;
        }
        else
        {
            Content = False;
        }
    }

}