using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FlipView {
    public partial class Page1 : ContentPage {

        public Dictionary<string, string> Imgs {
            get;
            set;
        }

        public Page1() {
            InitializeComponent();

            this.Imgs = new Dictionary<string, string>(){
                {"http://f.hiphotos.baidu.com/image/w%3D400/sign=cf3db92c0b24ab18e016e03705fbe69a/f703738da97739122e40d96bfa198618367ae252.jpg","彩虹@山"},
                {"http://e.hiphotos.baidu.com/image/w%3D400/sign=8b347e407c3e6709be0044ff0bc69fb8/e7cd7b899e510fb32396f5f0da33c895d0430ccd.jpg","礼物"},
                {"http://e.hiphotos.baidu.com/image/w%3D400/sign=9915223123a446237ecaa462a8237246/11385343fbf2b2116724dd05c98065380cd78e77.jpg","小鸟"}
            };



            this.BindingContext = this;
        }
    }
}
