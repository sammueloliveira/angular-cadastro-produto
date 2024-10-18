import { TipoProdutoEnum } from "../enums/tipo.produto.enum";

export interface Product {
    id?: number; 
    nome: string; 
    precoDeVenda: number | null; 
    descricao: string; 
    quantidade: number | null; 
    tipo: TipoProdutoEnum; 
    dataDeCadastro: string; 
}
