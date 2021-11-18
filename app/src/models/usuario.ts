export interface Usuario {    
    id: number;
    email: string;
    nome: string;
    perfil: {
        codigo: number,
        descricao: string
    }    
}
