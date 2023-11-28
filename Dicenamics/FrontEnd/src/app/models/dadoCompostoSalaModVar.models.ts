import { DadoCompostoSala } from "./dadoCompostoSala.models"
import { ModificadorVariavel } from "./modificadorVariavel.models"

export interface DadoCompostoSalaModVar {
    conectDadoVarId? : number
    dadoId? : number
    dadoCompostoSala? : DadoCompostoSala
    modificadorId : number
    modificadorVariavel : ModificadorVariavel
}