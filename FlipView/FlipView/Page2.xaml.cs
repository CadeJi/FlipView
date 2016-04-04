using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlipView {
    public partial class Page2 : ContentPage {
        public Page2() {
            //InitializeComponent();
            this.LoadFromXaml(typeof(Page2));

            var btn = this.FindByName<Button>("btn");
            btn.Clicked += btn_Clicked;
        }

        void btn_Clicked(object sender, EventArgs e) {
            App.Current.MainPage.Navigation.PushAsync(new Page1());
        }
    }
}
