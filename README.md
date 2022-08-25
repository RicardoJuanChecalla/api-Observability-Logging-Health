https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-sln

mkdir aspnet-microservices
cd aspnet-microservices
dotnet new sln
dotnet new webapi -au none -o Services/Catalog/Catalog.Api
dotnet sln add Services/Catalog/Catalog.Api
dotnet add ./Services/Catalog/Catalog.Api/Catalog.Api.csproj package MongoDB.Driver --version 2.17.1
dotnet new webapi -au none -o Services/Basket/Basket.Api
dotnet sln add Services/Basket/Basket.Api
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj package Microsoft.Extensions.Caching.StackExchangeRedis
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj package Newtonsoft.Json
dotnet new webapi -au none -o Services/Discount/Discount.Api
dotnet sln add Services/Discount/Discount.Api
dotnet add ./Services/Discount/Discount.Api/Discount.Api.csproj package Npgsql
dotnet add ./Services/Discount/Discount.Api/Discount.Api.csproj package Dapper
dotnet new grpc -au none --no-https -o Services/Discount/Discount.Grpc
dotnet sln add Services/Discount/Discount.Grpc
dotnet add ./Services/Discount/Discount.Grpc/Discount.Grpc.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet new webapi -au none -o Services/Ordering/Ordering.Api
dotnet sln add Services/Ordering/Ordering.Api
dotnet new classlib -o Services/Ordering/Ordering.Domain
dotnet sln add Services/Ordering/Ordering.Domain
dotnet new classlib -o Services/Ordering/Ordering.Application
dotnet sln add Services/Ordering/Ordering.Application
dotnet new classlib -o Services/Ordering/Ordering.Infrastructure
dotnet sln add Services/Ordering/Ordering.Infrastructure
dotnet add Services/Ordering/Ordering.Application/Ordering.Application.csproj reference Services/Ordering/Ordering.Domain/Ordering.Domain.csproj
dotnet add Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj reference Services/Ordering/Ordering.Application/Ordering.Application.csproj
dotnet add Services/Ordering/Ordering.Api/Ordering.Api.csproj reference Services/Ordering/Ordering.Application/Ordering.Application.csproj
dotnet add Services/Ordering/Ordering.Api/Ordering.Api.csproj reference Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj
dotnet add ./Services/Ordering/Ordering.Application/Ordering.Application.csproj package AutoMapper
dotnet add ./Services/Ordering/Ordering.Application/Ordering.Application.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add ./Services/Ordering/Ordering.Application/Ordering.Application.csproj package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add ./Services/Ordering/Ordering.Application/Ordering.Application.csproj package Microsoft.Extensions.Logging.Abstractions
dotnet add ./Services/Ordering/Ordering.Application/Ordering.Application.csproj package FluentValidation
dotnet add ./Services/Ordering/Ordering.Application/Ordering.Application.csproj package FluentValidation.DependencyInjectionExtensions
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj package Microsoft.EntityFrameworkCore.Tools
dotnet add ./Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add ./Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj package SendGrid
dotnet add ./Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Design
dotnet new classlib -o BuildingBlocks/EventBus.Messages
dotnet sln add BuildingBlocks/EventBus.Messages
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj reference ./BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj package MassTransit --version 7.3.1
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj package MassTransit.RabbitMQ --version 7.3.1
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj package MassTransit.AspNetCore --version 7.3.1
dotnet add ./Services/Basket/Basket.Api/Basket.Api.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj reference ./BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj package MassTransit --version 7.3.1
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj package MassTransit.RabbitMQ --version 7.3.1
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj package MassTransit.AspNetCore --version 7.3.1
dotnet add ./Services/Ordering/Ordering.Api/Ordering.Api.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet new web -o ApiGateways/OcelotApiGw
dotnet sln add ApiGateways/OcelotApiGw
dotnet add ./ApiGateways/OcelotApiGw/OcelotApiGw.csproj package Ocelot
dotnet add ./ApiGateways/OcelotApiGw/OcelotApiGw.csproj package Ocelot.Cache.CacheManager
dotnet new webapi -au none -o ApiGateways/Shopping.Aggregator
dotnet sln add ApiGateways/Shopping.Aggregator

-------------------------------------------------------------------------------------------------------------------------------------
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef migrations add ./Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj InitialCreate
dotnet ef migrations add Migration_Initial --configuration ./Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj
--Update-Package -projectName Discount.Api
--dotnet nuget delete Microsoft.AspNetCore.Mvc 1.0 --non-interactive
https://github.com/mehmetozkaya/AspnetMicroservices/blob/main/src/Services/Ordering/Ordering.Application/Behaviours/UnhandledExceptionBehaviour.cs
https://medium.com/aspnetrun/building-ocelot-api-gateway-microservices-with-asp-net-core-and-docker-container-13f96026e86c
PM>Add-Migration InitialCreate
-----------------------------------------------------------------
docker pull mongo
docker run -d -p 27017:27017 --name shopping-mongo mongo:latest
docker logs -f shopping-mongo
docker exec -it shopping-mongo /bin/bash
	ls
	mongo
	show dbs
	use CatalogDb
	db.createCollection('Products')
	db.Products.insert 
	db.Products.insertMany([{ 'Name':'Asus Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':54.93 }, { 'Name':'HP 		Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':88.93 } ])
	db.Products.find({}).pretty()
	db.Products.remove({})
	show databases
	show collections
	db.Products.find({}).pretty()
docker run -d -p 3000:3000 mongoclient/mongoclient
----------------------------------------------------------------
docker pull redis
docker run -d -p 6379:6379 --name aspnet-redis redis
docker exec -it aspnet-redis /bin/bash
	redis-cli
	ping
	set key value
	get key value
	set name Ricardo
	get name
-----------------------------------------------------------------
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down
 docker ps -aq
 docker stop $(docker ps -aq)
 docker rm $(docker ps -aq)
 docker rmi $(docker images -q)
 docker system prune
-----------------------------------------------------------------
dotnet restore ./services/catalog/catalog.api/catalog.api.csproj
dotnet build ./services/catalog/catalog.api/catalog.api.csproj
dotnet run --project ./services/catalog/catalog.api/catalog.api.csproj
-----------------------------------------------------------------
dotnet restore ./services/basket/basket.api/basket.api.csproj
dotnet build ./services/Basket/Basket.api/Basket.api.csproj
dotnet run --project ./services/Basket/Basket.api/Basket.api.csproj
-----------------------------------------------------------------
dotnet restore ./services/Discount/Discount.api/Discount.api.csproj
dotnet build ./services/Discount/Discount.api/Discount.api.csproj
dotnet run --project ./services/Discount/Discount.api/Discount.api.csproj
-----------------------------------------------------------------
dotnet restore ./services/Discount/Discount.Grpc/Discount.Grpc.csproj
dotnet build ./services/Discount/Discount.Grpc/Discount.Grpc.csproj
dotnet run --project ./services/Discount/Discount.Grpc/Discount.Grpc.csproj
-----------------------------------------------------------------
dotnet restore ./services/Ordering/Ordering.api/Ordering.api.csproj
dotnet build ./services/Ordering/Ordering.api/Ordering.api.csproj
dotnet run --project ./services/Ordering/Ordering.api/Ordering.api.csproj
-----------------------------------------------------------------
dotnet restore ./BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj
dotnet build ./BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj
dotnet run --project ./BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj
-----------------------------------------------------------------
dotnet restore ./ApiGateways/OcelotApiGw/OcelotApiGw.csproj
dotnet build ./ApiGateways/OcelotApiGw/OcelotApiGw.csproj
dotnet run --project ./ApiGateways/OcelotApiGw/OcelotApiGw.csproj
-----------------------------------------------------------------
dotnet restore ./ApiGateways/Shopping.Aggregator/Shopping.Aggregator.csproj
dotnet build ./ApiGateways/Shopping.Aggregator/Shopping.Aggregator.csproj
dotnet run --project ./ApiGateways/Shopping.Aggregator/Shopping.Aggregator.csproj
-----------------------------------------------------------------
dotnet restore ./WebApps/AspnetRunBasics/AspnetRunBasics.csproj
dotnet build ./WebApps/AspnetRunBasics/AspnetRunBasics.csproj
dotnet run --project ./WebApps/AspnetRunBasics/AspnetRunBasics.csproj

-----------------------------------------------------------------

https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0
MongoDB.Driver.IMongoCollection to FindAsync with .net core 6.0
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
https://www.mongodb.com/developer/languages/csharp/create-restful-api-dotnet-core-mongodb/
https://qappdesign.com/code/using-mongodb-with-net-core-webapi/
https://docs.portainer.io/start/intro
https://hub.docker.com/r/dpage/pgadmin4