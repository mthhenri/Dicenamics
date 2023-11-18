import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { DadoComposto } from 'src/app/models/dadoComposto.models';
import { DadoCompostoModFixo } from 'src/app/models/dadoCompostoModFixo.models';
import { ModificadorFixo } from 'src/app/models/modificadorFixo.models';
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
  step? : number = -1

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

  setStep(index? : number){
    this.step = index
  }

  dadoBonito(dado : DadoComposto){
    let texto = ''
    texto = `${dado.quantidade}d${dado.faces}${dado.condicao}`
    if(dado.fixos != null){
      dado.fixos.forEach(fixo => {
        if(fixo.modificadorFixo.valor < 0){
          texto += `${fixo.modificadorFixo.valor}`
        } else {
          texto += `+${fixo.modificadorFixo.valor}`
        }
      });
    }
    if(dado.variaveis != null){
      dado.variaveis.forEach(variavel => {
        texto += `+${variavel.modificadorVariavel.dado.quantidade}d${variavel.modificadorVariavel.dado.faces}`
      });
    }
    return texto
  }

  formatarString(str?: string): string {
    if(str != undefined && str != ""){
      return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
    } else {
      return ""
    }
  }

  modificadorBonito(dadoMods : DadoComposto){
    let texto = ''
    if(dadoMods.fixos != undefined && dadoMods.fixos != null){
      for (let index = 0; index < dadoMods.fixos.length; index++) {
        const mod = dadoMods.fixos[index];
        texto += ` | ${this.formatarString(mod.modificadorFixo.nome)} ${mod.modificadorFixo.valor}`
        // if(index === dadoMods.fixos.length - 1){
        //   texto += `${this.formatarString(mod.modificadorFixo.nome)} ${mod.modificadorFixo.valor} `
        // } else {
        //   texto += `${this.formatarString(mod.modificadorFixo.nome)} ${mod.modificadorFixo.valor} | `
        // }
      }
    }
    if(dadoMods.variaveis != undefined && dadoMods.variaveis != null){
      for (let index = 0; index < dadoMods.variaveis.length; index++) {
        const mod = dadoMods.variaveis[index];
        texto += ` | ${mod.modificadorVariavel.nome} ${mod.modificadorVariavel.dado.quantidade}d${mod.modificadorVariavel.dado.faces}`
        // if(index === dadoMods.variaveis.length - 1){
        //   texto += `${mod.modificadorVariavel.nome} ${mod.modificadorVariavel.dado.quantidade}d${mod.modificadorVariavel.dado.faces} `
        // } else {
        //   texto += `${mod.modificadorVariavel.nome} ${mod.modificadorVariavel.dado.quantidade}d${mod.modificadorVariavel.dado.faces} | `
        // }
      }
    }
    return texto
  }

  setResult(resultadosNovos : number[][]){
    this.result = resultadosNovos
  }

  getResult() : number[][] {
    return this.result
  }

  gerarResultados(dadoRolado: DadoComposto){
    this.client
      .get<number[][]>(`https://localhost:7151/dicenamics/dados/composto/rolar/${dadoRolado.dadoId}`)
      .subscribe({
        next: (resultados) => {
          this.setResult(resultados)
          this.rolarDado('150ms', '150ms', dadoRolado)
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  rolarDado(enterAnimationDuration: string, exitAnimationDuration: string, dadoRolado: DadoComposto) {
    this.dialog.open(RolagemDadoComponent, {
      width: '400px',
      enterAnimationDuration,
      exitAnimationDuration,
      data : {
        resultados: this.getResult(),
        dado: dadoRolado
      }
    });
  }

  realmenteApagar(dadoId?: number, dadoNome? : string){
    this.dialog.open(ApagarConfirmarComponent, { width: '350px', data: {id: dadoId, nome: dadoNome} })
  }

  apagarDado(id : number, nome : string){
    this.client
      .delete(`https://localhost:7151/dicenamics/dados/composto/deletar/${id}`)
      .subscribe({
        next: (result) => {
          window.location.reload();
          // const bar = this.snackBar.open(`O dado "${nome}" foi apagado!`, 'Beleza!', {
          //   horizontalPosition: 'center',
          //   verticalPosition: 'bottom'
          // })
        },
        error: (error) => {
          console.log(error)
        }
      })
    
  }

  criarDado(){
    this.router.navigate(["dicenamics/dados/criar"])
  }

  editarDado(dadoEnviado? : number){
    this.router.navigate(["dicenamics/dados/editar", dadoEnviado], { queryParams : { dadoId : dadoEnviado }})
  }
}
