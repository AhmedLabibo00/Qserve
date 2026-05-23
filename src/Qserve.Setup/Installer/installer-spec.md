# Qserve Installer Spec (v10.1)

Output: `Qserve_Setup_v10.1.exe`

## Engine
Use WiX Toolset (MSI + Burn bootstrapper) to support:
- Install
- Upgrade
- Repair
- Uninstall
- Silent install (`/quiet`)
- Wizard UX

## Install Targets
- App binaries: `C:\Program Files\Qserve\`
- User/system data: `C:\ProgramData\Qserve\`

Create on install:
- `Database`
- `Backup`
- `Archive`
- `Voices`
- `Bells`
- `Logos`
- `Config`

## ARP Registration
Register Qserve in Add/Remove Programs with:
- Product name: Qserve
- Version: 10.1
- Publisher: Labibcoplast
- Contact: +201065400659 / +201128839129
- Email: aahmedlabib98@gmail.com

## Shortcuts
- Desktop shortcut
- Start Menu shortcut
- Program folder

## Upgrade Rules
- Detect previous installation
- Offer upgrade path
- Preserve `Qserve.db`, settings, audio files, logos, archive, backups

## Uninstall Modes
1. Remove application only (keep ProgramData)
2. Remove application and user data (delete ProgramData)

## Startup Options (stored in Config)
- Start With Windows
- Launch Full Screen
- Open Last Screen
- Auto Backup
- Auto Restore

## Backup Policy
- Automatic + manual backup
- Restore backup flow
- Retention: last 30 backups in `C:\ProgramData\Qserve\Backup\`
