import { AppComponent } from 'src/app/app.component';
import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-sair-confirmar',
  templateUrl: './sair-confirmar.component.html',
  styleUrls: ['./sair-confirmar.component.css']
})
export class SairConfirmarComponent {
  constructor(
    public dialogRef: MatDialogRef<SairConfirmarComponent>,
    public appComponent: AppComponent
    ) {}
}
