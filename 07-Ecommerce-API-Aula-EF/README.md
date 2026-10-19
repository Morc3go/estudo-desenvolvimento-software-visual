# 07 — API do Ecommerce com banco de dados (código final do professor)

Cópia fiel da pasta `Ecommerce/` do repositório da disciplina, na versão do commit
**"API - CRUD completo com banco de dados" (05/10)**. Só o arquivo `Ecommerce.db` ficou de fora: ele é gerado
pelas migrations (veja abaixo).

## Como rodar

```bash
cd 07-Ecommerce-API-Aula-EF/API
dotnet tool install --global dotnet-ef   # só na primeira vez
dotnet ef database update                # cria o Ecommerce.db aplicando as 2 migrations
dotnet run                               # http://localhost:5195
```

Depois use `Tests/Produto.http`. Os ids de DELETE e PUT nesse arquivo são de exemplo. Cadastre um produto,
copie o `id` da resposta e cole nas requisições.

## O que mudou em relação à versão em lista (pasta 04/05)

| Antes (lista em memória) | Agora (banco SQLite com EF Core) |
|---|---|
| `List<Produto> produtos = new List<Produto>();` | `builder.Services.AddDbContext<AppDataContext>();` + `[FromServices] AppDataContext ctx` |
| `produtos.Count == 0` | `ctx.Produtos.Count() == 0` |
| `produtos` | `ctx.Produtos.ToList()` |
| `foreach` procurando nome repetido | `ctx.Produtos.FirstOrDefault(x => x.Nome == produto.Nome)` |
| `produtos.FirstOrDefault(x => x.Id == id)` | `ctx.Produtos.Find(id)` (busca pela chave primária) |
| `produtos.Add(produto)` | `ctx.Produtos.Add(produto); ctx.SaveChanges();` |
| `produtos.Remove(p)` | `ctx.Produtos.Remove(p); ctx.SaveChanges();` |
| alterar a propriedade e pronto | alterar + `ctx.Produtos.Update(p); ctx.SaveChanges();` |
| parâmetros sem atributo | `[FromRoute]`, `[FromBody]`, `[FromServices]` explícitos (`using Microsoft.AspNetCore.Mvc;`) |
| dados somem ao parar a API | dados ficam no arquivo `Ecommerce.db` |

## Os 5 passos da configuração com o banco (comentário de `Data/AppDataContext.cs`)

1. **Instalar as bibliotecas**
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
   dotnet add package Microsoft.EntityFrameworkCore.Design
   ```
2. **Criar a classe de dados**: `Data/AppDataContext.cs`.
3. **Criar a herança com a biblioteca**: `public class AppDataContext : DbContext`.
4. **Indicar as classes de modelo que vão virar tabelas**: `public DbSet<Produto> Produtos { get; set; }`.
5. **Sobrescrever o método de configuração** com o banco usado e a string de conexão:
   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       optionsBuilder.UseSqlite("Data Source=Ecommerce.db");
   }
   ```

Depois disso: registrar no `Program.cs` (`builder.Services.AddDbContext<AppDataContext>();`), gerar a migration e
aplicar no banco.

## As duas migrations: o modelo mudou, então nova migration

| Migration | O que faz |
|---|---|
| `InitialCreate` | `CREATE TABLE Produtos (Id TEXT PK, Nome TEXT, CriadoEm TEXT)` |
| `AddValorTableProduto` | `ALTER TABLE Produtos ADD Valor REAL NOT NULL DEFAULT 0.0` |

A segunda nasceu porque a propriedade `public double Valor { get; set; }` foi **acrescentada** em `Produto`.
O fluxo é sempre:

```bash
# 1) altera a classe de modelo
dotnet ef migrations add AddValorTableProduto   # 2) gera o código da mudança (Up/Down)
dotnet ef database update                       # 3) aplica no banco
```

Cada migration tem `Up()` (aplica a mudança) e `Down()` (desfaz). O arquivo `AppDataContextModelSnapshot.cs` guarda
o "retrato" atual do modelo, e é por comparação com ele que o EF sabe o que mudou na próxima migration.

## Detalhes do código do professor que valem atenção

- O comentário acima do `MapPut` diz `//DELETE: /api/produto/alterar/...`, mas o endpoint é **PUT**. É só um
  comentário copiado.
- Produto duplicado responde **400 Bad Request** ("Esse produto ja existe!"). Na pasta 06 usei **409 Conflict**, que
  também é aceito.
- O `Program.cs` ainda declara a `List<Produto> produtos`, mas ela não é mais usada: sobrou da versão em memória.
- O campo de preço se chama **`Valor`** (na pasta 06 eu usei `Preco`).
