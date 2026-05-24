# Qserve 10.1 — Phase 5 (Printing and Audio)

## Printing
Implemented:
- Printer selection via `IPrinterDiscovery` + `PrintingService.GetAvailablePrinters()`
- Paper size options (`58mm`, `80mm`, `A4`)
- Logo path support in ticket content
- Header and footer fields
- Ticket preview generation pipeline
- Print payload generation pipeline

Ticket content contract includes:
- Logo
- Facility
- Queue
- Current
- Waiting
- Date
- Time
- Address
- Footer

## Audio
Implemented:
- Modes: Silent, Bell, Announcement
- Bell volume and announcement volume validation (`0..100`)
- Upload format validation for: mp3, wav, ogg
- Test Bell and Test Announcement actions

## Notes
- This phase provides service/contracts/models logic only.
- No UI changes were added in this phase.
