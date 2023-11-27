import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { DadoCompostoSala } from 'src/app/models/dadoCompostoSala.models';
import { Sala } from 'src/app/models/sala.models';
import { DadosPessoaisComponent } from 'src/app/user/pages/dados-pessoais/dados-pessoais.component';
import { faLock } from '@fortawesome/free-solid-svg-icons';
import { faLockOpen } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-cadastro-dados-sala',
  templateUrl: './cadastro-dados-sala.component.html',
  styleUrls: ['./cadastro-dados-sala.component.css']
})
export class CadastroDadosSalaComponent {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public appComponent: AppComponent,
    private client: HttpClient,
    private snackBar: MatSnackBar,
    public dadosPessoais: DadosPessoaisComponent,
    private formBuilder: FormBuilder
  ){

  }

  ngOnInit(){
    this.route.paramMap.subscribe(params => {
      const salaIdLink = params.get('idLink');
      const dadoId = params.get('dadoId');

      if(salaIdLink != null){
        this.client
          .get<Sala>(`https://localhost:7151/dicenamics/sala/buscar/link/${salaIdLink}`)
          .subscribe({
            next: (salaEncontrada) => {
              this.salaDado = salaEncontrada
            },
            error: (error) => {
              console.log(error)
            }
          })
      }
      if(dadoId != null){
        this.dadoId = parseInt(dadoId)
        this.client
          .get<DadoCompostoSala>(`https://localhost:7151/dicenamics/dados/salas/composto/buscar/${dadoId}`)
          .subscribe({
            next: (dado) => {
              this.dadoEditar = dado
              
              this.nome.setValue(dado.nome)
              this.dado.setValue(`${dado.quantidade}d${dado.faces}`)
              this.condicao.setValue(dado.condicao)
              this.acessoPrivado = dado.acessoPrivado

              this.acaoPg = "Editar"
              this.acaoDado = "Salvar"
            },
            error: (error) => {
              console.log(error)
            }
          })
      }
    });
  }

  nome = new FormControl('', Validators.required)
  dado = new FormControl('', Validators.required)
  condicao = new FormControl('')
  acessoPrivado : boolean = false
  acaoPg : string = "Criar"
  acaoDado : string = "Criar"
  salaDado! : Sala

  dadoId : number = 0
  dadoEditar! : DadoCompostoSala

  publico = faLockOpen
  privado = faLock

  voltarPg(){
    window.history.back();
  }

  tipoDado(){
    if(this.acessoPrivado){
      return "Privado"
    } else {
      return "Público"
    }
  }

  criarDadoSala(){
    if(!this.dadoEditar){
      console.log("NOVO")
      if(this.nome.value && this.dado.value){
        let dadoCriar : DadoCompostoSala
        let userUser = this.appComponent.pegarUser()?.username
        let condicao = this.condicao.value || ""
        const padrao = /^(\d+)d(\d+)$/;      
        const dadoFormat = this.dado.value.match(padrao)
        
        if(userUser && dadoFormat){
          dadoCriar = {
            acessoPrivado : this.acessoPrivado,
            nome : this.nome.value,
            criadorUsername : userUser,
            faces : parseInt(dadoFormat[2], 10),
            quantidade : parseInt(dadoFormat[1], 10),
            condicao : condicao,
            fixosId : [],
            variaveisId : []
          }
  
          this.client
            .put<DadoCompostoSala>(`https://localhost:7151/dicenamics/sala/adicionar/dado/composto/${this.salaDado.salaId}`, dadoCriar)
            .subscribe({
              next: (dado) => {
                window.history.back();
              },
              error: (error) => {
                console.log(error)
              }
            })
        }
      }   
    } else {
      if(this.nome.value && this.dado.value){
        let dadoCriar : DadoCompostoSala
        let userUser = this.appComponent.pegarUser()?.username
        let condicao = this.condicao.value || ""
        const padrao = /^(\d+)d(\d+)$/;      
        const dadoFormat = this.dado.value.match(padrao)
        
        if(userUser && dadoFormat){
          dadoCriar = {
            dadoCompostoSalaId : this.dadoId,
            acessoPrivado : this.acessoPrivado,
            nome : this.nome.value,
            criadorUsername : userUser,
            faces : parseInt(dadoFormat[2], 10),
            quantidade : parseInt(dadoFormat[1], 10),
            condicao : condicao,
            fixosId : [],
            variaveisId : []
          }
  
          this.client
            .put<DadoCompostoSala>(`https://localhost:7151/dicenamics/sala/atualizar/dado/composto/${this.salaDado.salaId}/${this.dadoId}`, dadoCriar)
            .subscribe({
              next: (dado) => {
                window.history.back();
              },
              error: (error) => {
                console.log(error)
              }
            })
        }
      }
    }
  }
}
