using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinApp
{
    public class User : INotifyPropertyChanged
    {
        //Método OnPropertyChanged()
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonProperty("id")]
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set
            {
                _surname = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        [JsonProperty("surname")]
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        [JsonProperty("age")]
        public ushort Age
        {
            get => _age;
            set
            {
                _age = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        [JsonProperty("creationDate")]
        public DateTime CreationDate
        {
            get => _creationDate;
            set
            {
                _creationDate = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        private string _id;
        private string _name;
        private string _surname;
        private ushort _age;
        private DateTime _creationDate;
    }
}
