# Histórico das aulas pelo repositório do professor

Linha do tempo dos commits de [dideconto/2026-desenvolvimento-software-visual-segunda](https://github.com/dideconto/2026-desenvolvimento-software-visual-segunda),
com o que cada aula acrescentou e onde está neste repositório de estudos.

| Data | Commit | O que entrou | Onde estudar aqui |
|---|---|---|---|
| 24/08 | Lista de Exercícios de Ambientação | `ListaExercicio/Exe06a` (Bubble Sort manual) e `Exe06b` (`Array.Sort`) | `03-ListaExercicio` |
| 31/08 | API - Listagem de produtos | Solução `Ecommerce`, projeto `API` (`dotnet new web`), `Produto` com getters/setters comentados no estilo Java × propriedades C#, `GET /api/produto/listar`, arquivo `.http` | `02-Orientacao-Objetos`, `04-Ecommerce-API-Aula` |
| 14/09 | API - Cadastrar Produto | `POST /api/produto/cadastrar` recebendo `Produto` pelo corpo (JSON) | `04-Ecommerce-API-Aula` |
| 21/09 | API - Validações e busca de produto | Validações (nulo, nome vazio, nome repetido → 400), `GET /api/produto/buscar/{nome}` → 404 | `04-Ecommerce-API-Aula` (é exatamente este commit) |
| 28/09 | API - CRUD completo em lista | `DELETE /api/produto/remover/{id}`, `PUT /api/produto/alterar/{id}`, busca com **lambda** `FirstOrDefault(x => x.Nome == nome)` | `05-Ecommerce-API-CRUD` |
| 28/09 | API - Configuração com o banco de dados | Pacotes do EF Core + SQLite, `Data/AppDataContext.cs`, migration `InitialCreate`, `AddDbContext` | `07-Ecommerce-API-Aula-EF` |
| 05/10 | API - CRUD completo com banco de dados | Endpoints passam a usar `ctx` (`[FromServices]`), `Find`, `Add`/`Update`/`Remove` + `SaveChanges`, propriedade `Valor` e migration `AddValorTableProduto` | `07-Ecommerce-API-Aula-EF` |

## Evolução de um mesmo endpoint: buscar por nome

```csharp
// 21/09 — foreach na lista
app.MapGet("/api/produto/buscar/{nome}", (string nome) =>
{
    foreach (Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == nome)
            return Results.Ok(produtoCadastrado);
    }
    return Results.NotFound("Produto não encontrado!");
});

// 28/09 — expressão lambda na lista
Produto? produtoEncontrado = produtos.FirstOrDefault(x => x.Nome == nome);

// 05/10 — expressão lambda no banco (o EF traduz para SQL: WHERE Nome = @nome LIMIT 1)
app.MapGet("/api/produto/buscar/{nome}",
    ([FromRoute] string nome, [FromServices] AppDataContext ctx) =>
{
    Produto? produtoEncontrado = ctx.Produtos.FirstOrDefault(x => x.Nome == nome);
    if (produtoEncontrado is null)
        return Results.NotFound("Produto não encontrado!");
    return Results.Ok(produtoEncontrado);
});
```

## Evolução do alterar (PUT)

```csharp
// 28/09 — lista: altera o objeto em memória (é referência, então já altera o que está na lista)
produtoEncontrado.Nome = produtoAlterado.Nome;
return Results.Ok(produtoEncontrado);

// 05/10 — banco: precisa marcar e salvar
produtoEncontrado.Nome = produtoAlterado.Nome;
produtoEncontrado.Valor = produtoAlterado.Valor;
ctx.Produtos.Update(produtoEncontrado);
ctx.SaveChanges();
return Results.Ok(produtoEncontrado);
```

## Próximas aulas (pelo calendário)

Depois da API, o plano de aula segue para **React**: introdução e configuração do ambiente, componentização e estado,
CRUD consumindo a API em C# e validação de formulários. A **Prova A1** está marcada para **23/11**. Quando esses
commits aparecerem no repositório do professor, dá para acrescentar as pastas aqui do mesmo jeito.
