import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(
    private router: Router,
    public appComponent: AppComponent
  ) {}

  hide = true;

  hideTurn() {
    this.hide = !this.hide
  }

  realizarLogin() {
    this.appComponent.logando = !this.appComponent.logando
    this.appComponent.login()
    this.router.navigate([""])
  }

  realizarCadastro() {
    this.router.navigate(["cadastrar"])
  }
}
