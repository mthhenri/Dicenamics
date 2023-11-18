import { DadoSimples } from "./dadoSimples.models"

export interface ModificadorVariavel {
    modificadorVariavelId? : number
    nome? : string
    dadoSimplesId? : number
    dado : DadoSimples
}