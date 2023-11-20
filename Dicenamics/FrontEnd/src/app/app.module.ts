import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarMainComponent } from './Nav/nav-bar-main/nav-bar-main.component';
import { LoginComponent } from './user/pages/login/login/login.component';
import { CadastroComponent } from './user/pages/cadastro/cadastro/cadastro.component';
import { InicialComponent } from './pages/inicial/inicial.component';
import { SairConfirmarComponent } from './pages/sair-confirmar/sair-confirmar.component';
import { DadosPessoaisComponent } from './user/pages/dados-pessoais/dados-pessoais.component';
import { RolagemDadoComponent } from './pages/rolagem-dado/rolagem-dado.component';
import { ApagarConfirmarComponent } from './pages/apagar-confirmar/apagar-confirmar.component';
import { DadosPessoaisCadastroComponent } from './user/pages/dados-pessoais/cadastro/dados-pessoais-cadastro/dados-pessoais-cadastro.component';
import { SintaxePageComponent } from './sintaxe/sintaxe-page/sintaxe-page.component';
import { UsuarioComponent } from './user/pages/usuario/usuario.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarMainComponent,
    LoginComponent,
    CadastroComponent,
    InicialComponent,
    SairConfirmarComponent,
    DadosPessoaisComponent,
    RolagemDadoComponent,
    ApagarConfirmarComponent,
    DadosPessoaisCadastroComponent,
    SintaxePageComponent,
    UsuarioComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatExpansionModule,
    FormsModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    MatGridListModule,
    MatCardModule,
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
