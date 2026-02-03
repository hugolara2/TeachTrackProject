# Use a build argument to set the UID for the non-root user
ARG APP_UID=1000

# ==========================================
# 1. Build Stage
# ==========================================
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /source

ARG BUILD_ARGS_HUSKY=1 

# --- COPY CSPROJ FILES ---
# 1. The Core (Domain Entities)
COPY ./TeachTrack.Core/*.csproj ./TeachTrack.Core/

# 2. The Infrastructure (DbContext & Configurations)
COPY ./TeachTrack.Infrastructure/*.csproj ./TeachTrack.Infrastructure/

# 3. The Services (Application Logic) 
COPY ./TeachTrack.Service/*.csproj ./TeachTrack.Service/

# 4. The API (Entry Point)
COPY ./TeachTrack.Api/*.csproj ./TeachTrack.Api/

# --- RESTORE ---
# Restore the API. Since API depends on Infra, and Infra depends on Core, 
# this single command restores EVERYTHING.
RUN dotnet restore ./TeachTrack.Api/TeachTrack.Api.csproj /p:HUSKY=${BUILD_ARGS_HUSKY}

# --- COPY SOURCE & BUILD ---
COPY . .

ARG TARGETARCH
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./TeachTrack.Api/TeachTrack.Api.csproj -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

# ==========================================
# 2. Development Stage
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS development
WORKDIR /src

# Copy all project definitions again for the Dev environment
COPY ./TeachTrack.Core/*.csproj ./TeachTrack.Core/
COPY ./TeachTrack.Infrastructure/*.csproj ./TeachTrack.Infrastructure/
COPY ./TeachTrack.Service/*.csproj ./TeachTrack.Service/
COPY ./TeachTrack.Api/*.csproj ./TeachTrack.Api/

# Restore dependencies
RUN dotnet restore ./TeachTrack.Api/TeachTrack.Api.csproj /p:HUSKY=0 

COPY . .
WORKDIR /src/TeachTrack.Api

CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://0.0.0.0:8080"]

# ==========================================
# 3. Final Stage
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app
COPY --from=build /app .

RUN adduser \
    --system \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${APP_UID}" \
    appuser
USER appuser

ENTRYPOINT ["dotnet", "TeachTrack.Api.dll"]