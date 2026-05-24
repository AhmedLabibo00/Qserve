# Qserve.db generation

`Qserve.db` is intentionally not committed because some PR systems reject binary files.

Generate locally from migrations:

```bash
sqlite3 Qserve.db < database/migrations/001_init_qserve.sql
sqlite3 Qserve.db < database/migrations/002_ticket_number_uniqueness.sql
sqlite3 Qserve.db < database/migrations/003_schema_version.sql
```

This keeps the repository text-only and PR-friendly.
