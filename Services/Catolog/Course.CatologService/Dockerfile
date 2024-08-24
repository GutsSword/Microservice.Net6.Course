FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/FreeCourse.Shared.csproj Shared/Course.Shared/
COPY Services/Catolog/Course.CatologService/*.csproj Services/Catolog/Course.CatologService/
RUN dotnet restore Services/Catolog/Course.CatologService/*.csproj
COPY . .
RUN dotnet publish Services/Catolog/Course.CatologService/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.CatologService.dll" ]
