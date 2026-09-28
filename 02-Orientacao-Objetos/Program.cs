// =====================================================================
// 02 - ORIENTAÇÃO A OBJETOS EM C# (aula de 17/08)
// Classe, atributos, getters e setters, objetos, construtores,
// encapsulamento, herança, polimorfismo, interface e relacionamento.
// Rodar:  dotnet run --project 02-Orientacao-Objetos
// =====================================================================
Console.Clear();

// ---- Criando OBJETOS (instâncias) ----
Pessoa p1 = new Pessoa();                  // construtor vazio
p1.Nome = "Ana";                           // set da propriedade
p1.Idade = 17;
p1.SetCpf("12345678901");                  // setter "estilo Java"
Console.WriteLine($"{p1.Nome} | CPF {p1.GetCpf()} | maior de idade? {p1.MaiorDeIdade}");

Pessoa p2 = new Pessoa("Bruno", 30);       // construtor com parâmetros
Pessoa p3 = new Pessoa { Nome = "Carla", Idade = 25 }; // inicializador de objeto
Console.WriteLine(p2.Apresentar());
Console.WriteLine(p3);                     // chama ToString()

// ---- Encapsulamento: validação no set ----
try
{
    p2.Idade = -5;
}
catch (ArgumentException e)
{
    Console.WriteLine($"Erro: {e.Message}");
}

// ---- Herança e Polimorfismo ----
Aluno aluno1 = new Aluno("Diego", 21, "2026001");
Aluno aluno2 = new Aluno("Elisa", 22, "2026002");
Professor prof = new Professor("Diogo", 35, "Desenvolvimento de Software Visual");

List<Pessoa> pessoas = new List<Pessoa> { p2, aluno1, prof };
foreach (Pessoa p in pessoas)
{
    // O método chamado depende do tipo REAL do objeto (polimorfismo)
    Console.WriteLine(p.Apresentar());
}

// ---- Interface ----
IAvaliador avaliador = prof;
Console.WriteLine($"Nota de {aluno1.Nome}: {avaliador.Avaliar(aluno1)}");

// ---- Relacionamento entre classes ----
Turma turma = new Turma { Codigo = "DSV-2026-2", Professor = prof };
turma.Matricular(aluno1);
turma.Matricular(aluno2);
turma.Matricular(aluno1);                  // duplicado: bloqueado
Console.WriteLine($"Turma {turma.Codigo} - prof. {turma.Professor?.Nome} - {turma.QuantidadeAlunos()} alunos");
foreach (Aluno a in turma.Alunos)
{
    Console.WriteLine($"  - {a.Matricula}: {a.Nome}");
}

// ---- Referência x valor ----
Pessoa ref1 = new Pessoa("Original", 10);
Pessoa ref2 = ref1;                        // copia a REFERÊNCIA, não o objeto
ref2.Nome = "Alterado";
Console.WriteLine($"ref1.Nome = {ref1.Nome}  (classes são tipos por referência)");

// ---- Produto (o mesmo modelo da API) ----
Produto produto = new Produto { Nome = "Mouse", Preco = 50, Quantidade = 3 };
Console.WriteLine($"{produto.Id} | {produto.Nome} | R$ {produto.Preco} x {produto.Quantidade} = {produto.ValorEmEstoque()} | {produto.CriadoEm}");
