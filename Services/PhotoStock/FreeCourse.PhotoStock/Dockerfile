FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/FreeCourse.Shared.csproj Shared/Course.Shared/
COPY Services/PhotoStock/FreeCourse.PhotoStock/*.csproj Services/PhotoStock/FreeCourse.PhotoStock/
RUN dotnet restore Services/PhotoStock/FreeCourse.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/FreeCourse.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.PhotoStock.dll" ]
