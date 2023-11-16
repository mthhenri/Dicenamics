import { ModificadorFixo } from "./modificadorFixo.models"

export interface DadoCompostoModFixo {
    [x: string]: any
    modificadodorId : number
    modificadorFixo : ModificadorFixo
}