using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections;
using System.ComponentModel;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using System.Linq.Expressions;
using System.Collections.ObjectModel;


namespace FlipView.WinPhone.Controls {
    public partial class FlipView : UserControl, INotifyPropertyChanged {

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FlipView), new PropertyMetadata(Orientation.Horizontal, Changed));
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(FlipView), new PropertyMetadata(null, Changed));
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(FlipView), new PropertyMetadata(null, Changed));

        private int currIdx = 0;
        public int CurrIdx {
            get {
                return this.currIdx;
            }
            set {
                this.currIdx = value < 0 ? 0 : (value >= this.Count ? this.Count - 1 : value);
                this.NotifyOfPropertyChange("CurrIdx");
            }
        }

        public Orientation Orientation {
            get {
                return (Orientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public DataTemplate ItemTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemTemplateProperty);
            }
            set {
                this.SetValue(ItemTemplateProperty, value);
            }
        }

        private int Count {
            get;
            set;
        }

        public ObservableCollection<bool> Points {
            get;
            private set;
        }

        public ScrollBarVisibility HScrollVisibility {
            get;
            set;
        }

        public ScrollBarVisibility VScrollVisibility {
            get;
            set;
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var fv = d as FlipView;
            Debug.WriteLine("======>{0}", e.Property.ToString());
            if (e.Property.Equals(ItemsSourceProperty)) {
                fv.Count = fv.ItemsSource.Cast<object>().Count();
                fv.Points = new ObservableCollection<bool>(Enumerable.Range(0, fv.Count).Select(i => i == fv.CurrIdx));
                fv.NotifyOfPropertyChange("Points");
            } else if (e.Property.Equals(OrientationProperty)) {
                fv.UpdateOrientation((Orientation)e.NewValue);
            }
        }

        public void UpdateOrientation(Orientation o) {
            if (o == System.Windows.Controls.Orientation.Horizontal) {
                this.HScrollVisibility = ScrollBarVisibility.Hidden;
                this.VScrollVisibility = ScrollBarVisibility.Disabled;
            } else {
                this.HScrollVisibility = ScrollBarVisibility.Disabled;
                this.VScrollVisibility = ScrollBarVisibility.Hidden;
            }
        }


        public FlipView() {
            InitializeComponent();
            this.UpdateOrientation(this.Orientation);

            this.sv.AddHandler(ManipulationCompletedEvent, new EventHandler<ManipulationCompletedEventArgs>(sv_ManipulationCompleted), true);
            this.Points = new ObservableCollection<bool>();
            this.DataContext = this;
        }

        void sv_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e) {
            if (e.TotalManipulation.Translation.X == 0)
                return;

            var sc = sender as ScrollViewer;

            this.Points[this.CurrIdx] = false;
            this.CurrIdx += e.TotalManipulation.Translation.X > 0 ? -1 : 1;
            this.Points[this.CurrIdx] = true;

            var t = this.Width * this.CurrIdx;
            Debug.WriteLine(t);
            sc.ScrollToHorizontalOffset(t);
            e.Handled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        public virtual void NotifyOfPropertyChange(string propertyName) {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
