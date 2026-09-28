//POSTMAN
//INSONMIA
//REST CLIENT - Extensão do VSCODE

//TERMINAL
//1 - Criar solução
//2 - Entrar na pasta da solução
//3 - Criar o projeto
//4 - Vincular o projeto para a solução
// Console.Clear();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new List<Produto>();

//FUNCIONALIDADES - EndPoints
//Requisições
// - Método HTTP
// - URL
// - Opcional - Corpo/Parâmetros de URL

//Resposta
// - Dado/Informação/Mensagem
// - Código de Status HTTP

//GET: http://localhost:5195/
app.MapGet("/", () => "API do Ecommerce");

//GET: /api/produto/listar
app.MapGet("/api/produto/listar", () =>
{
    if (produtos.Count == 0)
    {
        return Results.BadRequest("A lista de produtos está vazia");
    }
    return Results.Ok(produtos);
});

//POST: /api/produto/cadastrar
app.MapPost("/api/produto/cadastrar", (Produto? produto) =>
{

    if (produto is null)
    {
        return Results.BadRequest("O produto não pode ser nulo");
    }

    if (produto.Nome == "")
    {
        return Results.BadRequest("O nome não pode ser vazio");
    }

    foreach (Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == produto.Nome)
        {
            return Results.BadRequest("Já existe um produto com o mesmo nome");
        }
    }
    produtos.Add(produto);
    return Results.Created("", produto);
});

//GET: /api/produto/buscar/nomDe_produto
app.MapGet("/api/produto/buscar/{nome}", (string nome) =>
{
    foreach (Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == nome)
        {
            return Results.Ok(produtoCadastrado);
        }
    }
    return Results.NotFound("Produto não encontrado!");
});

app.Run();

//EXERCÍCIO
// 2 - Remoção de um produto
// 3 - Alteração de produto


