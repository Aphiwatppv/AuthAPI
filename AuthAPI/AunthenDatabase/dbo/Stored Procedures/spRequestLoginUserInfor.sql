CREATE PROCEDURE [dbo].[spRequestLoginUserInfor]
    @UserName NVARCHAR(255)
AS
BEGIN
    SELECT 
        UserID,
        PasswordHash,
        Salt

    FROM [dbo].[AuthenMainUser]
    WHERE UserName = @UserName
END
GO
