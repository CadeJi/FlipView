using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using FlipView.Controls;
using Xamarin.Forms;
using FlipView.Droid.Renders;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(Flip), typeof(FlipViewRender))]
namespace FlipView.Droid.Renders {
    public class FlipViewRender : ViewRenderer<Flip, Android.Widget.RelativeLayout> {

        private LinearLayout Container = null;
        private LinearLayout PointsContainer = null;

        private int PrevX = 0;
        private int Count = 0;

        private int _idx = 0;
        private int Idx {
            get {
                return this._idx;
            }
            set {
                this._idx = value < 0 ? 0 : (value >= this.Count ? this.Count - 1 : value);
            }
        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            switch (e.PropertyName) {
                case "Width":
                case "Height":
                    break;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            var width = (int)this.Element.WidthRequest;
            var height = (int)this.Element.HeightRequest;

            var root = new Android.Widget.RelativeLayout(this.Context);
            root.SetBackgroundColor(Color.Yellow.ToAndroid());

            var scroller = new HorizontalScrollView(this.Context);
            root.AddView(scroller);
            scroller.SetBackgroundColor(Color.Green.ToAndroid());

            this.Container = new Android.Widget.LinearLayout(this.Context);
            this.Container.Orientation = Orientation.Horizontal;

            scroller.AddView(this.Container);
            scroller.Touch += scroller_Touch;

            this.PointsContainer = new LinearLayout(this.Context);
            this.PointsContainer.Orientation = Orientation.Horizontal;
            this.PointsContainer.TextAlignment = Android.Views.TextAlignment.Center;

            var lp = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.WrapContent, 20);
            lp.AddRule(LayoutRules.AlignParentBottom);
            lp.AddRule(LayoutRules.CenterHorizontal);
            root.AddView(this.PointsContainer, lp);

            this.SetNativeControl(root);
            root.Invalidate();
            root.RequestLayout();

            this.SetItems();
            this.SetPoints();
        }

        void scroller_Touch(object sender, Android.Views.View.TouchEventArgs e) {

            e.Handled = false;
            var scroller = (HorizontalScrollView)sender;
            switch (e.Event.Action) {
                case MotionEventActions.Down:
                    this.PrevX = scroller.ScrollX;
                    break;
                case MotionEventActions.Up:
                    var delta = scroller.ScrollX - PrevX;
                    Idx += delta < 0 ? -1 : 1;

                    this.Snap();
                    break;
            }
        }

        private void Snap() {
            Console.WriteLine(this.Idx);
        }

        private void SetItems() {
            this.Container.RemoveAllViewsInLayout();
            foreach (var v in this.Element.Children) {
                var render = RendererFactory.GetRenderer(v);
                //this.Container.AddView(render.ViewGroup, new LinearLayout.LayoutParams((int)this.Element.WidthRequest, (int)this.Element.HeightRequest));
                this.Container.AddView(render.ViewGroup, new LinearLayout.LayoutParams((int)this.Element.WidthRequest, LayoutParams.MatchParent));
            }
            this.Count = this.Element.Children.Count();
        }

        private void SetPoints() {
            var lp = new LinearLayout.LayoutParams(15, 15);
            lp.LeftMargin = 5;
            lp.RightMargin = 5;
            for (var i = 0; i < this.Count; i++) {
                var v = new Android.Views.View(this.Context);
                v.SetBackgroundColor(Color.White.ToAndroid());

                this.PointsContainer.AddView(v, lp);
            }
        }
    }
}