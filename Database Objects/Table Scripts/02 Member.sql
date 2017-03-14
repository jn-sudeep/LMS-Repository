
-- Create Table Script : Member

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Member]
	(
		[ID] 			[Int] 		IDENTITY(1, 1) NOT NULL
							CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
							([ID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
		[Name] 			[VarChar](30) 	NOT NULL
							Constraint [Member_Name_Unique] Unique

	) 

GO
SET ANSI_PADDING OFF