param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$repo = Join-Path $root ".."
$outRoot = Join-Path $repo "dist"
$installRoot = Join-Path $outRoot "installer"
$portableRoot = Join-Path $outRoot "Qserve_Portable"

New-Item -ItemType Directory -Force -Path $installRoot,$portableRoot | Out-Null

# Publish EXE
$uiProj = Join-Path $repo "src\Qserve.UI\Qserve.UI.csproj"
dotnet publish $uiProj -c $Configuration -r $Runtime --self-contained false -p:PublishSingleFile=true -o $installRoot

# ProgramData seed folders (for installer custom actions)
$programDataLayout = @("Database","Backup","Archive","Voices","Bells","Logos","Config")
foreach($folder in $programDataLayout){ New-Item -ItemType Directory -Force -Path (Join-Path $installRoot "ProgramDataTemplate\$folder") | Out-Null }

# Portable package layout
Copy-Item "$installRoot\Qserve.exe" "$portableRoot\Qserve.exe" -Force
$portableFolders = @("Voices","Bells","Logos","Backup","Archive","Config")
foreach($folder in $portableFolders){ New-Item -ItemType Directory -Force -Path (Join-Path $portableRoot $folder) | Out-Null }
if(Test-Path (Join-Path $repo "Qserve.db")) { Copy-Item (Join-Path $repo "Qserve.db") (Join-Path $portableRoot "Qserve.db") -Force }

Compress-Archive -Path "$portableRoot\*" -DestinationPath (Join-Path $outRoot "Qserve_Portable.zip") -Force
Write-Host "Publish assets created under dist/."
