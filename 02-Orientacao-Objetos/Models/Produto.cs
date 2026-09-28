// Mesma classe usada na API do Ecommerce (aula de 24/08).
// Id é gerado automaticamente com Guid; CriadoEm recebe a data atual.
public class Produto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; } = string.Empty;
    public double Preco { get; set; }
    public int Quantidade { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;

    public double ValorEmEstoque() => Preco * Quantidade;
}
