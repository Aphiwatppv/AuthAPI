CREATE TABLE [dbo].[SessionManager]
(
    SessionID INT IDENTITY(1000000,1) PRIMARY KEY,
    UserID INT NOT NULL,
    TimeSpentInSystem INT NOT NULL,
    SessionStart DATETIMEOFFSET NOT NULL,
    SessionEnd DATETIMEOFFSET NULL,
    FOREIGN KEY (UserID) REFERENCES AuthenMainUser(UserID)
)