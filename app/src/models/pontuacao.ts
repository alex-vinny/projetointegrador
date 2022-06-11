export interface Pontuacao {
    tipoJogo: string;
    usuario: string;
    dataJogo: Date;
    qtdItems: number; //Qtd itens jogados
    qtdPontos: number //Qtd pontos feitos;
    qtdErros: number //Qtd pontos erros;
}
