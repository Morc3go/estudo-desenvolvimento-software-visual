// =====================================================================
// 01 - FUNDAMENTOS DE C# (aula de 10/08 - ambientação ao C#)
// Rodar:  dotnet run --project 01-Fundamentos-CSharp
// =====================================================================
// "Top-level statements": desde o .NET 6 não é preciso escrever
// class Program { static void Main() { ... } } — o compilador gera isso.
// "ImplicitUsings" (no .csproj) já importa System, System.Collections.Generic,
// System.Linq etc. Por isso usamos Console, List e Random sem "using".

Console.Clear();

// ---------------------------------------------------------------------
// 1) VARIÁVEIS E TIPOS
// ---------------------------------------------------------------------
int idade = 20;                 // inteiro
double altura = 1.75;           // ponto flutuante (padrão para decimais)
decimal preco = 19.90m;         // "m" = decimal, ideal para dinheiro
float peso = 70.5f;             // "f" = float
bool ativo = true;              // verdadeiro / falso
char letra = 'A';               // um caractere, aspas SIMPLES
string nome = "Maria";          // texto, aspas DUPLAS
var cidade = "Curitiba";        // var: o compilador deduz o tipo (string)
const double PI = 3.14159;      // constante: não pode ser alterada

// Interpolação de string: $"...{variavel}..."
Console.WriteLine($"Nome: {nome}, idade: {idade}, altura: {altura}, cidade: {cidade}");
Console.WriteLine($"Preço: {preco:C2} | peso: {peso} | ativo: {ativo} | letra: {letra} | PI: {PI}");

// ---------------------------------------------------------------------
// 2) CONVERSÕES (ler do teclado sempre devolve string)
// ---------------------------------------------------------------------
string textoNumero = "42";
int convertido = int.Parse(textoNumero);           // lança exceção se inválido
bool ok = int.TryParse("abc", out int resultado);  // não lança: devolve false
Console.WriteLine($"Parse: {convertido} | TryParse(\"abc\"): {ok} -> {resultado}");
double dobro = Convert.ToDouble("2,5".Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture) * 2;
Console.WriteLine($"Convert.ToDouble: {dobro}");
// Para ler do usuário:  string? entrada = Console.ReadLine();

// ---------------------------------------------------------------------
// 3) OPERADORES
// ---------------------------------------------------------------------
int a = 17, b = 5;
Console.WriteLine($"{a}+{b}={a + b} | {a}-{b}={a - b} | {a}*{b}={a * b}");
Console.WriteLine($"{a}/{b}={a / b} (divisão INTEIRA) | {a}%{b}={a % b} (resto)");
Console.WriteLine($"{a}/(double){b}={a / (double)b}");
Console.WriteLine($"a>b && b>0: {a > b && b > 0} | a<b || b==5: {a < b || b == 5} | !ativo: {!ativo}");

// ---------------------------------------------------------------------
// 4) CONDICIONAIS
// ---------------------------------------------------------------------
double nota = 7.5;
if (nota >= 7)
{
    Console.WriteLine("Aprovado");
}
else if (nota >= 4)
{
    Console.WriteLine("Exame");
}
else
{
    Console.WriteLine("Reprovado");
}

string situacao = nota >= 6 ? "Passou" : "Não passou"; // operador ternário
Console.WriteLine(situacao);

int diaSemana = 2;
switch (diaSemana)
{
    case 1: Console.WriteLine("Domingo"); break;
    case 2: Console.WriteLine("Segunda"); break;
    default: Console.WriteLine("Outro dia"); break;
}

// ---------------------------------------------------------------------
// 5) LAÇOS DE REPETIÇÃO
// ---------------------------------------------------------------------
for (int i = 1; i <= 3; i++)            // quando sei quantas vezes
{
    Console.Write($"for {i} | ");
}
Console.WriteLine();

int contador = 0;
while (contador < 3)                    // testa ANTES de executar
{
    Console.Write($"while {contador} | ");
    contador++;
}
Console.WriteLine();

int x = 10;
do                                      // executa PELO MENOS uma vez
{
    Console.WriteLine($"do-while executou com x = {x}");
} while (x < 5);

// ---------------------------------------------------------------------
// 6) VETORES (ARRAYS) - tamanho FIXO
// ---------------------------------------------------------------------
int[] numeros = new int[5];             // 5 posições, índices 0..4, iniciam em 0
int[] primos = { 2, 3, 5, 7, 11 };      // já inicializado
numeros[0] = 10;
Console.WriteLine($"numeros.Length = {numeros.Length}, primos[4] = {primos[4]}");

Random random = new Random();
for (int i = 0; i < numeros.Length; i++)
{
    numeros[i] = random.Next(100);      // 0..99
}
Console.WriteLine("Vetor aleatório: " + string.Join(", ", numeros));

foreach (int p in primos)               // percorre sem índice (somente leitura)
{
    Console.Write(p + " ");
}
Console.WriteLine();

// ---------------------------------------------------------------------
// 7) LISTAS (List<T>) - tamanho DINÂMICO (usado na API!)
// ---------------------------------------------------------------------
List<string> frutas = new List<string>();
frutas.Add("Maçã");
frutas.Add("Banana");
frutas.Add("Uva");
frutas.Remove("Banana");
Console.WriteLine($"Frutas ({frutas.Count}): {string.Join(", ", frutas)}");
Console.WriteLine($"Contém Uva? {frutas.Contains("Uva")}");

// ---------------------------------------------------------------------
// 8) MÉTODOS (funções) - declarados no fim do arquivo
// ---------------------------------------------------------------------
Console.WriteLine($"Soma(2,3) = {Somar(2, 3)}");
Console.WriteLine($"Média = {CalcularMedia(new double[] { 8, 6, 10 })}");
Saudar("Diogo");

// ---------------------------------------------------------------------
// 9) LINQ + LAMBDA (aparece no Entity Framework)
// ---------------------------------------------------------------------
List<int> valores = new List<int> { 5, 12, 8, 21, 3, 14 };
List<int> pares = valores.Where(v => v % 2 == 0).ToList();       // filtra
List<int> ordenados = valores.OrderBy(v => v).ToList();            // ordena
int primeiroMaiorQue10 = valores.FirstOrDefault(v => v > 10);      // 1º que satisfaz (ou 0)
Console.WriteLine($"Pares: {string.Join(",", pares)} | Ordenados: {string.Join(",", ordenados)}");
Console.WriteLine($"Primeiro > 10: {primeiroMaiorQue10} | Soma: {valores.Sum()} | Máx: {valores.Max()} | Qtd>10: {valores.Count(v => v > 10)}");

// ---------------------------------------------------------------------
// 10) TRATAMENTO DE EXCEÇÕES
// ---------------------------------------------------------------------
try
{
    int zero = 0;
    Console.WriteLine(10 / zero);
}
catch (DivideByZeroException e)
{
    Console.WriteLine($"Erro tratado: {e.Message}");
}
finally
{
    Console.WriteLine("finally sempre executa");
}

// ===== Métodos locais (precisam vir depois do código de topo) =====
static int Somar(int n1, int n2)
{
    return n1 + n2;
}

static double CalcularMedia(double[] notas)
{
    double soma = 0;
    foreach (double n in notas) soma += n;
    return soma / notas.Length;
}

static void Saudar(string pessoa) => Console.WriteLine($"Olá, {pessoa}!"); // expression-bodied
