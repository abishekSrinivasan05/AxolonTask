using AxolonTask.Table;
using AxolonTask.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AxolonTask
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            /* if(usernameEntry.Text=="Abishek"&& passwordEntry.Text=="Axolon@123")
             {
                 Navigation.PushAsync(new HomePage());
                 usernameEntry.Text = "";
                 passwordEntry.Text = "";
             }
             else
             {
                DisplayAlert("Error","User Doesn't Exist","Ok");
             }*/
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbPath);
            var loginAuth = db.Table<RegUserTable>().Where(u => u.UserName.Equals(usernameEntry.Text) && u.Password.Equals(passwordEntry.Text)).FirstOrDefault();
            if(loginAuth!=null)
            {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var alert = await this.DisplayAlert("Error", "Username or Password is incorrect", "Ok", "Cancel");
              if(alert==true)
                    {
                       await Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        var regAlert= await this.DisplayAlert("Alert", "Do you want to register", "Yes", "No");
                    if(regAlert==true)
                        {
                           await Navigation.PushAsync(new RegistrationPage());

                        }

                    else
                        {
                            await Navigation.PushAsync(new LoginPage());
                        }
                    }
                });
                

            }

        }

        private void RegisterLabel_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }
    }
}