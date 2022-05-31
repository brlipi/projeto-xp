using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XamarinApp
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _title;
        //Mapeia o elemento title do serviço rest para o modelo
        [JsonProperty("title")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                //Notifica a sua View ou ViewModel que o valor que a propriedade
                //no modelo mudou e a view precisa ser atualizada
                OnPropertyChanged();
            }
        }

        //Método OnPropertyChanged()
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
