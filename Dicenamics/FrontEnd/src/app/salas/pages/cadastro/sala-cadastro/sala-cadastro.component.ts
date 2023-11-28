import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { Sala } from 'src/app/models/sala.models';

@Component({
  selector: 'app-sala-cadastro',
  templateUrl: './sala-cadastro.component.html',
  styleUrls: ['./sala-cadastro.component.css']
})
export class SalaCadastroComponent {

  constructor(
    private router: Router,
    public appComponent: AppComponent,
    private client: HttpClient,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
  ){

  }

  ngOnInit(){

  }

  acao : string = "Criar"
  acaoBtn : string = "Criar"
  iconBtn : string = "done"
  nome = new FormControl('', Validators.required)
  descricao = new FormControl('')
  userId = this.appComponent.pegarUser()?.usuarioId

  voltar(){
    this.router.navigate(["dicenamics/salas"])
  }

  criarSala(){
    if(!this.descricao.value){
      this.descricao.setValue("Sem descrição")
    }
    if(this.nome.value && this.descricao.value && this.userId){
      let salaNova : Sala

      salaNova = {
        nome : this.nome.value,
        descricao : this.descricao.value,
        usuarioMestreId : this.userId,
        convidadosId : [],
        dadosSimplesSalaId : [],
        dadosCompostosSalaId : []
      }

      this.client
        .post<Sala>(`https://localhost:7151/dicenamics/sala/criar`, salaNova)
        .subscribe({
          next: (sala) => {
            console.log(sala)
            this.router.navigate(["dicenamics/salas"])
          },
          error: (error) => {
            console.log(error)
          }
        })
    }
  }

}
