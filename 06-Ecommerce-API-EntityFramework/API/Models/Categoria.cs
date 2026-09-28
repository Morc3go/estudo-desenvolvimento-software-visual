using System.ComponentModel.DataAnnotations;

// RELACIONAMENTO 1:N -> uma Categoria tem vários Produtos.
public class Categoria
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [StringLength(50)]
    public string Nome { get; set; } = string.Empty;
}
