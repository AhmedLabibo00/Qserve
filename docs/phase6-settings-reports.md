# Qserve 10.1 — Phase 6 (Settings and Reports)

## Implemented Settings
- Language
- Appearance
- Services
- Counters
- Audio
- Printing
- Displays
- About (model-ready)

## Implemented Daily Summary
- Customers
- Services
- Average Waiting
- Finished
- Transfer
- Cancelled

## Export
- PDF
- Print
- Backup

## Queue Reset Modes
- Daily
- Monthly
- Continuous
- Manual

## Day Lifecycle
- Start New Day
- Archive Database

## Notes
- Implemented as business/services layer contracts and logic.
- No UI implementation included in this phase.

## Key Mapping System
- Added configurable mapping model for keyboard/touch/external input device actions.
- Default shortcuts include F11/F/B/R/T/Enter/F10 and Service 1..9 mapped to 1..9.
- Added conflict detection (duplicate key-device-scope blocked).
- Added JSON import/export hooks for `KeyMappings.json`.
- Added reset options (page/default/all) service-level support.
- Added debounce guard (200 ms) to prevent key spam.
- Added UI page skeleton: `KeyMappingSettingsView.xaml`.
