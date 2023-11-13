export interface Usuario {
    usuarioId : number,
    username : string,
    nickname : string,
    senha : string,
    dadosSimplesPessoaisIds? : number[],
    dadosCompostosPessoaisIds? : number[]
}