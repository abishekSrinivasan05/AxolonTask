using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AxolonTask.Table;

namespace AxolonTask.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<RegUserTable>();

            var item = new RegUserTable()
            {
                UserName = usernameRegEntry.Text,
                Password = passwordRegEntry.Text,
                Email = emailIdRegEntry.Text,
                PhoneNumber = phoneNumberRegEntry.Text

            };
            if (usernameRegEntry.Text == ""|| usernameRegEntry.Text == null || passwordRegEntry.Text == ""|| passwordRegEntry.Text == null || passwordRegEntry.Text == "" || passwordRegEntry.Text ==null|| phoneNumberRegEntry.Text == ""|| phoneNumberRegEntry.Text == null)
            {
                var emptyAlert = DisplayAlert("Error", "Enter all the details", "Ok");
            }
            else
            {
                var loginAuth = db.Table<RegUserTable>().Where(u => u.UserName.Equals(usernameRegEntry.Text) && u.Password.Equals(passwordRegEntry.Text)).FirstOrDefault();
                if (loginAuth == null)
                {
                    db.Insert(item);
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        var alert = DisplayAlert("Alert", "User has been Registered Successfully", "Ok");
                       
                            await Navigation.PushAsync(new LoginPage());
                       
                       
                    });
                }
                else
                {
                    var regAlert = DisplayAlert("Error", "User Already Exists", "Ok");
                }
            }
            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}