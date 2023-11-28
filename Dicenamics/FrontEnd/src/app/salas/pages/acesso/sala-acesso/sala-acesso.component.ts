import { RolagemDadoSala } from './../../../../models/rolagemDadoSala.models';
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
import { faUsers } from '@fortawesome/free-solid-svg-icons';
import { faGear } from '@fortawesome/free-solid-svg-icons';
import { faEye } from '@fortawesome/free-solid-svg-icons';
import { faCubes } from '@fortawesome/free-solid-svg-icons';
import { faEyeLowVision } from '@fortawesome/free-solid-svg-icons';
import { faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { faLock } from '@fortawesome/free-solid-svg-icons';
import { faLockOpen } from '@fortawesome/free-solid-svg-icons';
import { faArrowRight } from '@fortawesome/free-solid-svg-icons';

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
        this.client
          .get<Sala>(`https://localhost:7151/dicenamics/sala/buscar/link/${salaIdLink}`)
          .subscribe({
            next: (salaEncontrada) => {
              this.salaAcesso = salaEncontrada

              this.salaAcesso.dadosCompostosSala = salaEncontrada.dadosCompostosSala
              this.nomeSala.setValue(this.salaAcesso.nome)
              this.descSala.setValue(this.salaAcesso.descricao)
              this.idSala = this.salaAcesso.idSimples
              
              this.buscarResultados();
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
  jogadores = faUsers
  mestreIco = faGear
  utilitariosIco = faCubes
  rolagemGmIco = faEyeSlash
  rolagemSecretaIco = faEyeLowVision
  rolagemPublicaIco = faEye
  publico = faLockOpen
  privado = faLock
  setaDireita = faArrowRight

  salaAcesso! : Sala
  nomeSala = new FormControl('')
  descSala = new FormControl('')
  idSala? : number
  gmRoll : boolean = false
  playerRoll : boolean = false
  nomeJogadorAdd = new FormControl('')
  rolagensLista : RolagemDadoSala[] = []

  voltar(){
    this.router.navigate(["dicenamics/salas"])
  }

  buscarResultados(){
    setInterval(() => {
      this.client
        .get<RolagemDadoSala[]>(`https://localhost:7151/dicenamics/dados/rolagens/listar/sala/${this.salaAcesso.salaId}`)
        .subscribe({
          next: (rolagems) => {
            this.rolagensLista = this.sortResoltadosDate(rolagems);
          },
          error: (error) => {
            console.log(error);
          }
        });
    }, 3000);
  }

  buscarResultadosUnique(){
    this.client
        .get<RolagemDadoSala[]>(`https://localhost:7151/dicenamics/dados/rolagens/listar/sala/${this.salaAcesso.salaId}`)
        .subscribe({
          next: (rolagems) => {
            this.rolagensLista = this.sortResoltadosDate(rolagems);
          },
          error: (error) => {
            console.log(error);
          }
        });
  }

  sortResoltadosDate(rolagens : RolagemDadoSala[]){
    return rolagens.sort((a, b) => {
      let dataA = new Date(a.roladoEm)
      let dataB = new Date(b.roladoEm)
      const dateA = dataA.getTime()
      const dateB = dataB.getTime()

      // Ordem decrescente (do mais recente para o mais antigo).
      return dateB - dateA;
    });
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

  apagarDado(dado : DadoCompostoSala){
    let apagar = this.snackBar.open(`Deseja realmente apagar o dado "${dado.nome}"?`, "Sim", {
      duration: 3500
    })

    apagar.onAction().subscribe(() => {
      this.client
        .delete<DadoCompostoSala>(`https://localhost:7151/dicenamics/dados/salas/composto/deletar/${dado.dadoCompostoSalaId}`)
        .subscribe({
          next: (dado) => {
            this.ngOnInit();
          },
          error: (error) => {
            console.log(error)
          }
        })
    })
  }

  editarDadoSala(dado : DadoCompostoSala){
    this.router.navigate([`dicenamics/salas/acesso/${this.salaAcesso.idLink}/dados/editar/${dado.dadoCompostoSalaId}`])
  }

  dadoBonito(dado : DadoCompostoSala){
    let texto = ''
    texto = `${dado.quantidade}d${dado.faces}${dado.condicao}`
    if(dado.fixos != null){
      dado.fixos.forEach(fixo => {
        if(fixo.modificadorFixo.valor < 0){
          texto += `${fixo.modificadorFixo.valor}`
        } else {
          texto += `+${fixo.modificadorFixo.valor}`
        }
      });
    }
    if(dado.variaveis != null){
      dado.variaveis.forEach(variavel => {
        texto += `+${variavel.modificadorVariavel.dado.quantidade}d${variavel.modificadorVariavel.dado.faces}`
      });
    }
    return texto
  }

  dataBonita(data : Date){
    let date = new Date(data.toString())

    const formatoData = new Intl.DateTimeFormat('pt-BR', {
      year: 'numeric',
      month: 'numeric',
      day: 'numeric',
      hour: 'numeric',
      minute: 'numeric',
      second: 'numeric',
      hour12: false, // Use 24-hour format
    });
    
    let dataNova = formatoData.format(date);

    return dataNova
  }

  resultadoRolagemExibir(tipoRoll : string){
    let classe
    if(tipoRoll.toUpperCase() === "GM"){
      classe = "gmTypeRoll"
    } else if (tipoRoll.toUpperCase() === "SECRETA"){
      classe = "secretTypeRoll"
    } else {
      classe = "normalTypeRoll"
    }
    return classe
  }

  verificaTipoRolagemPlayer(rolagem : RolagemDadoSala){
    if(rolagem.tipoRolagem.toUpperCase() === "GM"){
      return false
    } else if(rolagem.tipoRolagem.toUpperCase() === "SECRETA" && rolagem.usuarioUsername != this.appComponent.pegarUser()?.username) {
      return false
    } else {
      return true
    }
  }

  criadorDadoMostrar(dado : DadoCompostoSala){
    return "Criador: @" + dado.criador?.username
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

  rolarDado(dado : DadoCompostoSala){
    let dadoId = dado.dadoCompostoSalaId
    let salaId = this.salaAcesso.salaId
    let tipoRolagem : string = "publica"
    if(this.gmRoll){
      tipoRolagem = "gm"
    }
    if(this.playerRoll){
      tipoRolagem = "secreta"
    }
    let usuarioUsername = this.appComponent.pegarUser()?.username

    this.client
      .post<RolagemDadoSala>(`https://localhost:7151/dicenamics/dados/rolagens/gerar/${dadoId}/${salaId}/${tipoRolagem}/${usuarioUsername}`, null)
      .subscribe({
        next: (rolagem) => {
          this.buscarResultadosUnique()
        },
        error: (error) => {
          console.log(error)
        }
      })

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
            window.location.reload();
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
              window.location.reload();
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

  resultadoRefine(dado : DadoCompostoSala, resultado : string){
    let resultados : number[][]
    resultados = JSON.parse(resultado)

    let dadosRoladosS: string = "";
    let valoresFinais: number[] = []
    let valoresFinaisS: string = "";

    if(dadosRoladosS === ""){
      let valor = 0

      // Formatação para Sem Condição
      if(dado.condicao === ""){
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(index === 0){
            dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === resultados[0].length - 1){
            dadosRoladosS += valor.toString() + " ]"
          } else {
            dadosRoladosS += valor.toString() + " , "
          }
        }
      }

      // Formatação para Maior Valor
      if(dado.condicao === "mrv"){       
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(resultados[0].length === 1){
            dadosRoladosS += "[ " + valor.toString() + " ]"
          } else {
            if(index === 0){
              dadosRoladosS += "[ " + valor.toString() + " , "
            } else if(index === resultados[0].length - 1){
              dadosRoladosS += valor.toString() + " ]"
            } else {
              dadosRoladosS += valor.toString() + " , "
            }
          }
        }

        valor = 0;
        for (let i = 0; i < resultados[0].length; i++) {
          if (resultados[0][i] >= valor) {
            valor = resultados[0][i];
          }
        }
        dadosRoladosS += " = " + valor
      }

      // Formatação para Menor Valor
      if(dado.condicao === "mnv"){  
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(resultados[0].length === 1){
            dadosRoladosS += "[ " + valor.toString() + " ]"
          } else {
            if(index === 0){
              dadosRoladosS += "[ " + valor.toString() + " , "
            } else if(index === resultados[0].length - 1){
              dadosRoladosS += valor.toString() + " ]"
            } else {
              dadosRoladosS += valor.toString() + " , "
            }
          }
        }

        valor = dado.faces + 1
        for (let i = 0; i < resultados[0].length; i++) {
          if (resultados[0][i] <= valor) {
            valor = resultados[0][i];
          }
        }
        dadosRoladosS += " = " + valor
      }

      // Formatação para Somar Tudo
      if(dado.condicao === "std"){  
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(resultados[0].length === 1){
            dadosRoladosS += "[ " + valor.toString() + " ]"
          } else {
            if(index === 0){
              dadosRoladosS += "[ " + valor.toString() + " + "
            } else if(index === resultados[0].length - 1){
              dadosRoladosS += valor.toString() + " ]"
            } else {
              dadosRoladosS += valor.toString() + " + "
            }
          }
        }
      }

      // Formatação para Acima De
      if(dado.condicao.substring(0, 3) === "acd"){
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(index === 0){
            dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === resultados[0].length - 1){
            dadosRoladosS += valor.toString() + " ]"
          } else {
            dadosRoladosS += valor.toString() + " , "
          }
        }
      }

      // Formatação para Abaixo De
      if(dado.condicao.substring(0, 3) === "abd"){
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(index === 0){
            dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === resultados[0].length - 1){
            dadosRoladosS += valor.toString() + " ]"
          } else {
            dadosRoladosS += valor.toString() + " , "
          }
        }
      }

      // Formatação para Valor Escolhido
      if(dado.condicao.substring(0, 3) === "ved"){
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(index === 0){
            dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === resultados[0].length - 1){
            dadosRoladosS += valor.toString() + " ]"
          } else {
            dadosRoladosS += valor.toString() + " , "
          }
        }
      }

      // Formatação para De A
      if(dado.condicao.substring(0, 2) === "de"){
        for (let index = 0; index < resultados[0].length; index++) {
          const valor = resultados[0][index];
  
          if(index === 0){
            dadosRoladosS += "[ " + valor.toString() + " , "
          } else if(index === resultados[0].length - 1){
            dadosRoladosS += valor.toString() + " ]"
          } else {
            dadosRoladosS += valor.toString() + " , "
          }
        }
      }

      // Fazer formatação para as outras condições

      if(dado.fixos != undefined && dado.fixos.length != 0){
        for (let index = 0; index < dado.fixos.length; index++) {
          const fixo = dado.fixos[index].modificadorFixo;
          if(fixo.valor > 0){
            dadosRoladosS += " + " + fixo.valor
          } else {
            dadosRoladosS += " " + fixo.valor
          }
        }
      }

      if(dado.variaveis != undefined && dado.variaveis.length != 0){
        for (let index = 2; index < resultados.length; index++) {
          const element : number[] = resultados[index];
          dadosRoladosS += " + [ "
          for (let index = 0; index < element.length; index++) {
            const val = element[index];
            if(index != element.length - 1){
              dadosRoladosS += val + " + "
            } else {
              dadosRoladosS += val
            }
          }
          dadosRoladosS += " ]"   
        }
      }

      for (let index = 0; index < resultados[1].length; index++) {
        const valor = resultados[1][index];
        valoresFinais.push(valor)
      }

      valoresFinais.sort(function(a, b) {
        return b - a
      });

      for (let index = 0; index < valoresFinais.length; index++) {
        const valor = valoresFinais[index];
        if(index === valoresFinais.length - 1){
          valoresFinaisS += valor.toString()
        } else {
          valoresFinaisS += valor.toString() + " | "
        }
      }

    }
    return {todosOsDados: dadosRoladosS, valoresFinais: valoresFinaisS}
  }
}
