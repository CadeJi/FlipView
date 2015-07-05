using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

namespace FlipView.WinPhone.Controls {

    [TemplatePart(Name = SV, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = PT, Type = typeof(ItemsControl))]
    public class FlipView : ItemsControl {

        private const string SV = "SV";
        private const string PT = "PT";

        public ObservableCollection<bool> Points {
            get;
            set;
        }

        private int Count {
            get {
                return this.Items.Count;
            }
        }

        private int currIdx = 0;
        public int CurrIdx {
            get {
                return this.currIdx;
            }
            set {
                this.currIdx = value < 0 ? 0 : (value >= this.Count ? this.Count - 1 : value);
            }
        }

        #region dependency
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FlipView), new PropertyMetadata(Orientation.Horizontal, Changed));

        public Orientation Orientation {
            get {
                return (Orientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }
        #endregion


        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {

        }

        public FlipView() {
            this.DefaultStyleKey = typeof(FlipView);
            this.Points = new ObservableCollection<bool>();
        }

        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            base.OnItemsChanged(e);
            if (this.ItemsSource != null)
                this.Points = new ObservableCollection<bool>(this.ItemsSource.Cast<object>().Select((o, i) => i == this.CurrIdx));
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            var sv = (ScrollViewer)this.GetTemplateChild(SV);
            //sv.ManipulationCompleted += sv_ManipulationCompleted;
            sv.AddHandler(ManipulationCompletedEvent, new EventHandler<ManipulationCompletedEventArgs>(sv_ManipulationCompleted), true);

            var pt = (ItemsControl)this.GetTemplateChild(PT);
            pt.ItemsSource = this.Points;
        }

        void sv_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e) {
            if (e.TotalManipulation.Translation.X == 0)
                return;

            var sc = sender as ScrollViewer;
            Debug.WriteLine("{0},{1}", sc.ExtentWidth, sc.HorizontalOffset);


            this.Points[this.CurrIdx] = false;
            this.CurrIdx += e.TotalManipulation.Translation.X > 0 ? -1 : 1;
            this.Points[this.CurrIdx] = true;

            var t = this.Width * this.CurrIdx;
            //sc.ScrollToHorizontalOffset(t);
            sc.ScrollToHorizontalOffset(this.CurrIdx);
            e.Handled = true;
        }




        #region
        protected override bool IsItemItsOwnContainerOverride(object item) {
            return false;
        }

        protected override DependencyObject GetContainerForItemOverride() {
            return new ContentControl() {
                Background = new SolidColorBrush(Colors.Purple)
            };
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            var ct = (ContentControl)element;
            var render = RendererFactory.GetRenderer((View)item);
            ct.Content = render;//render == render.ContainerElement;
        }

        //protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize) {
        //    foreach (var item in this.Items) {
        //        var v = (View)item;
        //        v.Layout(new Rectangle(0, 0, finalSize.Width, finalSize.Height));
        //    }
        //    return base.ArrangeOverride(finalSize);
        //}

        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize) {
            return base.MeasureOverride(availableSize);
        }

        #endregion
    }
}
