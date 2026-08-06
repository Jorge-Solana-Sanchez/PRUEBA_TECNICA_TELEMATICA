# Frontend - Ejercicio 2
En este ejercicio se ha ampliado el listado de usuarios para añadir funcionalidad de edición y creación mediante un modal, conectándolo con la API desarrollada en los ejercicios de backend.


## Cambios realizados
* Acción de editar: botón "Editar" en cada fila para modificar los datos de un usuario desde un modal.
* Acción de añadir: botón "Añadir Usuario" para registrar un nuevo usuario a través del mismo modal.
* Restaurar datos: botón "Restaurar para volver a cargar la lista original de usuarios.
* Integración API: añadidas peticiones HTTP (POST, PUT) contra el backend.

## Requisitos Previos

Asegúrate de tener instalados los siguientes componentes:

* .NET 9.0 SDK
* Docker Desktop (debe estar en ejecución)
* Node.js (v18 o superior)

---

## Pasos para Ejecutar la Aplicación

### Backend

#### 1. Iniciar la Base de Datos MySQL (Docker)

Abre una terminal en la raíz del proyecto (donde se encuentra el archivo `docker-compose.yml`) y ejecuta:

```bash
docker compose up -d
```
#### 2. Iniciar la API
Ejecuta la API desde la terminal con el siguiente comando:
```bash
dotnet run --project apps/backend/TechnicalTest.Api
```

Creación de tablas: La aplicación está configurada para que, en el primer arranque, cree automáticamente la base de datos technical_test_db y la tabla Users en el contenedor MySQL si aún no existen.

#### 3. Verificar en Swagger

Una vez iniciada la API, accede a la url:

**http://localhost:5000/swagger**

Desde la interfaz gráfica de **Swagger** podrás probar los endpoints de usuarios:

* `GET /api/users` — Consulta la lista de usuarios persistidos en la base de datos MySQL.
* `POST /api/users` — Registra un nuevo usuario en la base de datos.
* `PUT /api/users/{id}` — Actualiza los datos de un usuario existente.

### Frontend

#### 1. Navegar a la carpeta del cliente:
```bash
cd apps/frontend/TechnicalTest.Web
```

#### 2. Instalar dependencias:
```bash
npm install
```

#### 3. Iniciar el servidor de desarrollo:
```bash
npm run dev
```

#### 4. Probar en la web
Abre tu navegador en la URL indicada en la consola:
http://localhost:5173   