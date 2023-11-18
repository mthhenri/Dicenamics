import { Component, Inject, Injectable } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppComponent } from 'src/app/app.component';
import { DadoComposto } from 'src/app/models/dadoComposto.models';
import { ResultadoRolagem } from 'src/app/models/resultadoRolagem.models';

@Injectable({
  providedIn: 'root',
})
@Component({
  selector: 'app-rolagem-dado',
  templateUrl: './rolagem-dado.component.html',
  styleUrls: ['./rolagem-dado.component.css']
})
export class RolagemDadoComponent {
  constructor(
    public dialogRef: MatDialogRef<RolagemDadoComponent>,
    public appComponent: AppComponent,
    @Inject(MAT_DIALOG_DATA) public data: {resultados: number[][], dado: DadoComposto}
  ) {}

  result! : number[][]
  dadoRolado! : DadoComposto

  ngOnInit(): void {
    this.result = this.data.resultados;
    this.dadoRolado = this.data.dado
  }  

  dadosRoladosS: string = "";
  valoresFinais: number[] = []
  valoresFinaisS: string = "";

  gerarResultado(){
    if(this.dadosRoladosS === ""){
      let valor = 0

      // Formatação para Sem Condição
      if(this.dadoRolado.condicao === ""){
        for (let index = 0; index < this.result[0].length; index++) {
          const valor = this.result[0][index];
  
          if(index === 0){
            this.dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === this.result[0].length - 1){
            this.dadosRoladosS += valor.toString() + " ]"
          } else {
            this.dadosRoladosS += valor.toString() + " , "
          }
        }
      }

      // Formatação para Maior Valor
      if(this.dadoRolado.condicao === "mrv"){       
        for (let index = 0; index < this.result[0].length; index++) {
          const valor = this.result[0][index];
  
          if(index === 0){
            this.dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === this.result[0].length - 1){
            this.dadosRoladosS += valor.toString() + " ]"
          } else {
            this.dadosRoladosS += valor.toString() + " , "
          }
        }

        for (let i = 1; i < this.result[0].length; i++) {
          if (this.result[0][i] >= valor) {
            valor = this.result[0][i];
          }
        }
        this.dadosRoladosS += " = " + valor
      }

      // Formatação para Menor Valor
      if(this.dadoRolado.condicao === "mnv"){  
        for (let index = 0; index < this.result[0].length; index++) {
          const valor = this.result[0][index];
  
          if(index === 0){
            this.dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === this.result[0].length - 1){
            this.dadosRoladosS += valor.toString() + " ]"
          } else {
            this.dadosRoladosS += valor.toString() + " , "
          }
        }

        valor = this.dadoRolado.faces + 1
        for (let i = 1; i < this.result[0].length; i++) {
          if (this.result[0][i] <= valor) {
            valor = this.result[0][i];
          }
        }
        this.dadosRoladosS += " = " + valor
      }

      // Formatação para Somar Tudo
      if(this.dadoRolado.condicao === "std"){  
        for (let index = 0; index < this.result[0].length; index++) {
          const valor = this.result[0][index];
  
          if(index === 0){
            this.dadosRoladosS += "[ " + valor.toString() + " + "
          } else if(index === this.result[0].length - 1){
            this.dadosRoladosS += valor.toString() + " ]"
          } else {
            this.dadosRoladosS += valor.toString() + " + "
          }
        }
      }

      // Fazer formatação para as outras condições

      if(this.dadoRolado.fixos != undefined && this.dadoRolado.fixos.length != 0){
        for (let index = 0; index < this.dadoRolado.fixos.length; index++) {
          const fixo = this.dadoRolado.fixos[index].modificadorFixo;
          if(fixo.valor > 0){
            this.dadosRoladosS += " + " + fixo.valor
          } else {
            this.dadosRoladosS += " - " + fixo.valor
          }
        }
      }

      if(this.dadoRolado.variaveis != undefined && this.dadoRolado.variaveis.length != 0){
        for (let index = 2; index < this.result.length; index++) {
          const element : number[] = this.result[index];
          this.dadosRoladosS += " + [ "
          for (let index = 0; index < element.length; index++) {
            const val = element[index];
            if(index != element.length - 1){
              this.dadosRoladosS += val + " + "
            } else {
              this.dadosRoladosS += val
            }
          }
          this.dadosRoladosS += " ]"   
        }
      }

      for (let index = 0; index < this.result[1].length; index++) {
        const valor = this.result[1][index];
        this.valoresFinais.push(valor)
      }

      this.valoresFinais.sort(function(a, b) {
        return b - a
      });

      for (let index = 0; index < this.valoresFinais.length; index++) {
        const valor = this.valoresFinais[index];
        if(index === this.valoresFinais.length - 1){
          this.valoresFinaisS += valor.toString()
        } else {
          this.valoresFinaisS += valor.toString() + " | "
        }
      }

    }
  }

  todosOsDados() {
    this.gerarResultado()
    return this.dadosRoladosS
  }
}
