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
        private HorizontalScrollView Scroller = null;

        private int RenderWidth = 0;

        private static readonly Color DefaultPointColor = Color.Gray;

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
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Flip> e) {
            base.OnElementChanged(e);

            var root = new Android.Widget.RelativeLayout(this.Context);
            root.SetBackgroundColor(Color.Yellow.ToAndroid());

            this.Scroller = new HorizontalScrollView(this.Context);
            root.AddView(this.Scroller, new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent));
            this.Scroller.SetBackgroundColor(Color.Green.ToAndroid());
            this.Scroller.HorizontalScrollBarEnabled = false;

            this.Container = new Android.Widget.LinearLayout(this.Context);
            this.Container.Orientation = Orientation.Horizontal;

            this.Scroller.AddView(this.Container);
            this.Scroller.Touch += scroller_Touch;

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
                    if (delta == 0)
                        return;

                    this.SetPointColor(this.Idx, Color.Gray);
                    Idx += delta < 0 ? -1 : 1;
                    this.SetPointColor(this.Idx, Color.White);

                    this.Snap();
                    break;
            }
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh) {
            base.OnSizeChanged(w, h, oldw, oldh);
            this.RenderWidth = w;
        }


        private void Snap() {
            this.Scroller.ScrollTo(this.RenderWidth * this.Idx, 0);
        }

        private void SetItems(int width, int height) {
            this.Container.RemoveAllViewsInLayout();
            //Form 中的大小转换到 Android 下, 要跟据 密度(Density) 转换, 最终结果可能并不是 Form 中指定的
            //var density = this.Context.Resources.DisplayMetrics.Density;
            //var w = this.Element.WidthRequest * density;
            //var h = this.Element.HeightRequest * density;

            foreach (var v in this.Element.Children) {
                var render = RendererFactory.GetRenderer(v);

                var c = new Android.Widget.FrameLayout(this.Context);
                c.SetBackgroundColor(Color.Blue.ToAndroid());
                c.AddView(render.ViewGroup);
                this.Container.AddView(c, width, height);
            }
            this.Count = this.Element.Children.Count();
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            base.OnLayout(changed, l, t, r, b);
            if (changed) {
                this.SetItems(r, b);
                this.SetPoints();
                this.SetPointColor(0, Color.White);
            }
        }

        private void SetPoints() {
            var lp = new LinearLayout.LayoutParams(10, 10);
            lp.LeftMargin = 5;
            lp.RightMargin = 5;

            var shape = new OvalShape();
            shape.Resize(10, 10);
            var dr = new ShapeDrawable(shape);
            dr.Paint.Color = DefaultPointColor.ToAndroid();

            for (var i = 0; i < this.Count; i++) {
                var v = new Android.Views.View(this.Context);
                v.SetBackgroundDrawable(dr);

                this.PointsContainer.AddView(v, lp);
            }
        }

        private void SetPointColor(int idx, Color? color = null) {
            var point = this.PointsContainer.GetChildAt(idx);
            if (point != null) {
                var shape = new OvalShape();
                var dr = new ShapeDrawable(shape);
                dr.Paint.Color = (color ?? DefaultPointColor).ToAndroid();
                point.SetBackgroundDrawable(dr);
            }
        }
    }
}