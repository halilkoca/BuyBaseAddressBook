# BuyBaseAddressBook
.Net Core 5 Microservices Arthitecture RabbitMQ, MongoDB, Redis  


Start Docker Compose
```
docker-compose -f .docker-compose.yml -f .docker-compose.override.yml up -d
```

```
Ocelot Gateway API Base Url -> http://localhost:8010/
Contact.API -> http://localhost:8000/swagger/index.html
Report.API -> http://localhost:8001/swagger/index.html
```

Ocelot End Point List:  

```
http://localhost:8010/Contact/Get
http://localhost:8010/Contact/Get/{id}
http://localhost:8010/Contact/GetByName/{name}
http://localhost:8010/Contact/Create
http://localhost:8010/Contact/CreateBulk
http://localhost:8010/Contact/Update
http://localhost:8010/Contact/Delete/{id}
http://localhost:8010/Contact/DeleteBulk
http://localhost:8010/ContactInformation/Create
http://localhost:8010/ContactInformation/Update
http://localhost:8010/ContactInformation/Delete
http://localhost:8010/ContactInformation/DeleteBulk
```


Down Docker Compose
```
docker-compose -f .docker-compose.yml -f .docker-compose.override.yml down  
```

Some docker commands
```
docker-compose build
docker-compose up --build
docker ps -aq
docker stop $(docker ps -aq)
docker rmi $(docker images -a)
docker system prune
docker images
```
```
dotnet test /p:CollectCoverage=true
```
