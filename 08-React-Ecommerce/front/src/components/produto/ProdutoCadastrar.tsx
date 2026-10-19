import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../../services/api";
import type { Produto } from "../../models/Produto";
import { validarProduto } from "./validacao";

// CREATE do CRUD, com validação de formulário
function ProdutoCadastrar() {
  const [nome, setNome] = useState<string>("");
  const [valor, setValor] = useState<string>("");
  const [erros, setErros] = useState<Record<string, string>>({});
  const [mensagem, setMensagem] = useState<string>("");
  const navigate = useNavigate();

  async function enviar(e: React.FormEvent) {
    e.preventDefault(); // impede o navegador de recarregar a página no submit

    const produto: Produto = { nome, valor: parseFloat(valor) };
    const errosEncontrados = validarProduto(produto);
    setErros(errosEncontrados);
    if (Object.keys(errosEncontrados).length > 0) return;

    try {
      // POST com o objeto no corpo: o axios converte para JSON
      await api.post("/api/produto/cadastrar", produto);
      navigate("/"); // volta para a listagem
    } catch (erro: any) {
      // Ex.: 400 "Esse produto ja existe!" vindo da API
      setMensagem(erro.response?.data ?? "Erro ao cadastrar");
    }
  }

  return (
    <div>
      <h2>Cadastrar produto</h2>
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

        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
}

export default ProdutoCadastrar;
