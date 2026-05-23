# Repository Contracts (Database Scope Only)

This folder defines repository boundaries for the SQLite layer. No UI/business logic.

## Repositories
- `ISettingsRepository`
- `IServicesRepository`
- `ICountersRepository`
- `IServiceCounterMappingRepository`
- `ITicketsRepository`
- `ITransfersRepository`
- `IDisplaysRepository`
- `IAudioRepository`
- `IStatisticsRepository`
- `IArchivesRepository`

## Common Rules
- Use parameterized SQL only.
- All writes run in transactions.
- Enforce foreign keys with `PRAGMA foreign_keys = ON` per connection.
- Migration runner must execute scripts in lexical order from `database/migrations`.
