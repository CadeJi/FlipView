using FlipView.Controls;
using FlipView.WinPhone.Render;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;


[assembly: ExportRenderer(typeof(Flip), typeof(FlipViewRender))]
namespace FlipView.WinPhone.Render {
    public class FlipViewRender : ViewRenderer<Flip, Controls.FlipView> {

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            var fv = new Controls.FlipView();

            fv.Orientation = System.Windows.Controls.Orientation.Horizontal;
            //fv.ItemsSource = this.Element.ItemsSource;
            fv.ItemsSource = this.Element.Children;
            //fv.Height = this.Element.HeightRequest;
            //fv.Width = this.Element.WidthRequest;

            //fv.ItemTemplate = (System.Windows.DataTemplate)System.Windows.Application.Current.Resources["FlipViewItem"];

            this.SetNativeControl(fv);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            Debug.WriteLine(e.PropertyName);

            switch (e.PropertyName) {
                case "Width":
                    this.Control.Width = this.Element.Width;
                    break;
                case "Height":
                    this.Control.Height = this.Element.Height;
                    break;
                case "Orientation":
                    this.Control.Orientation = (System.Windows.Controls.Orientation)((int)this.Element.Orientation);
                    break;
            }
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
            return base.GetDesiredSize(widthConstraint, heightConstraint);
        }

    }
}
