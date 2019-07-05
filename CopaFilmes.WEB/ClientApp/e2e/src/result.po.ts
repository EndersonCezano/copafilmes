import { browser, by, element } from 'protractor';

export class ResultPage {
  navigateTo() {
    return browser.get('/result');
  }

  getTitle() {
    return element(by.className('first-title')).getText();
  }

  getSubTitle() {
    return element(by.className('second-title')).getText();
  }
  
  countDivMovies() {
    return element.all(by.className('titulo-campeao')).count();
  }

  getNoMoviesMessage() {
    return element(by.tagName('h5')).getText();
  }

  clearSession() {
    browser.executeScript('window.sessionStorage.clear();');
  }

  getChampion() {
    return element.all(by.className('titulo-campeao')).get(0).getText();
  }

  getRunnerUp() {
    return element.all(by.className('titulo-campeao')).get(1).getText();
  }
}
