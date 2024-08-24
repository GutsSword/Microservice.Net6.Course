FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/FreeCourse.Shared.csproj Shared/Course.Shared/
COPY Services/Order/FreeCourse.Services.OrderService.Application/*.csproj Services/Order/FreeCourse.Services.OrderService.Application/
COPY Services/Order/FreeCourse.Services.OrderService.Core/*.csproj Services/Order/FreeCourse.Services.OrderService.Core/
COPY Services/Order/FreeCourse.Services.OrderService.Domain/*.csproj Services/Order/FreeCourse.Services.OrderService.Domain/
COPY Services/Order/FreeCourse.Services.OrderService.Domain.Core/*.csproj Services/Order/FreeCourse.Services.OrderService.Domain.Core/
COPY Services/Order/FreeCourse.Services.OrderService.Infrastructure/*.csproj Services/Order/FreeCourse.Services.OrderService.Infrastructure/
COPY Services/Order/FreeCourse.Services.OrderService.Api/*.csproj Services/Order/FreeCourse.Services.OrderService.Api/
RUN dotnet restore Services/Order/FreeCourse.Services.OrderService.Api/*.csproj
COPY . .
RUN dotnet publish Services/Order/FreeCourse.Services.OrderService.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.Services.OrderService.Api.dll" ]
