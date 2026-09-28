# Lista de Exercícios

## Exe06a — ordenar um vetor de 100 números aleatórios *à mão* (Bubble Sort)

Passo a passo (é o mesmo dos comentários do código da aula):
1. Criar um vetor `int[] vetor = new int[100];`
2. Usar um `for` para percorrer as posições
3. Gerar valores com `random.Next(1000)` (0 a 999)
4. Guardar cada valor em `vetor[i]`
5. Percorrer o vetor comparando `vetor[i]` com `vetor[i + 1]`
6. Se o atual for **maior** que o próximo, **trocar** os dois usando uma variável auxiliar
7. Repetir a passada enquanto houver troca (`do { ... } while (troca);`)

**Por que o `for` vai só até `vetor.Length - 1`?** Porque o código acessa `vetor[i + 1]`. Na última posição, `i + 1`
estouraria o vetor (`IndexOutOfRangeException`).

**Por que o `do-while`?** Uma única passada só garante que o **maior** valor foi para o fim. A versão entregue no zip
tinha só um `for`, então o vetor **não** terminava ordenado. Com o `do-while` e a flag `troca`, o laço repete até uma
passada inteira acontecer sem nenhuma troca, ou seja, até o vetor estar ordenado.

A troca sempre usa uma auxiliar:
```csharp
int aux = vetor[i];
vetor[i] = vetor[i + 1];
vetor[i + 1] = aux;
// ou, em C# moderno: (vetor[i], vetor[i + 1]) = (vetor[i + 1], vetor[i]);
```

Complexidade: O(n²) no pior caso. O(n) se o vetor já estiver ordenado (uma passada, nenhuma troca).

## Exe06b — o mesmo, usando o recurso pronto da linguagem

```csharp
Array.Sort(vetor);   // ordena "in place" (altera o próprio vetor), O(n log n)
```

## Extras (`Extras/Program.cs`)

Dez exercícios resolvidos no mesmo estilo: maior/menor/média, pares e ímpares, inverter o vetor, busca linear,
Bubble Sort e Selection Sort, busca binária, remover repetidos com `List`, matriz (diagonal), palíndromo e vogais,
fatorial e Fibonacci.

```bash
dotnet run --project 03-ListaExercicio/Extras
```
