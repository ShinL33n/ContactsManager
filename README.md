# ContactsManager - specyfikacja techniczna

## 1. Cel i zakres aplikacji

ContactsManager jest aplikacją webową do przeglądania i zarządzania listą kontaktów. System składa się z:

- backendu w ASP.NET Core Web API,
- frontendu w Blazor WebAssembly działającego jako SPA,
- wspólnej biblioteki z modelami DTO, enumami i pomocniczymi klasami,
- bazy danych SQL Server obsługiwanej przez Entity Framework Core.

Niezalogowany użytkownik może przeglądać podstawowe dane kontaktów. Zalogowany użytkownik może edytować własny kontakt, a administrator może tworzyć, edytować i usuwać dowolne kontakty.

## 2. Struktura rozwiązania

### `ContactsManager.Api`

Backend REST API odpowiedzialny za:

- obsługę żądań HTTP,
- autoryzację i uwierzytelnianie JWT,
- walidację danych,
- operacje CRUD na kontaktach,
- dostęp do bazy danych,
- udostępnianie słowników kategorii, podkategorii i wykształcenia.

### `ContactsManager.Client`

Frontend SPA napisany w Blazor WebAssembly. Odpowiada za:

- widoki logowania, rejestracji i kontaktów,
- komunikację z REST API,
- przechowywanie tokena JWT w localStorage,
- obsługę stanu zalogowania,
- walidację formularzy i komunikaty dla użytkownika.

### `ContactsManager.Shared`

Biblioteka współdzielona przez API i klienta. Zawiera:

- DTO żądań i odpowiedzi,
- stałe nazw kategorii, ról i poziomów wykształcenia,
- pomocnicze metody, na przykład obliczanie wieku.

### `ContactsManager.Tests`

Projekt testów jednostkowych oparty na xUnit (testów niestety brak)

## 3. Backend - klasy i metody

### `Program.cs`

Konfiguruje aplikację API i kontener zależności.

Najważniejsze elementy:

- `AddControllers()` - rejestruje kontrolery MVC/Web API,
- `AddOpenApi()` - włącza dokumentację OpenAPI,
- `AddDbContext<ApplicationDbContext>()` - konfiguruje Entity Framework Core z SQL Server,
- `AddJwtAuthentication()` - włącza uwierzytelnianie JWT,
- rejestruje IUserService, hasher haseł i JwtTokenService,
- wykonuje `MigrateAsync()` przy uruchomieniu, aby zastosować migracje bazy,
- konfiguruje HTTPS, CORS, uwierzytelnianie i autoryzację.

### `Data/ApplicationDbContext.cs`

Główny kontekst Entity Framework Core.

Udostępnia zbiory:

- `Users` - kontakty/użytkownicy,
- `Categories` - kategorie kontaktów,
- `Subcategories` - słownik podkategorii służbowych,
- `CustomSubcategories` - podkategorie wpisywane przez użytkownika,
- `Educations` - słownik poziomów wykształcenia.

Metoda `OnModelCreating()` konfiguruje model EF Core oraz dane początkowe. Seedowane są kategorie, podkategorie, poziomy wykształcenia i przykładowe konta użytkowników.

### `Models/User.cs`

Encja kontaktu przechowywana w bazie. Zawiera między innymi:

- identyfikator,
- imię i nazwisko,
- unikalny adres email,
- zahashowane hasło,
- rolę,
- telefon, datę urodzenia i wiek,
- adres, stanowisko i notatki,
- identyfikatory kategorii, podkategorii i wykształcenia,
- nawigacyjne właściwości do encji słownikowych.

### `Controllers/AuthController.cs`

Kontroler endpointów uwierzytelniania.

- `Login(LoginRequestDTO)` - wyszukuje użytkownika po emailu, weryfikuje hasło i zwraca token JWT.
- `Register(RegisterRequestDTO)` - sprawdza poprawność danych, kontroluje unikalność emaila, hashuje hasło, tworzy konto zwykłego użytkownika i zwraca token JWT.

Endpointy logowania i rejestracji są dostępne bez uwierzytelnienia.

### `Controllers/ContactsController.cs`

Kontroler REST obsługujący kontakty.

- `GetAllPublicAsync()` - zwraca publiczną listę kontaktów; dostępny anonimowo.
- `GetAllAsync()` - zwraca pełne dane kontaktów dla użytkownika zalogowanego.
- `GetPublicByIdAsync(Guid id)` - zwraca podstawowe dane wybranego kontaktu.
- `GetByIdAsync(Guid id)` - zwraca pełne dane kontaktu.
- `CreateAsync(ContactCreateDTO)` - tworzy kontakt; wymaga roli administratora.
- `UpdateAsync(ContactUpdateDTO)` - aktualizuje kontakt; administrator może edytować dowolny kontakt, a zwykły użytkownik tylko własny.
- `DeleteAsync(Guid id)` - usuwa kontakt; wymaga roli administratora.

### `Controllers/CatalogController.cs`

Udostępnia dane słownikowe potrzebne formularzowi kontaktu.

- `GetContactFormOptionsAsync()` - zwraca kategorie, podkategorie i poziomy wykształcenia. Dane są pobierane z bazy i sortowane po nazwie.

Dla każdej kategorii zwracany jest również tryb podkategorii:

- `None` - brak podkategorii,
- `Dictionary` - wybór podkategorii ze słownika,
- `Custom` - wpisanie własnej podkategorii.

### `Services/UserService.cs`

Warstwa logiki biznesowej kontaktów.

- `QueryUsers()` - buduje wspólne zapytanie EF Core z dołączonymi kategoriami, podkategoriami i wykształceniem; używa `AsNoTracking()` dla operacji odczytu.
- `GetAllUsersAsync()` - pobiera wszystkie kontakty.
- `GetUserByIdAsync(Guid id)` - pobiera kontakt po identyfikatorze.
- `GetUserByEmailAsync(string email)` - wyszukuje użytkownika po emailu.
- `CreateUserAsync(User user, string? customSubcategoryName)` - tworzy kontakt, ustala domyślną kategorię prywatną, sprawdza unikalność emaila i poprawność referencji słownikowych.
- `UpdateUserAsync(User user, string? customSubcategoryName)` - aktualizuje kontakt i opcjonalnie zmienia hasło.
- `DeleteUserAsync(Guid id)` - usuwa kontakt.
- `ReferencesAreValidAsync(...)` - sprawdza zgodność kategorii z wybraną podkategorią, tworzy nową własną podkategorię, jeśli jest wymagana.

### `Services/JwtTokenService.cs`

- `CreateToken(User user)` - tworzy podpisany token JWT zawierający identyfikator użytkownika, email i rolę. Token jest ważny przez 60 minut.

### `Mappings/ContactMappings.cs` i `Mappings/UserMapppings.cs`

Zawierają metody rozszerzające mapujące:

- DTO żądań na encję `User`,
- encję `User` na DTO publiczne i szczegółowe.

### `Startup/AuthConfig.cs`

Konfiguruje JWT Bearer Authentication. Sprawdza issuer, audience i klucz podpisujący token.

### `Startup/CorsConfig.cs`

Konfiguruje politykę CORS dla adresu aplikacji Blazor przechowywanego w konfiguracji.

## 4. Frontend - klasy i komponenty

### `Program.cs`

Konfiguruje aplikację Blazor WebAssembly:

- rejestruje komponent główny `App`,
- tworzy klientów HTTP dla autoryzacji, kontaktów i katalogu,
- dodaje `AuthTokenHandler`,
- włącza `AuthorizationCore` i kaskadowy stan uwierzytelnienia,
- rejestruje `JwtAuthenticationStateProvider`.

### Komponenty stron

- `Pages/Auth/Login.razor` - formularz logowania.
- `Pages/Auth/Register.razor` - formularz rejestracji
- `Pages/Contacts/ContactsList.razor` - lista kontaktów.
- `Pages/Contacts/ContactDetails.razor` - szczegóły wybranego kontaktu oraz przyciski edycji i usuwania zgodne z rolą użytkownika.
- `Pages/Contacts/ContactUpsert.razor` - wspólny formularz tworzenia i edycji kontaktu. Dynamicznie pokazuje odpowiedni rodzaj podkategorii.
- `Pages/Contacts/ContactsCompotents/_ContactCard.razor` - prezentacja pojedynczego kontaktu.
- `Layout/NavMenu.razor` - menu nawigacyjne zależne od stanu zalogowania.

### `Models/ContactFormModel.cs`

Model formularza Blazora. Nie jest encją bazy danych ani DTO API.

- przechowuje wartości pól formularza,
- implementuje `IValidatableObject`,
- sprawdza wymagane hasło przy tworzeniu,
- sprawdza złożoność i zgodność haseł,
- sprawdza wymaganą kategorię i podkategorię,
- `ToCreateDto()` - tworzy DTO żądania POST,
- `ToUpdateDto()` - tworzy DTO żądania PUT,
- `FromContact(ContactDetailsDTO)` - wypełnia formularz danymi kontaktu bez hasła.

### `Services/AuthService.cs`

- `LoginAsync(...)` - wysyła dane logowania i zapisuje JWT w localStorage.
- `RegisterAsync(...)` - rejestruje konto i zapisuje zwrócony JWT.
- `LogoutAsync()` - usuwa token z localStorage.

### `Services/ContactService.cs`

- `GetAllContactsAsync()` - pobiera listę publiczną albo pełną, zależnie od stanu logowania.
- `GetContactDetailsByIdAsync(Guid id)` - pobiera szczegóły publiczne albo pełne.
- `GetEditableContactAsync(Guid id)` - pobiera dane do formularza edycji.
- `CreateContactAsync(ContactCreateDTO)` - wysyła POST.
- `UpdateContactAsync(ContactUpdateDTO)` - wysyła PUT.
- `DeleteContactAsync(Guid id)` - wysyła DELETE.

### `Services/CatalogService.cs`

Pobiera z API dane słownikowe do formularza kontaktu.

### `Auth/JwtAuthenticationStateProvider.cs`

Odczytuje token z localStorage, sprawdza jego poprawność i termin ważności oraz buduje stan użytkownika Blazora na podstawie claims.

### `Auth/AuthTokenHandler.cs`

Delegating handler dodający token JWT do nagłówka Authorization żądań HTTP kierowanych do API.

## 5. Projekt Shared - DTO i pomocnicze typy

Najważniejsze DTO:

- `LoginRequestDTO` i `LoginResponseDTO` - dane logowania i odpowiedź z tokenem,
- `RegisterRequestDTO` - dane rejestracji,
- `ContactCreateDTO` - dane tworzenia kontaktu,
- `ContactUpdateDTO` - dane edycji kontaktu,
- `ContactPublicDTO` - bezpieczne, podstawowe dane kontaktu,
- `ContactDetailsDTO` - pełne dane kontaktu bez hasła,
- `ContactFormOptionsDTO` - komplet danych słownikowych formularza,
- `CategoryOptionDTO` i `LookupOptionDTO` - pojedyncze opcje słowników.

## 6. Baza danych i bezpieczeństwo

- Baza danych: Microsoft SQL Server, domyślnie SQL Server LocalDB.
- Dostęp do bazy: Entity Framework Core i ApplicationDbContext.
- Zmiany schematu: migracje EF Core w katalogu ContactsManager.Api/Data/Migrations.
- Hasła: przechowywane wyłącznie jako hash generowany przez PasswordHasher.
- Uwierzytelnianie: tokeny JWT Bearer.
- Autoryzacja: role Admin i User oraz kontrola właściciela kontaktu.
- Dane w odpowiedziach API: brak haseł i hashy haseł.
- CORS: ograniczony do skonfigurowanego adresu klienta.
- Walidacja: wykonywana zarówno w formularzu klienta, jak i po stronie API.

## 7. Wykorzystane biblioteki i technologie

### Backend

- .NET 10,
- ASP.NET Core Web API,
- Entity Framework Core 10,
- `Microsoft.EntityFrameworkCore.SqlServer` - provider SQL Server,
- `Microsoft.AspNetCore.Authentication.JwtBearer` - JWT Bearer Authentication,
- `Microsoft.AspNetCore.Identity` - hashowanie haseł,
- `Microsoft.AspNetCore.OpenApi` i `Scalar.AspNetCore` - dokumentacja i testowanie API.

### Frontend

- Blazor WebAssembly,
- `Microsoft.AspNetCore.Components.Authorization`,
- `Microsoft.AspNetCore.Components.WebAssembly`,
- `Microsoft.Extensions.Http` - klientów HTTP i obsługa komunikacji z API.

## 8. Wymagania środowiskowe

- Windows lub system obsługujący .NET,
- .NET SDK 10,
- SQL Server LocalDB albo inny SQL Server,
- Visual Studio 2022/2026

## 10. Kompilacja i uruchomienie

### Przywrócenie pakietów

Z katalogu głównego repozytorium:

```bash
dotnet restore ContactsManager.Api/ContactsManager.Api.csproj
dotnet restore ContactsManager.Client/ContactsManager.Client.csproj
dotnet restore ContactsManager.Tests/ContactsManager.Tests.csproj
```

Na Windows można używać również ścieżek z separatorem `\\`.

### Kompilacja

```bash
dotnet build ContactsManager.Api/ContactsManager.Api.csproj
dotnet build ContactsManager.Client/ContactsManager.Client.csproj
```

Kompilacja całego repozytorium może zostać wykonana poleceniem:

```bash
dotnet build
```

### Uruchomienie API

```bash
dotnet run --project ContactsManager.Api/ContactsManager.Api.csproj
```

Przy starcie API automatycznie wykonywane są dostępne migracje EF Core.

### Uruchomienie klienta

W drugim terminalu:

```bash
dotnet run --project ContactsManager.Client/ContactsManager.Client.csproj
```

Następnie należy otworzyć adres HTTPS wyświetlony przez .NET CLI lub Visual Studio.

### Uruchomienie w Visual Studio

1. Otworzyć projekt lub rozwiązanie w Visual Studio.
2. Ustawić `ContactsManager.Api` i `ContactsManager.Client` jako projekty uruchamiane
3. Sprawdzić adres API w `wwwroot/appsettings.json` klienta.
4. Uruchomić aplikację profilem HTTPS.
5. Przy pierwszym uruchomieniu API zastosuje migrację i utworzy bazę danych.

## 11. Najważniejsze endpointy REST API

| Metoda | Endpoint | Dostęp | Opis |
|---|---|---|---|
| `POST` | `/api/auth/login` | anonimowy | Logowanie i wydanie JWT |
| `POST` | `/api/auth/register` | anonimowy | Rejestracja zwykłego użytkownika |
| `GET` | `/api/contacts` | anonimowy | Publiczna lista kontaktów |
| `GET` | `/api/contacts/{id}` | anonimowy | Publiczne dane kontaktu |
| `GET` | `/api/contacts/full` | zalogowany | Pełna lista kontaktów |
| `GET` | `/api/contacts/{id}/full` | zalogowany | Pełne dane kontaktu |
| `POST` | `/api/contacts` | administrator | Utworzenie kontaktu |
| `PUT` | `/api/contacts` | właściciel/admin | Edycja kontaktu |
| `DELETE` | `/api/contacts/{id}/delete` | administrator | Usunięcie kontaktu |
| `GET` | `/api/catalog/contact-form` | zalogowany | Dane słownikowe formularza |
