using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxolonTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new LoginPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var imagePicker = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes=FilePickerFileType.Images,
                PickerTitle="Select a Image"

            });
            if(imagePicker!=null)
            {
                var imageList = new List<ImageSource>();
                foreach(var image in imagePicker)
                {
                    var stream = await image.OpenReadAsync();
                    imageList.Add(ImageSource.FromStream(() => stream));
                }

                imageCollectionView.ItemsSource = imageList;
               
            }
        }
    }
}