import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { DadoComposto } from 'src/app/models/dadoComposto.models';
import { ApagarConfirmarComponent } from 'src/app/pages/apagar-confirmar/apagar-confirmar.component';
import { RolagemDadoComponent } from 'src/app/pages/rolagem-dado/rolagem-dado.component';

@Injectable({
  providedIn: 'root',
})
@Component({
  selector: 'app-dados-pessoais',
  templateUrl: './dados-pessoais.component.html',
  styleUrls: ['./dados-pessoais.component.css']
})
export class DadosPessoaisComponent {
  constructor(
    public router : Router,
    public dialog: MatDialog,
    private appComponent: AppComponent,
    private client: HttpClient,
    private snackBar: MatSnackBar
  ){}

  result : number[][] = []
  dados : DadoComposto[] = []

  ngOnInit(){
    this.client
      .get<DadoComposto[]>(`https://localhost:7151/dicenamics/usuario/buscar/dados/compostos/${this.appComponent.pegarUser()?.usuarioId}`)
      .subscribe({
        next: (dados) => {
          this.dados = dados
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  dadoBonito(dado : DadoComposto){
    let texto = ''
    dado.fixos.forEach(fixo => {
      
    });
    texto = `${dado.quantidade}D${dado.faces}${dado.condicao}`
    return texto
  }

  setResult(resultadosNovos : number[][]){
    this.result = resultadosNovos
  }

  getResult() : number[][] {
    return this.result
  }

  gerarResultados(id : number){
    this.client
      .get<number[][]>(`https://localhost:7151/dicenamics/dados/composto/rolar/${id}`)
      .subscribe({
        next: (resultados) => {
          this.setResult(resultados)
          this.rolarDado('150ms', '150ms')
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  rolarDado(enterAnimationDuration: string, exitAnimationDuration: string) {
    this.dialog.open(RolagemDadoComponent, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
      data : {
        resultados: this.getResult()
      }
    });
  }

  realmenteApagar(dadoId: number, dadoNome : string){
    this.dialog.open(ApagarConfirmarComponent, { width: '350px', data: {id: dadoId, nome: dadoNome} })
  }

  apagarDado(id : number, nome : string){
    this.client
      .delete(`https://localhost:7151/dicenamics/dados/composto/deletar/${id}`)
      .subscribe({
        next: (result) => {
          const bar = this.snackBar.open(`O dado "${nome}" foi apagado!`, 'Beleza!', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
          })
          bar.onAction().subscribe(() => {
            this.ngOnInit()
          })
          this.ngOnInit()
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  criarDado(){
    this.router.navigate(["dadosPessoais/criar"])
  }
}
