# BuyBaseAddressBook
.Net Core 5 Microservices Arthitecture RabbitMQ, MongoDB, Redis


Start Container
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

Stop Container
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down

Some docker commands

docker-compose build
docker-compose up --build

docker ps -aq
docker stop $(docker ps -aq)
docker rmi $(docker images -a)
docker system prune
docker images


dotnet test /p:CollectCoverage=true
