using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUser : ContentPage
    {
        private User updateUser;
        private string Url = "http://10.0.2.2:8880/Users";
        private readonly HttpClient _client = new HttpClient();

        public UpdateUser()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<UpdateUser, User>(this, "UpdateUser", (pageSender, userItem) =>
            {
                updateUser = userItem;
            });
        }

        protected override void OnAppearing()
        {
            NameField.Text = updateUser.Name;
            SurnameField.Text = updateUser.Surname;
            AgeField.Text = updateUser.Age.ToString();
            base.OnAppearing();
        }

        private async void updateUserButton_Clicked(object sender, EventArgs e)
        {
            var name = NameField.Text;
            var surname = SurnameField.Text;
            var age = AgeField.Text;

            Url += "/" + updateUser.Id;

            if (!string.IsNullOrEmpty(name))
            {
                updateUser.Name = name;
            }
            if (!string.IsNullOrEmpty(age))
            {
                updateUser.Age = Convert.ToUInt16(age);
            }
            if (surname == null)
            {
                surname = "";
            }
            updateUser.Surname = surname;

            string content = JsonConvert.SerializeObject(updateUser);
            var response = await _client.PutAsync(Url, new StringContent(content, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                await Navigation.PopAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await DisplayAlert("Error", "The user could not be updated. Try again or go back to the previous page", "Ok");
            }
        }
    }
}