import { DadoComposto } from "./dadoComposto.models"
import { DadoSimples } from "./dadoSimples.models"

export interface Usuario {
    usuarioId? : number,
    username : string,
    nickname : string,
    senha : string,
    dadosSimplesPessoaisIds? : number[],
    dadosSimplesPessoais? : DadoSimples[]
    dadosCompostosPessoaisIds? : number[]
    dadosCompostosPessoais? : DadoComposto[]
}