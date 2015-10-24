using FlipView.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using FlipView.iOS.Renders;
using Xamarin.Forms;
using System.Drawing;


[assembly: ExportRenderer(typeof(Flip), typeof(FlipViewRender))]
namespace FlipView.iOS.Renders {
    public class FlipViewRender : ViewRenderer<Flip, Controls.FlipView> {

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            var fv = new Controls.FlipView();
            var items = this.GetChildrenViews().ToList();
            fv.SetItems(items);

            this.SetNativeControl(fv);
            this.Control.SizeToFit();
            this.AddSubview(this.Control.PageControl);
        }


        private IEnumerable<UIView> GetChildrenViews() {
            foreach (var v in this.Element.Children) {
                var render = RendererFactory.GetRenderer(v);
                yield return render.NativeView;
            }
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint) {
            this.Control.UpdateLayout(widthConstraint, heightConstraint);
            return UIViewExtensions.GetSizeRequest(this.NativeView, widthConstraint, heightConstraint, 44.0, 44.0);
        }
    }
}
