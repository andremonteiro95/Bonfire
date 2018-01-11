using BonfireMobileApp.Entities;
using BonfireMobileApp.Services;
using BonfireMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BonfireMobileApp
{
    public partial class MainPage : ContentPage
    {
        List<Content> list = new List<Content>();

        public MainPage()
        {
            InitializeComponent();

            mainLayout.Children.Add(new TopBar(false));

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout stackLayout = new StackLayout
            {
                Padding = 10,
                BackgroundColor = Color.FromHex("#182221")
            };

            Content c = new Content
            {
                Title = "TEST TEST TEST",
                Url = "https://static.pexels.com/photos/207962/pexels-photo-207962.jpeg"
            };

            Button bt = new Button
            {
                Text = "Blergh"
            };
            bt.Clicked += Bt_Clicked;

            BonfireService bs = new BonfireService();
            List<Content> x;
            Task.Run(async () => { x = await bs.GetContentsByBeacon("c032b026-6c91-4cde-9c4c-0853d462fab8"); }).GetAwaiter().GetResult();

            // TODO: Remover, é teste
            for (int i=0; i<4; i++) {
                stackLayout.Children.Add(new CardView(c));
                stackLayout.Children.Add(new CardView(new Entities.Content { Title = "Hello" }));
            }

            stackLayout.Children.Add(bt);

            scrollView.Content = stackLayout;

            mainLayout.Children.Add(scrollView);


        }

        private void Bt_Clicked(object sender, EventArgs e)
        {
            Content c = new Content
            {
                Title = "TEST TEST TEST",
                Url = "https://static.pexels.com/photos/207962/pexels-photo-207962.jpeg"
            };

            DescriptionPage newPage = new DescriptionPage(c);
            Navigation.PushAsync(newPage);

        }
    }
}
