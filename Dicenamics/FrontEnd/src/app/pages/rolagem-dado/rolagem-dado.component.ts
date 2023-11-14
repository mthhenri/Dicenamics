import { Component, Inject, Injectable } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppComponent } from 'src/app/app.component';

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
    @Inject(MAT_DIALOG_DATA) public data: {resultados: number[][]}
  ) {}

  result : number[][] = []

  ngOnInit(): void {
    this.result = this.data.resultados
  }  

  dadosRoladosS: string = "";
  dadosRoladosI: number[] = [];
  valoresFinais: number[] = [];
  valoresFinaisS: string = "";

  gerarResultado(){
    if(this.dadosRoladosS === ""){
      for (let index = 0; index < this.result[0].length; index++) {
        const valor = this.result[0][index];
        if(index === this.result[0].length - 1){
          this.dadosRoladosS += valor.toString()
          this.dadosRoladosI.push(valor)
        } else {
          this.dadosRoladosS += valor.toString() + ", "
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
