using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;

namespace FlipView.WinPhone {
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage {

        public ObservableCollection<string> Imgs {
            get;
            set;
        }

        public MainPage() {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new FlipView.App());


            //var fv = new Controls.FlipView() {
            //    Width = 450,
            //    Height = 100,
            //    ItemsSource = new object[]{
            //        new Button(){Content="TTT"},
            //        new TextBlock(){Text="AAA"}
            //    }
            //};

            //this.Content = fv;

            //this.Imgs = new ObservableCollection<string>(){
            //    "http://f.hiphotos.baidu.com/image/w%3D400/sign=cf3db92c0b24ab18e016e03705fbe69a/f703738da97739122e40d96bfa198618367ae252.jpg",
            //    "http://e.hiphotos.baidu.com/image/w%3D400/sign=8b347e407c3e6709be0044ff0bc69fb8/e7cd7b899e510fb32396f5f0da33c895d0430ccd.jpg",
            //    "http://e.hiphotos.baidu.com/image/w%3D400/sign=9915223123a446237ecaa462a8237246/11385343fbf2b2116724dd05c98065380cd78e77.jpg"
            //};

            //var fv = (FlipView.WinPhone.Controls.FlipView)this.FindName("fv");
            //fv.Items.Add(new Button() {
            //    Content = "a"
            //});
            //fv.Items.Add(new TextBlock() {
            //    Text = "b"
            //});

            //this.DataContext = this;
        }
    }
}
