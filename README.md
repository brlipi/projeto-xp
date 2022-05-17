# projeto-xp

## Inicialização do projeto em máquina local
- Via Visual Studio:
  1. Abra o arquivo "projeto-xp.Api.sln"
  2. Clique em "▶ MVCApp ▾" para executar a aplicação.
- Via linha de comando:
  1. Vá para o diretório ```projeto-xp/projeto-xp.Api/projeto-xp.Api/```
  2. Digite ```dotnet run```
- O banco de dados pode ser acessado pelo SSMS pelo nome "localhost\SQLEXPRESS"

## Inicialização do projeto em contêineres (docker-compose)
  1. Vá para o diretório ```projeto-xp/projeto-xp.Api/projeto-xp.Api/```
  2. Digite ```docker-compose up --build```
  3. Para acessar os contêineres, use o comando ```docker exec -it bash <container_name>``` (container_name pode ser encontrado no arquivo docker-compose.yml)

## Testes unitários
- Via Visual Studio:
  1. Abra o arquivo "projeto-xp.Api.sln"
  2. No topo da janela, clique em "Teste" -> "Executar todos os testes"
  3. Forma alternativa: Na janela do Visual Studio, pressione ```Alt+S```, depois pressione ```Enter```
- Via linha de comando:
  1. Vá para o diretório ```projeto-xp/projeto-xp.Tests/projeto-xp.Tests```
  2. Digite ```dotnet test```
