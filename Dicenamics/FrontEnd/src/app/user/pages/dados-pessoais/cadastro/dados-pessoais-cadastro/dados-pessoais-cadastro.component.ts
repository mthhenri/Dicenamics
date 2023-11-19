import { DadosPessoaisComponent } from 'src/app/user/pages/dados-pessoais/dados-pessoais.component';
import { DadoComposto } from 'src/app/models/dadoComposto.models';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { ModificadorFixo } from 'src/app/models/modificadorFixo.models';
import { DadoCompostoModFixo } from 'src/app/models/dadoCompostoModFixo.models';
import { ModificadorVariavel } from 'src/app/models/modificadorVariavel.models';
import { DadoSimples } from 'src/app/models/dadoSimples.models';

@Component({
  selector: 'app-dados-pessoais-cadastro',
  templateUrl: './dados-pessoais-cadastro.component.html',
  styleUrls: ['./dados-pessoais-cadastro.component.css']
})
export class DadosPessoaisCadastroComponent {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public appComponent: AppComponent,
    private client: HttpClient,
    private snackBar: MatSnackBar,
    public dadosPessoais: DadosPessoaisComponent,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(){
    this.dadoEditarId = 0
    this.route.queryParams.subscribe(params => {
      this.dadoEditarId = params['dadoId']
    })
    if(this.dadoEditarId != 0 && this.dadoEditarId != undefined){
      this.acaoDado = "Salvar Alterações"
      this.acao = "Editar"
      this.client
        .get<DadoComposto>(`https://localhost:7151/dicenamics/dados/composto/buscar/${this.dadoEditarId}`)
        .subscribe({
          next: async (dado) => {
            this.nome.setValue(dado.nome)
            this.dado.setValue(`${dado.quantidade}d${dado.faces}`)
            // this.faces.setValue(dado.faces.toString())
            // this.quantidade.setValue(dado.quantidade.toString())
            this.condicao.setValue(dado.condicao)
            await this.formModAddEditFix(dado);
            await this.formModAddEditVar(dado);
          },
          error: (error) => {
            console.log(error)
          }
        })
    } else {
      this.acaoDado = "Criar Dado"
      this.acao = "Criar"
    }
  }

  voltarPg(){
    this.router.navigate(["dicenamics/dados"])
  }

  nome = new FormControl('', Validators.required)
  dado = new FormControl('', Validators.required)
  // faces = new FormControl('', Validators.required)
  // quantidade = new FormControl('', Validators.required)
  condicao = new FormControl('')
  acaoDado! : String
  acao! : String
  dadoEditarId : number = 0
  dadoEditar! : DadoComposto
  modFixQuantidade : number[] = [0]
  modVarQuantidade : number[] = [0]
  modFixos : ModificadorFixo[] = []
  modVars : ModificadorVariavel[] = []
  formControlsFix: { [key: string]: FormControl } = {};
  formControlsVar: { [key: string]: FormControl } = {};

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

  async formModAddEditFix(dado : DadoComposto) : Promise<void> {
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

  async formModAddEditVar(dado : DadoComposto) : Promise<void> {
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

  async criarModFixo(dadoNovo: DadoComposto, mod: ModificadorFixo): Promise<void> {
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

  async criarModVar(dadoNovo: DadoComposto, mod: ModificadorVariavel): Promise<void> {
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
      .put<DadoSimples>(`https://localhost:7151/dicenamics/usuario/adicionarDado/simples/${this.appComponent.usuario.usuarioId}`, mod.dado)
      .subscribe({
        next: (dadoSimplesDB) => {
          mod.dadoSimplesId = dadoSimplesDB.dadoId;
          resolve()
        },
        error: (error) => {
          console.log(error);
          reject();
        }
      })
    })
  }
  

  async criarDado(){
    let nome : string | undefined = this.nome.value?.toString() 
    // let f : string | undefined = this.faces.value?.toString()
    // let q : string | undefined = this.quantidade.value?.toString() 
    let dadoCString = this.dado.value?.toString() || ''
    const padrao = /^(\d+)d(\d+)$/;      
    const dadoFormat = dadoCString.match(padrao)
    let condicao : string | undefined = this.condicao.value?.toString() 
    let userId : number | undefined = this.appComponent.pegarUser()?.usuarioId
    let dadoNovo! : DadoComposto

    if(nome != undefined && dadoFormat != undefined && condicao != undefined && userId != undefined){
      // let faces = parseInt(f)
      // let quantidade = parseInt(q)
      if(parseInt(dadoFormat[2], 10) > 1 || parseInt(dadoFormat[1], 10) > 0){
        dadoNovo = {
          dadoId : 0,
          nome : nome,
          faces : parseInt(dadoFormat[2], 10),
          quantidade : parseInt(dadoFormat[1], 10),
          condicao : condicao,
          fixosId : [],
          variaveisId : []
        }
      } else {
        return
      }      
    } else {
      console.log(dadoNovo)
      return
    }

    for (let index = 0; index < this.modFixQuantidade.length; index++) {    
      const nome = this.formControlsFix[`nome_fix_${index}`].value?.toString() || '';
      const valor = parseInt(this.formControlsFix[`valor_fix_${index}`].value?.toString() || '0', 10);
      let modF : ModificadorFixo = {
        nome : nome,
        valor : valor
      }

      if(modF.valor != null && modF.valor != 0 && modF.nome != undefined){
        await this.criarModFixo(dadoNovo, modF)
      }
    }

    for (let index = 0; index < this.modVarQuantidade.length; index++) {
      const nome = this.formControlsVar[`nome_var_${index}`].value?.toString() || '';
      const dadoString = this.formControlsVar[`dado_var_${index}`].value?.toString() || ''
      const padrao = /^(\d+)d(\d+)$/;
      
      const dadoFormat = dadoString.match(padrao)

      if(dadoFormat != undefined && parseInt(dadoFormat[2], 10) != null && parseInt(dadoFormat[1], 10) != null){
        let die : DadoSimples = {
          nome : nome,
          faces : parseInt(dadoFormat[2], 10),
          quantidade : parseInt(dadoFormat[1], 10)
        }
  
        let modV : ModificadorVariavel = {
          nome : nome,
          dado : die
        }
  
        if(modV.dado != null && modV.nome != undefined){
          await this.criarDadoSimplesMod(modV)
          await this.criarModVar(dadoNovo, modV)
        }
      }
    }
    
    this.client
      .put<DadoComposto>(`https://localhost:7151/dicenamics/usuario/adicionarDado/composto/${this.appComponent.usuario.usuarioId}`, dadoNovo)
      .subscribe({
        next: (dado) => {
          this.dadosPessoais.ngOnInit()
          this.router.navigate(["dicenamics/dados"])
          this.snackBar.open(`O dado ${dadoNovo.nome} foi criado!`, 'Beleza!', {
            duration: 1750,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
          }) 
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  async editarDado(){
    let nome : string | undefined = this.nome.value?.toString() 
    // let f : string | undefined = this.faces.value?.toString()
    // let q : string | undefined = this.quantidade.value?.toString() 
    let dadoCString = this.dado.value?.toString() || ''
    const padrao = /^(\d+)d(\d+)$/;      
    const dadoFormat = dadoCString.match(padrao)
    let condicao : string | undefined = this.condicao.value?.toString()

    if(nome != undefined && dadoFormat != undefined && condicao != undefined){
      // let faces = parseInt(f)
      // let quantidade = parseInt(q)

      if(parseInt(dadoFormat[2], 10) > 1 || parseInt(dadoFormat[1], 10) > 0){
        this.dadoEditar = {
          dadoId : 0,
          nome : nome,
          faces : parseInt(dadoFormat[2], 10),
          quantidade : parseInt(dadoFormat[1], 10),
          condicao : condicao,
          fixosId : [],
          variaveisId : []
        }
      } else {
        return
      }

      for (let index = 0; index < this.modFixQuantidade.length; index++) {    
        const nome = this.formControlsFix[`nome_fix_${index}`].value?.toString() || '';
        const valor = parseInt(this.formControlsFix[`valor_fix_${index}`].value?.toString() || '0', 10);
        let mod : ModificadorFixo = {
          nome : nome,
          valor : valor
        }
  
        if(mod.valor != null && mod.valor != 0 && mod.nome != undefined){
          await this.criarModFixo(this.dadoEditar, mod)
        }
      }

      for (let index = 0; index < this.modVarQuantidade.length; index++) {
        if(this.formControlsVar[`dado_var_${index}`].value != undefined && this.formControlsVar[`dado_var_${index}`].value != ""){
          const nome = this.formControlsVar[`nome_var_${index}`].value?.toString() || '';
          const dadoString = this.formControlsVar[`dado_var_${index}`].value?.toString() || ''
          const padrao = /^(\d+)d(\d+)$/;
          
          const dadoFormat = dadoString.match(padrao)
    
          let die : DadoSimples = {
            nome : nome,
            faces : parseInt(dadoFormat[2], 10),
            quantidade : parseInt(dadoFormat[1], 10)
          }
    
          let modV : ModificadorVariavel = {
            nome : nome,
            dado : die
          }
    
          if(modV.dado != null && modV.nome != undefined){
            await this.criarDadoSimplesMod(modV)
            await this.criarModVar(this.dadoEditar, modV)
          }
        }
      }

      this.client
        .put<DadoComposto>(`https://localhost:7151/dicenamics/dados/composto/atualizar/${this.dadoEditarId}`, this.dadoEditar)
        .subscribe({
          next: (dadoAtualizado) => {
            this.router.navigate(["dicenamics/dados"])
            this.snackBar.open(`O dado ${this.dadoEditar.nome} foi editado!`, 'Beleza!', {
              duration: 1750,
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            })
          },
          error: (error) => {
            console.log(error)
            if(error.staus === 400){
              console.log("BAD REQUEST")
            }
          }
        })

    } else {
      console.log(this.dadoEditar, "erro")
    }
    
  }
}
