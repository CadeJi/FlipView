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
                {"http://photocdn.sohu.com/20130528/Img377307672.jpg","小泽玛利亚"},
                {"http://news.youth.cn/yl/201406/W020140619035008418634.jpg","吉泽明步"},
                {"http://imgsrc.baidu.com/baike/pic/item/6609c93d70cf3bc7e43db93dd500baa1cd112a25.jpg","苍井空"}
            };



            this.BindingContext = this;
        }
    }
}
