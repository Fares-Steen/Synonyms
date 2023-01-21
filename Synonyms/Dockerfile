#docker push faresst/fmCreditService:latest2
#docker build -t faresst/creditservice:latest1 -f FM.CreditService/Dockerfile .
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY  Synonyms/src ./Synonyms/src

WORKDIR /source/Synonyms/src
RUN dotnet restore SynonymsApi
RUN find /source
RUN dotnet publish SynonymsApi -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SynonymsApi.dll"]