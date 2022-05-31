using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void createUserButton_Clicked(object sender, EventArgs e)
        {
            var name = NameField.Text;
            var surname = NameField.Text;
            var age = NameField.Text;

            Navigation.PopAsync();
        }
    }
}