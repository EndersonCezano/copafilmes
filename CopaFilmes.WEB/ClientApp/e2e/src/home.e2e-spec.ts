import { HomePage } from './home.po';
import { browser, by } from 'protractor';

describe('Home', () => {
  let page: HomePage;

  beforeEach(() => {
    page = new HomePage();
    page.navigateTo();
  });

  it('DeveExibirTituloESubtitulo', () => {
    expect(page.getTitle()).toEqual('CAMPEONATO DE FILMES');
    expect(page.getSubTitle()).toEqual('Fase de seleção');
  });

  it('DeveExibir16CardsFilmes', () => {
    expect(page.countCheckboxes()).toBe(16);
  });

  it('DeveExibirNenhumSelecionadoEBotaoDesabilitado', () => {
    expect(page.getSelectedMovies()).toEqual('Nenhum filme selecionado');
    expect(page.getStateButton()).toEqual(false);
  });

  it('DeveExibir1SelecionadoEBotaoDesabilitado', () => {
    page.clickCheckbox(0);
    expect(page.getSelectedMovies()).toEqual('1 filme selecionado');
    expect(page.getStateButton()).toEqual(false);
  });

  it('DeveExibir2SelecionadosEBotaoDesabilitado', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    expect(page.getSelectedMovies()).toEqual('2 filmes selecionados');
    expect(page.getStateButton()).toEqual(false);
  });

  it('DeveExibir3SelecionadosEBotaoDesabilitado', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    page.clickCheckbox(2);
    expect(page.getSelectedMovies()).toEqual('3 filmes selecionados');
    expect(page.getStateButton()).toEqual(false);
  });

  it('DeveExibir9SelecionadosEBotaoDesabilitado', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    page.clickCheckbox(2);
    page.clickCheckbox(3);
    page.clickCheckbox(4);
    page.clickCheckbox(5);
    page.clickCheckbox(6);
    page.clickCheckbox(7);
    page.clickCheckbox(8);
    expect(page.getSelectedMovies()).toEqual('9 filmes selecionados');
    expect(page.getStateButton()).toEqual(false);
  });

  it('DeveExibir14SelecionadosEBotaoDesabilitado', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    page.clickCheckbox(2);
    page.clickCheckbox(3);
    page.clickCheckbox(4);
    page.clickCheckbox(5);
    page.clickCheckbox(6);
    page.clickCheckbox(7);
    page.clickCheckbox(8);
    page.clickCheckbox(9);
    page.clickCheckbox(10);
    page.clickCheckbox(11);
    page.clickCheckbox(12);
    page.clickCheckbox(13);
    expect(page.getSelectedMovies()).toEqual('14 filmes selecionados');
    expect(page.getStateButton()).toEqual(false);
  });

  it('DeveExibir8SelecionadosEBotaoHabilitado', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    page.clickCheckbox(2);
    page.clickCheckbox(3);
    page.clickCheckbox(4);
    page.clickCheckbox(5);
    page.clickCheckbox(6);
    page.clickCheckbox(7);
    expect(page.getSelectedMovies()).toEqual('8 filmes selecionados');
    expect(page.getStateButton()).toEqual(true);
  });

  it('DeveExibirMarcarEAumentarContadorDesmacarEDiminuirContador', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    expect(page.getSelectedMovies()).toEqual('2 filmes selecionados');
    page.clickCheckbox(1);
    expect(page.getSelectedMovies()).toEqual('1 filme selecionado');
  });

  it('DeveNavegarParaPaginaResultado', () => {
    page.clickCheckbox(0);
    page.clickCheckbox(1);
    page.clickCheckbox(2);
    page.clickCheckbox(3);
    page.clickCheckbox(4);
    page.clickCheckbox(5);
    page.clickCheckbox(6);
    page.clickCheckbox(7);
    expect(page.getSelectedMovies()).toEqual('8 filmes selecionados');
    expect(page.getStateButton()).toEqual(true);
    page.clickButton();
    expect(browser.isElementPresent(by.id('resultpage_titlecard')));
  });
});
