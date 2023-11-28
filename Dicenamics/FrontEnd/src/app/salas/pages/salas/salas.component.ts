import { Sala } from './../../../models/sala.models';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.css']
})
export class SalasComponent {

  constructor(
    private router: Router,
    public appComponent: AppComponent,
    private client: HttpClient,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
  ){

  }

  ngOnInit(){
    this.client
      .get<Sala[]>(`https://localhost:7151/dicenamics/sala/listar/usuario/mestre/${this.appComponent.pegarUser()?.username}`)
      .subscribe({
        next: (salas) => {
          this.salasUserMestre = salas
        },
        error: (error) => {
          console.log(error)
        }
      })

    this.client
      .get<Sala[]>(`https://localhost:7151/dicenamics/sala/listar/usuario/jogador/${this.appComponent.pegarUser()?.username}`)
      .subscribe({
        next: (salas) => {
          this.salasUserJogador = salas
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  idSimples = new FormControl('')
  salasUserMestre : Sala[] = []
  salasUserJogador : Sala[] = []

  contarJogadores(sala : Sala){
    let numero : number = 0

    if(sala.convidados != null){
      sala.convidados.forEach(user => {
        numero += 1
      });
    }

    return numero
  }

  criarSala(){
    this.router.navigate(["dicenamics/salas/criar"])
  }

  entrarSala(sala : Sala){
    this.router.navigate([`dicenamics/salas/acesso/${sala.idLink}`])
  }

  entrarSalaCode(){
    let six = this.idSimples.value?.toString()

    if(six?.length == 6){
      let link : string

      this.client
        .get<Sala>(`https://localhost:7151/dicenamics/sala/buscar/six/${six}`)
        .subscribe({
          next: (sala) => {
            if(sala.idLink){
              link = sala.idLink
              this.router.navigate([`dicenamics/salas/acesso/${link}`])
            }
          },
          error: (error) => {
            console.log(error)
          }
        })      
    } else {
      if(six?.length != 6){
        this.snackBar.open("O ID informado é inválido!", "Ok", {
          duration: 2500
        })
      } else {
        this.snackBar.open("Essa sala não existe!", "Ok", {
          duration: 2500
        })
      }
    }
  }

}
