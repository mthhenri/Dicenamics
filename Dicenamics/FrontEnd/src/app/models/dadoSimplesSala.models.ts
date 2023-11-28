import { Usuario } from "./usuario.models"

export interface DadoSimplesSala {
    dadoSimplesSalaId? : number
    acessoPrivado : boolean
    criador? : Usuario
    criadorUsername : string
    criadorId? : number
    faces : number
    nome? : string
    quantidade : number
}