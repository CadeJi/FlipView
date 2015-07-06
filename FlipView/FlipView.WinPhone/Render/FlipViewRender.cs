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
            fv.ItemsSource = this.Element.Children;
            this.SetNativeControl(fv);
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            //Debug.WriteLine(e.PropertyName);

            //switch (e.PropertyName) {
            //    case "Orientation":
            //        this.Control.Orientation = (System.Windows.Controls.Orientation)((int)this.Element.Orientation);
            //        break;
            //}
        }
    }
}
