import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { Usuario } from 'src/app/models/usuario.models';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent {
  constructor(
    private router: Router,
    public appComponent: AppComponent,
    private client: HttpClient,
    private snackBar: MatSnackBar
  ) {}

  hide = true;
  username = new FormControl('', Validators.required)
  nickname = new FormControl('', Validators.required)
  senha = new FormControl('', Validators.required)

  hideTurn() {
    this.hide = !this.hide
  }

  realizarCadastro() {
    let u: string | undefined = this.username.value?.toString();
    let n: string | undefined = this.nickname.value?.toString();
    let s: string | undefined = this.senha.value?.toString();
    let userCriar!: Usuario

    // Verificar se os valores não são undefined antes de atribuir a outra variável
    if (u !== undefined && n !== undefined && s !== undefined) {
      userCriar = {
        usuarioId: 0,
        username: u,
        nickname: n,
        senha: s,
        dadosCompostosPessoaisIds: [],
        dadosSimplesPessoaisIds: []
      };
    } else {
      console.log(userCriar);
      return
    }

    this.client
      .post<Usuario>("https://localhost:7151/dicenamics/usuario/criar", userCriar)
      .subscribe({
        next: (user) => {
          userCriar = user
          this.snackBar.open(`Seja bem vindo ${userCriar.nickname}`, `Dicenamics 2023`, {
            duration: 1750,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
          })
          this.appComponent.gravarUser(userCriar)
          this.appComponent.logando = !this.appComponent.logando
          this.appComponent.login()
          this.router.navigate([""])
        },
        error: (error) => {
          console.log(error)
        }
      })
  }
}
