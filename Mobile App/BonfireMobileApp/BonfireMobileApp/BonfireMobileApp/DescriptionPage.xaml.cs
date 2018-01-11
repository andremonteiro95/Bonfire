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

            VideoPlayer vp = new VideoPlayer("https://www.youtube.com/watch?v=3ZCnhBMdm_Q");
            mainLayout.Children.Add(vp);
        }
    }
}