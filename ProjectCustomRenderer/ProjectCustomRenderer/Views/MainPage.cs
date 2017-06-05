using ProjectCustomRenderer.CustomViews;
using Xamarin.Forms;

namespace ProjectCustomRenderer.Views
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Content = new StackLayout
            {
                Children = {
                    //new Label {
                    //    Text = "Hello, Custom Renderer !",
                    //},
                    //new MyEntry {
                    //    Text = "In Shared Code",
                    //},
                    new DateTimePicker {
                        TextColor = Color.Black
                    },

                },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }
    }
}