import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './user/pages/login/login/login.component';
import { CadastroComponent } from './user/pages/cadastro/cadastro/cadastro.component';
import { InicialComponent } from './pages/inicial/inicial.component';

const routes: Routes = [
  // inserir as rotas aqui
  {
    path: "",
    component: InicialComponent
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "cadastrar",
    component: CadastroComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
