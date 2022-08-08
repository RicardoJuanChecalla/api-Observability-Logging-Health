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

--Update-Package -projectName Discount.Api
--dotnet nuget delete Microsoft.AspNetCore.Mvc 1.0 --non-interactive
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

https://docs.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-6.0
MongoDB.Driver.IMongoCollection to FindAsync with .net core 6.0
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
https://www.mongodb.com/developer/languages/csharp/create-restful-api-dotnet-core-mongodb/
https://qappdesign.com/code/using-mongodb-with-net-core-webapi/
https://docs.portainer.io/start/intro
https://hub.docker.com/r/dpage/pgadmin4