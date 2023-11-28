import { AppComponent } from 'src/app/app.component';
import { Component } from '@angular/core';
import { Router } from "@angular/router";

@Component({
  selector: 'app-nav-bar-main',
  templateUrl: './nav-bar-main.component.html',
  styleUrls: ['./nav-bar-main.component.css']
})
export class NavBarMainComponent {
  constructor(
    private router: Router,
    public appComponent: AppComponent
  ) {}

  login(){
    this.appComponent.logando = !this.appComponent.logando;
    this.router.navigate(["dicenamics/login"])
  }

  voltar(){
    this.appComponent.logando = !this.appComponent.logando;
    this.router.navigate(["dicenamics"])
  }
}
