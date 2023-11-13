import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { Usuario } from 'src/app/models/usuario.models';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private router: Router,
    private appComponent: AppComponent,
    private client: HttpClient,
    private snackBar: MatSnackBar
  ) {}

  hide = true;
  username = new FormControl('', Validators.required)
  senha = new FormControl('', Validators.required)

  hideTurn() {
    this.hide = !this.hide
  }

  realizarLogin() {
    let userbuscar = this.username.value?.toString()
    let senhaTestar = this.senha.value?.toString()
    this.client
      .get<Usuario>(`https://localhost:7151/dicenamics/usuario/buscar/u/${userbuscar}`)
      .subscribe({
        next: (user) => {
          if(senhaTestar === user.senha){
            this.snackBar.open(`Seja bem vindo ${user.nickname}`, `Dicenamics 2023`, {
              duration: 1750,
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            })
            this.appComponent.gravarUser(user)
            this.appComponent.logando = !this.appComponent.logando
            this.appComponent.login()
            this.router.navigate([""])
          } else {
            this.snackBar.open(`Senha errada!`, `Entendido`, {
              duration: 1750,
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            })
          }
        },
        error: (error) => {
          console.log(error)
          this.snackBar.open(`Usuário inválido!`, `Entendido`, {
            duration: 1750,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
          })
        }
      })
  }

  realizarCadastro() {
    this.router.navigate(["cadastrar"])
  }

}
