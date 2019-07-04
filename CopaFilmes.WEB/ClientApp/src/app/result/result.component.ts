import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse} from '@angular/common/http';
import { Filme } from '../home/filme';
import { Router } from '@angular/router';

@Component({
  selector: 'app-result',
  templateUrl: './result.component.html'
})


export class ResultComponent implements OnInit {

  private filmesSelecionados: string[] = [];
  public filmesVencedores: Filme[] = [];
  public mensagemCarregando = "Aguarde enquanto a classificação é gerada pelo servidor...";
  public mensagemErro = "";


  // construtor do componente apenas para receber as dependecias por injeção
  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
  }



  // ao iniciar o componente, gera o resultado
  ngOnInit(): void {

    this.filmesSelecionados = JSON.parse(sessionStorage.getItem("filmesSelecionados"));

    if ((!this.filmesSelecionados) || (this.filmesSelecionados.length == 0)) {
      this.mensagemCarregando = "Nenhum filme selecionado. Pressione o botão \"Novo campeonato\" para selecionar os filmes.";
    }
    else {
      var url = this.baseUrl + 'api/confrontos/gerarcampeonato';

      var postData = {
        'Selecao': this.filmesSelecionados
      };
      
      this.http.post<Filme[]>(url, postData).subscribe(
        result => {
          this.filmesVencedores = result;
        },
        error => {
          console.error(error);
          var httpError = error as HttpErrorResponse;
          this.mensagemCarregando = "Não foi possível gerar a classificação."
          this.mensagemErro = httpError.message;
          if (httpError.status == 400) {
            var obj = httpError.error.errors;
            console.log(obj);
          }

        }
      );
    }

  }



  // evento do botão Gerar Campeonato
  public goBack() {
    this.router.navigate(['']);
  }
}
