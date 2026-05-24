PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS KeyMappings (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Action TEXT NOT NULL,
    Key TEXT NOT NULL,
    Device TEXT NOT NULL,
    Scope TEXT NOT NULL CHECK(Scope IN ('Global','PageSpecific','Disabled')),
    Enabled INTEGER NOT NULL DEFAULT 1 CHECK(Enabled IN (0,1)),
    CreatedAt TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    UpdatedAt TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    UNIQUE(Key, Device, Scope)
);

CREATE INDEX IF NOT EXISTS IX_KeyMappings_Action ON KeyMappings(Action);

CREATE TRIGGER IF NOT EXISTS TRG_KeyMappings_UpdatedAt
AFTER UPDATE ON KeyMappings
FOR EACH ROW
BEGIN
    UPDATE KeyMappings
    SET UpdatedAt = (strftime('%Y-%m-%dT%H:%M:%fZ','now'))
    WHERE Id = NEW.Id;
END;
