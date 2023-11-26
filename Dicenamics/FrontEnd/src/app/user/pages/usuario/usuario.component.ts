import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { subscribeOn } from 'rxjs';
import { AppComponent } from 'src/app/app.component';
import { Usuario } from 'src/app/models/usuario.models';

@Injectable({
  providedIn: 'root',
})
@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public appComponent: AppComponent,
    private client: HttpClient,
    private formBuilder: FormBuilder,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
  ){}

  ngOnInit(){
    this.usernameEnviado = ""
    if(this.appComponent.pegarUser()?.username != undefined){
      this.usernameEnviado = this.appComponent.pegarUser()?.username
    }
    if(this.usernameEnviado != undefined && this.usernameEnviado != ""){
      this.client
        .get<Usuario>(`https://localhost:7151/dicenamics/usuario/buscar/u/${this.usernameEnviado}`)
        .subscribe({
          next: (usuario) => {
            this.usuarioLog = usuario
            this.username.setValue(usuario.username)
            this.nickname.setValue(usuario.nickname)
            this.senha.setValue(usuario.senha)
          },
          error: (error) => {
            console.log(error)
          }
        })
    }
  }

  usernameEnviado! : string | undefined
  usuarioLog! : Usuario
  userSalvar! : Usuario
  idUser : number = 0
  hide = true;
  username = new FormControl('', Validators.required)
  nickname = new FormControl('', Validators.required)
  senha = new FormControl('', Validators.required)

  hideTurn() {
    this.hide = !this.hide
  }

  voltar(){
    window.history.back();
  }

  salvarUser(){
    let snackBarRef = this.snackBar.open("Deseja realmente salvar? Caso não, ignore esse pop-up", "SIM", {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    })

    snackBarRef.onAction().subscribe(() => {
      let snackBarRef2 = this.snackBar.open("Tem certeza?", "ABSOLUTA", {
        duration: 5000,
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
      })

      snackBarRef2.onAction().subscribe(() => {
        this.realmenteSalvar();
      })
    })

    // this.dialog.open(SalvarUserComponent, {
    //   width: '350px',
    //   enterAnimationDuration,
    //   exitAnimationDuration,
    // })
  }

  async buscarUser(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.client
      .get<Usuario>(`https://localhost:7151/dicenamics/usuario/buscar/u/${this.appComponent.pegarUser()?.username}`)
      .subscribe({
        next: (usuario) => {
          this.usuarioLog = usuario
          resolve();
        },
        error: (error) => {
          console.log(error)
          reject();
        }
      })
    })
  }

  async realmenteSalvar(){
    if(this.username.value?.toString() != undefined && this.nickname.value?.toString() != undefined && this.senha.value?.toString() != undefined){
      await this.buscarUser();

      let simplesIds : number[] = []
      let compostosIds : number[] = []

      if(this.usuarioLog.dadosSimplesPessoais && this.usuarioLog.dadosCompostosPessoais){
        this.usuarioLog.dadosSimplesPessoais.forEach(id => {
          if(id.dadoId){
            simplesIds.push(id.dadoId)
          }
        });

        this.usuarioLog.dadosCompostosPessoais.forEach(id => {
          if(id.dadoId){
            compostosIds.push(id.dadoId)
          }
        });
      }   

      this.userSalvar = {
        username : this.username.value?.toString(),
        nickname : this.nickname.value?.toString(),
        senha : this.senha.value?.toString(),
        dadosSimplesPessoaisIds : simplesIds,
        dadosCompostosPessoaisIds : compostosIds
      }

      this.client
        .put<Usuario>(`https://localhost:7151/dicenamics/usuario/atualizar/${this.usuarioLog.usuarioId}`, this.userSalvar)
        .subscribe({
          next: (user) => {
            if(this.username.value?.toString() != undefined && this.nickname.value?.toString() != undefined && this.senha.value?.toString() != undefined){
              let userGravar : Usuario = {
                username : this.username.value?.toString(),
                nickname : this.nickname.value?.toString(),
                senha : this.senha.value?.toString(),
              }
              this.appComponent.gravarUser(userGravar)
              this.appComponent.voltarHome()
            }
          },
          error: (error) => {
            console.log(error)
            let snackBarRef = this.snackBar.open("Username já existente!", "Ok", {
              duration: 2500,
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
            })
            snackBarRef.afterDismissed().subscribe(() => {
              window.location.reload();
            })
          }
        })
      
    } else {
      console.log("erro")
    }
  }

  async apagarUser(){
    let snackBarRef = this.snackBar.open("Deseja realmente apagar? Caso não, ignore esse pop-up", "SIM", {
      duration: 5000,
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    })

    snackBarRef.onAction().subscribe(() => {
      let snackBarRef2 = this.snackBar.open("Tem certeza?", "ABSOLUTA", {
        duration: 5000,
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
      })

      snackBarRef2.onAction().subscribe(() => {
        let snackBarRef3 = this.snackBar.open("MESMO???", "DEIXA EU APAGAR LOGO", {
          duration: 5000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
        })
  
        snackBarRef3.onAction().subscribe(async () => {
          await this.buscarUser();
          if(this.usuarioLog.usuarioId){
            this.realmenteApagar(this.usuarioLog.usuarioId)
          }
        })
      })
    })
  }

  realmenteApagar(id : number){
    this.client
      .delete(`https://localhost:7151/dicenamics/usuario/excluir/${id}`)
      .subscribe({
        next: (user) => {
          this.appComponent.sair()
        },
        error: (error) => {
          
        }
      })
  }
}
