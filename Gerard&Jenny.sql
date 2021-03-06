USE [master]
GO
/****** Object:  Database [FriendShipFinder]    Script Date: 13-Mar-17 7:02:53 PM ******/
CREATE DATABASE [FriendShipFinder] ON  PRIMARY 
( NAME = N'FriendShipFinder', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FriendShipFinder.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FriendShipFinder_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\FriendShipFinder_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FriendShipFinder] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FriendShipFinder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FriendShipFinder] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FriendShipFinder] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FriendShipFinder] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FriendShipFinder] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FriendShipFinder] SET ARITHABORT OFF 
GO
ALTER DATABASE [FriendShipFinder] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FriendShipFinder] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FriendShipFinder] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FriendShipFinder] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FriendShipFinder] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FriendShipFinder] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FriendShipFinder] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FriendShipFinder] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FriendShipFinder] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FriendShipFinder] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FriendShipFinder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FriendShipFinder] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FriendShipFinder] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FriendShipFinder] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FriendShipFinder] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FriendShipFinder] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FriendShipFinder] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FriendShipFinder] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FriendShipFinder] SET  MULTI_USER 
GO
ALTER DATABASE [FriendShipFinder] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FriendShipFinder] SET DB_CHAINING OFF 
GO
USE [FriendShipFinder]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 13-Mar-17 7:02:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[PostedOn] [datetime] NULL,
	[Likes] [int] NULL,
	[Dislikes] [int] NULL,
	[Photo] [nvarchar](max) NULL,
	[Description] [nvarchar](150) NULL,
	[Status] [nvarchar](50) NULL,
	[UserID] [int] NULL,
	[video] [nvarchar](max) NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostComment]    Script Date: 13-Mar-17 7:02:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PostId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostDislike]    Script Date: 13-Mar-17 7:02:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostDislike](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
 CONSTRAINT [PK_Dislikes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PostLike]    Script Date: 13-Mar-17 7:02:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostLike](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[PostId] [int] NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 13-Mar-17 7:02:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Username] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[ProfilePicture] [nvarchar](max) NULL,
	[Gender] [nvarchar](30) NULL,
	[City] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[DOB] [datetime] NULL,
	[RegisteredDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Writing]    Script Date: 13-Mar-17 7:02:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Writing](
	[ContentID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Writing] PRIMARY KEY CLUSTERED 
(
	[ContentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [FriendShipFinder] SET  READ_WRITE 
GO
