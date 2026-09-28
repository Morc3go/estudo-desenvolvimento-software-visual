public class Professor : Pessoa, IAvaliador
{
    public string Disciplina { get; set; } = string.Empty;

    public Professor(string nome, int idade, string disciplina) : base(nome, idade)
    {
        Disciplina = disciplina;
    }

    public override string Apresentar() => $"Sou o professor {Nome}, de {Disciplina}.";

    // Implementação obrigatória do contrato IAvaliador
    public double Avaliar(Aluno aluno) => aluno.Nome.Length % 2 == 0 ? 10 : 8;
}
