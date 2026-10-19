// COMPONENTE = função que devolve JSX (HTML dentro do JavaScript).
// PROPS = parâmetros que o componente PAI passa para o FILHO (somente leitura).
interface SaudacaoProps {
  nome: string;
  turma?: string; // opcional
}

function Saudacao({ nome, turma = "DSV" }: SaudacaoProps) {
  return (
    <p>
      Olá, <strong>{nome}</strong>! Bem-vindo à turma {turma}.
    </p>
  );
}

export default Saudacao;
