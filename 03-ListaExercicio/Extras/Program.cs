// =====================================================================
// EXERCÍCIOS EXTRAS RESOLVIDOS (estilo da lista da disciplina)
// Treino de vetores, laços, métodos e lógica — base para a prova.
// Rodar:  dotnet run --project 03-ListaExercicio/Extras
// =====================================================================
Console.Clear();
Random random = new Random(42); // semente fixa: sempre gera os mesmos números

int[] vetor = new int[10];
for (int i = 0; i < vetor.Length; i++)
{
    vetor[i] = random.Next(100);
}
Console.WriteLine("Vetor: " + string.Join(" ", vetor));

// ---------------------------------------------------------------
// E1) Maior, menor, soma e média SEM usar LINQ
// ---------------------------------------------------------------
int maior = vetor[0], menor = vetor[0], soma = 0;
foreach (int v in vetor)
{
    if (v > maior) maior = v;
    if (v < menor) menor = v;
    soma += v;
}
double media = (double)soma / vetor.Length; // cast evita divisão inteira
Console.WriteLine($"E1) maior={maior} menor={menor} soma={soma} media={media:F2}");

// ---------------------------------------------------------------
// E2) Contar pares e ímpares
// ---------------------------------------------------------------
int pares = 0, impares = 0;
for (int i = 0; i < vetor.Length; i++)
{
    if (vetor[i] % 2 == 0) pares++; else impares++;
}
Console.WriteLine($"E2) pares={pares} impares={impares}");

// ---------------------------------------------------------------
// E3) Inverter o vetor (trocando as pontas)
// ---------------------------------------------------------------
int[] invertido = (int[])vetor.Clone();
for (int i = 0; i < invertido.Length / 2; i++)
{
    int aux = invertido[i];
    invertido[i] = invertido[invertido.Length - 1 - i];
    invertido[invertido.Length - 1 - i] = aux;
}
Console.WriteLine("E3) invertido: " + string.Join(" ", invertido));

// ---------------------------------------------------------------
// E4) Busca linear: posição de um valor (ou -1)
// ---------------------------------------------------------------
int procurado = vetor[3];
Console.WriteLine($"E4) {procurado} está na posição {BuscaLinear(vetor, procurado)}; 1000 está na posição {BuscaLinear(vetor, 1000)}");

// ---------------------------------------------------------------
// E5) Bubble Sort (igual ao Exe06a) e Selection Sort
// ---------------------------------------------------------------
int[] bubble = (int[])vetor.Clone();
BubbleSort(bubble);
Console.WriteLine("E5) bubble:    " + string.Join(" ", bubble));

int[] selection = (int[])vetor.Clone();
SelectionSort(selection);
Console.WriteLine("E5) selection: " + string.Join(" ", selection));

int[] nativo = (int[])vetor.Clone();
Array.Sort(nativo); // igual ao Exe06b
Console.WriteLine("E5) Array.Sort:" + string.Join(" ", nativo));

// ---------------------------------------------------------------
// E6) Busca binária (só funciona em vetor ORDENADO)
// ---------------------------------------------------------------
Console.WriteLine($"E6) busca binária de {bubble[7]}: posição {BuscaBinaria(bubble, bubble[7])}");

// ---------------------------------------------------------------
// E7) Remover duplicados usando List
// ---------------------------------------------------------------
List<int> semRepetidos = new List<int>();
foreach (int v in new[] { 1, 2, 2, 3, 1, 4 })
{
    if (!semRepetidos.Contains(v)) semRepetidos.Add(v);
}
Console.WriteLine("E7) sem repetidos: " + string.Join(" ", semRepetidos));

// ---------------------------------------------------------------
// E8) Matriz 3x3: soma da diagonal principal
// ---------------------------------------------------------------
int[,] matriz = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
int diagonal = 0;
for (int l = 0; l < matriz.GetLength(0); l++)
{
    for (int c = 0; c < matriz.GetLength(1); c++)
    {
        if (l == c) diagonal += matriz[l, c];
    }
}
Console.WriteLine($"E8) diagonal principal = {diagonal}");

// ---------------------------------------------------------------
// E9) Palíndromo e contagem de vogais (strings)
// ---------------------------------------------------------------
string palavra = "Arara";
string minuscula = palavra.ToLower();
string reversa = new string(minuscula.Reverse().ToArray());
int vogais = minuscula.Count(ch => "aeiou".Contains(ch));
Console.WriteLine($"E9) {palavra}: palíndromo={minuscula == reversa}, vogais={vogais}");

// ---------------------------------------------------------------
// E10) Fatorial (laço) e Fibonacci
// ---------------------------------------------------------------
Console.WriteLine($"E10) 5! = {Fatorial(5)} | Fibonacci(10): {string.Join(",", Fibonacci(10))}");

// ======================= MÉTODOS =======================
static int BuscaLinear(int[] v, int alvo)
{
    for (int i = 0; i < v.Length; i++)
    {
        if (v[i] == alvo) return i;
    }
    return -1;
}

static void BubbleSort(int[] v)
{
    bool troca;
    do
    {
        troca = false;
        for (int i = 0; i < v.Length - 1; i++)
        {
            if (v[i] > v[i + 1])
            {
                (v[i], v[i + 1]) = (v[i + 1], v[i]); // troca com tupla
                troca = true;
            }
        }
    } while (troca);
}

static void SelectionSort(int[] v)
{
    for (int i = 0; i < v.Length - 1; i++)
    {
        int posMenor = i;
        for (int j = i + 1; j < v.Length; j++)
        {
            if (v[j] < v[posMenor]) posMenor = j;
        }
        (v[i], v[posMenor]) = (v[posMenor], v[i]);
    }
}

static int BuscaBinaria(int[] v, int alvo)
{
    int inicio = 0, fim = v.Length - 1;
    while (inicio <= fim)
    {
        int meio = (inicio + fim) / 2;
        if (v[meio] == alvo) return meio;
        if (v[meio] < alvo) inicio = meio + 1; else fim = meio - 1;
    }
    return -1;
}

static long Fatorial(int n)
{
    long resultado = 1;
    for (int i = 2; i <= n; i++) resultado *= i;
    return resultado;
}

static List<int> Fibonacci(int n)
{
    List<int> seq = new List<int> { 0, 1 };
    while (seq.Count < n) seq.Add(seq[^1] + seq[^2]); // ^1 = último
    return seq;
}
