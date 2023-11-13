import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppComponent } from 'src/app/app.component';
import { DadosPessoaisComponent } from 'src/app/user/pages/dados-pessoais/dados-pessoais.component';

@Component({
  selector: 'app-apagar-confirmar',
  templateUrl: './apagar-confirmar.component.html',
  styleUrls: ['./apagar-confirmar.component.css']
})
export class ApagarConfirmarComponent {
  constructor(
    public dialogRef: MatDialogRef<ApagarConfirmarComponent>,
    public appComponent: AppComponent,
    public dadosPessoais: DadosPessoaisComponent,
    @Inject(MAT_DIALOG_DATA) public data: {id: number, nome: string}
    ) {}
}
