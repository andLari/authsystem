USE [master]
GO
/****** Object:  Database [db10]    Script Date: 04.06.2024 15:13:10 ******/
CREATE DATABASE [db10]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db10', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\db10.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'db10_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\db10_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [db10] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db10].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db10] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db10] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db10] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db10] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db10] SET ARITHABORT OFF 
GO
ALTER DATABASE [db10] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db10] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db10] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db10] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db10] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db10] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db10] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db10] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db10] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db10] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db10] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db10] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db10] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db10] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db10] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db10] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db10] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db10] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db10] SET  MULTI_USER 
GO
ALTER DATABASE [db10] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db10] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db10] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db10] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [db10]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 04.06.2024 15:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 04.06.2024 15:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDRequest] [int] NOT NULL,
	[Resources] [varchar](50) NOT NULL,
	[Time] [varchar](50) NOT NULL,
	[Price] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 04.06.2024 15:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateOfAdd] [datetime] NOT NULL,
	[Hardware] [varchar](50) NOT NULL,
	[TypeOfProblem] [varchar](300) NOT NULL,
	[Description] [varchar](300) NOT NULL,
	[IDClient] [int] NOT NULL,
	[IDMaster] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[Contact] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 04.06.2024 15:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04.06.2024 15:13:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[IDRole] [int] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users]    Script Date: 04.06.2024 15:13:10 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users]
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD  CONSTRAINT [FK_Reports_Requests1] FOREIGN KEY([IDRequest])
REFERENCES [dbo].[Requests] ([ID])
GO
ALTER TABLE [dbo].[Reports] CHECK CONSTRAINT [FK_Reports_Requests1]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Clients] FOREIGN KEY([IDClient])
REFERENCES [dbo].[Clients] ([ID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Clients]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Users] FOREIGN KEY([IDMaster])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role1] FOREIGN KEY([IDRole])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role1]
GO
USE [master]
GO
ALTER DATABASE [db10] SET  READ_WRITE 
GO
