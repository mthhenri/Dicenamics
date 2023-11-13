import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarMainComponent } from './Nav/nav-bar-main/nav-bar-main.component';
import { LoginComponent } from './user/pages/login/login/login.component';
import { CadastroComponent } from './user/pages/cadastro/cadastro/cadastro.component';
import { InicialComponent } from './pages/inicial/inicial.component';
import { SairConfirmarComponent } from './pages/sair-confirmar/sair-confirmar.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarMainComponent,
    LoginComponent,
    CadastroComponent,
    InicialComponent,
    SairConfirmarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
