import { DadosPessoaisComponent } from 'src/app/user/pages/dados-pessoais/dados-pessoais.component';
import { DadoComposto } from 'src/app/models/dadoComposto.models';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-dados-pessoais-cadastro',
  templateUrl: './dados-pessoais-cadastro.component.html',
  styleUrls: ['./dados-pessoais-cadastro.component.css']
})
export class DadosPessoaisCadastroComponent {
  constructor(
    private router: Router,
    public appComponent: AppComponent,
    private client: HttpClient,
    private snackBar: MatSnackBar,
    public dadosPessoais: DadosPessoaisComponent
  ) {}

  nome = new FormControl('', Validators.required)
  faces = new FormControl('', Validators.required)
  quantidade = new FormControl('', Validators.required)
  condicao = new FormControl('', Validators.required)

  criarDado(){
    let nome : string | undefined = this.nome.value?.toString() 
    let f : string | undefined = this.faces.value?.toString()
    let q : string | undefined = this.quantidade.value?.toString() 
    let condicao : string | undefined = this.condicao.value?.toString() 
    let userId : number | undefined = this.appComponent.pegarUser()?.usuarioId
    let dadoNovo! : DadoComposto

    if(nome != undefined && f != undefined && q != undefined && condicao != undefined && userId != undefined){
      let faces = parseInt(f)
      let quantidade = parseInt(q)

      dadoNovo = {
        dadoId : 0,
        nome : nome,
        faces : faces,
        quantidade : quantidade,
        condicao : condicao,
        usuarioId : userId,
        fixos : [],
        variaveis : []
      }
    } else {
      console.log(dadoNovo)
      return
    }

    this.client
      .put<DadoComposto>(`https://localhost:7151/dicenamics/usuario/adicionarDado/composto/${dadoNovo.usuarioId}`, dadoNovo)
      .subscribe({
        next: (dado) => {
          this.router.navigate(["dadosPessoais"])
          this.snackBar.open(`O dado ${dado.nome} foi criado!`, 'Beleza!', {
            duration: 1750,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
          })          
          this.dadosPessoais.ngOnInit()
        },
        error: (error) => {
          console.log(error)
        }
      })
  }
}
