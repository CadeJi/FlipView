using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FlipView.Controls {
    public class Flip : View {

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create<Flip, IEnumerable>(p => p.ItemsSource, null);
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create<Flip, ScrollOrientation>(p => p.Orientation, ScrollOrientation.Horizontal);

        public IEnumerable ItemsSource {
            get {
                return (IEnumerable)this.GetValue(ItemsSourceProperty);
            }
            set {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        public ScrollOrientation Orientation {
            get {
                return (ScrollOrientation)this.GetValue(OrientationProperty);
            }
            set {
                this.SetValue(OrientationProperty, value);
            }
        }

        public Flip() {
            this.BindingContext = this;
        }
    }
}
