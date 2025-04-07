# Provider-risk-screening-app

Este proyecto está compuesto por un **frontend en Angular 19.2.6** y un **backend en .NET 9.0.202**. A continuación, se detallan los pasos para ejecutar ambos entornos en desarrollo y producción.

---

## Requisitos Previos

### Backend (.NET)

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio Code (opcional)

### Frontend (Angular)

- [Node.js y npm](https://nodejs.org/) (v20 o superior)
- [Angular CLI](https://angular.io/cli) (npm install -g @angular/cli)

---

## Despliegue en Desarrollo

### 1. Clonar el repositorio
```bash
git clone https://github.com/JohnSovero/provider-risk-screening-app.git
cd provider-risk-screening-app
```

### 2. Backend

```bash
cd backend
dotnet restore
dotnet build
```
Configura tu cadena de conexión en appsettings.json:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=RiskScreeningDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
}
```

Inicializa la base de datos con las migraciones existentes:
```bash
dotnet ef database update
```
Inicia el backend:
```bash
dotnet run
```

Por defecto, el backend se ejecuta en https://localhost:8080.

### 3. Frontend
```bash
cd ../frontend
npm install
ng serve
```

Se ejecutará en http://localhost:4200.
Asegúrate de que src/environments/environment.ts tenga la URL correcta del backend:

```ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:8080/api'
};
```

## Despliegue en Producción

### 1. Compilar el Frontend
```bash
cd frontend
ng build --configuration production
```

Esto generará los archivos estáticos en frontend/dist/.

### 2. Publicar el backend
```bash
cd backend
dotnet publish -c Release -o ./publish
```