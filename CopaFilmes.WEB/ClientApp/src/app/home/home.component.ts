import { Component, Inject, OnInit  } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Filme } from './filme';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})

export class HomeComponent implements OnInit {

  public filmesSelecionados: string[] = [];
  private nenhumFilme = "Nenhum filme selecionado";

  public mensagemContadorSelecao = this.nenhumFilme;
  public mensagemCarregando = "Aguarde enquanto a lista de filmes é carregada...";
  public mensagemErro = "";
  public filmes: Filme[] = [];

  // construtor do componente, apenas para receber as dependencias por injecao
  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }


  // ao iniciar o componente, carrega os filmes da api
  ngOnInit(): void {
    sessionStorage.clear();

    var url = this.baseUrl + 'api/confrontos/filmes';

    this.http.get<Filme[]>(url).subscribe(
      result => {
        this.filmes = result;
      },
      error => {
        console.error(error);
        var httpError = error as HttpErrorResponse;
        this.mensagemCarregando = "Não foi possível carregar a lista de filmes."
        this.mensagemErro = httpError.message;
      }
    );
  }



  // evento do botão Gerar Campeonato
  public executeChampionship() {
    if (this.filmesSelecionados.length != 8)
      alert("É necessário selecionar exatamente 8 filmes para continuar.");
    else {
      sessionStorage.setItem("filmesSelecionados", JSON.stringify(this.filmesSelecionados));
      this.router.navigate(['result']);
    }
  }



  // método para obter a cor do texto do contador
  public getColorTotalFilmes() {
    let styles = {
      'color': (this.filmesSelecionados.length == 8) ? 'blue' : 'red'
    };
    return styles;
  }



  // método para habilitar ou não o botão de gerar campeonato
  public getDisableState() {
    return (this.filmesSelecionados.length != 8);
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

    var count = this.filmesSelecionados.length;

    if (count == 0)
      this.mensagemContadorSelecao = this.nenhumFilme;
    else if (count == 1)
      this.mensagemContadorSelecao = "1 filme selecionado";
    else
      this.mensagemContadorSelecao = count + " filmes selecionados";
  }
}
