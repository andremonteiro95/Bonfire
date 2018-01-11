using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BonfireMobileApp.Views
{
    class TopBar : ContentView
    {
        Grid grid;
        Image icon_bt;

        public TopBar(bool backButton) {
            grid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.FromHex("#292929"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                        new RowDefinition { Height = new GridLength(35, GridUnitType.Absolute) },
                    },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = GridLength.Star},
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Absolute) }
                }
            };

            Image img = new Image
            {
                Source = ImageSource.FromFile("logo2.png"),
                Scale = 0.7,
                HorizontalOptions = LayoutOptions.Center
            };

            grid.Children.Add(img, 1, 0);

            if (backButton)
            {
                Image icon_back = new Image
                {
                    Source = ImageSource.FromFile("ic_arrow_back.png"),
                    Scale = 1,
                    HorizontalOptions = LayoutOptions.Start
                };
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (s, e) => {
                    Navigation.PopAsync();
                };
                icon_back.GestureRecognizers.Add(tap);
                grid.Children.Add(icon_back, 0, 0);
            }
            else
            {
                icon_bt = new Image
                {
                    Source = ImageSource.FromFile("ic_bluetooth.png"),
                    Scale = 0.8,
                    HorizontalOptions = LayoutOptions.End
                };

                grid.Children.Add(icon_bt, 2, 0);
            }

            Content = grid;
        }

        public void EnableTestButton(EventHandler eventHandler)
        {
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += eventHandler;
            icon_bt.GestureRecognizers.Add(tap);
        }
    }
}
