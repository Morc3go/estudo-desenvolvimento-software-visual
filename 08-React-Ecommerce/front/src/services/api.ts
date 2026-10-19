import axios from "axios";

// Instância do axios com o endereço da API (Properties/launchSettings.json do C#).
// Assim cada componente chama só api.get("/api/produto/listar").
const api = axios.create({
  baseURL: "http://localhost:5195",
});

export default api;

// O mesmo GET com fetch (nativo do navegador, sem biblioteca):
// const resposta = await fetch("http://localhost:5195/api/produto/listar");
// const dados = await resposta.json();
