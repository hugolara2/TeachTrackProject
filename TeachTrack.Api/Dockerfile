# Use a build argument to set the UID for the non-root user
ARG APP_UID=1000

# 1. Build Stage: Optimized for layer caching
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

# Copy only project files first to leverage Docker cache
COPY ./TeachTrack.Api/*.csproj ./TeachTrack.Api/
COPY ./TeachTrack.Data/*.csproj ./TeachTrack.Data/
# (Add a line for each project in your solution)

# Restore dependencies
RUN dotnet restore ./TeachTrack.Api/TeachTrack.Api.csproj

# Now copy the rest of the source code
COPY . .

# Publish the application
ARG TARGETARCH
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./TeachTrack.Api/TeachTrack.Api.csproj -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS development
WORKDIR /source

COPY ./TeachTrack.Api/*.csproj ./TeachTrack.Api/
COPY ./TeachTrack.Model/*.csproj ./TeachTrack.Model/
COPY ./TeachTrack.Service/*.csproj ./TeachTrack.Service/
COPY ./TeachTrack.Core/*.csproj ./TeachTrack.Core/

RUN dotnet restore ./TeachTrack.Api/TeachTrack.Api.csproj

COPY . .

WORKDIR /source/TeachTrack.Api

CMD ["dotnet", "watch", "run", "--no-launch-profile", "--urls", "http://0.0.0.0:8080"]

# 2. Final Stage: Creates the small, secure production image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app .

# Create a non-root user and switch to it
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
