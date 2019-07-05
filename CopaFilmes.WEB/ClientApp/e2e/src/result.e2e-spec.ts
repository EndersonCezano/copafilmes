import { ResultPage } from './result.po';
import { HomePage } from './home.po';

describe('Result', () => {
  let page: ResultPage;
  let home: HomePage;

  beforeEach(() => {
    page = new ResultPage();
    home = new HomePage();
  });

  it('DeveExibirTituloESubtitulo', () => {
    home.navigateToResultPage8First();
    expect(page.getTitle()).toEqual('CAMPEONATO DE FILMES');
    expect(page.getSubTitle()).toEqual('Resultado final');
  });

  it('DeveExibir2CardsFilmesVencedores', () => {
    home.navigateToResultPage8First();
    expect(page.countDivMovies()).toBe(2);
  });

  it('DeveExibirMensagemNenhumSelecionadoSeNavegarDireto', () => {
    page.clearSession();
    page.navigateTo();    
    expect(page.getNoMoviesMessage()).toEqual('Nenhum filme selecionado. Pressione o botão \"Novo campeonato\" para selecionar os filmes.');
  });

  it('DeveClassificarCorretamente8primeiros', () => {
    home.navigateToResultPage(new Array(
      "tt3606756", // os incriveis
      "tt4881806", // jurassic world
      "tt5164214", // oito mulheres
      "tt7784604", // hereditario
      "tt4154756", // vingadores
      "tt5463162", // deadpool
      "tt3778644", // han solo
      "tt3501632"  // thor
    ));
    expect(page.getChampion()).toEqual('Vingadores: Guerra Infinita');
    expect(page.getRunnerUp()).toEqual('Os Incríveis 2');
  });

  it('DeveClassificarCorretamente8ultimos', () => {
    home.navigateToResultPage(new Array(
      "tt2854926", // te peguei
      "tt0317705", // incriveis 1
      "tt3799232", // barraca beijo
      "tt1365519", // tomb raider
      "tt1825683", // pantera negra
      "tt5834262", // hotel artemis
      "tt7690670", // superfly
      "tt6499752"  // upgrade
    ));
    expect(page.getChampion()).toEqual('Os Incríveis');
    expect(page.getRunnerUp()).toEqual('Upgrade');
  });

  it('DeveClassificarCorretamentePosicoesImpares', () => {
    home.navigateToResultPage(new Array(
      "tt3606756", // incriveis 2
      "tt5164214", // oito mulheres
      "tt4154756", // vingadores
      "tt3778644", // han solo
      "tt2854926", // te peguei
      "tt3799232", // barraca beijo
      "tt1825683", // pantera negra
      "tt7690670"  // superfly
    ));
    expect(page.getChampion()).toEqual('Vingadores: Guerra Infinita');
    expect(page.getRunnerUp()).toEqual('Os Incríveis 2');
  });

  it('DeveClassificarCorretamentePosicoesPares', () => {
    home.navigateToResultPage(new Array(
      "tt4881806", // jurassic world
      "tt7784604", // hereditario
      "tt5463162", // deadpool
      "tt3501632", // thor
      "tt0317705", // incriveis 1
      "tt1365519", // tomb raider
      "tt5834262", // hotel artemis
      "tt6499752"  // upgrade
    ));
    expect(page.getChampion()).toEqual('Deadpool 2');
    expect(page.getRunnerUp()).toEqual('Os Incríveis');
  });

  it('DeveClassificarCorretamenteComEmpateNotaQuartasFinal', () => {
    home.navigateToResultPage(new Array(
      "tt5164214", // oito mulheres ==> 6.3 na posição 5
      "tt7784604", // hereditario
      "tt5463162", // deadpool
      "tt3778644", // han solo
      "tt3501632", // thor
      "tt1825683", // pantera negra
      "tt5834262", // hotel artemis ==> 6.3 na posição 4
      "tt6499752"  // upgrade
    ));
    expect(page.getChampion()).toEqual('Deadpool 2');
    expect(page.getRunnerUp()).toEqual('Hereditário');
  });

  it('DeveClassificarCorretamenteComEmpateNotaSemiFinal', () => {
    home.navigateToResultPage(new Array(
      "tt3606756", // incriveis 2
      "tt4881806", // jurassic world
      "tt7784604", // hereditario ==> 7.8 posicao 2 vai passar para semi no jogo 1
      "tt0317705", // incriveis 1
      "tt3799232", // barraca beijo
      "tt1365519", // tomb raider
      "tt7690670", // superfly
      "tt6499752"  // upgrade == 7.8 posicao 8 vai passar para semi no jogo 1
    ));
    expect(page.getChampion()).toEqual('Os Incríveis 2');
    expect(page.getRunnerUp()).toEqual('Hereditário');
  });

  
});
