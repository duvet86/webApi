CREATE SCHEMA core;
GO

CREATE TABLE core.Person (
	Pk_Person_Id int IDENTITY(1,1) PRIMARY KEY,
	LastName nvarchar(255) NOT NULL,
	FirstName nvarchar(255) NOT NULL,
	DateOfBirth date NOT NULL
);
GO

CREATE TABLE core.Login (
	Pk_Login_Id int IDENTITY(1,1) PRIMARY KEY,
	Username nvarchar(255) UNIQUE NOT NULL,
	PasswordHash BINARY(64) NOT NULL,
	PasswordSalt UNIQUEIDENTIFIER,
	Fk_Person_Id int UNIQUE FOREIGN KEY REFERENCES core.PERSON(Pk_Person_Id)
);
GO

CREATE FUNCTION core.IsValidUser (
	@p_UserName NVARCHAR(254),
    @p_Password NVARCHAR(50)
)
RETURNS bit
AS 
BEGIN
	DECLARE @userID INT

	IF EXISTS (SELECT TOP 1 Pk_Login_Id FROM core.Login WHERE Username = @p_UserName)
		BEGIN
			SET @userID = (
				SELECT Pk_Login_Id FROM core.Login
				WHERE Username = @p_UserName AND PasswordHash=HASHBYTES('SHA2_512', @p_Password + CAST(PasswordSalt AS NVARCHAR(36)))
			)

			IF(@userID IS NULL)
			RETURN 0;
		ELSE 
			RETURN 1;
		END

	RETURN 0;
END;
GO

CREATE PROCEDURE core.RegisterUser
    @p_UserName nvarchar(255), 
    @p_Password nvarchar(255),
	@p_LastName nvarchar(255),
    @p_FirstName nvarchar(255),
	@p_DateOfBirth date,
    @responseMessage nvarchar(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER = NEWID()
    BEGIN TRY
		BEGIN TRANSACTION

			INSERT INTO core.Person(LastName, FirstName, DateOfBirth)
			VALUES(@p_LastName, @p_FirstName, @p_DateOfBirth)

			INSERT INTO core.Login(Username, PasswordHash, PasswordSalt, Fk_Person_Id)
			VALUES(@p_UserName, HASHBYTES('SHA2_512', @p_Password + CAST(@salt AS NVARCHAR(36))), @salt, (SELECT SCOPE_IDENTITY()))

		   SET @responseMessage='Success'

		COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
		ROLLBACK TRANSACTION
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH
END