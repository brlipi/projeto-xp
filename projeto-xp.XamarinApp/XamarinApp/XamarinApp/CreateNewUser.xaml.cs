using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateNewUser : ContentPage
    {
        public CreateNewUser()
        {
            InitializeComponent();
        }

        private User newUser = new User();
        private string Url = "http://10.0.2.2:8880/Users";
        private readonly HttpClient _client = new HttpClient();

        private async void createUserButton_Clicked(object sender, EventArgs e)
        {
            var name = NameField.Text;
            var surname = SurnameField.Text;
            var age = AgeField.Text;

            if (surname == null)
            {
                surname = "";
            }
            if ((name == null && age == null) || (name == "" && age == ""))
            {
                await DisplayAlert("Error", "Please insert a Name and an Age", "Ok");
            }
            else if (name == null || name == "")
            {
                await DisplayAlert("Error", "Please insert a Name", "Ok");
            }
            else if (age == null || age == "")
            {
                await DisplayAlert("Error", "Please insert an Age", "Ok");
            }
            else
            {
                newUser.Name = name;
                newUser.Surname = surname;
                newUser.Age = Convert.ToUInt16(age);

                // Serializa ou converte o Post criado em uma string JSON
                string content = JsonConvert.SerializeObject(newUser);
                // Envia uma requisição POST para a Uri especificada em uma operação assíncrona
                // e com a codificação correta(utf8) e com o content type(application/json).
                var response = await _client.PostAsync(Url, new StringContent(content, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PopAsync();
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    await DisplayAlert("Error", "The user could not be registered. Try again or go back to the previous page", "Ok");
                }
            }
        }
    }
}