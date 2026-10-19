import type { Produto } from "../../models/Produto";

// Validação no FRONT-END: dá resposta rápida ao usuário.
// Não substitui a validação da API: o back-end SEMPRE valida de novo.
export function validarProduto(produto: Produto): Record<string, string> {
  const erros: Record<string, string> = {};

  if (produto.nome.trim() === "") {
    erros.nome = "O nome é obrigatório";
  } else if (produto.nome.trim().length < 3) {
    erros.nome = "O nome deve ter pelo menos 3 caracteres";
  }

  if (Number.isNaN(produto.valor) || produto.valor <= 0) {
    erros.valor = "O valor deve ser maior que zero";
  }

  return erros;
}
