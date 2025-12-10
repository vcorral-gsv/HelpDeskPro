---

# HelpDeskPro – 📘 Especificación de la API

> **Proyecto de práctica en .NET + EF Core**
> Sistema de gestión de tickets de soporte técnico para empresas.

---

## 📍 1. Objetivo del proyecto

Implementar una API REST segura que permita:

* Crear y gestionar **tickets de incidencias**
* Controlar acceso por **roles**
* Gestionar equipos, categorías, etiquetas
* Añadir comentarios y adjuntos
* Controlar workflow de estados
* Filtrar, paginar y ordenar resultados

---

## 👤 2. Roles en el sistema

| Rol          | Permisos principales                                |
| ------------ | --------------------------------------------------- |
| **Admin**    | Gestión completa del sistema                        |
| **Agent**    | Gestiona tickets asignados a su equipo o a sí mismo |
| **Customer** | Crea y ve únicamente sus propios tickets            |

---

## 🗄️ 3. Modelo de datos

### 📊 3.1. Diagrama de entidades y relaciones (ER)

```
+---------+    N:N    +--------+
|  User   |----------|  Team   |
+---------+          +--------+
     | 1                    ^
     | Reporter             |
     v                      |
 +--------+   1        N    |
 | Ticket |------------------ 
 +--------+                 |
     | 1                     |
     |                       |
     v                       |
+------------+        +-------------+
| TicketComment |     | TicketAttachment |
+------------+        +-------------+

Ticket —— N:N —— Tag
```

(Relaciones simplificadas para visualización rápida)

---

### 📌 3.2. Entidades con campos

> *(PK = clave primaria, FK = clave foránea)*

#### 🧑 User

| Campo        | Tipo      | Reglas                   |
| ------------ | --------- | ------------------------ |
| Id (PK)      | int       | Identity                 |
| Email        | string    | Unique, Required         |
| PasswordHash | string    | Required                 |
| FirstName    | string    | Required                 |
| LastName     | string    | Required                 |
| Role         | enum      | Admin / Agent / Customer |
| IsActive     | bool      | default: true            |
| CreatedAt    | DateTime  | UTC                      |
| LastLoginAt  | DateTime? |                          |

Relaciones:

* N:N con `Team`
* 1:N como Reporter en `Ticket`
* 1:N como Assignee en `Ticket`

---

#### 🛠️ Team

| Campo       | Tipo     |
| ----------- | -------- |
| Id (PK)     | int      |
| Name        | string   |
| Description | string?  |
| IsActive    | bool     |
| CreatedAt   | DateTime |

---

#### 🎫 Ticket

| Campo            | Tipo      | Descripción                                         |
| ---------------- | --------- | --------------------------------------------------- |
| Id (PK)          | int       |                                                     |
| Code             | string    | Ej: HD-000123 (único, autogenerado)                 |
| Title            | string    |                                                     |
| Description      | string    |                                                     |
| Status           | enum      | Open, InProgress, WaitingCustomer, Resolved, Closed |
| Priority         | enum      | Low, Normal, High, Critical                         |
| CategoryId (FK)  | int       |                                                     |
| ReporterId (FK)  | int       |                                                     |
| AssigneeId (FK)? | int       |                                                     |
| TeamId (FK)?     | int       |                                                     |
| CreatedAt        | DateTime  |                                                     |
| UpdatedAt        | DateTime  |                                                     |
| ResolvedAt       | DateTime? |                                                     |
| ClosedAt         | DateTime? |                                                     |
| DueDate          | DateTime? |                                                     |
| IsDeleted        | bool      | Soft-delete                                         |

---

#### 🧩 TicketCategory

| Campo       | Tipo    |
| ----------- | ------- |
| Id          | int     |
| Name        | string  |
| Description | string? |
| IsActive    | bool    |

---

#### 🏷️ Tag + N:N Assignment

| Campo     | Tipo        |
| --------- | ----------- |
| Id        | int         |
| Name      | string      |
| Color     | string? HEX |
| CreatedAt | DateTime    |

Tabla intermedia:

| TicketId (FK) | TagId (FK) | PK compuesta |

---

#### 💬 TicketComment

| Campo      | Tipo     |
| ---------- | -------- |
| Id         | int      |
| TicketId   | FK       |
| AuthorId   | FK       |
| Body       | string   |
| IsInternal | bool     |
| CreatedAt  | DateTime |

---

#### 📎 TicketAttachment

| Campo         | Tipo     |
| ------------- | -------- |
| Id            | int      |
| TicketId      | FK       |
| FileName      | string   |
| ContentType   | string   |
| FileSizeBytes | long     |
| StorageUrl    | string   |
| UploadedById  | FK       |
| UploadedAt    | DateTime |

---

## 🚦 4. Reglas de negocio

| Acción               | Regla                               |
| -------------------- | ----------------------------------- |
| Crear ticket         | Estado inicial = **Open**           |
| Ver tickets          | Customer solo los suyos             |
| Cambiar estado       | Validar workflow permitido          |
| Cerrar ticket        | Customer puede cerrar los suyos     |
| Comentarios internos | Solo Agent/Admin los ven y publican |
| Soft-delete          | Admin puede ocultar tickets         |

### 🧭 Flujo de estados permitido

```
Open → InProgress
Open → WaitingCustomer
Open → Resolved

InProgress → WaitingCustomer
InProgress → Resolved

WaitingCustomer → InProgress
WaitingCustomer → Resolved

Resolved → Closed
Resolved → InProgress

Closed → (sin cambios)
```

---

## 🔌 5. API REST

### Reglas generales

* JSON
* JWT: `Authorization: Bearer <token>`
* Paginación:

  * Query: `page`, `pageSize`
* Filtros: `status`, `priority`, `search`, `teamId`, etc.

---

### 🔐 5.1. Autenticación

| Método | Endpoint             | Rol         | Descripción   |
| ------ | -------------------- | ----------- | ------------- |
| POST   | `/api/auth/register` | Anónimo     | Alta customer |
| POST   | `/api/auth/login`    | Anónimo     | Login + JWT   |
| POST   | `/api/auth/refresh`  | Autenticado | Nuevo token   |

---

### 👤 5.2. Usuarios (solo Admin)

| Método | Endpoint                     | Descripción       |
| ------ | ---------------------------- | ----------------- |
| GET    | `/api/users`                 | Listado + filtros |
| GET    | `/api/users/{id}`            | Detalle           |
| POST   | `/api/users`                 | Crear usuario/rol |
| PUT    | `/api/users/{id}`            | Actualizar        |
| PATCH  | `/api/users/{id}/deactivate` | Desactivar        |

---

### 🛠️ 5.3. Teams

| Método | Endpoint                  | Permisos      |
| ------ | ------------------------- | ------------- |
| GET    | `/api/teams`              | Admin / Agent |
| POST   | `/api/teams`              | Admin         |
| PATCH  | `/api/teams/{id}/members` | Admin         |

---

### 📂 5.4. Categorías y Tags

CRUD estándar, permisos:

* **Categories**: Admin gestiona, Agents/Customers consultan
* **Tags**: Admin crea/modifica, Agents consultan

---

### 🎫 5.5. Tickets

| Método | Endpoint                   | Descripción                   |
| ------ | -------------------------- | ----------------------------- |
| GET    | `/api/tickets`             | Listado paginado + filtros    |
| GET    | `/api/tickets/{id}`        | Detalle completo              |
| POST   | `/api/tickets`             | Crear ticket                  |
| PUT    | `/api/tickets/{id}`        | Editar contenido              |
| PATCH  | `/api/tickets/{id}/status` | Cambiar estado con validación |
| PATCH  | `/api/tickets/{id}/assign` | Asignar agente/equipo         |
| DELETE | `/api/tickets/{id}`        | Soft-delete (Admin)           |

📌 **Visibilidad controlada según rol** en backend.

---

### 💬 Comentarios

* `POST /api/tickets/{id}/comments`
* `GET /api/tickets/{id}/comments`

Validar visibilidad según `IsInternal`.

---

### 📎 Adjuntos

Solo metadata simulada:

* `GET /api/tickets/{id}/attachments`
* `POST /api/tickets/{id}/attachments`
* `DELETE /api/tickets/{id}/attachments/{attachmentId}`

---

## 📦 6. DTOs resumen

### Ticket (Listado)

```json
{
  "id": 123,
  "code": "HD-000123",
  "title": "No puedo acceder al correo",
  "status": "Open",
  "priority": "High",
  "category": { "id": 1, "name": "Software" },
  "reporter": { "id": 10, "fullName": "John Doe" },
  "assignee": { "id": 5, "fullName": "Agent Smith" },
  "team": { "id": 2, "name": "Soporte Nivel 1" },
  "createdAt": "2025-02-01T10:00:00Z"
}
```

---

### Ticket (Detalle)

Incluye:

* Comentarios según visibilidad
* Adjuntos
* Tags
* Fechas

---

## 🛠️ 7. TODO técnico (Siguiente paso para tu implementación)

* Creación del proyecto: `Web API + Swagger + Identity + EF Core`
* Migrations + Seeds básicos (Roles, Admin)
* AutoMapper Profiles
* Middleware de manejo global de errores
* Autorización con Policies:

  * `RequireAdmin`
  * `RequireAgent`
  * `CanViewTicket`
  * `CanEditTicket`

---

## 🧪 8. Casos de prueba recomendados

| Caso                                | Usuario  | Resultado esperado           |
| ----------------------------------- | -------- | ---------------------------- |
| Customer intenta ver ticket de otro | Customer | 403                          |
| Agent cambia estado inválido        | Agent    | 400 "Transition not allowed" |
| Customer añade comentario interno   | Customer | 403                          |
| Admin desactiva usuario             | Admin    | User no puede loguear        |

---

## ✔️ Objetivos didácticos

Este proyecto te permite practicar:

| Tema                           | Cobertura |
| ------------------------------ | --------- |
| .NET Web API                   | ✔️        |
| EF Core – Relaciones N:N       | ✔️        |
| DTO, AutoMapper                | ✔️        |
| JWT Auth y Claims              | ✔️        |
| Pagination, Filtering, Sorting | ✔️        |
| Reglas de negocio avanzadas    | ✔️        |
| Middlewares y validaciones     | ✔️        |
| Arquitectura limpia escalable  | ✔️        |

---

## 🎯 Resultado final esperado

Un backend **robusto**, **realista** y **profesional**, digno de portfolio.

---