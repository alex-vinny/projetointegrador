import { Categoria } from "./categoria";
import { Usuario } from "./usuario";

export interface Palavra {
    // categoria: string;
    // dica: string;
    // dicaSemAcento: string;
    // id: number;
    // serieEscolar: string
    // valor: string
    // valorSemAcento: string;

    id: number;
    palavra: string;
    palavraSemAcento: string;
    dica: string;
    dicaSemAcento: string;
    serieEscolar?: any;
    categoriaId: number;
    categoria: string;
}
