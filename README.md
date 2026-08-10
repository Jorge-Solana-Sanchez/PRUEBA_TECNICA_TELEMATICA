# Ejercicio 3 - Pruebas del Frontend

Pruebas para la aplicación de gestión de usuarios (React + TypeScript). Incluye tests de componentes con Jest y tests E2E con Playwright.

---

## Tecnologías

- **React / TypeScript / Vite**
- **Jest + React Testing Library**
- **Playwright**

---

## Pruebas realizadas

Se han cubierto los puntos solicitados en el ejercicio:

1. **Botón de editar por fila:** Comprueba que cada fila de la tabla muestra su correspondiente botón de edición.
2. **Modal de edición:** Verifica que al hacer clic en "Editar" se abre el modal cargando los datos del usuario.

- **Jest:** Prueba el renderizado de los componentes (`UserTable` y `UserModal`).
- **Playwright:** Prueba el flujo en el navegador interceptando la API.

---

## Ejecución de pruebas

Entra en la carpeta del frontend e instala las dependencias si no lo has hecho antes:

```bash
cd apps/frontend/TechnicalTest.Web
npm install
```

## Comandos de Pruebas
Desde la carpeta apps/frontend/TechnicalTest.Web puedes ejecutar los siguientes comandos:

### Ejecutar Pruebas Unitarias y de Componentes (Jest)

```bash
npm run test:jest
```

### Ejecutar Pruebas E2E (Playwright)
Para ejecutar las pruebas en la consola:

```bash
npm run test:e2e
```

Para abrirlas en la interfaz de Playwright:

```bash
npm run test:e2e:ui
```

## Ejecución de la Aplicación en Desarrollo
Para levantar la web en local:

```bash
npm run dev
```

La aplicación estará disponible en http://localhost:5173.
