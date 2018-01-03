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
        public DescriptionPage()
        {
            InitializeComponent();

            VideoPlayer vp = new VideoPlayer("https://www.youtube.com/watch?v=3ZCnhBMdm_Q");
            Content = vp;
        }
    }
}