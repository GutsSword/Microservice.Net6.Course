FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/FreeCourse.Shared.csproj Shared/Course.Shared/
COPY Services/Discount/FreeCourse.DiscountService/*.csproj Services/Discount/FreeCourse.DiscountService/
RUN dotnet restore Services/Discount/FreeCourse.DiscountService/*.csproj
COPY . .
RUN dotnet publish Services/Discount/FreeCourse.DiscountService/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.DiscountService.dll" ]
