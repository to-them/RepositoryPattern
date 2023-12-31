USE [dbTest]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStudent]    Script Date: 12/26/2022 1:19:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spUpdateStudent] 
	-- Add the parameters for the stored procedure here
	@Id int,
	@Name VARCHAR(100),
    @Email VARCHAR(50),
    @Mobile VARCHAR(50),
	@Gender	VARCHAR(20),
	@DateOfBirth datetime  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Students SET [Name]=@Name, Email=@Email, Mobile=@Mobile, Gender=@Gender, DateOfBirth=@DateOfBirth
	WHERE Id=@Id
END
