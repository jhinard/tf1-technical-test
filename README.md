# TF1.LeaveManagement

## 🧱 Architecture
The project follows the principles of Domain-Driven Design (DDD)

It is divided into 4 layers :

 - **Domain**: business logic (entities, aggregates, enums, value objects)
 - **Application**: application services and interfaces
 - **Infrastructure**: implementation of an in-memory repository
 - **API**: RESTful controller


## 🚀 How to run

    dotnet build
    dotnet run --project src/TF1.LeaveManagement.Api

and go to: [http://localhost:7146/index.html](http://localhost:7146/index.html)

#### API Endpoints

#### User Story 1 : Soumettre une demande de congé

-   `POST /api/v1/leave-requests`
    
Payload :
```json
{
  "employeeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "startDate": "2025-04-20",
  "endDate": "2025-04-20",
  "type": "Vacation",
  "comment": "Un commentaire optionnel, bla bla"
}
```

#### User Story 2 : Approuver ou rejeter une demande

-   `POST /api/v1/leave-requests/{id}/approve`

Payload :
```json
{
  "comment": "un commentaire"
}
```

    
-   `POST /api/v1/leave-requests/{id}/reject`

Payload :
```json
{
  "comment": "un commentaire"
}
```

#### Bonus : Lister les demandes

-   `GET /api/v1/leave-requests`
    
    Allows you to test memory persistence


