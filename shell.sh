#!/bin/sh
set -e # Exit early if any commands fail

(
  cd "$(dirname "$0")" # Ensure compile steps are run within the repository directory
  /opt/homebrew/bin/dotnet build --configuration Release --output /tmp/shell-csharp shell-csharp.csproj
)

exec /opt/homebrew/bin/dotnet /tmp/shell-csharp/shell-csharp.dll "$@"
