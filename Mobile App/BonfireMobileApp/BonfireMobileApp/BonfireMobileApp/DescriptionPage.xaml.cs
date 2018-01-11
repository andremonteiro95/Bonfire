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

            var x = this.BindingContext;

            VideoPlayer vp = new VideoPlayer("https://www.youtube.com/watch?v=3ZCnhBMdm_Q");
            base.Content = vp;

            StackLayout stackLayout = new StackLayout
            {
                Padding = 0,
                BackgroundColor = Color.FromHex("#182221")
            };

            Label title = new Label
            {
                Text = "Title",
                FontSize = 14,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = Xamarin.Forms.Color.DarkRed,
            };


            stackLayout.Children.Add(title);
            Content = stackLayout;
        }




        public DescriptionPage(Content c)
        {
            InitializeComponent();


            this.actualContent = c;


            if (actualContent.Url.Contains("youtube"))
            {
                //Falta testar
                VideoPlayer vp = new VideoPlayer(actualContent.Url);
                Content = vp;
            }

            else
            {
                StackLayout stackLayout = new StackLayout
                {
                    Padding = 0,
                    BackgroundColor = Color.FromHex("#182221")
                };


                Label title = new Label
                {
                    Text = actualContent.Title,
                    FontSize = 30,
                    FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                    TextColor = Xamarin.Forms.Color.DarkRed,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Start
                };

                Label description = new Label
                {
                    Text = actualContent.Description,
                    FontSize = 22,
                    TextColor = Xamarin.Forms.Color.GhostWhite,
                };

                Label dates = new Label
                {
                    Text = "[" + actualContent.StartDate + " até " + actualContent.EndDate + "]",
                    FontSize = 26,
                    TextColor = Xamarin.Forms.Color.GhostWhite,
                };

                Image image = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Source = actualContent.Url,
                };

                stackLayout.Children.Add(title);
                stackLayout.Children.Add(dates);
                stackLayout.Children.Add(image);
                stackLayout.Children.Add(description);


                Content = stackLayout;

            }
        }
    }
}