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

  gerarResultado() : string{
    var dadosRolados: string = '';
    var valoresFinais: string = '';

    for (let index = 0; index < this.result[0].length; index++) {
      const valor = this.result[0][index];

      if(index === this.result[0].length - 1){
        dadosRolados += valor.toString()
      } else {
        dadosRolados += valor.toString() + ", "
      }
    }

    for (let index = 0; index < this.result[1].length; index++) {
      const valor = this.result[1][index];

      if(index === this.result[1].length - 1){
        valoresFinais += valor.toString()
      } else {
        valoresFinais += valor.toString() + ", "
      }
    }

    return `<span> Os dados rolados foram: <br>${dadosRolados} </span><br><hr><span>Aplicando a condição se obtém: <br>${valoresFinais}<hr>`
  }
}
