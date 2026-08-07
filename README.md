# Ejercicio 3 - Frontend

---
Suite de tests para la aplicación de gestión de usuarios. Se han implementado tanto pruebas de componentes con Jest como pruebas de integración E2E con Playwright.

##      Tecnologías Utilizadas

- **Frontend:** React, TypeScript, Vite.
- **Testing Unitario y de Componentes:** Jest, React Testing Library.
- **Testing E2E (End-to-End):** Playwright.

---

## Pruebas Implementadas

Se han cubierto los tests propuestos en el enunciado mediante dos enfoques complementarios:

1. **Comprobar que en cada fila de la tabla existe un botón de editar.**
2. **Comprobar que al pulsar el botón de editar se renderiza el panel modal con la información del usuario.**

- **Con Jest:** Se verifica la lógica de renderizado de los componentes (`UserTable` y `UserModal`) de forma aislada e instantánea en memoria.
- **Con Playwright:** Se simula la interacción real de un usuario en un navegador, interceptando la API para garantizar pruebas E2E deterministas e independientes de la base de datos.

---

##  Instrucciones de Ejecución

### 1. Requisitos Previos
Asegúrate de tener instalado:
- **Node.js** (v18 o superior)

### 2. Instalación
Accede a la carpeta del proyecto frontend e instala las dependencias:

```bash
cd apps/frontend/TechnicalTest.Web
npm install
```

## Comandos de Pruebas
Desde la carpeta apps/frontend/TechnicalTest.Web puedes ejecutar los siguientes comandos:

### Ejecutar Pruebas Unitarias y de Componentes (Jest)
Lanza la suite de pruebas rápidas con Jest:

```bash
npm run test:jest
```

### Ejecutar Pruebas E2E (Playwright)
Lanza la suite de pruebas de navegador en modo consola:

```bash
npm run test:e2e
```

Si deseas ver la ejecución de Playwright en modo interactivo mediante interfaz gráfica:

```bash
npm run test:e2e:ui
```

## Ejecución de la Aplicación en Desarrollo
Para levantar el cliente web en modo desarrollo:

```bash
npm run dev
```

La aplicación estará disponible en http://localhost:5173.