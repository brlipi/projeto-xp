# projeto-xp

## Requisitos:
- Visual Studio (Testado na versão 2022 Community Edition)
- .NET 6.0
- SQL Server 2019
- Docker
- Docker-compose
- Android Emulator on Visual Studio

## Inicialização do projeto em máquina local
**OBS:** Nesse modo, o projeto só contará com a API. Para utilizar também o Website, considere a inicialização do projeto via docker-compose.

- Via Visual Studio:
  1. Abra o arquivo "projeto-xp.Api.sln".
  2. Clique em "▶ MVCApp ▾" para executar a aplicação.
- Via linha de comando:
  1. Vá para o diretório ```projeto-xp/projeto-xp.Api/projeto-xp.Api/```
  2. Digite ```dotnet run```.
- O banco de dados pode ser acessado pelo SSMS pelo nome "localhost\SQLEXPRESS".

## Inicialização do projeto em contêineres (docker-compose)
  1. Vá para o diretório ```projeto-xp/projeto-xp.Api/projeto-xp.Api/```.
  2. Digite ```docker-compose up --build```.
  3. Para acessar os contêineres, use o comando ```docker exec -it bash <container_name>``` (container_name para cada um dos contêineres pode ser encontrado no arquivo docker-compose.yml).

## Testes unitários
- Via Visual Studio:
  1. Abra o arquivo "projeto-xp.Api.sln".
  2. No topo da janela, clique em "Teste" -> "Executar todos os testes".
  3. Forma alternativa: Na janela do Visual Studio, pressione ```Alt+S```, depois pressione ```Enter```.
- Via linha de comando:
  1. Vá para o diretório ```projeto-xp/projeto-xp.Tests/projeto-xp.Tests```.
  2. Digite ```dotnet test```.

## Inicialização do projeto Xamarin.Android
1. Abra o arquivo "projeto-xp.Api.sln".
2. Talvez seja necessário mudar o projeto de inicialização, para isso:
    1. No canto direito, em "Gerenciador de Soluções", procure por "XamarinApp.Android e clique com o botão direito no item.
    2. Clique em "Definir como Projeto de Inicialização".
3. No topo da janela do Visual Studio deve aparecer um botão o nome do emulador Android escolhido, ex: "▶ Pixel 5 - API 30 (Android 11.0 - API 30) ▾".
4. Clique no botão para iniciar a execução da aplicação pelo emulador Android.
5. Para execução da aplicação em um Smartphone, siga as instruções da seguinte documentação: <https://docs.microsoft.com/en-us/xamarin/android/get-started/installation/set-up-device-for-development>.