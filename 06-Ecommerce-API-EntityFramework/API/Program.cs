// =====================================================================
// 06 - ECOMMERCE API COM ENTITY FRAMEWORK CORE + SQLITE (aulas 14 e 21/09)
// Os dados agora ficam num BANCO (arquivo Ecommerce.db) e sobrevivem
// quando a API é reiniciada.
//
// Pacotes (já no API.csproj):
//   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
//   dotnet add package Microsoft.EntityFrameworkCore.Design
// Ferramenta de migrations:
//   dotnet tool install --global dotnet-ef
// Criar/atualizar o banco (rodar dentro da pasta API):
//   dotnet ef migrations add NomeDaMigracao
//   dotnet ef database update
// =====================================================================
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// INJEÇÃO DE DEPENDÊNCIA: registra o contexto; cada requisição recebe
// uma instância de AppDataContext como parâmetro do endpoint.
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapGet("/", () => "API do Ecommerce - Entity Framework");

// ------------------------- PRODUTOS ---------------------------------

//GET: /api/produto/listar
app.MapGet("/api/produto/listar", ([FromServices] AppDataContext ctx) =>
{
    // Include = JOIN: traz a categoria junto (carregamento adiantado)
    List<Produto> produtos = ctx.Produtos.Include(p => p.Categoria).ToList();
    if (produtos.Count == 0)
    {
        return Results.NotFound("A lista de produtos está vazia");
    }
    return Results.Ok(produtos);
});

//GET: /api/produto/buscar/{id}
app.MapGet("/api/produto/buscar/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    // Find busca pela CHAVE PRIMÁRIA
    Produto? produto = ctx.Produtos.Find(id);
    return produto is null ? Results.NotFound("Produto não encontrado!") : Results.Ok(produto);
});

//GET: /api/produto/filtrar?precoMaximo=100   (LINQ -> vira SQL WHERE/ORDER BY)
app.MapGet("/api/produto/filtrar", ([FromQuery] double precoMaximo, [FromServices] AppDataContext ctx) =>
{
    List<Produto> baratos = ctx.Produtos
        .Where(p => p.Preco <= precoMaximo)
        .OrderBy(p => p.Preco)
        .ToList();
    return Results.Ok(baratos);
});

//POST: /api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext ctx) =>
{
    // Minimal APIs NÃO validam Data Annotations sozinhas: validamos manualmente
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(produto, new ValidationContext(produto), erros, true))
    {
        return Results.BadRequest(erros);
    }

    if (ctx.Produtos.Any(p => p.Nome == produto.Nome))
    {
        return Results.Conflict("Já existe um produto com o mesmo nome");
    }

    if (produto.CategoriaId is not null && ctx.Categorias.Find(produto.CategoriaId) is null)
    {
        return Results.NotFound("Categoria não encontrada!");
    }

    ctx.Produtos.Add(produto);  // marca para INSERT
    ctx.SaveChanges();          // executa o SQL de fato
    return Results.Created($"/api/produto/buscar/{produto.Id}", produto);
});

//PUT: /api/produto/alterar/{id}
app.MapPut("/api/produto/alterar/{id}", ([FromRoute] string id, [FromBody] Produto produtoAlterado, [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }

    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(produtoAlterado, new ValidationContext(produtoAlterado), erros, true))
    {
        return Results.BadRequest(erros);
    }

    produto.Nome = produtoAlterado.Nome;
    produto.Preco = produtoAlterado.Preco;
    produto.Quantidade = produtoAlterado.Quantidade;
    produto.CategoriaId = produtoAlterado.CategoriaId;

    ctx.Produtos.Update(produto); // marca para UPDATE
    ctx.SaveChanges();
    return Results.Ok(produto);
});

//PATCH: /api/produto/estoque/{id}?quantidade=10
app.MapPatch("/api/produto/estoque/{id}", ([FromRoute] string id, [FromQuery] int quantidade, [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    if (quantidade < 0)
    {
        return Results.BadRequest("Quantidade não pode ser negativa");
    }
    produto.Quantidade = quantidade;
    ctx.SaveChanges(); // o EF rastreia a mudança (change tracking)
    return Results.Ok(produto);
});

//DELETE: /api/produto/remover/{id}
app.MapDelete("/api/produto/remover/{id}", ([FromRoute] string id, [FromServices] AppDataContext ctx) =>
{
    Produto? produto = ctx.Produtos.Find(id);
    if (produto is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    ctx.Produtos.Remove(produto); // marca para DELETE
    ctx.SaveChanges();
    return Results.Ok(produto);
});

// ------------------------- CATEGORIAS -------------------------------

app.MapGet("/api/categoria/listar", ([FromServices] AppDataContext ctx) =>
    Results.Ok(ctx.Categorias.ToList()));

app.MapPost("/api/categoria/cadastrar", ([FromBody] Categoria categoria, [FromServices] AppDataContext ctx) =>
{
    List<ValidationResult> erros = new List<ValidationResult>();
    if (!Validator.TryValidateObject(categoria, new ValidationContext(categoria), erros, true))
    {
        return Results.BadRequest(erros);
    }
    ctx.Categorias.Add(categoria);
    ctx.SaveChanges();
    return Results.Created($"/api/categoria/listar", categoria);
});

app.Run();
