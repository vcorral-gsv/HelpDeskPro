# Instrucciones para GitHub Copilot: Conventional Commits

Objetivo
- Generar mensajes de commit siguiendo el estándar Conventional Commits v1.0.0 (https://www.conventionalcommits.org/en/v1.0.0/).

Reglas generales
- Usa el formato: `tipo(scope opcional): descripcion breve`
- La descripción debe ser concisa en español, en minúsculas y sin punto final.
- Incluye cuerpo del commit cuando aporte contexto (cambios no triviales, motivación, implicaciones).
- Usa `BREAKING CHANGE:` en el cuerpo o footer para cambios incompatibles.
- Referencia issues usando `Closes #<id>`, `Fixes #<id>` cuando aplique.

Tipos permitidos
- `feat`: nueva funcionalidad
- `fix`: corrección de bug
- `perf`: mejora de rendimiento
- `refactor`: cambios internos sin alterar comportamiento público
- `docs`: cambios en documentación
- `test`: añadir o actualizar pruebas
- `build`: cambios en build, dependencias o CI
- `chore`: otros cambios menores (tareas de mantenimiento)
- `style`: cambios de formato/sin lógica (lint, espacios, etc.)

Scope (opcional)
- Usa scopes como nombres de carpeta, módulo, capa o servicio. Ej.: `api`, `ui`, `infra`, `auth`, `db`, `validators`.

Cuerpo (opcional)
- Explica el “qué” y “por qué”, evita repetir la descripción.
- Si hay breaking changes, añade sección:
  - `BREAKING CHANGE: <explicación del impacto y pasos de migración>`

Footer (opcional)
- Referencias a tickets o impactos:
  - `Closes #123`
  - `Fixes #456`

Ejemplos válidos
- `feat(api): añadir endpoint de tickets con paginación`
- `fix(validators): corregir validación de email en registro`
- `refactor(utils): extraer helper para manejo de fechas`
- `perf(db): optimizar consulta de listados con índices`
- `docs: actualizar README con instrucciones de despliegue`
- `build(ci): añadir job para ejecutar tests en PR`
- `chore: actualizar dependencias menores`
- `style: aplicar formato según editorconfig`

Ejemplo con cuerpo y cierre de issue
```
fix(api): manejar nulos en mapeo de dto

Se añade verificación de nulos para evitar NullReferenceException
al convertir entidades a DTO.

Fixes #42
```

Ejemplo con breaking change
```
feat(auth): renombrar claim de rol a roleName

BREAKING CHANGE: los consumidores deben actualizar la lectura del claim
`role` a `roleName`. Consultar guía de migración en docs/auth.md.
```