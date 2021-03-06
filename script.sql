USE [ITCompanyDB]
GO
/****** Object:  Table [dbo].[AdminCredentialsTable]    Script Date: 12/18/2020 12:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminCredentialsTable](
	[Admin_Name] [varchar](150) NOT NULL,
	[Admin_Password] [varchar](500) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeDetailsTable]    Script Date: 12/18/2020 12:16:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeDetailsTable](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Employee_FirstName] [varchar](250) NOT NULL,
	[Employee_LastName] [varchar](250) NOT NULL,
	[Employee_Age] [int] NOT NULL,
	[Employee_Gender] [varchar](50) NOT NULL,
	[Employee_Address] [varchar](500) NOT NULL,
	[Employee_Phone] [varchar](100) NOT NULL,
	[Employee_State] [varchar](100) NOT NULL,
	[Employee_Designation] [varchar](250) NOT NULL,
 CONSTRAINT [PK_EmployeeDetailsTable] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeProjectMapping]    Script Date: 12/18/2020 12:16:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeProjectMapping](
	[EmployeeID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTechnologyMapping]    Script Date: 12/18/2020 12:16:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTechnologyMapping](
	[EmployeeID] [int] NOT NULL,
	[TechnologyID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectDetailsTable]    Script Date: 12/18/2020 12:16:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDetailsTable](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_ProjectDetailsTable] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TechnologyDetailsTable]    Script Date: 12/18/2020 12:16:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TechnologyDetailsTable](
	[TechnologyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_TechnologyDetailsTable] PRIMARY KEY CLUSTERED 
(
	[TechnologyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AdminCredentialsTable] ([Admin_Name], [Admin_Password]) VALUES (N'Admin', N'OzO8tvUkT4EcArzBe63pog==')
INSERT [dbo].[AdminCredentialsTable] ([Admin_Name], [Admin_Password]) VALUES (N'Admin2', N'AT7gO/ouSP0Wr7KuIsRRow==')
INSERT [dbo].[AdminCredentialsTable] ([Admin_Name], [Admin_Password]) VALUES (N'Admin3', N'CQBtNFKyW+jiCCv5MNQYqg==')
SET IDENTITY_INSERT [dbo].[EmployeeDetailsTable] ON 

INSERT [dbo].[EmployeeDetailsTable] ([EmployeeID], [Employee_FirstName], [Employee_LastName], [Employee_Age], [Employee_Gender], [Employee_Address], [Employee_Phone], [Employee_State], [Employee_Designation]) VALUES (1, N'Rahul', N'Thangaraj', 22, N'Male', N'Chennai', N'6381534313', N'Tamil Nadu', N'Developer')
INSERT [dbo].[EmployeeDetailsTable] ([EmployeeID], [Employee_FirstName], [Employee_LastName], [Employee_Age], [Employee_Gender], [Employee_Address], [Employee_Phone], [Employee_State], [Employee_Designation]) VALUES (2, N'Neon', N'Dev', 21, N'Male', N'Siruseri', N'9878675645', N'TN', N'Software Architech')
INSERT [dbo].[EmployeeDetailsTable] ([EmployeeID], [Employee_FirstName], [Employee_LastName], [Employee_Age], [Employee_Gender], [Employee_Address], [Employee_Phone], [Employee_State], [Employee_Designation]) VALUES (3, N'Jaya', N'Dhurai', 35, N'Female', N'Madras', N'9686484578', N'DL', N'Primary Manager')
SET IDENTITY_INSERT [dbo].[EmployeeDetailsTable] OFF
INSERT [dbo].[EmployeeTechnologyMapping] ([EmployeeID], [TechnologyID]) VALUES (2, 4)
INSERT [dbo].[EmployeeTechnologyMapping] ([EmployeeID], [TechnologyID]) VALUES (3, 1)
INSERT [dbo].[EmployeeTechnologyMapping] ([EmployeeID], [TechnologyID]) VALUES (1, 3)
SET IDENTITY_INSERT [dbo].[ProjectDetailsTable] ON 

INSERT [dbo].[ProjectDetailsTable] ([ProjectID], [Name]) VALUES (1, N'Microsoft')
SET IDENTITY_INSERT [dbo].[ProjectDetailsTable] OFF
SET IDENTITY_INSERT [dbo].[TechnologyDetailsTable] ON 

INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (1, N'c#')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (2, N'Python')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (3, N'C')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (4, N'C++')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (5, N'Hadoop')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (6, N'JavaScript')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (7, N'BI')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (8, N'ML')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (9, N'DevOps')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (10, N'AWS')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (11, N'Jenkins')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (12, N'Selinium')
INSERT [dbo].[TechnologyDetailsTable] ([TechnologyID], [Name]) VALUES (13, N'White Hat')
SET IDENTITY_INSERT [dbo].[TechnologyDetailsTable] OFF
ALTER TABLE [dbo].[EmployeeProjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjectMapping_EmployeeDetailsTable] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[EmployeeDetailsTable] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeProjectMapping] CHECK CONSTRAINT [FK_EmployeeProjectMapping_EmployeeDetailsTable]
GO
ALTER TABLE [dbo].[EmployeeProjectMapping]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeProjectMapping_ProjectDetailsTable] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[ProjectDetailsTable] ([ProjectID])
GO
ALTER TABLE [dbo].[EmployeeProjectMapping] CHECK CONSTRAINT [FK_EmployeeProjectMapping_ProjectDetailsTable]
GO
ALTER TABLE [dbo].[EmployeeTechnologyMapping]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTechnologyMapping_EmployeeDetailsTable] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[EmployeeDetailsTable] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeTechnologyMapping] CHECK CONSTRAINT [FK_EmployeeTechnologyMapping_EmployeeDetailsTable]
GO
ALTER TABLE [dbo].[EmployeeTechnologyMapping]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTechnologyMapping_TechnologyDetailsTable] FOREIGN KEY([TechnologyID])
REFERENCES [dbo].[TechnologyDetailsTable] ([TechnologyID])
GO
ALTER TABLE [dbo].[EmployeeTechnologyMapping] CHECK CONSTRAINT [FK_EmployeeTechnologyMapping_TechnologyDetailsTable]
GO
