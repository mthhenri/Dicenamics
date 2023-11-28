import { Sala } from "./sala.models"
import { Usuario } from "./usuario.models"

export interface SalaUsuario {
    salaUsuarioId : number
    salaId : number
    sala : Sala
    usuarioId : number
    usuario : Usuario
}