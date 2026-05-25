PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS SecuritySettings (
    Id INTEGER PRIMARY KEY CHECK (Id = 1),
    SettingsPassword TEXT NOT NULL,
    PasswordEnabled INTEGER NOT NULL DEFAULT 1 CHECK (PasswordEnabled IN (0,1)),
    LastChanged TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    FailedAttempts INTEGER NOT NULL DEFAULT 0 CHECK (FailedAttempts >= 0)
);

INSERT OR IGNORE INTO SecuritySettings (Id, SettingsPassword, PasswordEnabled)
VALUES (1, '0000', 1);
