using FlipView.Controls;
using FlipView.WinPhone.Render;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;


[assembly: ExportRenderer(typeof(Flip), typeof(FlipViewRender))]
namespace FlipView.WinPhone.Render {
    public class FlipViewRender : ViewRenderer<Flip, Controls.FlipView> {

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            var fv = new Controls.FlipView();
            //fv.SetBinding(Controls.FlipView.OrientationProperty, new System.Windows.Data.Binding("Orientation"));
            //fv.SetBinding(Controls.FlipView.ItemsSourceProperty, new System.Windows.Data.Binding("ItemsSource"));
            //fv.SetBinding(Controls.FlipView.WidthProperty, new System.Windows.Data.Binding("WidthRequest"));
            //fv.SetBinding(Controls.FlipView.HeightProperty, new System.Windows.Data.Binding("HeightRequest"));
            //fv.DataContext = this.Element.BindingContext;

            fv.Orientation = System.Windows.Controls.Orientation.Horizontal;
            fv.ItemsSource = this.Element.ItemsSource;
            fv.Height = this.Element.HeightRequest;
            fv.Width = this.Element.WidthRequest;

            this.SetNativeControl(fv);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            Debug.WriteLine(e.PropertyName);

            if (e.PropertyName.Equals(Flip.OpacityProperty.PropertyName))
                this.Control.Orientation = (System.Windows.Controls.Orientation)((int)this.Element.Orientation);
        }

    }
}
