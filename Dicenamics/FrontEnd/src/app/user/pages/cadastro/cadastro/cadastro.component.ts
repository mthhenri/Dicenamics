import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent {
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
}
