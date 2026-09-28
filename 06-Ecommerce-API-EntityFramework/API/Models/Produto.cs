using System.ComponentModel.DataAnnotations;

// ENTIDADE: cada propriedade vira uma COLUNA da tabela "Produtos".
// Por convenção, a propriedade "Id" vira a CHAVE PRIMÁRIA.
// DATA ANNOTATIONS: atributos entre [] que descrevem regras de validação
// e também influenciam o esquema do banco (ex.: tamanho da coluna).
public class Produto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, 100000, ErrorMessage = "O preço deve estar entre 0,01 e 100.000")]
    public double Preco { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa")]
    public int Quantidade { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.Now;

    // CHAVE ESTRANGEIRA (FK) + PROPRIEDADE DE NAVEGAÇÃO
    public string? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
