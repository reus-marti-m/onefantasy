_© 2025 Marc Reus Martí. Tots els drets reservats.  
Aquest codi és propietari i no pot ser utilitzat, copiat ni distribuït sense permís exprés de l’autor._

# OneFantasy (TFG)
**Autor:** Marc Reus Martí  
**Assignatura:** 05.678 – TFG Desenvolupament Web (Universitat Oberta de Catalunya)  

OneFantasy pretén ser un joc fantasy de futbol —amb possibilitat d’extensió a altres esports— en format competitiu. Així, aquest projecte inclou la demo funcional que mostra com jugar-hi mitjançant reptes diaris. Cal destacar que encara no és un joc complet. 

## Disseny de la interfície
El disseny de la interfície s'ha creat amb Figma:  
[Prototipus Figma](https://www.figma.com/proto/edSPpMYNTvGSXCo9gEaNOm/TFG?node-id=0-1&t=XEylq2RLvQSlJYJs-1)

## Funcionalitats implementades
L'atual versió de l'aplicació web inclou:  
- **Sistema d’autenticació** (registre/login amb email i contrasenya, o accés com a convidat).
- **Pàgina principal amb llistes de reptes i de lligues** (dades de lligues amb mock).  
- **Mecànica de jugar reptes diaris i consultar-ne els resultats**: Els reptes diaris (o “participacions diàries”) tenen un pressupost fictici limitat, i estan formats per dos grups de minijocs. Cada minijoc té una predicció sobre un aspecte d’un partit de futbol. Punts clau:  
  1. **Selecció d’opcions:** l’usuari tria una o dues opcions per minijoc.  
  2. **Pressupost:** cada opció té un “cost” fictici basat en la probabilitat d’ocurrència. L’usuari ha de gestionar el pressupost disponible per cobrir totes les prediccions.  
  3. **Resolució i puntuació:** un cop esdevingut l’esdeveniment, s’assignen punts fixos per cada predicció encertada, independentment del cost de l’opció.  


Pots veure una demostració en vídeo aquí:  
[Vídeo demo](https://drive.google.com/file/d/1yjYSVD7WkO5jvrZRIeIYCY8_HydRiUQI/view?usp=sharing)


## Configuració en local

Aquestes instruccions descriuen dues vies per arrancar el projecte:  
- **Opció A:** clonar des de GitHub (sense fitxer `OneFantasy.db`)  
- **Opció B:** utilitzar el bundle de l'entrega de la PAC3 (inclou `OneFantasy.db` ja creat)

### Prerequisits  
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Node.js ≥16.x](https://nodejs.org/) + npm  
- Angular CLI (`npm install -g @angular/cli`)  

### 1. Obtenir el codi  
```bash
# Des de GitHub:
git clone https://github.com/EL_TEUNOM/one-fantasy.git one-fantasy
cd one-fantasy

# Utilitzant el bundle de l'entrega (assumint que el codi està descomprimit dins la carpeta one-fantasy):
cd one-fantasy
```

### 2. Backend

Obre terminal al directori del backend i restaura paquets:  
```bash
cd backend/OneFantasy.Api
dotnet restore
```

**2.A. Clonant des de GitHub (sense la BD)**  
```bash
dotnet tool restore      # si no tens el CLI d’EF instal·lat
dotnet ef database update
```

**2.B. Usant el bundle (inclou OneFantasy.db)**  
_No cal fer migracions; la BD ja ve creada al directori_

Per arrancar el servidor:  
```bash
dotnet run                # arranca a https://localhost:5001
```

**2.C. Provar l’API amb Swagger**

El backend inclou Swagger UI per facilitar la prova dels endpoints.
Un cop el servidor està en marxa a `https://localhost:5001`, hi pots accedir des de `https://localhost:5001/swagger/index.html`

Les rutes autenticades requereixen un token JWT:

1. Fes una petició `POST /api/auth/login` amb el teu email i contrasenya. Algunes peticions requereixen el rol d'admin.
2. Copia el valor del camp `token` de la resposta.
3. A la Swagger UI, fes clic a **Authorize** que apareix a dalt a la dreta.
4. Enganxa `<token>` al camp de text i prem **Authorize** (**sense** el prefix Bearer).
5. Ara podràs invocar les rutes protegides directament des de Swagger.

### 3. Frontend

Obre una altra terminal, ves al directori del frontend i instal·la dependències:  
```bash
cd frontend
npm install
```

Després arrenca l’app Angular:  
```bash
ng serve --open           # obre http://localhost:4200
```

### 4. Provar l’aplicació

1. Accedeix a http://localhost:4200  
2. Registra’t o entra com a **convidat**  
3. Juga reptes i consulta resultats  


## Backend – Detalls tècnics

### 1. Tecnologies i Frameworks
- **.NET SDK:** Microsoft.NET.Sdk.Web (TargetFramework: net8.0)  
- **Plataforma:** ASP.NET Core 8.0 amb C#  
- **Servidor web:** Kestrel  
- **ORM:** Entity Framework Core 8.0
- **Identitat:** ASP.NET Core Identity
- **Autenticació:**  JWT Bearer
- **Mapping:** AutoMapper
- **Documentació:** Swagger / OpenAPI
- **Dependency Injection:** contenidor nadiu d’ASP.NET Core  

### 2. Estructura de carpetes
```plaintext
backend/
└─ OneFantasy.Api/
   ├─ Controllers/                  # Web API endpoints
   ├─ DTOs/                         # Data Transfer Objects
   ├─ Data/                         # AppDbContext, configuració EF Core
   ├─ Domain/                       # Lògica de negoci i contractes
   │   ├─ Abstractions/             # Interfícies
   │   ├─ Exceptions/               # Excepcions custom
   │   ├─ Extensions/               # Mètodes d’extensió
   │   ├─ Filters/                  # Filtres i operacions Swagger
   │   ├─ Helpers/                  # Utilitats i càlculs comuns
   │   ├─ Implementations/          # Serveis
   │   └─ Mappers/                  # Perfils d’AutoMapper
   ├─ Migrations/                   # Migracions EF Core
   ├─ Models/                       # Entitats del domini
   ├─ Properties/                   # Configuració de projecte
   ├─ Program.cs                    # Punt d’entrada i configuració del host
   ├─ Startup.cs                    # Registre de serveis i pipeline de middleware
   ├─ appsettings.json              # Configuració general (producció)
   └─ appsettings.Development.json  # Configuració per a dev (SQLite, CORS, JWT)
```

### 3. Patrons arquitecturals
- **Clean / Onion Architecture**  
  - **Controllers:** binding de la request i delegació als serveis de domini  
  - **Domain:**  
    - **Abstractions:** interficies de serveis i repositoris
    - **Exceptions:** definició d’excepcions custom
    - **Extensions:** mètodes d’extensió per registrar serveis i configurar opcions
    - **Filters:** filtres per a Swagger i validacions
    - **Helpers:** utilitats i càlculs comuns
    - **Implementations:** lògica de negoci 
    - **Mappers:** perfils d’AutoMapper (DTO ↔ Model)
  - **Data:** `AppDbContext` amb `DbSet<T>` i configuracions Fluent API  
- **Dependency Injection:**  
  - Registre de serveis amb el contenidor nadiu d’ASP.NET Core  
  - Registre automàtic de serveis de domini via `services.AddDomainServices()`  
- **Middleware / Cross-cutting:**  
  - **Error handling:**
    - `UseExceptionHandler("/error")` invoca `ErrorController`  
    - Captura excepcions via `IExceptionHandlerFeature`  
    - Mapatge de `OneFantasyException` a `ProblemDetails`  
    - Fallback 500 amb `ProblemDetails` genèric  
  - **Seguretat:** 
    - `UseAuthentication()` (JWT)  
    - `UseAuthorization()` amb polítiques (`RequireUser`, `RequireAdmin`)  
  - **CORS:** política “FrontendPolicy” per a Angular  
  - **HTTPS redirection**, **logging** i **Swagger UI** en dev  

### 4. API RESTful
- **Rutes principals:**  
  - `/api/auth/*` → gestió usuaris
  - `/api/competitions/*` → gestió competicions  
  - `/api/competitions/{id}/seasons/*` → gestió de temporades  
  - `/api/seasons/{sid}/participations/*` → gestió de participacions
  - `/api/seasons/{sid}/teams/*` → gestió d'equips 
  - `/api/teams/{tid}/players/*` → gestió de jugadors  
- **Intercanvi:** JSON + codis HTTP estàndard  
- **OpenAPI:** classes documentades amb XML Comments i Data Annotations  

### 5. Base de dades i migracions
- **Context:** 
  - `AppDbContext : IdentityDbContext<ApplicationUser>` exposa tots els `DbSet<T>`  
- **OnModelCreating (Fluent API):**  
  - Clau primària amb `HasKey(...)` a cada entitat  
  - Propietats amb `IsRequired()`, `HasMaxLength()`, `HasDefaultValue()`  
  - Relacions 1–N i N–N amb `HasMany`/`WithOne` i `HasForeignKey`  
  - Herència amb `HasDiscriminator<string>("…")` per a Participation, MinigameGroup, Minigame i Option  
  - Índex únic `(UserId, ParticipationId)` a `UserParticipation`  
- **Migracions EF Core:**  
  1. `dotnet ef migrations add InitialCreate`  
  2. `dotnet ef database update`  
- **Persistència:**
  - SQLite en dev (`OneFantasy.db`)  
  - En producció, es podrà canviar fàcilment a SQL Server / PostgreSQL 

### 6. Configuració i desplegament
- **appsettings.json / appsettings.Development.json:**  
  - **ConnectionStrings:** `DefaultConnection` (SQLite en dev)  
  - **Jwt:** clau simètrica, Issuer, Audience, temps d’expiració de JWT i refresh tokens  
  - **Cors:** origens permesos (`http://localhost:4200`)  
- **Startup.cs / Program.cs:**  
  - Registre de serveis (DbContext, Identity, Authentication/JWT, CORS, AutoMapper, Domain Services)  
  - Configuració de Middleware (HTTPS, CORS, Authentication, Authorization, Swagger a dev)
 
### 7. Deute tècnic
Tot i que l’arquitectura està preparada per ser testejable, per falta de temps no s'han pogut incloure tests automatitzats de cap tipus (ni unitaris ni d'integració). Aquesta serà una millora a considerar en futurs desenvolupaments.


## Frontend – Detalls tècnics

### 1. Tecnologies i Frameworks
- **Angular:** 19.2.7 amb TypeScript 5.7.2  
- **UI:** Angular Material & CDK  
- **RxJS:** ~7.8  
- **HTTP:** `@angular/common/http` (HttpClient)  
- **API Client:** generat amb NSwag (`openapi-to-typescript-client`)  
- **Parches:** Node.js scripts per ajustar `api.ts` (polítiques d’array i polimorfisme)  
- **CLI & Build:** Angular CLI (`ng serve`, `ng build`, `ng test`) 

### 2. Estructura de carpetes
```plaintext
frontend/
├─ public/               # fitxers estàtics
├─ scripts/              # scripts de patch (array-ops, polymorphism)
└─ src/
   ├─ app/
   │   ├─ core/                # serveis singleton, interceptors, auth
   │   │   ├─ auth/            # guardes, interceptor i servei d’autenticació
   │   │   ├─ api.ts           # client NSwag generat
   │   │   └─ refresh.service.ts
   │   ├─ modules/             # feature modules
   │   │   ├─ auth/            # diàleg login/registre
   │   │   ├─ common/          # diàleg canvis pendents i scss comú a varis mòduls
   │   │   ├─ help/help/       # modal d'ajuda (disponible pròximament)         
   │   │   ├─ leagues/         # llistes de lligues públiques i privades (implementat amb dades mockejades), detall de lliga (disponible pròximament) i modal de creació de lliga (disponible pròximament)
   │   │   ├─ notifications/   # panell de notificacions (disponible pròximament)
   │   │   ├─ participations/  # llista de participacions (reptes diaris) i vista detall (formulari jugar/consultar resultats)
   │   │   ├─ preferences/     # modal de preferències (disponible pròximament)
   │   │   ├─ profile/         # modal de perfil (disponible pròximament)
   │   │   ├─ rules/           # modal de regles (disponible pròximament)
   │   │   ├─ settings/        # modal de configuració (disponible pròximament)
   │   │   ├─ shell/           # shellComponent amb layout responsive per mobile/desktop
   │   │   └─ welcome/         # pàgina de benvinguda des d'on fer login o entrar com a convidat 
   │   ├─ app.component.ts|html|scss
   │   └─ app.routes.ts        # definició de rutes i outlets
   ├─ environments/
   │   ├─ environment.ts       # dev (apiUrl=https://localhost:5001)
   │   └─ environment.prod.ts  # prod
   └─ styles.scss
```

### 3. Patrons arquitecturals
- **Modular**: separació clara entre `core` (serveis de nivell app) i `modules` (funcionalitats)  
- **Dependency Injection**: servei d’autenticació, API client i guards injectats automàticament per Angular  
- **Interceptors HTTP**:  
  - `AuthInterceptor` afegeix el token JWT a les peticions, gestiona errors 401 i refresca el token quan cal  
- **Observables & RxJS**: tot l’HTTP torna `Observable<T>` i s’apliquen operadors (`catchError`, `switchMap`, etc.)  
- **Code-gen + Parches**: NSwag genera `api.ts` automàticament i dos scripts (`patch-arrays-only-array-ops.js` i `patch-polymorphism.js`) fan post-processament per netejar branches d’array i afegir suport a DTOs discriminats 
- **Responsive & Layout**: `ShellComponent` defineix `isMobile` via `@HostListener('window:resize')` i usa `showDetail` per alternar entre llista i detall segons amplada de pantalla i ruta.  
- **Guards**:  
  - `AuthGuard` (CanActivate): comprova `localStorage.getItem('token')` i redirigeix a `/` si no hi ha token  
  - `PendingChangesGuard` (CanDeactivate): impedeix navegar fora de `ParticipationDetail` si hi ha canvis no desats  

### 4. Interacció amb l’API
- **NSwag config (`nswag.json`)**: genera `src/app/core/api.ts` a partir de `/swagger/v1/swagger.json`  
- **Patch scripts**:  
  1. **patch-arrays-only-array-ops.js**: elimina branches innecessaris en processos d’array  
  2. **patch-polymorphism.js**: afegeix tipus i `fromJS()` per a DTOs discriminats  

### 5. Routing i navegació
- **app.routes.ts** defineix:  
  - `/` → `WelcomeComponent`  
  - `/app` → `ShellComponent` (AuthGuard)  
    - child routes:  
      - `participations/:id` (PendingChangesGuard)  
      - `leagues/:id`  
      - outlets de modal per `profile`, `rules`, `help`, `settings`, `create-league`, `preferences`  
  - `**` → redirigeix a `/`  

### 6. Scripts, Build i Desplegament
- **package.json – scripts**:  
  - `npm start` → `ng serve`  
  - `npm run build` → `ng build`  
  - `npm test` → `ng test`  
  - `npm run gen:api` → `nswag run` + `npm run patch:array-ops` + `npm run patch:polymorphism`  
- **Environments**: `environment.ts` / `environment.prod.ts` amb `apiUrl`  

### 7. Deute tècnic
Tot i que l’arquitectura està prepadada per ser testejable, per falta de temps no s'han pogut incloure tests automatitzats de cap tipus (ni dels components ni e2e). Aquesta serà una millora a considerar en futurs desenvolupaments.
