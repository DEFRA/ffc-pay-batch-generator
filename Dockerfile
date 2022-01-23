ARG PARENT_VERSION=1.2.10-dotnet6.0

# Development
FROM defradigital/dotnetcore-development:${PARENT_VERSION} AS development
ARG PARENT_VERSION
LABEL uk.gov.defra.ffc.parent-image=defradigital/dotnetcore-development:${PARENT_VERSION}

COPY --chown=dotnet:dotnet ./Directory.Build.props ./Directory.Build.props
COPY --chown=dotnet:dotnet ./FFCPayBatchGenerator/*.csproj ./FFCPayBatchGenerator/
RUN dotnet restore ./FFCPayBatchGenerator/FFCPayBatchGenerator.csproj
COPY --chown=dotnet:dotnet ./FFCPayBatchGenerator/ ./FFCPayBatchGenerator/
RUN dotnet publish ./FFCPayBatchGenerator/ -c Release -o /home/dotnet/out

ENTRYPOINT dotnet watch --project ./FFCPayBatchGenerator run

# Production
FROM defradigital/dotnetcore:${PARENT_VERSION} AS production
ARG PARENT_VERSION
LABEL uk.gov.defra.ffc.parent-image=defradigital/dotnetcore:${PARENT_VERSION}
COPY --from=development /home/dotnet/out/ ./

# Override entrypoint using shell form so that environment variables are picked up
ENTRYPOINT dotnet FFCPayBatchGenerator.dll
