# Installation and Deployment

## Production Installer
Installer package name: `Qserve_Setup_v10.1.exe`

### Supports
- Install / Upgrade / Repair / Uninstall
- Silent install (planned WiX CLI):
  - `Qserve_Setup_v10.1.exe /quiet`

### Paths
- Application: `C:\Program Files\Qserve\`
- Data: `C:\ProgramData\Qserve\`

### Data Preserved on Upgrade
- `Qserve.db`
- `Backup`
- `Archive`
- `Voices`, `Bells`
- `Logos`
- `Config`

## Portable
`Qserve_Portable.zip` extracts to:
- `Qserve.exe`
- `Qserve.db`
- `Voices/`, `Bells/`, `Logos/`, `Backup/`, `Archive/`, `Config/`

Portable mode runs with local settings and no installer.
