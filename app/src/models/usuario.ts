export interface Usuario {
    usuario: {
        id: number;
        email: string;
        nome: string;
        perfil: {
            codigo: number,
            descricao: string
        }
    }
}
