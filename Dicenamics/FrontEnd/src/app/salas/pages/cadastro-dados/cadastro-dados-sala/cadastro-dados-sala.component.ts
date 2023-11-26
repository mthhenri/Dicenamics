import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { DadoCompostoSala } from 'src/app/models/dadoCompostoSala.models';
import { Sala } from 'src/app/models/sala.models';
import { DadosPessoaisComponent } from 'src/app/user/pages/dados-pessoais/dados-pessoais.component';

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

      if(salaIdLink != null){
        console.log(salaIdLink)
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
    });
  }

  nome = new FormControl('', Validators.required)
  dado = new FormControl('', Validators.required)
  condicao = new FormControl('', Validators.required)
  acessoPrivado : boolean = false
  acaoPg : string = "Criar"
  acaoDado : string = "Criar"
  salaDado! : Sala

  voltarPg(){
    window.history.back();
  }

  criarDadoSala(){
    if(this.nome.value && this.dado.value && this.condicao.value){
      let dadoCriar : DadoCompostoSala
      let userUser = this.appComponent.pegarUser()?.username
      const padrao = /^(\d+)d(\d+)$/;      
      const dadoFormat = this.dado.value.match(padrao)
      
      if(userUser && dadoFormat){
        dadoCriar = {
          acessoPrivado : this.acessoPrivado,
          nome : this.nome.value,
          criadorUsername : userUser,
          faces : parseInt(dadoFormat[2], 10),
          quantidade : parseInt(dadoFormat[1], 10),
          condicao : this.condicao.value,
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
  }
}
