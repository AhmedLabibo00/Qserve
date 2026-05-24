PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS Settings (
    SettingKey TEXT PRIMARY KEY,
    SettingValue TEXT NOT NULL,
    UpdatedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    CHECK(length(trim(SettingKey)) > 0)
);

CREATE TABLE IF NOT EXISTS Services (
    ServiceId INTEGER PRIMARY KEY AUTOINCREMENT,
    ServiceCode TEXT NOT NULL UNIQUE,
    ServiceNameAr TEXT NOT NULL,
    ServiceNameEn TEXT NOT NULL,
    QueuePrefix TEXT NOT NULL UNIQUE,
    IsActive INTEGER NOT NULL DEFAULT 1 CHECK(IsActive IN (0,1)),
    CreatedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    CHECK(length(trim(ServiceCode)) > 0),
    CHECK(length(trim(QueuePrefix)) BETWEEN 1 AND 8)
);

CREATE TABLE IF NOT EXISTS Counters (
    CounterId INTEGER PRIMARY KEY AUTOINCREMENT,
    CounterCode TEXT NOT NULL UNIQUE,
    CounterNameAr TEXT NOT NULL,
    CounterNameEn TEXT NOT NULL,
    IsActive INTEGER NOT NULL DEFAULT 1 CHECK(IsActive IN (0,1)),
    CreatedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    CHECK(length(trim(CounterCode)) > 0)
);

CREATE TABLE IF NOT EXISTS ServiceCounterMapping (
    MappingId INTEGER PRIMARY KEY AUTOINCREMENT,
    ServiceId INTEGER NOT NULL,
    CounterId INTEGER NOT NULL,
    Priority INTEGER NOT NULL DEFAULT 100,
    IsActive INTEGER NOT NULL DEFAULT 1 CHECK(IsActive IN (0,1)),
    CreatedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    UNIQUE(ServiceId, CounterId),
    FOREIGN KEY(ServiceId) REFERENCES Services(ServiceId) ON DELETE CASCADE,
    FOREIGN KEY(CounterId) REFERENCES Counters(CounterId) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Tickets (
    TicketId INTEGER PRIMARY KEY AUTOINCREMENT,
    TicketNumber TEXT NOT NULL UNIQUE,
    ServiceId INTEGER NOT NULL,
    CounterId INTEGER NULL,
    Status TEXT NOT NULL CHECK(Status IN ('Waiting','Called','Serving','Completed','Cancelled','Transferred')),
    QueueDate TEXT NOT NULL,
    QueueSequence INTEGER NOT NULL,
    CreatedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    CalledAtUtc TEXT NULL,
    ClosedAtUtc TEXT NULL,
    CHECK(length(trim(TicketNumber)) > 0),
    CHECK(QueueSequence > 0),
    FOREIGN KEY(ServiceId) REFERENCES Services(ServiceId),
    FOREIGN KEY(CounterId) REFERENCES Counters(CounterId),
    UNIQUE(ServiceId, QueueDate, QueueSequence)
);

CREATE TABLE IF NOT EXISTS Transfers (
    TransferId INTEGER PRIMARY KEY AUTOINCREMENT,
    TicketId INTEGER NOT NULL,
    FromServiceId INTEGER NULL,
    ToServiceId INTEGER NOT NULL,
    FromCounterId INTEGER NULL,
    ToCounterId INTEGER NULL,
    Reason TEXT NULL,
    TransferredAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    FOREIGN KEY(TicketId) REFERENCES Tickets(TicketId) ON DELETE CASCADE,
    FOREIGN KEY(FromServiceId) REFERENCES Services(ServiceId),
    FOREIGN KEY(ToServiceId) REFERENCES Services(ServiceId),
    FOREIGN KEY(FromCounterId) REFERENCES Counters(CounterId),
    FOREIGN KEY(ToCounterId) REFERENCES Counters(CounterId)
);

CREATE TABLE IF NOT EXISTS Displays (
    DisplayId INTEGER PRIMARY KEY AUTOINCREMENT,
    DisplayCode TEXT NOT NULL UNIQUE,
    DisplayName TEXT NOT NULL,
    AssignedCounterId INTEGER NULL,
    IsActive INTEGER NOT NULL DEFAULT 1 CHECK(IsActive IN (0,1)),
    LastHeartbeatUtc TEXT NULL,
    FOREIGN KEY(AssignedCounterId) REFERENCES Counters(CounterId)
);

CREATE TABLE IF NOT EXISTS Audio (
    AudioId INTEGER PRIMARY KEY AUTOINCREMENT,
    AudioKey TEXT NOT NULL UNIQUE,
    LanguageCode TEXT NOT NULL CHECK(LanguageCode IN ('ar','en')),
    FilePath TEXT NOT NULL,
    IsEnabled INTEGER NOT NULL DEFAULT 1 CHECK(IsEnabled IN (0,1)),
    UpdatedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now'))
);

CREATE TABLE IF NOT EXISTS Statistics (
    StatisticsId INTEGER PRIMARY KEY AUTOINCREMENT,
    ServiceId INTEGER NOT NULL,
    CounterId INTEGER NULL,
    StatDate TEXT NOT NULL,
    TicketsIssued INTEGER NOT NULL DEFAULT 0 CHECK(TicketsIssued >= 0),
    TicketsServed INTEGER NOT NULL DEFAULT 0 CHECK(TicketsServed >= 0),
    AvgWaitSeconds INTEGER NOT NULL DEFAULT 0 CHECK(AvgWaitSeconds >= 0),
    AvgServeSeconds INTEGER NOT NULL DEFAULT 0 CHECK(AvgServeSeconds >= 0),
    UNIQUE(ServiceId, CounterId, StatDate),
    FOREIGN KEY(ServiceId) REFERENCES Services(ServiceId),
    FOREIGN KEY(CounterId) REFERENCES Counters(CounterId)
);

CREATE TABLE IF NOT EXISTS Archives (
    ArchiveId INTEGER PRIMARY KEY AUTOINCREMENT,
    ArchiveType TEXT NOT NULL,
    SourceTable TEXT NOT NULL,
    SourceRecordId INTEGER NOT NULL,
    PayloadJson TEXT NOT NULL,
    ArchivedAtUtc TEXT NOT NULL DEFAULT (strftime('%Y-%m-%dT%H:%M:%fZ','now')),
    CHECK(json_valid(PayloadJson))
);

CREATE INDEX IF NOT EXISTS IX_Tickets_Service_Status_Date ON Tickets(ServiceId, Status, QueueDate);
CREATE INDEX IF NOT EXISTS IX_Tickets_Counter_Status_Date ON Tickets(CounterId, Status, QueueDate);
CREATE INDEX IF NOT EXISTS IX_Transfers_TicketId ON Transfers(TicketId);
CREATE INDEX IF NOT EXISTS IX_ServiceCounterMapping_ServiceId ON ServiceCounterMapping(ServiceId);
CREATE INDEX IF NOT EXISTS IX_ServiceCounterMapping_CounterId ON ServiceCounterMapping(CounterId);

CREATE TRIGGER IF NOT EXISTS TRG_Services_QueuePrefix_NoWhitespace
BEFORE INSERT ON Services
FOR EACH ROW
WHEN NEW.QueuePrefix LIKE '% %'
BEGIN
    SELECT RAISE(ABORT, 'QueuePrefix must not contain whitespace');
END;
