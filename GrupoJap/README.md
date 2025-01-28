# Projeto de Sistema de Aluguel de Viaturas - GrupoJap

Este é um sistema simples de aluguer de veículos, desenvolvido com ASP.NET Core, Entity Framework Core e SQL Server. O sistema permite o registo e gestão de clientes, contratos de aluguer e veículos.

## ✨ Funcionalidades Principais
- **Gestão de Veículos**
  - Registo com marca, modelo, matrícula única e ano de fabrico
  - Status automático (Disponível/Alugado)
  
- **Gestão de Clientes**
  - Registo com nome, email único e validação de telefone
  - Armazenamento de carta de condução

- **Contratos de Aluguer**
  - Associação cliente-veículo
  - Validação de datas de aluguer


## Tecnologias Utilizadas

- **ASP.NET Core**: Framework de desenvolvimento web.
- **Entity Framework Core**: ORM para trabalhar com a base de dados.
- **SQL Server**: Base de dados relacional.
- **Bootstrap**: Framework CSS para o design da interface.
- **Validations Customizadas**: Validações customizadas como a do ano de fabricação das viaturas.


## ✅ Validações Implementadas
### Veículos
- Matrícula única
- Ano de fabrico ≤ ano atual
- Campos obrigatórios

### Clientes
- Email único
- Formato de telefone válido (ex: 912345678)
- Carta de condução obrigatória

### Contratos
- Data início ≥ data atual
- Data fim > data início


## Estrutura do Projeto

O projeto segue uma arquitetura MVC (Model-View-Controller), dividindo o código em três camadas principais:

- **Models**: Contém as entidades que representam as tabelas na base de dados.
- **Controllers**: Contém a lógica de controle das requisições HTTP.
- **Views**: Contém as páginas HTML que são renderizadas no browser.

### Tabelas na Base de Dados

As principais tabelas da base de dados são:

1. **Client (Cliente)**:
   - `ClientId`: Identificador único do cliente.
   - `Name`: Nome do cliente.
   - `Email`: Email do cliente.
   - `PhoneNumber`: Número de telefone do cliente.
   - `DrivingLicense`: Número da carta de condução.

2. **Vehicle (Viatura)**:
   - `VehicleId`: Identificador único da viatura.
   - `Brand`: Marca da viatura.
   - `Model`: Modelo da viatura.
   - `LicensePlate`: Matrícula da viatura.
   - `ManufacturingYear`: Ano de fabricação da viatura.
   - `FuelType`: Tipo de combustível (usando `enum`).
   - `Status`: Estado da viatura (disponível, alugado).

3. **RentalContract (Contrato de Aluguer)**:
   - `RentalContractId`: Identificador único do contrato.
   - `ClientId`: Relacionado ao cliente que fez o contrato.
   - `VehicleId`: Relacionado à viatura alugada.
   - `StartDate`: Data de início do contrato.
   - `EndDate`: Data de fim do contrato.
   - `InitialKilometers`: Quilometragem inicial da viatura.

### Explicação sobre o uso do Enum para `FuelType`

Optei por usar um **enum** para o tipo de combustível (`FuelType`) das viaturas em vez de criar uma tabela separada, devido aos seguintes motivos:

- **Simplicidade**: O número de tipos de combustível é limitado e fixo, como **Gasolina**, **Diesel**, **Elétrico**, etc. Usar um **enum** facilita a manutenção do código e evita a necessidade de uma tabela extra na base de dados.
  
- **Desempenho**: Usar um **enum** no lugar de uma tabela adicional pode melhorar o desempenho, pois não há a sobrecarga de uma consulta extra para obter os tipos de combustível.
  
- **Clareza**: A utilização de um **enum** torna o código mais legível e intuitivo, já que é possível definir diretamente na aplicação os valores possíveis para o tipo de combustível.

O `enum` para o tipo de combustível é definido assim:

```csharp
namespace GrupoJap.Enums
{
    public enum FuelType
    {
        Gasolina,
        Gasóleo,
        Elétrico,
        Híbrido
    }
}

## Como Instalar e Executar o Projeto

Para instalar e executar o projeto no seu ambiente local, siga os passos abaixo.


### Passo 1: Clonar o Repositório

Primeiro, clone o repositório do projeto para a sua máquina local. Abra o terminal ou prompt de comando e execute o comando abaixo:

```bash
git clone https://github.com/Fabioferreira15/GrupoJapDesafio


### Passo 1: Configurar a Base de Dados

O projeto utiliza o SQL Server como base de dados. Para configurar a base de dados, siga os passos abaixo:

1. Abra o arquivo `appsettings.json` na raiz do projeto.
2. Em `ConnectionStrings`, altere a string de conexão para a sua base de dados local.

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR_;Database=VehicleRentalDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. Abra o terminal ou prompt de comando e execute o comando abaixo para criar a base de dados:
```bash
dotnet ef database update


### Passo 3: Restaurar as Dependências do Projeto

Para restaurar as dependências do projeto, abra o terminal ou prompt de comando na pasta raiz do projeto e execute o comando abaixo:
```bash
dotnet restore


### Passo 4: Criar a migração

Para criar a migração, abra o terminal ou prompt de comando na pasta raiz do projeto e execute o comando abaixo:
```bash
dotnet ef migrations add InitialCreate

Ou se usar o Gerenciador de Pacotes do NuGet, execute o comando abaixo:
```bash
Add-Migration InitialCreate

### Passo 5: Atualizar a base de dados

Para aplicar a migração e atualizar a base de dados, execute o comando abaixo:
```bash
dotnet ef database update

Ou se usar o Gerenciador de Pacotes do NuGet, execute o comando abaixo:
```bash
Update-Database



### Passo 5: Executar o Projeto

Após configurar a base de dados, você pode executar o projeto. Abra o terminal ou prompt de comando na pasta raiz do projeto e execute o comando abaixo:
```bash
dotnet run



## Conclusão








