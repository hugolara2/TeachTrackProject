# Use a build argument to set the UID for the non-root user
ARG APP_UID=1000

# 1. Build Stage: Optimized for layer caching
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

# Define a new argument to control Husky during the build
ARG BUILD_ARGS_HUSKY=1 

# Copy only project files first to leverage Docker cache
COPY ./TeachTrack.Api/*.csproj ./TeachTrack.Api/
COPY ./TeachTrack.Data/*.csproj ./TeachTrack.Data/
# (Add a line for each project in your solution)

# Restore dependencies
# Pass the property explicitly to the dotnet command during the build
RUN dotnet restore ./TeachTrack.Api/TeachTrack.Api.csproj /p:HUSKY=${BUILD_ARGS_HUSKY}

# Now copy the rest of the source code
COPY . .

# Publish the application
ARG TARGETARCH
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./TeachTrack.Api/TeachTrack.Api.csproj -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS development
WORKDIR /src

COPY ./TeachTrack.Api/*.csproj ./TeachTrack.Api/
COPY ./TeachTrack.Model/*.csproj ./TeachTrack.Model/
COPY ./TeachTrack.Service/*.csproj ./TeachTrack.Service/
COPY ./TeachTrack.Core/*.csproj ./TeachTrack.Core/

# We also need to pass the property here to prevent failure during dotnet restore in development stage
RUN dotnet restore ./TeachTrack.Api/TeachTrack.Api.csproj /p:HUSKY=0 

COPY . .

WORKDIR /src/TeachTrack.Api

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