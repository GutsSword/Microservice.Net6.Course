FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/Course.Shared/FreeCourse.Shared.csproj Shared/Course.Shared/
COPY Services/FakePayment/FreeCourse.FakePaymentService/*.csproj Services/FakePayment/FreeCourse.FakePaymentService/
RUN dotnet restore Services/FakePayment/FreeCourse.FakePaymentService/*.csproj
COPY . .
RUN dotnet publish Services/FakePayment/FreeCourse.FakePaymentService/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeCourse.FakePaymentService.dll" ]
