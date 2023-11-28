import { DadoCompostoSala } from "./dadoCompostoSala.models"
import { DadoSimplesSala } from "./dadoSimplesSala.models"
import { SalaUsuario } from "./salaUsuario.models"
import { Usuario } from "./usuario.models"

export interface Sala {
    salaId? : number
    nome : string
    descricao : string
    idSimples? : number
    idLink? : string
    usuarioMestreId : number
    usuarioMestre? : Usuario
    convidadosId? : number[]
    convidados? : SalaUsuario[]
    dadosSimplesSala? : DadoSimplesSala[]
    dadosSimplesSalaId : number[]
    dadosCompostosSala? : DadoCompostoSala[]
    dadosCompostosSalaId : number[]
}