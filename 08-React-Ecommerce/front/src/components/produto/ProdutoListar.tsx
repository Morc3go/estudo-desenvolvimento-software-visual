import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../../services/api";
import type { Produto } from "../../models/Produto";

// READ + DELETE do CRUD
function ProdutoListar() {
  const [produtos, setProdutos] = useState<Produto[]>([]);
  const [mensagem, setMensagem] = useState<string>("");

  async function carregar() {
    try {
      const resposta = await api.get<Produto[]>("/api/produto/listar");
      setProdutos(resposta.data);
      setMensagem("");
    } catch (erro: any) {
      // A API do professor devolve 400 quando a lista está vazia
      setProdutos([]);
      setMensagem(erro.response?.data ?? "Não foi possível conectar à API. Ela está rodando?");
    }
  }

  // useEffect com [] vazio: roda UMA vez, quando o componente aparece na tela.
  // É o lugar certo para buscar dados na API.
  useEffect(() => {
    carregar();
  }, []);

  async function remover(id: string) {
    if (!confirm("Deseja remover este produto?")) return;
    try {
      await api.delete(`/api/produto/remover/${id}`);
      carregar(); // recarrega a lista depois de remover
    } catch (erro: any) {
      setMensagem(erro.response?.data ?? "Erro ao remover");
    }
  }

  return (
    <div>
      <h2>Produtos</h2>
      {mensagem && <p className="aviso">{mensagem}</p>}
      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Valor</th>
            <th>Criado em</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {/* map transforma cada produto em uma linha. "key" identifica cada item para o React. */}
          {produtos.map((produto) => (
            <tr key={produto.id}>
              <td>{produto.nome}</td>
              <td>R$ {produto.valor.toFixed(2)}</td>
              <td>{new Date(produto.criadoEm!).toLocaleString("pt-BR")}</td>
              <td>
                <Link to={`/produto/alterar/${produto.id}`}>Alterar</Link>{" "}
                <button onClick={() => remover(produto.id!)}>Remover</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ProdutoListar;
