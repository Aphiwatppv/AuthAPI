CREATE PROCEDURE [dbo].[spRegisterUser]
    @UserName NVARCHAR(255),
    @PasswordHash NVARCHAR(256),
    @Salt NVARCHAR(256),
    @Email NVARCHAR(255),
    @IsActive BIT,
    @Result INT OUTPUT  -- Add an output parameter to indicate success or failure
AS
BEGIN
    SET @Result = -1  -- Default to a failure code

    BEGIN TRY
        IF NOT EXISTS (SELECT * FROM [dbo].[AuthenMainUser] WHERE UserName = @UserName OR Email = @Email)
        BEGIN
            INSERT INTO [dbo].[AuthenMainUser] (UserName, PasswordHash, Salt, Email, IsActive)
            VALUES (@UserName, @PasswordHash, @Salt, @Email, @IsActive)
            
            SET @Result = 0  -- Indicate success
        END
        ELSE
        BEGIN
            SET @Result = 1  -- Indicate username or email exists
        END
    END TRY
    BEGIN CATCH
        SET @Result = ERROR_NUMBER() -- Or a custom error code
    END CATCH
END
GO
