﻿using System.Collections.Specialized;
using System.Windows;
using System.Windows.Media;
using ICSharpCode.WpfDesign;
using ICSharpCode.WpfDesign.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Helper;
using XamarinFormsClasses.Base;

namespace XamarinFormsClasses.Controls
{  
    [ExtensionFor(typeof(Grid))]
    public class Grid_Extension : ViewExtension
    {
        public override FrameworkElement CreateView(DesignItem item)
        {
            var grid = new System.Windows.Controls.Grid();
	        grid.Background = TransparentBrush.GetTransparentBrush();

	        foreach (var collectionElement in item.ContentProperty.CollectionElements)
	        {
		        grid.Children.Add(collectionElement.View);
	        }

            item.ContentProperty.CollectionElements.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (DesignItem newItem in args.NewItems)
                    {
                        grid.Children.Add(newItem.View);
                    }
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (DesignItem oldItem in args.OldItems)
                    {
                        grid.Children.Remove(oldItem.View);
                    }
                }
            };

            return grid;
        }

        
        public override void SetBindings(DesignItem item, FrameworkElement view)
        {
            base.SetBindings(item, view);
        }        
    }
}
