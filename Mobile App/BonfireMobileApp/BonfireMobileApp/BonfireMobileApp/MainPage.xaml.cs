using Acr.UserDialogs;
using BonfireMobileApp.Entities;
using BonfireMobileApp.Services;
using BonfireMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BonfireMobileApp
{
    public partial class MainPage : ContentPage
    {
        StackLayout stackLayout;
        TopBar topBar;
        List<Content> listContents = new List<Content>();
        string uuid = null;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            topBar = new TopBar(false);
            mainLayout.Children.Add(topBar);
            topBar.EnableTestButton((s, e) =>
            {
                PromptConfig pc = new PromptConfig
                {
                    Text = "c032b026-6c91-4cde-9c4c-0853d462fab8",
                    OnAction = result =>
                    {
                        uuid = result.Value;
                    }
                };
                UserDialogs.Instance.Prompt(pc);
            });

            // Init Thread looking for UUID
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    UUIDTask();
                }
            }, TaskCreationOptions.LongRunning);

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            stackLayout = new StackLayout
            {
                Padding = 10,
                BackgroundColor = Color.FromHex("#182221")
            };

            Label labelSearching = new Label
            {
                Text = "Searching for nearby beacons...",
                TextColor = Color.GhostWhite,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 15)
            };

            Image img = new Image
            {
                Source = ImageSource.FromFile("ic_bonfire.png"),
                HorizontalOptions = LayoutOptions.Center
            };

            stackLayout.Children.Add(labelSearching);
            stackLayout.Children.Add(img);

            scrollView.Content = stackLayout;

            mainLayout.Children.Add(scrollView);
        }

        private void Bt_Clicked(object sender, EventArgs e)
        {
            PromptConfig pc = new PromptConfig {
                Text = "c032b026-6c91-4cde-9c4c-0853d462fab8",
                OnAction = result =>
                {
                    uuid = result.Value;
                }
            };
            UserDialogs.Instance.Prompt(pc);
        }

        private void UUIDTask()
        {
            if (uuid != null)
            {
                BonfireService bs = new BonfireService();
                Task.Run(async () => { listContents = await bs.GetContentsByBeacon(uuid); }).Wait();

                uuid = null;

                Device.BeginInvokeOnMainThread( () =>
                {
                    stackLayout.Children.Clear();

                    foreach (Content c in listContents)
                    {
                        CardView card = new CardView(c);

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += (s, e) => {
                            DescriptionPage newPage = new DescriptionPage(c);
                            Navigation.PushAsync(newPage);
                        };
                        card.GestureRecognizers.Add(tapGestureRecognizer);

                        stackLayout.Children.Add(card);
                    }
                });
            }
        }
    }
}
