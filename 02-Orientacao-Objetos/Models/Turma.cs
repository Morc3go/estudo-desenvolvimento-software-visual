// RELACIONAMENTO ENTRE CLASSES
// Turma "TEM UM" Professor (1:1) e "TEM VÁRIOS" Alunos (1:N).
public class Turma
{
    public string Codigo { get; set; } = string.Empty;
    public Professor? Professor { get; set; }          // associação (pode ser nulo: "?")
    public List<Aluno> Alunos { get; set; } = new();    // coleção (1:N)

    public void Matricular(Aluno aluno)
    {
        if (Alunos.Any(a => a.Matricula == aluno.Matricula))
        {
            Console.WriteLine($"Aluno {aluno.Matricula} já matriculado!");
            return;
        }
        Alunos.Add(aluno);
    }

    public int QuantidadeAlunos() => Alunos.Count;
}
