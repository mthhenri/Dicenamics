import { DadoCompostoSala } from "./dadoCompostoSala.models"
import { Sala } from "./sala.models"

export interface RolagemDadoSala {
    rolagemDadoSalaId : number
    roladoEm : Date
    usuarioUsername : string
    resultados : string
    resultadosList : number[][]
    tipoRolagem : string
    dadoId : number
    dadoComposto : DadoCompostoSala
    salaId : number
    sala : Sala
}