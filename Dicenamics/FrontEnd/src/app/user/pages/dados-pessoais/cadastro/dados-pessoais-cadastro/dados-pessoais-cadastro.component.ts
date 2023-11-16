import { DadosPessoaisComponent } from 'src/app/user/pages/dados-pessoais/dados-pessoais.component';
import { DadoComposto } from 'src/app/models/dadoComposto.models';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { ModificadorFixo } from 'src/app/models/modificadorFixo.models';

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
          next: (dado) => {
            this.nome.setValue(dado.nome)
            this.faces.setValue(dado.faces.toString())
            this.quantidade.setValue(dado.quantidade.toString())
            this.condicao.setValue(dado.condicao)
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

  nome = new FormControl('', Validators.required)
  faces = new FormControl('', Validators.required)
  quantidade = new FormControl('', Validators.required)
  condicao = new FormControl('')
  acaoDado! : String
  acao! : String
  dadoEditarId : number = 0
  dadoEditar! : DadoComposto
  modFixQuantidade : number[] = [0]
  modFixos : ModificadorFixo[] = []
  formControls: { [key: string]: FormControl } = {};

  addModFix(index : number){
    this.modFixQuantidade.push(this.modFixQuantidade[index] + 1)
  }

  removeModFixo(index : number){
    this.modFixQuantidade = this.modFixQuantidade.filter(e => e != index)
  }

  getFormControlFixo(index: number, campo: string): FormControl {
    const controlName = `${campo}_${index}`;  
    if (!this.formControls[controlName]) {
      this.formControls[controlName] = this.formBuilder.control('', Validators.required);
    }
    return this.formControls[controlName];
  }

  async criarModFixo(dadoNovo: DadoComposto, mod: ModificadorFixo): Promise<void> {
    return new Promise((resolve, reject) => {
      this.client
        .post<ModificadorFixo>(`https://localhost:7151/dicenamics/modificadores/fixo/criar`, mod)
        .subscribe({
          next: (modCriado) => {
            if (modCriado.modificadorFixoId != undefined) {
              console.log(modCriado.modificadorFixoId)
              dadoNovo.fixos.push(modCriado.modificadorFixoId);
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
  

  async criarDado(){
    let nome : string | undefined = this.nome.value?.toString() 
    let f : string | undefined = this.faces.value?.toString()
    let q : string | undefined = this.quantidade.value?.toString() 
    let condicao : string | undefined = this.condicao.value?.toString() 
    let userId : number | undefined = this.appComponent.pegarUser()?.usuarioId
    let dadoNovo! : DadoComposto

    if(nome != undefined && f != undefined && q != undefined && condicao != undefined && userId != undefined){
      let faces = parseInt(f)
      let quantidade = parseInt(q)

      dadoNovo = {
        dadoId : 0,
        nome : nome,
        faces : faces,
        quantidade : quantidade,
        condicao : condicao,
        fixos : [],
        variaveis : []
      }
    } else {
      console.log(dadoNovo)
      return
    }

    for (let index = 0; index < this.modFixQuantidade.length; index++) {    
      const nome = this.formControls[`nome_${index}`].value?.toString() || '';
      const valor = parseInt(this.formControls[`valor_${index}`].value?.toString() || '0', 10);
      console.log(nome, valor)
      let mod : ModificadorFixo = {
        nome : nome,
        valor : valor
      }

      if(mod.valor != null && mod.valor != 0 && mod.nome != undefined){
        await this.criarModFixo(dadoNovo, mod)
      }
    }

    console.log(dadoNovo.fixos.toString())
    
    this.client
      .put<DadoComposto>(`https://localhost:7151/dicenamics/usuario/adicionarDado/composto/${this.appComponent.usuario.usuarioId}`, dadoNovo)
      .subscribe({
        next: (dado) => {
          console.log(dado.dadoId)
          this.router.navigate(["dadosPessoais"])
          this.snackBar.open(`O dado ${dadoNovo.nome} foi criado!`, 'Beleza!', {
            duration: 1750,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
          }) 
          this.dadosPessoais.ngOnInit()
          this.router.navigate(["dicenamics/dadosPessoais"])
        },
        error: (error) => {
          console.log(error)
        }
      })
  }

  editarDado(){
    let nome : string | undefined = this.nome.value?.toString() 
    let f : string | undefined = this.faces.value?.toString()
    let q : string | undefined = this.quantidade.value?.toString() 
    let condicao : string | undefined = this.condicao.value?.toString()

    if(nome != undefined && f != undefined && q != undefined && condicao != undefined){
      let faces = parseInt(f)
      let quantidade = parseInt(q)

      this.dadoEditar = {
        nome : nome,
        faces : faces,
        quantidade : quantidade,
        condicao : condicao,
        fixos : [],
        variaveis : []
      }

      this.client
        .put<DadoComposto>(`https://localhost:7151/dicenamics/dados/composto/atualizar/${this.dadoEditarId}`, this.dadoEditar)
        .subscribe({
          next: (dadoAtualizado) => {
            this.router.navigate(["dadosPessoais"])
            this.snackBar.open(`O dado ${this.dadoEditar.nome} foi editado!`, 'Beleza!', {
              duration: 1750,
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            })
            this.router.navigate(["dicenamics/dadosPessoais"])
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
