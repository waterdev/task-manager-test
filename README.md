
# task-manager

# Getting started

No external dependencies ,simply dotnet run TaskManager.Api
InMemory Db seeds itself with initial data, you can use this list of predefined IDs to test 

Guid TaskListId = new Guid("3582b784-669e-49c9-bcf4-0d4b336e8003");<br />
Guid UserId = new Guid("dbe4dd38-54b7-4b9c-a831-a480c74bfd91");<br />
Guid UserId2 = new Guid("5bab8aa3-e66c-421d-a2cf-02708d47b929");<br />
Guid UserId3 = new Guid("5e747c0b-4c42-4ed3-9592-409105546e3a");<br />

## Built with
Solution implemented with Clean Architecture in mind, it consists of 4 main layers ((Api, Infra) - Presentation -> Application -> Domain
( which has our table modules from EF)

EF Core with InMemoryDb(Code first approach), Automapper so we don't leak our domain entities outside, 
CQRS with MediatR library for our Presentation -> Application communication,
Repository pattern without UOW for the sake of simplicity
   
##Notes
Access policies are implemented only in DeleteTaskList, RevokeUserAccessToTaskList