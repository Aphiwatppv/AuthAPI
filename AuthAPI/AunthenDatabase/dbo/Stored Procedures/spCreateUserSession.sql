CREATE PROCEDURE [dbo].[spCreateUserSession]
    @UserID INT,
    @SessionStart DATETIMEOFFSET = NULL
AS
BEGIN
    -- Use current system time if no session start time is provided
    IF @SessionStart IS NULL
    BEGIN
        SET @SessionStart = SYSDATETIMEOFFSET()
    END

    -- Insert a new session record for the user
    INSERT INTO [dbo].[SessionManager] (UserID, TimeSpentInSystem, SessionStart, SessionEnd)
    VALUES (@UserID, 0, @SessionStart, NULL)

    -- Retrieve and return the SessionID of the newly created session
    DECLARE @SessionID INT = SCOPE_IDENTITY()
    SELECT @SessionID AS SessionID
END
GO
