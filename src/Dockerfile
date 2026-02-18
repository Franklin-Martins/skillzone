FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 8.*

EXPOSE 8080

CMD bash -c "dotnet restore && dotnet watch run --urls http://0.0.0.0:8080"
