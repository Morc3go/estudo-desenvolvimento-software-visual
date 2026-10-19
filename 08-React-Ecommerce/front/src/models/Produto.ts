// Mesmo formato do JSON que a API em C# devolve.
// O C# usa PascalCase (Nome), mas o JSON sai em camelCase (nome).
export interface Produto {
  id?: string;        // "?" = opcional: no cadastro quem gera é a API
  nome: string;
  valor: number;
  criadoEm?: string;
}
