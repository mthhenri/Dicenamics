import { AppComponent } from './../../app.component';
import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-bar-main',
  templateUrl: './nav-bar-main.component.html',
  styleUrls: ['./nav-bar-main.component.css']
})
export class NavBarMainComponent {
  constructor(
    public appComponent: AppComponent
  ){}
}
