# Qserve 10.1 — Phase 2 (Database Only)

## Database
- Name: `Qserve.db`
- Engine: Embedded SQLite
- Mode: Offline local file only

## Generated Items
- Schema: `database/migrations/001_init_qserve.sql`
- Validation/ticket format trigger: `database/migrations/002_ticket_number_uniqueness.sql`
- Migration version marker: `database/migrations/003_schema_version.sql`
- Repository boundaries: `database/repositories/README.md`

## Tables
- Settings
- Services
- Counters
- ServiceCounterMapping
- Tickets
- Transfers
- Displays
- Audio
- Statistics
- Archives

## Relations
- `ServiceCounterMapping.ServiceId -> Services.ServiceId`
- `ServiceCounterMapping.CounterId -> Counters.CounterId`
- `Tickets.ServiceId -> Services.ServiceId`
- `Tickets.CounterId -> Counters.CounterId`
- `Transfers.TicketId -> Tickets.TicketId`
- `Transfers.FromServiceId/ToServiceId -> Services.ServiceId`
- `Transfers.FromCounterId/ToCounterId -> Counters.CounterId`
- `Displays.AssignedCounterId -> Counters.CounterId`
- `Statistics.ServiceId -> Services.ServiceId`
- `Statistics.CounterId -> Counters.CounterId`

## Mapping Rules Covered
- Multiple Services -> Same Counter: supported by many-to-many mapping table.
- One Service -> Multiple Counters: supported by many-to-many mapping table.

## Validation Rules Included
- Unique service code and queue prefix.
- Queue prefix no whitespace.
- Unique counter code.
- Unique service/counter pair in mapping.
- Ticket status enumeration check.
- Positive queue sequence check.
- Duplicate queue format prevention:
  - `Tickets.TicketNumber` is UNIQUE.
  - Trigger enforces `PREFIX-NUMBER` format.
  - Unique `(ServiceId, QueueDate, QueueSequence)`.
