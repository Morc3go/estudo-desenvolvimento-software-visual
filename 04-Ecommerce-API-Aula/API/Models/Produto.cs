public class Produto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; } = String.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}