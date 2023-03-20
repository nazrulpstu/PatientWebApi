USE [PatientDB]
GO

/****** Object: Table [dbo].[Appuser] Script Date: 2023-03-19 11:54:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Appuser] (
    [Id]       INT          NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [UserName] VARCHAR (10) NULL,
    [PassWord] VARCHAR (10) NULL,
    [Email]    VARCHAR (50) NULL,
    [isActive] SMALLINT     NULL
);
INSERT INTO [dbo].[Appuser] ([Id], [Name], [UserName], [PassWord], [Email], [isActive]) VALUES (1, N'nazrul', N'nazrul', N'12345', N'nazrulca77@gmail.com', 1)

/*********/
USE [PatientDB]
GO

/****** Object: Table [dbo].[patient] Script Date: 2023-03-19 11:54:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[patient] (
    [Id]        INT           NOT NULL,
    [FirstName] VARCHAR (50)  NULL,
    [LastName]  VARCHAR (50)  NULL,
    [FullName]  VARCHAR (50)  NULL,
    [Age]       SMALLINT      NULL,
    [Gender]    VARCHAR (30)  NULL,
    [Address]   VARCHAR (100) NULL,
    [City]      VARCHAR (50)  NULL,
    [Province]  VARCHAR (50)  NULL,
    [PostCode]  VARCHAR (8)   NULL,
    [IsActive]  SMALLINT      NULL,
    [IsDelete]  SMALLINT      NULL,
    [IsUpdate]  SMALLINT      NULL
);
/***********************/
CREATE PROCEDURE [dbo].[PatientInfoCRUD]
	 @StatementType NVARCHAR(20) = '',
						  @id integer = 0,
                         @FirstName VARCHAR(10)= '',
                         @LastName VARCHAR(10)='',
                         @FullName VARCHAR(10)='',
                         @Age     INTEGER =0,
						 @Gender VARCHAR(10)='',@Address VARCHAR(10)='',@City VARCHAR(10)='',
						 @Province VARCHAR(10)='',@PostCode VARCHAR(10)='',@IsActive integer=1,@IsDelete integer=0

AS
	  BEGIN
	SET NOCOUNT ON;
	  IF @StatementType = 'Insert'
        BEGIN

	  DECLARE @InsertedId INT;
	  DECLARE @MaxId INT;
	  SELECT @MaxId=isnull(MAX(Id),0)+1 FROM patient;

            INSERT INTO patient
                        (Id,FirstName,LastName,FullName,Age,Gender,Address,City,Province,PostCode,IsActive,IsDelete)
						OUTPUT INSERTED.Id
            VALUES     ( @MaxId,
                         @FirstName,
                         @LastName,
                         @FullName,
                         @Age,
						 @Gender,@Address,@City,@Province,@PostCode,@IsActive,@IsDelete);
						 SET @InsertedId = SCOPE_IDENTITY();
        END

		  IF @StatementType = 'Select'
        BEGIN
            SELECT *
            FROM   patient;
        END
		 IF @StatementType = 'Update'
        BEGIN
            UPDATE patient
            SET   FirstName=@FirstName,
			LastName=@LastName,
			FullName=@FullName,
			Age=@Age,
			Gender=@Gender,
			Address=@Address,
			City=@City,
			Province=@Province,
			PostCode=@PostCode,
			IsActive=@IsActive,
			IsDelete=@IsDelete
            WHERE  Id = @id;
        END
      ELSE IF @StatementType = 'Delete'
        BEGIN
            DELETE FROM patient
            WHERE  Id = @id;
        END

	  END

