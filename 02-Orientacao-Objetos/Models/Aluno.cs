// HERANÇA: Aluno "é uma" Pessoa. Herda Nome, Idade, métodos...
public class Aluno : Pessoa
{
    public string Matricula { get; set; } = string.Empty;

    // "base(...)" chama o construtor da classe pai
    public Aluno(string nome, int idade, string matricula) : base(nome, idade)
    {
        Matricula = matricula;
    }

    // POLIMORFISMO: mesmo método, comportamento diferente
    public override string Apresentar()
    {
        return base.Apresentar() + $" Minha matrícula é {Matricula}.";
    }
}
