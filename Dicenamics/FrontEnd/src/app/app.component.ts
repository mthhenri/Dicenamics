import { Component, ElementRef, Injectable, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { delay } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
@Component({
  selector: 'app-root',
  templateUrl: `./app.component.html`,
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = 'FrontEnd';
  chegada: boolean = true;

  login() {
    this.chegada = !this.chegada;
    localStorage.setItem("login" , this.chegada ? '1' : '0');
  }

  checkLogin() {
    const val = localStorage.getItem("login");
    return val ? parseInt(val) === 1 : false;
  }
}