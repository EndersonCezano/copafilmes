# Campeonato de filmes

- [Projetos](#projetos)
- [Rodando o projeto](#rodando-o-projeto)
- [Sobre o projeto API especificamente](#sobre-o-projeto-api-especificamente)
- [Sobre o projeto WEB especificamente](#sobre-o-projeto-web-especificamente)
- [Testes](#testes)
  - [Testes do server](#api)
  - [Testes do app](#web)


## Projetos
* API (Backend)
* API.Test (Testes do backend)
* WEB (Frontend/Angular - App e testes)

Toda a construção dos testes foi em cima do Visual Studio Community 2017, com instalações dos pacotes `Desenvolvimento para desktop com .NET`, `ASP.NET e desenvolvimento Web` e `Desenvolvimento em node.js`.

Apesar de poder criar um único projeto que contenha app/server juntos, resolvi criar uma API à parte, pois considerei um cenário em que um outro projeto front (mobile, por exemplo) poderia compartilhar a camada server, aproveitando a regra de negócios da API.

Foi removida toda camada server do projeto WEB, permanecendo apensa o app e testes e2e.

## Rodando o projeto

Como os três projetos estão na mesma _Solution_ e o projeto WEB consome a API, configurei a Solution para `Multiple startup projects`, onde ao pressionar F5 será compilada API e rodando sem debug, e em seguida compilada a WEB em debug. Portanto, ao abrir a Solution e rodar, a página WEB já vai startar e já consumindo a API que foi aberta.

* Linha de comando:
```
  dotnet run -p <projectpath>/CopaFilmes.API/CopaFilmes.API.csproj;
  dotnet run -p <projectpath>/CopaFilmes.WEB/CopaFilmes.WEB.csproj;
```
## Sobre o projeto API especificamente

Essa é uma área onde tenho mais familiaridade, onde navego um pouco mais à vontade.

Utilizei o framework .Net Core 2.2 e xUnit para testes.

Criei um projeto com injeção de dependências e filtro de validação. O serviço de listagem dos filmes que consome a API oficial do Azure é singleton, instanciado uma única vez por API rodando, o que implica que só vai buscar a listagem de filmes uma vez e guardará a listagem para próximas requisições. Acredito que ali caberia uma camada cache como Redis/Memcached, dando um flush quando os filmes forem alterados.

Pelo fato do Visual Studio já fazer a integração com testes, criei um projeto de testes separado, isolando o código da API.

## Sobre o projeto WEB especificamente

Essa é uma área onde tenho uma curva de aprendizado um pouco acentuada.

Utilizei Angular 6.1.10, como framework e Protractor para testes e2e de interface e Karma para testes de componentes.

Criei um projeto que consome a API externa, ao invés de mantê-la junto, considerando um cenário de múltiplos fronts consumindo a mesma API. Removi toda a camada server desse projeto.

## Testes

### API

Os testes foram focados nos controllers, como testes de integração, pois a API é executada em background com o controlador `Microsoft.AspNetCore.TestHost\TestServer` realizando as chamadas nas rotas diretamente, considerando que ali passaria por todos os ambientes da API (DI, filters, controllers, services, erros, httpclient para chamada externa, etc).
Nesse ponto, há um `mock` da API Azure, para não ter que ir todo teste consumindo rota externa e também para que, se houver alteração de dados dos filmes oficiais, estes não impactariam os testes.

Também há testes dos serviços isoladamente para validar o algoritmo de classificação e um teste que vai no Azure pegar a lista oficial sem o uso do mock para certificar que chamada externa está ok.

Os testes podem ser visualizados na janela `Test Explorer` do Visual Studio e serem executados por lá.

* Linha de comando:
```
dotnet test <projectpath>/CopaFilmes.API.Test
```

### WEB

#### E2E com Protractor

Aqui eu tive uma dificuldade inicial pois ao realizar os testes `e2e` experimentei um erro descrito em https://github.com/angular/angular-cli/issues/13113. 

Havia instalado a versão do node.js 16.0 e que gerava o erro, precisei realizar o downgrade para 10.4.1 do node.js para funcionar.

Enfim, consegui executar o e2e de interface contidos na pasta `<projectpath>/CopaFilmes.WEB/ClientApp/e2e/src/`.

> **Observação importante**

> **Gostaria de ter criado um mock da API para os testes não ter que depender deça, mas por não ter muito conhecimento, não consegui pesquisar a tempo. Portanto, para rodar os testes é necessário levantar a API antes.**

* Linha de comando:
```
dotnet run -p <projectpath>/CopaFilmes.API/CopaFilmes.API.csproj;
cd <projectpath>/CopaFilmes.WEB/ClientApp;
ng e2e;
```

#### Karma/Jasmine

Essa é parte que infelizmente deixei a desejar e não gostei do resultado.

Não consegui realizar os testes de componentes do Angular pois os meus componentes têm injeção de dependências em seus construtores e o Jasmine não sabe lidar com isso ao criar suas camadas.

O meu erro foi similar ao descrito em https://stackoverflow.com/questions/41019109/error-no-provider-for-router-while-writing-karma-jasmine-unit-test-cases.

Tentei várias soluções e nenhuma efetiva. Infelizmente não fiz os testes de lógica dos componentes.

Se executar a linha de comando, o erro será apresentado:
 ```
 cd <projectpath>/CopaFilmes.WEB/ClientApp;
 ng test;
 ```
