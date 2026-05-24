PRAGMA foreign_keys = ON;

-- Extra guard against duplicate queue format by validating TicketNumber pattern prefix+sequence.
-- Expected format example: XR-0001
CREATE TRIGGER IF NOT EXISTS TRG_Tickets_TicketNumber_Format
BEFORE INSERT ON Tickets
FOR EACH ROW
WHEN NEW.TicketNumber NOT GLOB '[A-Za-z0-9][A-Za-z0-9]*-[0-9][0-9][0-9][0-9]*'
BEGIN
    SELECT RAISE(ABORT, 'Invalid ticket format. Expected PREFIX-NUMBER');
END;
