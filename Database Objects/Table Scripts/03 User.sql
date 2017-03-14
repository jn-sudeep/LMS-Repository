
-- Create Table Script : User

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[User]
	(
		[ID] 		[Int] 		IDENTITY(1, 1) NOT NULL
						CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
						([ID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
		[Name] 		[VarChar](30) 	NOT NULL
						Constraint [User_Name_Unique] Unique,
		[Password] 	[VarChar](50) 	NOT NULL,
		[IsAdmin]	[Bit]		NULL
	) 

GO
SET ANSI_PADDING OFF