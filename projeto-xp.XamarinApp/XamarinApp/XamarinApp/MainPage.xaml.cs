﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        //Esta é a url publica que oferece o serviço
        //private const string Url = "http://jsonplaceholder.typicode.com/posts";
        private string Url = "http://10.0.2.2:8880/Users";
        //Cria uma instância de HttpClient (Microsoft.Net.Http)
        private readonly HttpClient _client = new HttpClient();
        //Definimos uma coleção do tipo ObservableCollection que é coleção de dados dinâmica
        //e que permite atualizar o estado da UI em temporeal quando os dados forem alterados
        private ObservableCollection<User> _users;

        private int position;

        protected override async void OnAppearing()
        {
            // Envia uma requisição GET para a url especificada e retorna
            // o Response (Body) como uma string em uma operação assíncrona
            string content = await _client.GetStringAsync(Url);
            // Deserializa ou converte a string JSON em uma coleção de Post
            List<User> users = JsonConvert.DeserializeObject<List<User>>(content);
            // Convertendo a Lista para uma ObservalbleCollection de Post
            _users = new ObservableCollection<User>(users);
            //Atribui a ObservableCollection ao Listview MainPage 
            MainPage.ItemsSource = _users;
            base.OnAppearing();
        }

        /// <summary>
        /// Evento Click do botão Incluir. 
        /// Envia uma requisição POST incluindo um objeto Post no servidor e na coleção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnAdd(object sender, EventArgs e)
        {
            // Cria uma instância de Post atribuindo um TimeStamp à propriedade Title
            User post = new User { Title = $"Title: Timestamp {DateTime.UtcNow.Ticks}" };
            // Serializa ou converte o Post criado em uma string JSON
            string content = JsonConvert.SerializeObject(post);
            // Envia uma requisição POST para a Uri especificada em uma operação assíncrona
            // e com a codificação correta(utf8) e com o content type(application/json).
            await _client.PostAsync(Url, new StringContent(content, Encoding.UTF8, "application/json"));
            // Atualiza a UI inserindo um elemento na coleção
            _users.Add(post);
        }

        /// <summary>
        /// Evento Click do botão Atualizar
        /// Envia uma requisição PUT para atualizar o primieiro objeto Post
        /// no servidor e na coleção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnUpdate(object sender, EventArgs e)
        {
            // Atribui o segundo objeto Post da Coleção para uma nova instância de Post
            User post = _users[position];
            // Anexa a string [atualizado] ao valor atual da propriedade Title
            post.Title += " [atualizado]";
            // Serializa ou converte o post criado em uma string JSON
            string content = JsonConvert.SerializeObject(post);
            // Envia uma requisição PUT para a uri definida como uma operação assincrona
            await _client.PutAsync(Url + "/" + post.Id, new StringContent(content, Encoding.UTF8, "application/json"));
        }

        /// <summary>
        /// Evento Click do botão Deletar
        /// Envia uma requisição DELETE removendo o primeiro objeto post
        /// no servidor e na coleção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDelete(object sender, EventArgs e)
        {
            // Atribui o primeiro objeto Post da Coleção para uma nova instância de Post
            User post = _users[position];
            // Envia uma requisição DELETE para a Uri de forma assíncrona
            await _client.DeleteAsync(Url + "/" + post.Id);
            // Remove a primeira ocorrencia do objeto especificado na coleção Post
            _users.Remove(post);
        }

        private void MainPage_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            position = e.SelectedItemIndex;
            //DisplayAlert("")
        }
    }
}