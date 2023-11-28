import { DadoCompostoSalaModFixo } from "./dadoCompostoSalaModFixo.models"
import { DadoCompostoSalaModVar } from "./dadoCompostoSalaModVar.models"
import { Usuario } from "./usuario.models"

export interface DadoCompostoSala {
    dadoCompostoSalaId? : number
    acessoPrivado : boolean
    criador? : Usuario
    criadorUsername : string
    criadorId? : number
    faces : number
    nome : string
    quantidade : number
    condicao : string
    fixos? : DadoCompostoSalaModFixo[]
    fixosId : number[]
    variaveis? : DadoCompostoSalaModVar[]
    variaveisId : number[]
}