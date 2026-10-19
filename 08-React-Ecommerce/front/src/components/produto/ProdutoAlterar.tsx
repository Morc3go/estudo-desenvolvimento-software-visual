import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../../services/api";
import type { Produto } from "../../models/Produto";
import { validarProduto } from "./validacao";

// UPDATE do CRUD: carrega o produto pelo id da URL e envia um PUT
function ProdutoAlterar() {
  const { id } = useParams(); // pega o {id} da rota /produto/alterar/:id
  const [nome, setNome] = useState<string>("");
  const [valor, setValor] = useState<string>("");
  const [erros, setErros] = useState<Record<string, string>>({});
  const [mensagem, setMensagem] = useState<string>("");
  const navigate = useNavigate();

  // Dependência [id]: roda de novo se o id da URL mudar
  useEffect(() => {
    api
      .get<Produto>(`/api/produto/buscarid/${id}`)
      .then((resposta) => {
        setNome(resposta.data.nome);
        setValor(String(resposta.data.valor));
      })
      .catch(() => setMensagem("Produto não encontrado"));
  }, [id]);

  async function enviar(e: React.FormEvent) {
    e.preventDefault();
    const produto: Produto = { nome, valor: parseFloat(valor) };
    const errosEncontrados = validarProduto(produto);
    setErros(errosEncontrados);
    if (Object.keys(errosEncontrados).length > 0) return;

    try {
      await api.put(`/api/produto/alterar/${id}`, produto);
      navigate("/");
    } catch (erro: any) {
      setMensagem(erro.response?.data ?? "Erro ao alterar");
    }
  }

  return (
    <div>
      <h2>Alterar produto</h2>
      {mensagem && <p className="aviso">{mensagem}</p>}
      <form onSubmit={enviar} noValidate>
        <label>
          Nome
          <input value={nome} onChange={(e) => setNome(e.target.value)} />
        </label>
        {erros.nome && <span className="erro">{erros.nome}</span>}

        <label>
          Valor
          <input type="number" step="0.01" value={valor} onChange={(e) => setValor(e.target.value)} />
        </label>
        {erros.valor && <span className="erro">{erros.valor}</span>}

        <button type="submit">Salvar</button>
      </form>
    </div>
  );
}

export default ProdutoAlterar;
