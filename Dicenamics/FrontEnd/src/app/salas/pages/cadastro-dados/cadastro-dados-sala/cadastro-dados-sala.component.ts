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
import { DadoCompostoModVar } from 'src/app/models/dadoCompostoModVar.models';
import { DadoCompostoModFixo } from 'src/app/models/dadoCompostoModFixo.models';
import { ModificadorVariavel } from 'src/app/models/modificadorVariavel.models';
import { DadoSimplesSala } from 'src/app/models/dadoSimplesSala.models';
import { ModificadorFixo } from 'src/app/models/modificadorFixo.models';

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
  modFixQuantidade : number[] = [0]
  modVarQuantidade : number[] = [0]
  modFixos : DadoCompostoModFixo[] = []
  modVars : DadoCompostoModVar[] = []
  formControlsFix: { [key: string]: FormControl } = {};
  formControlsVar: { [key: string]: FormControl } = {};

  acaoPg : string = "Criar"
  acaoDado : string = "Criar"
  salaDado! : Sala

  dadoId : number = 0
  dadoEditar! : DadoCompostoSala
  

  publico = faLockOpen
  privado = faLock

  addModFix(index : number){
    this.modFixQuantidade.push(this.modFixQuantidade[index] + 1)
  }

  addModVar(index : number){
    this.modVarQuantidade.push(this.modVarQuantidade[index] + 1)
  }

  removeModFixo(index : number){
    this.modFixQuantidade = this.modFixQuantidade.filter(e => e != index)
  }

  removeModVar(index : number){
    this.modVarQuantidade = this.modVarQuantidade.filter(e => e != index)
  }

  async formModAddEditFix(dado : DadoCompostoSala) : Promise<void> {
    return new Promise((resolve, reject) => {
      if(dado.fixos != undefined && dado.fixos != null){

        for (let index = 0; index < dado.fixos.length - 1; index++) {
          this.addModFix(index)
        }

        dado.fixos.forEach((fixo, index) => {
          const nome = fixo.modificadorFixo.nome?.toString();
          const valor = fixo.modificadorFixo.valor?.toString();
        
          this.atualizarFormControlFixNome(index, nome);
          this.atualizarFormControlFixValor(index, valor);
        });

        resolve()
      }
      reject()
    })
  }

  async formModAddEditVar(dado : DadoCompostoSala) : Promise<void> {
    return new Promise((resolve, reject) => {
      if(dado.variaveis != undefined && dado.variaveis != null){

        for (let index = 0; index < dado.variaveis.length - 1; index++) {
          this.addModVar(index)
        }

        dado.variaveis.forEach((variavel, index) => {
          const nome = variavel.modificadorVariavel.nome?.toString();
          const dado = variavel.modificadorVariavel.dado?.quantidade + "d" + variavel.modificadorVariavel.dado?.faces
        
          this.atualizarFormControlVarNome(index, nome);
          this.atualizarFormControlVarDado(index, dado);
        });

        resolve()
      }
      reject()
    })
  }

  atualizarFormControlFixNome(index: number, nome: string | undefined): void {
    let formControlName = `nome_fix_${index}`;
    if (this.formControlsFix[formControlName]) {
      this.formControlsFix[formControlName].setValue(nome || '');
    } else {
      this.formControlsFix[formControlName] = this.formBuilder.control('');
      this.formControlsFix[formControlName].setValue(nome || '');
    }
  }
  
  atualizarFormControlFixValor(index: number, valor: string | undefined): void {
    let formControlValue = `valor_fix_${index}`;
    if (this.formControlsFix[formControlValue]) {
      this.formControlsFix[formControlValue].setValue(parseInt(valor || '0', 10));
    } else {
      this.formControlsFix[formControlValue] = this.formBuilder.control('', Validators.required);
      this.formControlsFix[formControlValue].setValue(parseInt(valor || '0', 10));
    }
  }

  atualizarFormControlVarNome(index: number, nome: string | undefined): void {
    let formControlName = `nome_var_${index}`;
    if (this.formControlsVar[formControlName]) {
      this.formControlsVar[formControlName].setValue(nome || '');
    } else {
      this.formControlsVar[formControlName] = this.formBuilder.control('');
      this.formControlsVar[formControlName].setValue(nome || '');
    }
  }
  
  atualizarFormControlVarDado(index: number, dado: string | undefined): void {
    let formControlValue = `dado_var_${index}`;
    if (this.formControlsVar[formControlValue]) {
      this.formControlsVar[formControlValue].setValue(dado || '');
    } else {
      this.formControlsVar[formControlValue] = this.formBuilder.control('', Validators.required);
      this.formControlsVar[formControlValue].setValue(dado || '');
    }
  }

  getFormControlFixo(index: number, campo: string): FormControl {
    const controlName = `${campo}_fix_${index}`;  
    if (!this.formControlsFix[controlName]) {
      if(campo === "nome"){
        this.formControlsFix[controlName] = this.formBuilder.control('');
      } else {
        this.formControlsFix[controlName] = this.formBuilder.control('', Validators.required);
      }
    }
    return this.formControlsFix[controlName];
  }

  getFormControlVar(index: number, campo: string): FormControl {
    const controlName = `${campo}_var_${index}`;  
    if (!this.formControlsVar[controlName]) {
      if(campo === "nome"){
        this.formControlsVar[controlName] = this.formBuilder.control('');
      } else {
        this.formControlsVar[controlName] = this.formBuilder.control('', Validators.required);
      }
    }
    return this.formControlsVar[controlName];
  }

  async criarModFixo(dadoNovo: DadoCompostoSala, mod: ModificadorFixo): Promise<void> {
    return new Promise((resolve, reject) => {
      this.client
        .post<ModificadorFixo>(`https://localhost:7151/dicenamics/modificadores/fixo/criar`, mod)
        .subscribe({
          next: (modCriado) => {
            if (modCriado.modificadorFixoId != undefined) {
              dadoNovo.fixosId.push(modCriado.modificadorFixoId);
              resolve();
            }
          },
          error: (error) => {
            console.log(error);
            reject();
          }
        });
    });
  }

  async criarModVar(dadoNovo: DadoCompostoSala, mod: ModificadorVariavel): Promise<void> {
    return new Promise((resolve, reject) => {
      this.client
        .post<ModificadorVariavel>(`https://localhost:7151/dicenamics/modificadores/variavel/criar/${mod.dadoSimplesId}`, mod)
        .subscribe({
          next: (modCriado) => {
            if (modCriado.modificadorVariavelId != undefined) {
              dadoNovo.variaveisId.push(modCriado.modificadorVariavelId);
              resolve();
            }
          },
          error: (error) => {
            console.log(error);
            reject();
          }
        });
    });
  }

  async criarDadoSimplesMod(mod: ModificadorVariavel) : Promise<void> {
    return new Promise((resolve, reject) => {
      this.client
      .put<DadoSimplesSala>(`https://localhost:7151/dicenamics/sala/adicionar/dado/simples/${this.appComponent.usuario.usuarioId}`, mod.dado)
      .subscribe({
        next: (dadoSimplesDB) => {
          mod.dadoSimplesId = dadoSimplesDB.dadoSimplesSalaId;
          resolve()
        },
        error: (error) => {
          console.log(error);
          reject();
        }
      })
    })
  }

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

  async criarDadoSala(){
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

          // for (let index = 0; index < this.modFixQuantidade.length; index++) {    
          //   const nome = this.formControlsFix[`nome_fix_${index}`].value?.toString() || '';
          //   const valor = parseInt(this.formControlsFix[`valor_fix_${index}`].value?.toString() || '0', 10);
          //   let modF : ModificadorFixo = {
          //     nome : nome,
          //     valor : valor
          //   }
      
          //   if(modF.valor != null && modF.valor != 0 && modF.nome != undefined){
          //     await this.criarModFixo(dadoCriar, modF)
          //   }
          // }
      
          // for (let index = 0; index < this.modVarQuantidade.length; index++) {
          //   const nome = this.formControlsVar[`nome_var_${index}`].value?.toString() || '';
          //   const dadoString = this.formControlsVar[`dado_var_${index}`].value?.toString() || ''
          //   const padrao = /^(\d+)d(\d+)$/;
            
          //   const dadoFormat = dadoString.match(padrao)
      
          //   if(dadoFormat != undefined && parseInt(dadoFormat[2], 10) != null && parseInt(dadoFormat[1], 10) != null){
          //     let die : DadoSimplesSala = {
          //       nome: nome,
          //       faces: parseInt(dadoFormat[2], 10),
          //       quantidade: parseInt(dadoFormat[1], 10),
          //       acessoPrivado: false,
          //       criadorUsername: this.appComponent.pegarUser()?.username || ""
          //     }
        
          //     let modV : ModificadorVariavel = {
          //       nome : nome,
          //       dado : die
          //     }
        
          //     if(modV.dado != null && modV.nome != undefined){
          //       await this.criarDadoSimplesMod(modV)
          //       await this.criarModVar(dadoCriar, modV)
          //     }
          //   }
          // }

  
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
