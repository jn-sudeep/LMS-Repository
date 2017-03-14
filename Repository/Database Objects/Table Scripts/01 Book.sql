
-- Create Table Script : Book

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Book]
	(
		[ID] 		[Int]		IDENTITY(1, 1) NOT NULL
						CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
						([ID] ASC) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
		[Name] 		[VarChar](30) 	NOT NULL
						Constraint [Book_Name_Unique] Unique,
		[IssueDate] 	[datetime] 	NULL,
		[MemberID] 	[int] 		NULL
	) 

GO
SET ANSI_PADDING OFF