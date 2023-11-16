import { ModificadorFixo } from "./modificadorFixo.models"

export interface DadoComposto {
    dadoId? : number
    nome : string
    faces : number
    quantidade : number 
    condicao : string
    fixos : number[]
    variaveis : number[]
}