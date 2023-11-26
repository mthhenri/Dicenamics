import { DadoSimplesSala } from "./dadoSimplesSala.models"
import { ModificadorFixo } from "./modificadorFixo.models"

export interface DadoCompostoSalaModFixo {
    conectDadoVarId? : number
    dadoId? : number
    dadoCompostoSala? : DadoSimplesSala
    modificadorId : number
    modificadorFixo : ModificadorFixo
}