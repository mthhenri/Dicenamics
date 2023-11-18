import { Usuario } from './models/usuario.models';
import { Component, Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SairConfirmarComponent } from './pages/sair-confirmar/sair-confirmar.component';


@Injectable({
  providedIn: 'root',
})
@Component({
  selector: 'app-root',
  templateUrl: `./app.component.html`,
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  constructor(
    private router: Router,
    public dialog: MatDialog
  ){}

  title = 'FrontEnd';
  chegada: boolean = true;
  logando: boolean = false;
  usuario: Usuario = { username : '', nickname : '', senha : ''};

  gravarUser(usuario : Usuario){
    this.usuario = usuario;
    localStorage.setItem('usuario', JSON.stringify(this.usuario));
  }

  pegarUser() : Usuario | null {
    const user = localStorage.getItem('usuario')
    //const user : Usuario = { username : '', nickname : '', senha : ''}
    //console.log(user)
    if(user != undefined && user != null){
      this.usuario = JSON.parse(user);
      return this.usuario
    }
    return null
  }

  checkLogando() {
    return this.logando
  }

  login() {
    localStorage.setItem("login", false ? '1' : '0');
  }

  checkLogin() {
    const val = localStorage.getItem("login");
    if(val === null){
      return true
    } else {
      let userCheck = parseInt(val)
      if(userCheck === 1){
        return true
      } else {
        return false
      }
    }

    // if(val != null || val != undefined){
    //   let userCheck = parseInt(val)
    //   if(userCheck === 1){
    //     return true
    //   } else {
    //     return false
    //   }
    // } else {
    //   return false
    // }
  }

  abrirPopUp(enterAnimationDuration: string, exitAnimationDuration: string) : void {
    this.dialog.open(SairConfirmarComponent, {
      width: '350px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }

  sair() {
    let newUser : Usuario = {
      username : '',
      nickname : '',
      senha : ''
    }
    this.gravarUser(newUser)
    localStorage.setItem("login", true ? '1' : '0');
    this.router.navigate(["dicenamics"])
  }

  voltarHome() {
    this.router.navigate(["dicenamics"])
  }

  navUsuario() {
    this.router.navigate(["dicenamics"])
  }

  navSalas() {
    this.router.navigate(["dicenamics"])
  }

  navDadosPessoais() {
    this.router.navigate(["dicenamics/dados"])
  }

  navSintaxe() {
    this.router.navigate(["dicenamics"])
  }
}