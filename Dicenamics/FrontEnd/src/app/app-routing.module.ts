import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './user/pages/login/login/login.component';
import { CadastroComponent } from './user/pages/cadastro/cadastro/cadastro.component';
import { InicialComponent } from './pages/inicial/inicial.component';
import { DadosPessoaisComponent } from './user/pages/dados-pessoais/dados-pessoais.component';
import { DadosPessoaisCadastroComponent } from './user/pages/dados-pessoais/cadastro/dados-pessoais-cadastro/dados-pessoais-cadastro.component';
import { SintaxePageComponent } from './sintaxe/sintaxe-page/sintaxe-page.component';

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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
