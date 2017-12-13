using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BonfireMobileApp.Views
{
    class CardView : ContentView
    {
        public CardView(Entities.Content content)
        {
            Grid outerGrid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                BackgroundColor = Color.Black,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Absolute) },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Absolute) }
                }
            };

            Grid grid = new Grid
            {
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.FromHex("#292929"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (100, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };

            if (content.Url != null)
            {
                Image img = new Image
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    Aspect = Aspect.AspectFill
                };
                img.Source = ImageSource.FromUri(new Uri(content.Url));
                grid.Children.Add(img, 0, 0);
            }
            else
            {
                grid.RowDefinitions[0].Height = 1;
            }

            Grid innerGrid = new Grid
            {
                Padding = new Thickness(5, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = GridLength.Star }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };

            Label labelTitle = new Label
            {
                Text = content.Title,
                TextColor = Color.White
            };

            innerGrid.Children.Add(labelTitle, 0, 0);

            grid.Children.Add(innerGrid, 0, 1);

            outerGrid.Children.Add(grid, 1, 1);

            Content = outerGrid;
        }
    }
}
