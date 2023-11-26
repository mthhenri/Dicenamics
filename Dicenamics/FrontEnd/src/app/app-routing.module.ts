import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './user/pages/login/login/login.component';
import { CadastroComponent } from './user/pages/cadastro/cadastro/cadastro.component';
import { InicialComponent } from './pages/inicial/inicial.component';
import { DadosPessoaisComponent } from './user/pages/dados-pessoais/dados-pessoais.component';
import { DadosPessoaisCadastroComponent } from './user/pages/dados-pessoais/cadastro/dados-pessoais-cadastro/dados-pessoais-cadastro.component';
import { SintaxePageComponent } from './sintaxe/sintaxe-page/sintaxe-page.component';
import { UsuarioComponent } from './user/pages/usuario/usuario.component';
import { SalasComponent } from './salas/pages/salas/salas.component';
import { SalaCadastroComponent } from './salas/pages/cadastro/sala-cadastro/sala-cadastro.component';
import { SalaAcessoComponent } from './salas/pages/acesso/sala-acesso/sala-acesso.component';
import { CadastroDadosSalaComponent } from './salas/pages/cadastro-dados/cadastro-dados-sala/cadastro-dados-sala.component';

const routes: Routes = [
  // inserir as rotas aqui
  {
    path: "",
    component: InicialComponent
  },
  {
    path: "dicenamics",
    component: InicialComponent
  },
  {
    path: "dicenamics/login",
    component: LoginComponent
  },
  {
    path: "dicenamics/usuario/:usuarioUser",
    component: UsuarioComponent
  },
  {
    path: "dicenamics/cadastrar",
    component: CadastroComponent
  },
  {
    path: "dicenamics/dados",
    component: DadosPessoaisComponent
  },
  {
    path: "dicenamics/dados/criar",
    component: DadosPessoaisCadastroComponent
  },
  {
    path: "dicenamics/dados/editar/:dadoId",
    component: DadosPessoaisCadastroComponent
  },
  {
    path: "dicenamics/sintaxe",
    component: SintaxePageComponent
  },
  {
    path: "dicenamics/salas",
    component: SalasComponent
  },
  {
    path: "dicenamics/salas/criar",
    component: SalaCadastroComponent
  },
  {
    path: "dicenamics/salas/acesso/:idLink",
    component: SalaAcessoComponent
  },
  {
    path: "dicenamics/salas/acesso/:idLink/dados/criar",
    component: CadastroDadosSalaComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
