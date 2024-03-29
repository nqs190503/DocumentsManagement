USE [13_NguyenQuangSu_TranLongKhanh_Project]
GO
/****** Object:  Table [dbo].[documents]    Script Date: 15/03/2024 5:34:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documents](
	[fileId] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [varchar](100) NOT NULL,
	[fileContent] [varchar](max) NOT NULL,
	[fileStatus] [bit] NOT NULL,
	[userId] [int] NOT NULL,
	[lastModifier] [datetime] NULL,
 CONSTRAINT [PK_documents] PRIMARY KEY CLUSTERED 
(
	[fileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userdetails]    Script Date: 15/03/2024 5:34:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userdetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Password] [varchar](150) NOT NULL,
	[Mobile] [char](10) NOT NULL,
 CONSTRAINT [PK_userdetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[versions]    Script Date: 15/03/2024 5:34:20 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[versions](
	[versionID] [int] IDENTITY(1,1) NOT NULL,
	[docID] [int] NOT NULL,
	[updatedContent] [nvarchar](max) NULL,
	[updatedTime] [datetime] NULL,
 CONSTRAINT [PK_versions] PRIMARY KEY CLUSTERED 
(
	[versionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[documents] ON 

INSERT [dbo].[documents] ([fileId], [fileName], [fileContent], [fileStatus], [userId], [lastModifier]) VALUES (1, N'su', N'asd123123', 0, 1, CAST(N'2024-03-04T21:46:37.983' AS DateTime))
INSERT [dbo].[documents] ([fileId], [fileName], [fileContent], [fileStatus], [userId], [lastModifier]) VALUES (5, N'su1', N'123', 0, 1, CAST(N'2024-03-04T21:59:43.890' AS DateTime))
SET IDENTITY_INSERT [dbo].[documents] OFF
GO
SET IDENTITY_INSERT [dbo].[userdetails] ON 

INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (1, N'su', N'user@mail.com', N'123', N'123       ')
INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (2, N'user1@mail.com', N'123@gmail.com', N'123', N'1234567890')
INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (3, N'user@mail.com', N'su@gmail.com', N'1234', N'1234567890')
INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (4, N'user@mail.com', N'su@gmail.com', N'1234', N'1234567890')
INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (5, N'user@mail.com', N'su@gmail.com', N'123', N'1234567890')
INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (6, N'user@mail.com', N'su@gmail.com', N'123', N'1234567890')
INSERT [dbo].[userdetails] ([Id], [Name], [Email], [Password], [Mobile]) VALUES (7, N'user@mail.com', N'su@gm.com', N'123', N'1234567890')
SET IDENTITY_INSERT [dbo].[userdetails] OFF
GO
SET IDENTITY_INSERT [dbo].[versions] ON 

INSERT [dbo].[versions] ([versionID], [docID], [updatedContent], [updatedTime]) VALUES (1, 1, N'su123', CAST(N'2024-03-04T00:00:00.000' AS DateTime))
INSERT [dbo].[versions] ([versionID], [docID], [updatedContent], [updatedTime]) VALUES (2, 1, N'asdasdasd', CAST(N'2024-03-04T21:46:37.860' AS DateTime))
INSERT [dbo].[versions] ([versionID], [docID], [updatedContent], [updatedTime]) VALUES (3, 5, N'123', CAST(N'2024-03-04T21:58:53.317' AS DateTime))
INSERT [dbo].[versions] ([versionID], [docID], [updatedContent], [updatedTime]) VALUES (4, 5, N'thay doi ban moi', CAST(N'2024-03-04T21:59:24.797' AS DateTime))
SET IDENTITY_INSERT [dbo].[versions] OFF
GO
ALTER TABLE [dbo].[documents]  WITH CHECK ADD  CONSTRAINT [FK_documents_userdetails] FOREIGN KEY([userId])
REFERENCES [dbo].[userdetails] ([Id])
GO
ALTER TABLE [dbo].[documents] CHECK CONSTRAINT [FK_documents_userdetails]
GO
ALTER TABLE [dbo].[versions]  WITH CHECK ADD  CONSTRAINT [FK_versions_documents] FOREIGN KEY([docID])
REFERENCES [dbo].[documents] ([fileId])
GO
ALTER TABLE [dbo].[versions] CHECK CONSTRAINT [FK_versions_documents]
GO
