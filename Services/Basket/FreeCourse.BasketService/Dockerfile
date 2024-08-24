FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/FreeCourse.Shared.csproj Shared/Course.Shared/
COPY Services/Basket/FreeCourse.BasketService/*.csproj Services/Basket/FreeCourse.BasketService/
RUN dotnet restore Services/Basket/FreeCourse.BasketService/*.csproj
COPY . .
RUN dotnet publish Services/Basket/FreeCourse.BasketService/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.BasketService.dll" ]
