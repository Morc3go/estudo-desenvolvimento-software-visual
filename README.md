# Estudos — Desenvolvimento de Software Visual (2026/2)

Material de estudo da disciplina **Desenvolvimento de Software Visual** (Ciência da Computação, 4º semestre,
prof. Diogo Steinke Deconto), organizado por aula: os códigos vistos em sala, os exercícios resolvidos,
conteúdo extra e explicações: C#, Orientação a Objetos, Minimal APIs e Entity Framework.
Atualizado com o repositório do professor até o commit **"API - CRUD completo com banco de dados" (05/10)**.

📄 **Resumo para a prova:** [`docs/Resumo-Prova-A2.pdf`](docs/Resumo-Prova-A2.pdf)  
🗓️ **Aula a aula pelos commits do professor:** [`docs/historico-das-aulas.md`](docs/historico-das-aulas.md)

## Conteúdo

| Pasta | Aula | O que tem |
|---|---|---|
| [`01-Fundamentos-CSharp`](01-Fundamentos-CSharp/Program.cs) | 10/08 | Tipos, conversões, operadores, if/switch, laços, vetores, `List<T>`, métodos, LINQ, exceções |
| [`02-Orientacao-Objetos`](02-Orientacao-Objetos) | 17/08 | Classe, objeto, atributos, getters/setters, propriedades, construtores, herança, polimorfismo, interface, relacionamento entre classes |
| [`03-ListaExercicio`](03-ListaExercicio) | — | Exe06a (Bubble Sort manual) e Exe06b (`Array.Sort`) da aula **comentados**, mais 10 exercícios extras resolvidos |
| [`04-Ecommerce-API-Aula`](04-Ecommerce-API-Aula/API/Program.cs) | 31/08–21/09 | **Código do professor** (commit de 21/09): Minimal API em memória com listar, cadastrar, validações e busca por nome |
| [`05-Ecommerce-API-CRUD`](05-Ecommerce-API-CRUD/API/Program.cs) | 28/09 | **Exercício resolvido**: remoção (DELETE) e alteração (PUT/PATCH), com CRUD completo em lista |
| [`06-Ecommerce-API-EntityFramework`](06-Ecommerce-API-EntityFramework/API) | extra | Versão ampliada com **EF Core + SQLite**: Data Annotations, relacionamento 1:N (Categoria), PATCH, filtro com LINQ |
| [`07-Ecommerce-API-Aula-EF`](07-Ecommerce-API-Aula-EF) | 28/09–05/10 | **Código final do professor**: CRUD completo com banco (EF Core + SQLite), 2 migrations, com explicação do que mudou |
| [`docs/`](docs) | — | Resumo em PDF (teoria, pegadinhas, questões com gabarito) e histórico das aulas |

## Como rodar

### 1. Pré-requisitos
- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0). Confira com `dotnet --version`.
- [VS Code](https://code.visualstudio.com/) com as extensões **C# Dev Kit** e **REST Client** (para os arquivos `.http`).
- Só para o projeto 06: a ferramenta de migrations
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### 2. Projetos de console (01, 02, 03)
```bash
dotnet build EstudosDSV.sln                       # compila todos os consoles
dotnet run --project 01-Fundamentos-CSharp
dotnet run --project 02-Orientacao-Objetos
dotnet run --project 03-ListaExercicio/Exe06a
dotnet run --project 03-ListaExercicio/Exe06b
dotnet run --project 03-ListaExercicio/Extras
```

### 3. APIs (04, 05, 06)
Todas sobem em `http://localhost:5195`, então rode **uma por vez**.
```bash
cd 05-Ecommerce-API-CRUD/API
dotnet run
```
Depois abra `Tests/Produto.http` no VS Code e clique em **Send Request** acima de cada requisição.
Também dá para usar Postman, Insomnia ou `curl`:
```bash
curl http://localhost:5195/api/produto/listar
curl -X POST http://localhost:5195/api/produto/cadastrar -H "Content-Type: application/json" -d '{"nome":"Teclado","preco":150,"quantidade":5}'
```

Para as APIs com banco (06 e 07), crie o banco antes de rodar:
```bash
cd 07-Ecommerce-API-Aula-EF/API      # ou 06-Ecommerce-API-EntityFramework/API
dotnet ef database update     # aplica a migration e cria o arquivo Ecommerce.db
dotnet run
```

## Como os projetos foram criados (CLI)
```bash
dotnet new sln -n Ecommerce                 # 1 - cria a solução
cd Ecommerce                                # 2 - entra na pasta
dotnet new web -o API                       # 3 - cria o projeto (Minimal API vazia)
dotnet sln add API                          # 4 - vincula o projeto à solução
dotnet new console -o Exe06a                # projeto de console
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add Inicial            # gera a migration a partir das classes
dotnet ef database update                   # aplica no banco
```

## Origem
Os códigos das pastas `03-ListaExercicio/Exe06a`, `Exe06b`, `04-Ecommerce-API-Aula` e `07-Ecommerce-API-Aula-EF` são os do repositório da
disciplina ([dideconto/2026-desenvolvimento-software-visual-segunda](https://github.com/dideconto/2026-desenvolvimento-software-visual-segunda)).
O resto (projetos 01, 02, 05, 06, os extras e a documentação) é material complementar de estudo.
O arquivo `Ecommerce.db` do professor não foi copiado: ele é recriado com `dotnet ef database update`.
Todos os projetos foram compilados e as APIs testadas endpoint por endpoint com o .NET 8.
