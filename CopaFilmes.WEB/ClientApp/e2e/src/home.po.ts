import { browser, by, element } from 'protractor';

export class HomePage {
  navigateTo() {
    return browser.get('/');
  }

  navigateToResultPage8First() {
    browser.get('/');
    this.clickCheckboxes([0, 1, 2, 3, 4, 5, 6, 7]);
    this.clickButton();
  }

  navigateToResultPage(ids: string[]) {
    browser.get('/');
    this.clickCheckboxesByIds(ids);
    this.clickButton();
  }

  getTitle() {
    return element(by.className('first-title')).getText();
  }

  getSubTitle() {
    return element(by.className('second-title')).getText();
  }

  getSelectedMovies() {
    return element(by.className('col-lg-9')).getText();
  }

  getStateButton() {
    return element(by.tagName('button')).isEnabled();
  }

  clickButton() {
    element.all(by.tagName('button')).click();
  }

  clickCheckboxes(indexes: number[]) {
    for (let index of indexes) {
      this.clickCheckbox(index);
    }
  }

  clickCheckbox(index) {
    var elem = element.all(by.tagName('input')).get(index);
    browser.executeScript('window.scrollTo(0, 10000);');
    elem.click();
  }

  clickCheckboxesByIds(ids: string[]) {
    for (let id of ids) {
      this.clickCheckboxById(id);
    };
  }

  clickCheckboxById(id) {
    var elem = element(by.id(id));
    browser.executeScript('window.scrollTo(0, 10000);');
    elem.click();
  }

  countCheckboxes() {
    return element.all(by.tagName('input')).count();
  }
}
