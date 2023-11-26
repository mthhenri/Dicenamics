import { DadoCompostoSala } from "./dadoCompostoSala.models"
import { Sala } from "./sala.models"

export interface RolagemDadoSala {
    rolagemDadoSalaId : number
    roladoEm : Date
    usuarioUsername : string
    resultados : string
    resultadosList : number[][]
    TipoRolagem : string
    dadoId : number
    dadoComposto : DadoCompostoSala
    salaId : number
    sala : Sala
}