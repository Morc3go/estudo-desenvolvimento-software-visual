// CLASSE = molde/forma. OBJETO = instância criada com "new".
// Atributos (campos) guardam o estado; métodos definem o comportamento.
public class Pessoa
{
    // ATRIBUTO PRIVADO (campo) + GETTER/SETTER "à moda Java"
    // Encapsulamento: ninguém de fora mexe direto em _cpf.
    private string _cpf = string.Empty;

    public string GetCpf()
    {
        return _cpf;
    }

    public void SetCpf(string cpf)
    {
        if (cpf.Length != 11)
        {
            throw new ArgumentException("CPF deve ter 11 dígitos");
        }
        _cpf = cpf;
    }

    // PROPRIEDADE AUTOMÁTICA (jeito C#): o compilador cria o campo escondido.
    public string Nome { get; set; } = string.Empty;

    // PROPRIEDADE COM LÓGICA no set (validação) usando campo de apoio.
    private int _idade;
    public int Idade
    {
        get { return _idade; }
        set
        {
            if (value < 0) throw new ArgumentException("Idade não pode ser negativa");
            _idade = value; // "value" é o valor que está sendo atribuído
        }
    }

    // PROPRIEDADE SOMENTE LEITURA calculada
    public bool MaiorDeIdade => Idade >= 18;

    // CONSTRUTOR vazio (necessário para "new Pessoa { ... }" e para JSON/EF)
    public Pessoa() { }

    // CONSTRUTOR com parâmetros (sobrecarga)
    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }

    // MÉTODO virtual: pode ser sobrescrito (override) nas classes filhas
    public virtual string Apresentar()
    {
        return $"Olá, eu sou {Nome} e tenho {Idade} anos.";
    }

    // Sobrescreve o ToString herdado de object
    public override string ToString() => $"Pessoa({Nome}, {Idade})";
}
