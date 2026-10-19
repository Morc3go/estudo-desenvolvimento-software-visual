import { BrowserRouter, Link, Route, Routes } from "react-router-dom";
import ProdutoListar from "./components/produto/ProdutoListar";
import ProdutoCadastrar from "./components/produto/ProdutoCadastrar";
import ProdutoAlterar from "./components/produto/ProdutoAlterar";
import Contador from "./components/exemplos/Contador";
import Saudacao from "./components/exemplos/Saudacao";

// SPA (Single Page Application): uma única página HTML. O React Router troca
// o componente exibido conforme a URL, sem recarregar a página.
function App() {
  return (
    <BrowserRouter>
      <nav>
        <Link to="/">Listar</Link>
        <Link to="/produto/cadastrar">Cadastrar</Link>
        <Link to="/exemplos">Exemplos</Link>
      </nav>
      <main>
        <Routes>
          <Route path="/" element={<ProdutoListar />} />
          <Route path="/produto/cadastrar" element={<ProdutoCadastrar />} />
          <Route path="/produto/alterar/:id" element={<ProdutoAlterar />} />
          <Route
            path="/exemplos"
            element={
              <>
                <Saudacao nome="Matheus" />
                <Contador />
              </>
            }
          />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;
