import { Categoria } from "./categoria";
import { Usuario } from "./usuario";

export interface Palavra {
    categoria: string;
    dica: string;
    dicaSemAcento: string;
    id: number;
    serieEscolar: string
    valor: string
    valorSemAcento: string;
}
