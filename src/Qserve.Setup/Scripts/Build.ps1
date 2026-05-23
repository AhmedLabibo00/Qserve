param(
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$out = Join-Path $root "out"

Write-Host "Building Qserve ($Configuration)..."
dotnet restore "$root\..\Qserve.sln"
dotnet build "$root\..\Qserve.sln" -c $Configuration --no-restore

New-Item -ItemType Directory -Force -Path $out | Out-Null
Write-Host "Build completed."
