using FlipView.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FlipView {
    public class App : Application {
        public App() {
            // The root page of your application
            MainPage = new ContentPage {
                Content = new StackLayout {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
						new Flip {
							WidthRequest = 450,
                            HeightRequest = 150,
                            ItemsSource = new string[]{
                                "a","b","c"
                            }
						}
					}
                }
            };
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
