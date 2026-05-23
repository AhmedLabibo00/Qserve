# Qserve 10.1

Offline-first radiology queue management desktop application.

## Build Targets
- Production installer: `Qserve_Setup_v10.1.exe`
- Portable package: `Qserve_Portable.zip`
- Build scripts: `Build.ps1`, `Publish.ps1`

## Quick Build
```powershell
./Build.ps1 -Configuration Release
./Publish.ps1 -Configuration Release -Runtime win-x64
```

## Technology
- C#
- WPF
- SQLite

No internet dependency is required at runtime.
