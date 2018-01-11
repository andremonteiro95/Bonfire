using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BonfireMobileApp.Views
{
    public class VideoPlayer : ContentView
    {
        public VideoPlayer(string url)
        {
            WebView webView = new WebView();
            HtmlWebViewSource htmlWebViewSource = new HtmlWebViewSource();

            url = url.Replace("watch?v=", "embed/");
            string iframeURL = string.Format("<iframe width=\"100%\" src=\"{0}\" frameborder=\"0\" gesture=\"media\" allow=\"encrypted-media\" allowfullscreen></iframe>", url);
                         string finalUrl = string.Format("<html><body style=\"background-color:transparent;\">{0}</body></html>", iframeURL);

            htmlWebViewSource.Html = finalUrl;
            webView.Source = htmlWebViewSource;
            webView.HeightRequest = 300;
            webView.HorizontalOptions = LayoutOptions.FillAndExpand;

            Content = webView;
           
        }
    }
}