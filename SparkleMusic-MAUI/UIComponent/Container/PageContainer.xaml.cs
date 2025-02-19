using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkleMusic_MAUI.UIComponent.Container;

public partial class PageContainer : ContentView
{
    private ContentView ContentBody { get; set; }
    public PageContainer(ContentView contentView)
    {
        InitializeComponent();
        ContentBody = contentView;
        BindingContext = this;
    }

    public PageContainer()
    {
        InitializeComponent();
        ContentBody = new ContentView();
        BindingContext = this;
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (Parent != null)
        {
            PageContent.Content = ContentBody;
        }
    }
}