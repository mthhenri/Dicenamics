import { DadoCompostoModFixo } from "./dadoCompostoModFixo.models"

export interface DadoComposto {
    dadoId? : number
    nome : string
    faces : number
    quantidade : number 
    condicao : string
    fixosId : number[]
    fixos? : DadoCompostoModFixo[]
    variaveis : number[]
}