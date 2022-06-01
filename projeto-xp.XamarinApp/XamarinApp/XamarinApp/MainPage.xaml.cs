using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private string Url = "http://10.0.2.2:8880/Users";
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<User> _users;
        private int? position = null;

        protected override async void OnAppearing()
        {
            string content = await _client.GetStringAsync(Url);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
            _users = new ObservableCollection<User>(users);
 
            MainPage_ListView.ItemsSource = _users;
            base.OnAppearing();
        }

        private async void OnAdd(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNewUser());
        }

        private async void OnUpdate(object sender, EventArgs e)
        {
            if (position != null)
            {
                int updatePosition = (int)position;
                var updateUserPage = new UpdateUser();
                MessagingCenter.Send(updateUserPage, "UpdateUser", _users[updatePosition]);
                await Navigation.PushAsync(updateUserPage);
            }
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            if (position != null)
            {
                int deletePosition = (int)position;
                User post = _users[deletePosition];

                var response = await _client.DeleteAsync(Url + "/" + post.Id);

                if (response.IsSuccessStatusCode)
                {
                    _users.Remove(post);
                }
            }
        }

        private void MainPage_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            position = e.SelectedItemIndex;
        }
    }
}
