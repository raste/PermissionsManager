USE [master]
GO
/****** Object:  Database [PermissionsManagement]    Script Date: 09/06/2011 11:18:21 ******/
CREATE DATABASE [PermissionsManagement]
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'PermissionsManagement', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PermissionsManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PermissionsManagement] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [PermissionsManagement] SET ANSI_NULLS OFF
GO
ALTER DATABASE [PermissionsManagement] SET ANSI_PADDING OFF
GO
ALTER DATABASE [PermissionsManagement] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [PermissionsManagement] SET ARITHABORT OFF
GO
ALTER DATABASE [PermissionsManagement] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [PermissionsManagement] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [PermissionsManagement] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [PermissionsManagement] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [PermissionsManagement] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [PermissionsManagement] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [PermissionsManagement] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [PermissionsManagement] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [PermissionsManagement] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [PermissionsManagement] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [PermissionsManagement] SET  DISABLE_BROKER
GO
ALTER DATABASE [PermissionsManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [PermissionsManagement] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [PermissionsManagement] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [PermissionsManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [PermissionsManagement] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [PermissionsManagement] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [PermissionsManagement] SET  READ_WRITE
GO
ALTER DATABASE [PermissionsManagement] SET RECOVERY SIMPLE
GO
ALTER DATABASE [PermissionsManagement] SET  MULTI_USER
GO
ALTER DATABASE [PermissionsManagement] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [PermissionsManagement] SET DB_CHAINING OFF
GO
USE [PermissionsManagement]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 09/06/2011 11:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[username] [nvarchar](100) NOT NULL,
	[password] [nvarchar](300) NOT NULL,
	[name] [nvarchar](300) NOT NULL,
	[mainAdmin] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [Uix_User_Username] ON [dbo].[Users] 
(
	[username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Controls]    Script Date: 09/06/2011 11:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Controls](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Controls] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 09/06/2011 11:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [Uix_Role_Name] ON [dbo].[Roles] 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 09/06/2011 11:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[User] [bigint] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[dateAdded] [datetime] NOT NULL,
	[visible] [bit] NOT NULL,
	[solved] [bit] NOT NULL,
	[about] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Roles]    Script Date: 09/06/2011 11:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Roles](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[User] [bigint] NOT NULL,
	[Role] [bigint] NOT NULL,
	[active] [bit] NOT NULL,
 CONSTRAINT [PK_User_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [Uix_User_Role] ON [dbo].[User_Roles] 
(
	[User] ASC,
	[Role] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles_Controls]    Script Date: 09/06/2011 11:18:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles_Controls](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Role] [bigint] NOT NULL,
	[Control] [bigint] NOT NULL,
	[enabled] [bit] NOT NULL,
	[visible] [bit] NOT NULL,
 CONSTRAINT [PK_Roles_Controls] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [Uidx_Role_Control] ON [dbo].[Roles_Controls] 
(
	[Role] ASC,
	[Control] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Reports_User]    Script Date: 09/06/2011 11:18:21 ******/
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD  CONSTRAINT [FK_Reports_User] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Reports] CHECK CONSTRAINT [FK_Reports_User]
GO
/****** Object:  ForeignKey [FK_UserRoles_Role]    Script Date: 09/06/2011 11:18:21 ******/
ALTER TABLE [dbo].[User_Roles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[User_Roles] CHECK CONSTRAINT [FK_UserRoles_Role]
GO
/****** Object:  ForeignKey [FK_UserRoles_User]    Script Date: 09/06/2011 11:18:21 ******/
ALTER TABLE [dbo].[User_Roles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_User] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[User_Roles] CHECK CONSTRAINT [FK_UserRoles_User]
GO
/****** Object:  ForeignKey [FK_Roles_ControlsRoles_Control]    Script Date: 09/06/2011 11:18:21 ******/
ALTER TABLE [dbo].[Roles_Controls]  WITH CHECK ADD  CONSTRAINT [FK_Roles_ControlsRoles_Control] FOREIGN KEY([Control])
REFERENCES [dbo].[Controls] ([ID])
GO
ALTER TABLE [dbo].[Roles_Controls] CHECK CONSTRAINT [FK_Roles_ControlsRoles_Control]
GO
/****** Object:  ForeignKey [FK_RolesControls_Role]    Script Date: 09/06/2011 11:18:21 ******/
ALTER TABLE [dbo].[Roles_Controls]  WITH CHECK ADD  CONSTRAINT [FK_RolesControls_Role] FOREIGN KEY([Role])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[Roles_Controls] CHECK CONSTRAINT [FK_RolesControls_Role]
GO

INSERT INTO [dbo].[Controls] ([name]) VALUES ('Search for users')
INSERT INTO [dbo].[Controls] ([name]) VALUES ('Add report')
INSERT INTO [dbo].[Controls] ([name]) VALUES ('See visible reports')
INSERT INTO [dbo].[Controls] ([name]) VALUES ('See deleted reports')
INSERT INTO [dbo].[Controls] ([name]) VALUES ('Mark reports as solved')
INSERT INTO [dbo].[Controls] ([name]) VALUES ('Delete reports')
GO
