import { DadoCompostoModFixo } from "./dadoCompostoModFixo.models"
import { DadoCompostoModVar } from "./dadoCompostoModVar.models"

export interface DadoComposto {
    dadoId? : number
    nome : string
    faces : number
    quantidade : number 
    condicao : string
    fixosId : number[]
    fixos? : DadoCompostoModFixo[]
    variaveisId : number[]
    variaveis? : DadoCompostoModVar[]
}