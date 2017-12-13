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
        public TopBar(bool backButton) {
            Grid grid = new Grid
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
                // TODO: Renderizar um botão para voltar atrás
            }

            Content = grid;
        }
    }
}
