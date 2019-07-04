import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Filme } from './filme';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

  
export class HomeComponent {

  private filmesSelecionados = [];
  private nenhumFilme = "Nenhum filme selecionado";

  public currentCount = 0;
  public totalFilme = this.nenhumFilme;
  public filmes: Filme[] = [];

  constructor(private router: Router, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    var url = baseUrl + 'api/confrontos/filmes';

    http.get<Filme[]>(url).subscribe(
      result => {
        this.filmes = result;        
      },
      error => {
        console.error(error);
        var httpError = error as HttpErrorResponse;
        alert(httpError.message + '\n\nstatus code  : ' + httpError.status + ' (' + httpError.statusText + ')');
      }
    );

  }

  // evento do botão Gerar Campeonato
  public executeChampionship() {
    if (this.filmesSelecionados.length != 8)
      alert("É necessário selecionar exatamente 8 filmes para continuar.");

    else
      this.router.navigate(['fetch-data']);
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
