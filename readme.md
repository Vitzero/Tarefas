# 📝 Lista de Tarefas — Todo App

Aplicação fullstack de gerenciamento de tarefas com API REST em ASP.NET Core, frontend em Vue 3 e banco de dados MySQL.

---

## 🧱 Stack

| Camada | Tecnologia |
|--------|-----------|
| Frontend | Vue 3 + Vite (Composition API) |
| Backend | ASP.NET Core 8 (Web API) |
| Banco de dados | MySQL |
| ORM | Entity Framework Core 8 + Pomelo |
| Documentação da API | Swagger / OpenAPI |

---

## 📁 Estrutura do projeto

```
/
├── ListaTarefas/               # Backend C# ASP.NET Core
│   ├── Controllers/
│   │   └── TarefasController.cs
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── TypeConfig/
│   │       └── TarefaTypeConfiguration.cs
│   ├── Migrations/
│   ├── Models/
│   │   ├── Tarefa.cs
│   │   └── DTOs/
│   │       ├── CriarTarefaDto.cs
│   │       ├── AtualizarTarefaDto.cs
│   │       └── TarefaResponseDto.cs
│   ├── Repositories/
│   │   ├── ITarefasRepository.cs
│   │   └── TarefasRepository.cs
│   ├── Services/
│   │   ├── ITarefasService.cs
│   │   └── TarefasService.cs
│   ├── Program.cs
│   └── appsettings.json
│
└── src/                        # Frontend Vue 3
    ├── Views/
    │   └── Tarefas.vue         # Componente pai / orquestrador
    ├── Componentes/
    │   ├── HeaderTodo.vue
    │   ├── Botoes.vue
    │   ├── ListaSuspensa.vue
    │   ├── Tarefa.vue
    │   └── Modals/
    │       ├── CriarModal.vue
    │       └── EditarModal.vue
    ├── App.vue
    ├── main.js
    └── style.css
```

---

## ⚙️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [MySQL 8+](https://dev.mysql.com/downloads/)

---

## 🚀 Como rodar

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/lista-tarefas.git
cd lista-tarefas
```

### 2. Configure o banco de dados

Crie o banco no MySQL:

```sql
CREATE DATABASE ListaTarefasDB;
```

Abra `ListaTarefas/appsettings.json` e ajuste a connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=ListaTarefasDB;User=seu_usuario;Password=sua_senha;"
}
```

### 3. Rode as migrations

```bash
cd ListaTarefas
dotnet ef database update
```

### 4. Inicie o backend

```bash
dotnet run
```

A API estará disponível em `https://localhost:7265`. O Swagger pode ser acessado em `https://localhost:7265/swagger`.

### 5. Inicie o frontend

Em outro terminal:

```bash
cd src
npm install
npm run dev
```

O frontend estará disponível em `http://localhost:5173`.

---

## 🔌 Endpoints da API

Base URL: `https://localhost:7265/api/Tarefas`

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/Tarefas` | Lista todas as tarefas |
| GET | `/api/Tarefas?search=texto` | Filtra por título ou descrição |
| GET | `/api/Tarefas?status=0` | Filtra por status |
| POST | `/api/Tarefas` | Cria uma nova tarefa |
| PUT | `/api/Tarefas/{id}` | Atualiza uma tarefa existente |
| DELETE | `/api/Tarefas/{id}` | Remove uma tarefa (soft delete) |

### Corpo do POST e PUT

```json
{
  "titulo": "string",
  "descricao": "string",
  "status": 0
}
```

### Resposta do GET

```json
[
  {
    "id": 1,
    "titulo": "string",
    "descricao": "string",
    "status": 0,
    "dataDeCriacao": "2026-05-03T00:00:00.000Z"
  }
]
```

---

## 📊 Enum de status

| Valor | Nome |
|-------|------|
| `0` | Pendente |
| `1` | Em Andamento |
| `2` | Concluído |

---

## 🗄️ Model

```csharp
public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;
    public DateTime DataDeCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataExclusao { get; set; }
    public bool Deletado => DataExclusao != null; // soft delete
}
```

> O campo `DataExclusao` implementa soft delete — a tarefa nunca é removida fisicamente do banco, apenas marcada como deletada.

---

## ✅ Validações

As validações são feitas nos DTOs e retornam erro `400` com mensagem descritiva:

| Campo | Regra |
|-------|-------|
| `Titulo` | Obrigatório, mínimo 3 e máximo 60 caracteres |
| `Descricao` | Obrigatória, máximo 300 caracteres |
| `Status` | Obrigatório, valor do enum (0, 1 ou 2) |

---

## 🧩 Arquitetura do backend

O backend segue o padrão de camadas com separação clara de responsabilidades:

```
Controller → Service → Repository → DbContext → MySQL
```

- **Controller** — recebe as requisições HTTP e delega para o Service
- **Service** — contém a lógica de negócio
- **Repository** — acessa o banco via Entity Framework
- **DTOs** — separam o modelo interno da API pública

---

## 🖥️ Arquitetura do frontend

O frontend é organizado em componentes Vue 3 com Composition API:

```
App.vue
└── Tarefas.vue          ← orquestrador: fetch, estado, filtros
    ├── HeaderTodo.vue   ← título da página
    ├── Botoes.vue       ← pesquisa, filtro por status, botão criar
    │     emit: @atualizar:filtro, @abrir-criar
    ├── ListaSuspensa.vue ← recebe :tarefas[], repassa emits
    │   └── Tarefa.vue   ← card individual
    │         emit: @editar, @deletar
    ├── CriarModal.vue   ← modal de criação
    │     emit: @salvar, @fechar
    └── EditarModal.vue  ← modal de edição
          prop: :tarefa
          emit: @salvar, @fechar
```

### Fluxo de dados

- Props descem do pai para o filho (`:tarefas`, `:filtro`, `:tarefa`)
- Emits sobem do filho para o pai (`@salvar`, `@deletar`, `@editar`)
- `Tarefas.vue` é o único componente que faz chamadas à API
- Os filtros (`search` e `status`) são enviados como query params para a API — a filtragem é feita no backend

---

## 📦 Dependências do backend

| Pacote | Versão | Uso |
|--------|--------|-----|
| `Pomelo.EntityFrameworkCore.MySql` | 8.0.3 | ORM para MySQL |
| `Microsoft.EntityFrameworkCore.Design` | 8.0.3 | Migrations |
| `Swashbuckle.AspNetCore` | 6.6.2 | Swagger |

---

## 📦 Dependências do frontend

| Pacote | Uso |
|--------|-----|
| `vue` | Framework principal |
| `vite` | Build tool |
| `@kyvg/vue3-notification` | Notificações toast |
| `sweetalert2` | Confirmação de exclusão |

---

## 🔒 CORS

A API está configurada para aceitar requisições apenas de `http://localhost:5173` (origem do Vite em desenvolvimento). Para produção, atualize a política de CORS em `Program.cs`:

```csharp
policy.WithOrigins("https://seu-dominio.com")
```

---

## 📄 Licença

Este projeto é de uso educacional.
