using BonfireMobileApp.Entities;
using BonfireMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BonfireMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {

        private Content actualContent;

        public DescriptionPage()
        {
            InitializeComponent();

            Navigation.PopAsync();
        }

        public DescriptionPage(Content c)
        {
            InitializeComponent();

            this.actualContent = c;

            NavigationPage.SetHasNavigationBar(this, false);

            mainLayout.Children.Add(new TopBar(true));

            Grid grid = new Grid
            {
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.FromHex("#292929"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (2, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength (2, GridUnitType.Star) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };

            switch (CheckIfValidUrl(actualContent.Url))
            {
                case 1:
                    Image img = new Image
                    {
                        HorizontalOptions = LayoutOptions.Fill,
                        Aspect = Aspect.AspectFill
                    };
                    img.Source = ImageSource.FromUri(new Uri(actualContent.Url));
                    grid.Children.Add(img, 0, 0);
                    break;
                case 2:
                    VideoPlayer vp = new VideoPlayer(actualContent.Url);
                    grid.Children.Add(vp, 0, 0);
                    break;
                default:
                    grid.RowDefinitions[0].Height = 1;
                    break;
            }

            StackLayout stackStrings = new StackLayout();

            Label labelTitle = new Label {
                Text = actualContent.Title,
                TextColor = Color.GhostWhite,
                Margin = new Thickness(5, 1)
            };
            stackStrings.Children.Add(labelTitle);

            string fullDescription =
                actualContent.Description +
                "\nPromotion Ends " + actualContent.EndDate;

            Label labelDescription = new Label {
                Text = fullDescription,
                TextColor = Color.GhostWhite,
                Margin = new Thickness(5, 1),
                FontSize = 12
            };
            stackStrings.Children.Add(labelDescription);

            grid.Children.Add(stackStrings, 0, 1);

            mainLayout.Children.Add(grid);
        }

        public int CheckIfValidUrl(string url)
        {
            if (url == null)
                return 0;

            if (url.EndsWith(".jpg") || url.EndsWith(".jpeg") || url.EndsWith(".png") || url.EndsWith(".gif"))
                return 1;

            if ((url.StartsWith("https://youtube.com/") || url.StartsWith("https://www.youtube.com/"))
                && url.Contains("watch?v="))
                return 2;

            return 0;
        }
    }
}