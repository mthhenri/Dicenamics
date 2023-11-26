import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { Sala } from 'src/app/models/sala.models';
import { faCrown, faL } from '@fortawesome/free-solid-svg-icons';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { faDiceD20 } from '@fortawesome/free-solid-svg-icons';
import { faUserPlus } from '@fortawesome/free-solid-svg-icons';
import { SalaUsuario } from 'src/app/models/salaUsuario.models';
import { DadoCompostoSala } from 'src/app/models/dadoCompostoSala.models';

@Component({
  selector: 'app-sala-acesso',
  templateUrl: './sala-acesso.component.html',
  styleUrls: ['./sala-acesso.component.css']
})
export class SalaAcessoComponent {

  constructor(
    private router: Router,
    public appComponent: AppComponent,
    private client: HttpClient,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ){

  }

  ngOnInit(){
    this.route.paramMap.subscribe(params => {
      const salaIdLink = params.get('idLink');

      if(salaIdLink != null){
        console.log(salaIdLink)
        this.client
          .get<Sala>(`https://localhost:7151/dicenamics/sala/buscar/link/${salaIdLink}`)
          .subscribe({
            next: (salaEncontrada) => {
              this.salaAcesso = salaEncontrada

              this.salaAcesso.dadosCompostosSala = salaEncontrada.dadosCompostosSala
              this.nomeSala.setValue(this.salaAcesso.nome)
              this.descSala.setValue(this.salaAcesso.descricao)
              this.idSala = this.salaAcesso.idSimples
              
            },
            error: (error) => {
              console.log(error)
            }
          })
      }
    });
  }

  GMIcon = faCrown
  playerIcon = faUser
  playerAdd = faUserPlus
  d20 = faDiceD20

  salaAcesso! : Sala
  nomeSala = new FormControl('')
  descSala = new FormControl('')
  idSala? : number
  gmRoll : boolean = false
  playerRoll : boolean = false
  nomeJogadorAdd = new FormControl('')

  voltar(){
    this.router.navigate(["dicenamics/salas"])
  }

  checkRolagemGM(){
    if(this.gmRoll){
      return "Rolagem GM"
    } else {
      return "Rolagem Pública"
    }
  }

  checkRolagemPlayer(){
    if(this.playerRoll){
      return "Rolagem Secreta"
    } else {
      return "Rolagem Pública"
    }
  }

  userOk() {
    if (this.salaAcesso.convidados) {
      for (const convidado of this.salaAcesso.convidados) {
        if (convidado.usuarioId === this.appComponent.pegarUser()?.usuarioId) {
          return true;
        }
      }
    }
    return false;
  }

  userUNK() {
    if (this.salaAcesso.convidados) {
      for (const convidado of this.salaAcesso.convidados) {
        if (convidado.usuarioId === this.appComponent.pegarUser()?.usuarioId) {
          return false;
        }
      }
    }
    if(this.salaAcesso.usuarioMestreId === this.appComponent.pegarUser()?.usuarioId){
      return false;
    }
    return true;
  }

  criarDadoSala(){
    this.router.navigate([`dicenamics/salas/acesso/${this.salaAcesso.idLink}/dados/criar`])
  }

  dadosPrivadosFilter(){
    if(this.salaAcesso.dadosCompostosSala){
      let dadosPrivados : DadoCompostoSala[] = []
      this.salaAcesso.dadosCompostosSala.forEach(dado => {
        if(dado.criador?.username === this.appComponent.pegarUser()?.username && dado.acessoPrivado){
          dadosPrivados.push(dado)
        }
      });
      return dadosPrivados;
    }
    return null;
  }

  dadosPublicosFilter(){
    if(this.salaAcesso.dadosCompostosSala){
      let dadosPublicos : DadoCompostoSala[] = []
      this.salaAcesso.dadosCompostosSala.forEach(dado => {
        if(!dado.acessoPrivado){
          dadosPublicos.push(dado)
        }
      });
      return dadosPublicos;
    }
    return null;
  }

  editarSala(type : string){
    let editar = this.snackBar.open("Tem certeza que quer editar?", "Sim", {
      duration: 2500
    })

    editar.onAction().subscribe(() => {
      if(type === "name"){
        if(this.nomeSala.value){
          let sala : Sala
          let convidadosID : number[] = []
          let dadosSimplesSalaId : number[] = []
          let dadosCompostosSalaId : number[] = []

          if(this.salaAcesso.convidados){
            this.salaAcesso.convidados.forEach(user => {
              if(user.usuario.usuarioId){
                convidadosID.push(user.usuario.usuarioId)
              }
            });
          }

          if(this.salaAcesso.dadosSimplesSala){
            this.salaAcesso.dadosSimplesSala.forEach(dado => {
              if(dado.dadoSimplesSalaId){
                dadosSimplesSalaId.push(dado.dadoSimplesSalaId)
              }
            });
          }

          if(this.salaAcesso.dadosCompostosSala){
            this.salaAcesso.dadosCompostosSala.forEach(dado => {
              if(dado.dadoCompostoSalaId){
                dadosCompostosSalaId.push(dado.dadoCompostoSalaId)
              }
            });
          }

          sala = {
            nome : this.nomeSala.value,
            descricao : this.salaAcesso.descricao,
            usuarioMestreId : this.salaAcesso.usuarioMestreId,
            convidadosId : convidadosID,
            dadosSimplesSalaId : dadosSimplesSalaId,
            dadosCompostosSalaId : dadosCompostosSalaId
          }

          this.client
            .put<Sala>(`https://localhost:7151/dicenamics/sala/atualizar/${this.salaAcesso.salaId}`, sala)
            .subscribe({
              next: (sala) => {
                if(sala){
                  this.salaAcesso = sala
                  window.location.reload()
                }
              },
              error: (error) => {
                console.log(error)
              }
            })
        }
      } else {
        if(this.descSala.value){
          let sala : Sala
          let convidadosID : number[] = []
          let dadosSimplesSalaId : number[] = []
          let dadosCompostosSalaId : number[] = []

          if(this.salaAcesso.convidados){
            this.salaAcesso.convidados.forEach(user => {
              if(user.usuario.usuarioId){
                convidadosID.push(user.usuario.usuarioId)
              }
            });
          }

          if(this.salaAcesso.dadosSimplesSala){
            this.salaAcesso.dadosSimplesSala.forEach(dado => {
              if(dado.dadoSimplesSalaId){
                dadosSimplesSalaId.push(dado.dadoSimplesSalaId)
              }
            });
          }

          if(this.salaAcesso.dadosCompostosSala){
            this.salaAcesso.dadosCompostosSala.forEach(dado => {
              if(dado.dadoCompostoSalaId){
                dadosCompostosSalaId.push(dado.dadoCompostoSalaId)
              }
            });
          }

          sala = {
            nome : this.salaAcesso.nome,
            descricao : this.descSala.value,
            usuarioMestreId : this.salaAcesso.usuarioMestreId,
            convidadosId : convidadosID,
            dadosSimplesSalaId : dadosSimplesSalaId,
            dadosCompostosSalaId : dadosCompostosSalaId
          }

          this.client
            .put<Sala>(`https://localhost:7151/dicenamics/sala/atualizar/${this.salaAcesso.salaId}`, sala)
            .subscribe({
              next: (sala) => {
                if(sala){
                  this.salaAcesso = sala
                  window.location.reload()
                }
              },
              error: (error) => {
                console.log(error)
              }
            })
        }
      }
    })
  }

  removeUser(usuario : SalaUsuario){
    let removeUser = this.snackBar.open(`Remover acesso da sala de @${usuario.usuario.username}?`, "Sim", {
      duration: 3000
    })

    removeUser.onAction().subscribe(() => {
      this.client
        .put<Sala>(`https://localhost:7151/dicenamics/sala/remover/user/${this.salaAcesso.salaId}/${usuario.usuario.username}`, this.salaAcesso)
        .subscribe({
          next: (sala) => {
            this.salaAcesso = sala
          },
          error: (error) => {
            console.log(error)
          }
        })
    })
  }

  addUser(){
    if(this.nomeJogadorAdd.value){
      let add = this.snackBar.open(`Adicionar o usuário @${this.nomeJogadorAdd.value}?`, "Sim", {
        duration: 3500
      })

      add.onAction().subscribe(() => {
        this.client
          .put<Sala>(`https://localhost:7151/dicenamics/sala/adicionar/user/${this.salaAcesso.salaId}/${this.nomeJogadorAdd.value}`, this.salaAcesso)
          .subscribe({
            next: (sala) => {
              this.salaAcesso = sala
            },
            error: (error) => {
              console.log(error)
            }
          })
      })
    }
  }

  apagarSala(){
    let apagar1 = this.snackBar.open("Deseja realmente apagar a sala?", "Sim", {
      duration: 2500
    })

    apagar1.onAction().subscribe(() => {
      let apagar2 = this.snackBar.open("Tem certeza?", "SIM",{
        duration: 2500
      })

      apagar2.onAction().subscribe(() => {
        this.client
          .delete<Sala>(`https://localhost:7151/dicenamics/sala/deletar/${this.salaAcesso.salaId}`)
          .subscribe({
            next: (sala) => {
              this.router.navigate(["dicenamics/salas"])
            },
            error: (error) => {
              console.log(error)
            }
          })
      })
    })
  }

  sairSala(username? : string){
    let sair = this.snackBar.open("Deseja realmente sair a sala?", "Sim", {
      duration: 25004
    })

    sair.onAction().subscribe(() => {
      this.client
        .put<Sala>(`https://localhost:7151/dicenamics/sala/remover/user/${this.salaAcesso.salaId}/${username}`, this.salaAcesso)
        .subscribe({
          next: (sala) => {
            this.router.navigate(["dicenamics/salas"])
          },
          error: (error) => {
            console.log(error)
          }
        })
    })
  }
}
