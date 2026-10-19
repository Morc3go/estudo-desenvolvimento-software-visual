import { useState } from "react";

// ESTADO (useState): dado que, quando muda, faz o React RE-RENDERIZAR o componente.
// Variável comum não serve: muda o valor, mas a tela não é atualizada.
function Contador() {
  const [contador, setContador] = useState<number>(0);
  const [texto, setTexto] = useState<string>("");

  return (
    <div className="card">
      <h3>Contador: {contador}</h3>
      <button onClick={() => setContador(contador + 1)}>+1</button>
      <button onClick={() => setContador(contador - 1)}>-1</button>
      <button onClick={() => setContador(0)}>Zerar</button>

      {/* Input CONTROLADO: o valor vem do estado e o onChange atualiza o estado */}
      <p>
        <input
          value={texto}
          onChange={(e) => setTexto(e.target.value)}
          placeholder="Digite algo"
        />
      </p>
      <p>Você digitou: {texto} ({texto.length} caracteres)</p>

      {/* Renderização condicional */}
      {contador > 5 && <p className="aviso">Passou de 5!</p>}
      <p>{contador % 2 === 0 ? "Par" : "Ímpar"}</p>
    </div>
  );
}

export default Contador;
