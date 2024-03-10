# Task API

## Descrição
Esta é uma Web API construída em C# utilizando o framework .NET 8. Ela lida com operações relacionadas a usuários e tarefas. Cada usuário pode ter várias tarefas associadas a ele. O acesso aos dados do banco é realizado através do Entity Framework, e as migrações de banco de dados são gerenciadas com o FluentMigrator. A validação das entidades é feita utilizando FluentValidator, e exceções são tratadas de forma apropriada, não retornando detalhes sensíveis ou específicos do banco de dados.

## Funcionalidades Principais
- Gerenciamento de usuários
- Gerenciamento de tarefas
- Suporte a múltiplas linguagens (português e inglês)
- Restrições de permissões de usuário (por exemplo, usuários só podem modificar ou deletar suas próprias tarefas)
- Testes automatizados para validações, casos de uso e endpoints da Web API

## Tecnologias Utilizadas
- C#/.NET 8
- Entity Framework
- FluentMigrator
- FluentValidator

## Estrutura do Projeto
- **Domain/Entities**: Classes que representam entidades do banco de dados.
- **Infra/RepositoryAccess**: Repositórios que implementam interfaces IUpdateOnly, IReadOnly e IWriteOnly para cada entidade.
- **Application/UseCases**: Lógica de negócio e validação.
- **Controllers**: Endpoints da API.

## Configuração
1. Clone este repositório.
2. Configure sua conexão de banco de dados no arquivo de configuração.
3. Apos configurado, quando rodar o projeto ele ja ira criar o banco de dados caso não exista.
4. Compile e execute o projeto.

## Endpoints da API
![image](https://github.com/viniciustravenssoli/Tasks/assets/91105588/e9563f94-ee74-4636-9f02-fd42352911f8)

## Testes
- Os testes unitários estão localizados no diretório de testes.
- Execute os testes utilizando uma ferramenta de teste de sua escolha.


