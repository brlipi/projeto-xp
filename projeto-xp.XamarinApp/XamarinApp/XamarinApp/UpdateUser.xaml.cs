﻿using Newtonsoft.Json;
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
                //DisplayAlert("MessagingCenter", "Inside Subscribe's action", "Ok");
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

            if (name == null)
            {
                name = "";
            }
            if (surname == null)
            {
                surname = "";
            }
            if (age == null)
            {
                age = "";
            }

            updateUser.Name = name;
            updateUser.Surname = surname;
            updateUser.Age = Convert.ToUInt16(age);

            // Serializa ou converte o Post criado em uma string JSON
            string content = JsonConvert.SerializeObject(updateUser);
            // Envia uma requisição POST para a Uri especificada em uma operação assíncrona
            // e com a codificação correta(utf8) e com o content type(application/json).
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