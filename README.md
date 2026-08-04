# Ejercicio 2 (Persistencia MySQL)

Este proyecto implementa la API de usuarios en **.NET 9**. Se ha sustituido el almacenamiento original basado en archivos JSON por una base de datos relacional **MySQL** desplegada mediante **Docker**.

---

## Requisitos Previos

Asegúrate de tener instalados los siguientes componentes:

* .NET 9.0 SDK
* Docker Desktop (debe estar en ejecución)

---

## Pasos para Ejecutar la Aplicación

### 1. Iniciar la Base de Datos MySQL (Docker)

Abre una terminal en la raíz del proyecto (donde se encuentra el archivo `docker-compose.yml`) y ejecuta:

```bash
docker compose up -d
```
### 2. Iniciar la API
Ejecuta la API desde la terminal con el siguiente comando:
```bash
dotnet run --project apps/backend/TechnicalTest.Api
```

Creación de tablas: La aplicación está configurada para que, en el primer arranque, cree automáticamente la base de datos technical_test_db y la tabla Users en el contenedor MySQL si aún no existen.

### 3. Probar la Aplicación en Swagger

Una vez iniciada la API, accede a la url:

**http://localhost:5000/swagger**

Desde la interfaz gráfica de **Swagger** podrás probar los endpoints de usuarios:

* `GET /api/users` — Consulta la lista de usuarios persistidos en la base de datos MySQL.
* `POST /api/users` — Registra un nuevo usuario en la base de datos.
* `PUT /api/users/{id}` — Actualiza los datos de un usuario existente.