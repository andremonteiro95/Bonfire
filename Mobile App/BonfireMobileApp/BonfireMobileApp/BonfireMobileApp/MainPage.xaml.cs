﻿using BonfireMobileApp.Entities;
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

            // TODO: Remover, é teste
            for (int i=0; i<4; i++) {
                stackLayout.Children.Add(new CardView(c));
                stackLayout.Children.Add(new CardView(new Entities.Content { Title = "Hello" }));
            }

            scrollView.Content = stackLayout;

            mainLayout.Children.Add(scrollView);
        }
    }
}
