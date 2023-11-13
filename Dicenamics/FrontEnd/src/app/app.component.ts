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

  checkLogando() {
    return this.logando
  }

  login() {
    localStorage.setItem("login", false ? '1' : '0');
  }

  checkLogin() {
    const val = localStorage.getItem("login");
    return val ? parseInt(val) === 1 : false;;
    
  }

  abrirPopUp(enterAnimationDuration: string, exitAnimationDuration: string) : void {
    this.dialog.open(SairConfirmarComponent, {
      width: '350px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }

  sair() {
    localStorage.setItem("login", true ? '1' : '0');
  }

  voltarHome() {
    this.router.navigate([""])
  }

  navUsuario() {
    this.router.navigate([""])
  }

  navSalas() {
    this.router.navigate([""])
  }

  navDadosPessoais() {
    this.router.navigate([""])
  }

  navSintaxe() {
    this.router.navigate([""])
  }
}