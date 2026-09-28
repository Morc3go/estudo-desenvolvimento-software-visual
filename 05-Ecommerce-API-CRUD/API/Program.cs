// =====================================================================
// 05 - ECOMMERCE API COM CRUD COMPLETO (aulas de 31/08 e 01/09)
// Resolução do EXERCÍCIO da aula: "2 - Remoção de um produto" e
// "3 - Alteração de produto". Dados guardados EM MEMÓRIA (List<Produto>):
// ao parar a API, tudo é perdido -> por isso depois vem o Entity Framework.
//
// Rodar:  dotnet run --project 05-Ecommerce-API-CRUD/API
// Testar: abrir Tests/Produto.http no VS Code (extensão REST Client)
// =====================================================================
//
// CRUD x HTTP x Status
//  Create -> POST   -> 201 Created
//  Read   -> GET    -> 200 OK / 404 Not Found
//  Update -> PUT    -> 200 OK   (substitui o recurso inteiro)
//         -> PATCH  -> 200 OK   (altera só parte do recurso)
//  Delete -> DELETE -> 200 OK / 204 No Content
//  Erro de validação do cliente -> 400 Bad Request

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// "Banco de dados" em memória, com dados iniciais para facilitar os testes
List<Produto> produtos = new List<Produto>
{
    new Produto { Nome = "Mouse", Preco = 50, Quantidade = 10 },
    new Produto { Nome = "Monitor", Preco = 900, Quantidade = 3 },
};

//GET: /
app.MapGet("/", () => "API do Ecommerce - CRUD em memória");

//GET: /api/produto/listar
app.MapGet("/api/produto/listar", () =>
{
    if (produtos.Count == 0)
    {
        return Results.BadRequest("A lista de produtos está vazia");
    }
    return Results.Ok(produtos);
});

//GET: /api/produto/buscar/{nome}   (parâmetro de ROTA)
app.MapGet("/api/produto/buscar/{nome}", (string nome) =>
{
    // Versão com LINQ + lambda do foreach feito em aula
    Produto? resultado = produtos.FirstOrDefault(p => p.Nome == nome);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(resultado);
});

//GET: /api/produto/buscarid/{id}
app.MapGet("/api/produto/buscarid/{id}", (string id) =>
{
    Produto? resultado = produtos.FirstOrDefault(p => p.Id == id);
    return resultado is null ? Results.NotFound("Produto não encontrado!") : Results.Ok(resultado);
});

//POST: /api/produto/cadastrar      (dados no CORPO, em JSON)
app.MapPost("/api/produto/cadastrar", (Produto? produto) =>
{
    if (produto is null)
    {
        return Results.BadRequest("O produto não pode ser nulo");
    }
    if (string.IsNullOrWhiteSpace(produto.Nome))
    {
        return Results.BadRequest("O nome não pode ser vazio");
    }
    if (produtos.Any(p => p.Nome == produto.Nome))
    {
        return Results.BadRequest("Já existe um produto com o mesmo nome");
    }
    produtos.Add(produto);
    return Results.Created($"/api/produto/buscarid/{produto.Id}", produto);
});

//DELETE: /api/produto/remover/{id}   -> EXERCÍCIO 2
app.MapDelete("/api/produto/remover/{id}", (string id) =>
{
    Produto? resultado = produtos.FirstOrDefault(p => p.Id == id);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    produtos.Remove(resultado);
    return Results.Ok(resultado); // ou Results.NoContent() (204, sem corpo)
});

//PUT: /api/produto/alterar/{id}      -> EXERCÍCIO 3
// PUT = substitui TODOS os campos editáveis pelo que veio no corpo
app.MapPut("/api/produto/alterar/{id}", (string id, Produto produtoAlterado) =>
{
    Produto? resultado = produtos.FirstOrDefault(p => p.Id == id);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    if (string.IsNullOrWhiteSpace(produtoAlterado.Nome))
    {
        return Results.BadRequest("O nome não pode ser vazio");
    }
    // Id e CriadoEm NÃO mudam
    resultado.Nome = produtoAlterado.Nome;
    resultado.Preco = produtoAlterado.Preco;
    resultado.Quantidade = produtoAlterado.Quantidade;
    return Results.Ok(resultado);
});

//PATCH: /api/produto/estoque/{id}    -> alteração PARCIAL (só a quantidade)
app.MapPatch("/api/produto/estoque/{id}", (string id, int quantidade) =>
{
    // "quantidade" não está na rota nem é objeto -> vem da QUERY STRING:
    // /api/produto/estoque/{id}?quantidade=5
    Produto? resultado = produtos.FirstOrDefault(p => p.Id == id);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    if (quantidade < 0)
    {
        return Results.BadRequest("Quantidade não pode ser negativa");
    }
    resultado.Quantidade = quantidade;
    return Results.Ok(resultado);
});

app.Run();
