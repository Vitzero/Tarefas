### Entidades

Tarefa:
    -Id [pk]
    - Titulo [varchar(60)]
    - Descrição [varchar(300)]
    - Status [varchar(30)]
    - Data de Criação [Date]

### ENDPOINTS

HttpPost
api/tarefa

HttpPut
api/tarefa{id}

HttpGet
GET api/tarefas                          → todas as tarefas
GET api/tarefas?status=ativo             → por status
GET api/tarefas?titulo=reuniao           → por título
GET api/tarefas?descricao=bug            → por descrição
GET api/tarefas?status=ativo&titulo=fix  → combinando filtros

HttpDelete
api/tarefa{id}

### Estrutura do Projeto

TaskManager.API/
├── Controllers/         # Endpoints da API (camada de entrada)
├── Services/            # Regras de negócio
│   └── Interfaces/
├── Repositories/        # Acesso a dados
│   └── Interfaces/
├── Models/              # Entidades do banco de dados
├── DTOs/                # Objetos de transferência de dados
├── Migrations/          # Migrations do EF Core
├── appsettings.json     # Configurações da aplicação
└── Program.cs           # Configuração e inicialização