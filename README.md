# Projeto: Sistema de Faturas e Contratos

Este é um sistema de gestão de faturas e contratos que permite controlar informações sobre contratos de clientes, suas faturas e operadoras de serviços. O sistema é integrado com um banco de dados PostgreSQL, onde são armazenadas as informações de contratos, faturas e operadoras.

## Tecnologias Utilizadas

- **Frontend**: Angular 19, Angular Material, Chart.js
- **Backend**: C# e .NET 6
- **Banco de Dados**: PostgreSQL
- **Bibliotecas**: 
  - ng2-charts
  - Chart.js
  - RxJS
  - HttpClient

## Pré-requisitos

Antes de rodar o projeto, verifique se os seguintes requisitos estão instalados no seu ambiente:

- **Node.js**: [Link para download](https://nodejs.org/)
- **npm ou yarn**: Para instalar pacotes no frontend.
- **.NET SDK 6**: [Link para download](https://dotnet.microsoft.com/download)
- **PostgreSQL**: [Link para download](https://www.postgresql.org/download/)
- **Angular CLI**: Execute `npm install -g @angular/cli` para instalar o Angular CLI globalmente.

---

## Como Rodar o Projeto

### 1. **Clonando o Repositório**

Primeiro, faça o clone do repositório para a sua máquina:

```bash
git clone https://github.com/usuario/seu-projeto.git


2. Configuração do Banco de Dados
2.1 Criando o Banco de Dados no PostgreSQL
Execute o seguinte comando para criar um banco de dados no PostgreSQL:


CREATE DATABASE seu_banco_de_dados;


2.2 Conectando ao Banco de Dados
Conecte-se ao banco de dados recém-criado:

2.3 Criando as Tabelas
Execute os seguintes scripts SQL para criar as tabelas no banco de dados:

-- Table: public.contratos

-- DROP TABLE IF EXISTS public.contratos;

CREATE TABLE IF NOT EXISTS public.contratos
(
    id integer NOT NULL DEFAULT nextval('contratos_id_seq'::regclass),
    nome_filial character varying(100) COLLATE pg_catalog."default" NOT NULL,
    operadora_id integer NOT NULL,
    plano_contratado character varying(100) COLLATE pg_catalog."default" NOT NULL,
    data_inicio date NOT NULL,
    data_vencimento date NOT NULL,
    valor_mensal numeric(18,2) NOT NULL,
    status boolean NOT NULL,
    CONSTRAINT contratos_pkey PRIMARY KEY (id),
    CONSTRAINT fk_operadora FOREIGN KEY (operadora_id)
        REFERENCES public.operadoras_servicos (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.contratos
    OWNER to postgres;


-- Table: public.faturas

-- DROP TABLE IF EXISTS public.faturas;

CREATE TABLE IF NOT EXISTS public.faturas
(
    id integer NOT NULL DEFAULT nextval('faturas_id_seq'::regclass),
    contrato_id integer NOT NULL,
    data_emissao date NOT NULL,
    data_vencimento date NOT NULL,
    valor_cobrado numeric(18,2) NOT NULL,
    status character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT faturas_pkey PRIMARY KEY (id),
    CONSTRAINT fk_contrato FOREIGN KEY (contrato_id)
        REFERENCES public.contratos (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.faturas
    OWNER to postgres;

-- Table: public.operadoras_servicos

-- DROP TABLE IF EXISTS public.operadoras_servicos;

CREATE TABLE IF NOT EXISTS public.operadoras_servicos
(
    id integer NOT NULL DEFAULT nextval('operadoras_servicos_id_seq'::regclass),
    nome_operadora character varying(100) COLLATE pg_catalog."default" NOT NULL,
    tipo_servico character varying(50) COLLATE pg_catalog."default" NOT NULL,
    contato_suporte character varying(100) COLLATE pg_catalog."default",
    CONSTRAINT operadoras_servicos_pkey PRIMARY KEY (id)
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.operadoras_servicos
    OWNER to postgres;


3. Configuração do Backend (C# / .NET 6)
3.1 Conectando o Backend ao Banco de Dados
Configure a string de conexão no arquivo appsettings.json do backend:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=seu_usuario;Password=sua_senha;Database=seu_banco_de_dados;"
  }
}


3.2 Executando o Backend
Dentro da pasta do projeto backend, execute o comando abaixo para rodar a aplicação:

bash
Copiar
Editar
dotnet run
4. Configuração do Frontend (Angular)
4.1 Instalando Dependências
No diretório do frontend, instale as dependências do projeto:

bash
Copiar
Editar
npm install
4.2 Rodando o Frontend
Para rodar o frontend, execute o seguinte comando:

bash
Copiar
Editar
ng serve
O aplicativo estará disponível no endereço http://localhost:4200.

Funcionalidades
Painel Administrativo: Gerenciamento de contratos, faturas e operadoras de serviços.

Gráficos: Visualização de faturas por status (Pagas, Pendentes, Atrasadas) e comparação entre faturas emitidas e pagas.

Autenticação: Controle de acesso com autenticação via tokens JWT.

Contribuições
Se você deseja contribuir para o projeto, por favor, siga as etapas abaixo:

Fork o repositório.

Crie uma branch para a sua feature (git checkout -b feature-nome-da-feature).

Faça o commit das suas alterações (git commit -am 'Adicionando nova feature').

Push para a branch (git push origin feature-nome-da-feature).

Abra um Pull Request.

Licença
Este projeto está licenciado sob a MIT License - veja o arquivo LICENSE para mais detalhes.

pgsql
Copiar
Editar


Agora, você tem o **README.md** com as instruções de como rodar o projeto, as queries para criar o banco de dados e também o processo de configuração do backend e frontend, tudo de forma contínua e pronta para ser colado diretamente no seu repositório!
