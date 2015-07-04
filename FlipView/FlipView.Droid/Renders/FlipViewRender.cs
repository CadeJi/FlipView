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
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

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
            root.AddView(scroller, new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent));
            scroller.SetBackgroundColor(Color.Green.ToAndroid());

            this.Container = new Android.Widget.LinearLayout(this.Context);
            this.Container.Orientation = Orientation.Horizontal;

            scroller.AddView(this.Container);
            scroller.Touch += scroller_Touch;

            this.PointsContainer = new LinearLayout(this.Context);
            this.PointsContainer.Orientation = Orientation.Horizontal;
            //this.PointsContainer. = Android.Views.TextAlignment.Center;

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

        //Î´Ö´ÐÐ
        //public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint) {
        //    this.Measure(widthConstraint, heightConstraint);
        //    var w = this.MeasuredWidth;
        //    return base.GetDesiredSize(widthConstraint, heightConstraint);
        //}


        private void Snap() {
            Console.WriteLine(this.Idx);
        }

        private void SetItems() {
            this.Container.RemoveAllViewsInLayout();
            var density = this.Context.Resources.DisplayMetrics.Density;
            var w = this.Element.WidthRequest * density;
            var h = this.Element.HeightRequest * density;

            foreach (var v in this.Element.Children) {
                var render = RendererFactory.GetRenderer(v);

                var c = new Android.Widget.FrameLayout(this.Context);
                c.SetBackgroundColor(Color.Blue.ToAndroid());
                c.AddView(render.ViewGroup);
                this.Container.AddView(c, (int)w, (int)h);
            }
            this.Count = this.Element.Children.Count();
        }

        private void SetPoints() {
            var lp = new LinearLayout.LayoutParams(10, 10);
            lp.LeftMargin = 5;
            lp.RightMargin = 5;

            var shape = new OvalShape();
            shape.Resize(10, 10);
            var dr = new ShapeDrawable(shape);
            dr.Paint.Color = Color.White.ToAndroid();

            for (var i = 0; i < this.Count; i++) {
                var v = new Android.Views.View(this.Context);
                v.SetBackgroundDrawable(dr);

                this.PointsContainer.AddView(v, lp);
            }
        }
    }
}