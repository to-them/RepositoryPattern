USE [dbTest]
GO
/****** Object:  StoredProcedure [dbo].[spCreateStudent]    Script Date: 12/26/2022 12:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spCreateStudent]
	-- Add the parameters for the stored procedure here
	@Name VARCHAR(100),
    @Email VARCHAR(50),
    @Mobile VARCHAR(50),
	@Gender	VARCHAR(20),
	@DateOfBirth datetime,
    @Id int Out 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Students VALUES (@Name,@Email,@Mobile,@Gender,@DateOfBirth)
     SELECT @Id = SCOPE_IDENTITY()
END
