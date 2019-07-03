import { Component } from '@angular/core';
import { Filme } from './filme';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

  
export class HomeComponent {

  filmes = [
    new Filme("tt3606756", "Os Incríveis 2", 2018),
    new Filme("tt4881806", "Jurassic World: Reino Ameaçado", 2018),
    new Filme("tt5164214", "Oito Mulheres e um Segredo", 2018),
    new Filme("tt7784604", "Hereditário", 2018),
    new Filme("tt4154756", "Vingadores: Guerra Infinita", 2018),
    new Filme("tt5463162", "Deadpool 2", 2018),
    new Filme("tt3778644", "Han Solo: Uma História Star Wars", 2018),
    new Filme("tt3501632", "Thor: Ragnarok", 2017),
    new Filme("tt2854926", "Te Peguei!", 2018),
    new Filme("tt0317705", "Os Incríveis", 2004),
    new Filme("tt3799232", "A Barraca do Beijo", 2018),
    new Filme("tt1365519", "Tomb Raider: A Origem", 2018),
    new Filme("tt1825683", "Pantera Negra", 2018),
    new Filme("tt5834262", "Hotel Artemis", 2018),
    new Filme("tt7690670", "Superfly", 2018),
    new Filme("tt6499752", "Upgrade", 2018),
  ];

  private filmesSelecionados = [];
  private nenhumFilme = "Nenhum filme selecionado";
  public currentCount = 0;
  public totalFilme = this.nenhumFilme;

  // evento do botão Gerar Campeonato
  public executeChampionship() {
    if (this.filmesSelecionados.length != 8)
      alert("É necessário selecionar exatamente 8 filmes para continuar.");

    else
      alert(this.filmesSelecionados)
  }

  // método para obter a cor do texto do contador
  public getColorTotalFilmes() {
    let styles = {
      'color': (this.currentCount == 8) ? 'blue' : 'red'
    };
    return styles;
  }

  // método para habilitar ou não o botão de gerar campeonato
  public getDisableState() {
    return (this.currentCount != 8);
  }

  // verifica se um filme está ou não selecionados
  public checkItemSelected(id) {
    return this.filmesSelecionados.includes(id);
  }

  // evento de click na div de filmes (qualquer elemento interno)
  public changeStatus(id) {
    if (this.checkItemSelected(id)) {
      const index = this.filmesSelecionados.indexOf(id);
      this.filmesSelecionados.splice(index, 1);
    }
    else
      this.filmesSelecionados.push(id);

    this.currentCount = this.filmesSelecionados.length;

    if (this.currentCount == 0)
      this.totalFilme = this.nenhumFilme;
    else if (this.currentCount == 1)
      this.totalFilme = "1 filme selecionado";
    else
      this.totalFilme = this.currentCount + " filmes selecionados";
  }
}
