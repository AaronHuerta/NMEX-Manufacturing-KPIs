USE [master]
GO
/****** Object:  Database [NissanLogin]    Script Date: 01/04/2024 08:53:55 p. m. ******/
CREATE DATABASE [NissanLogin]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NissanLogin', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NissanLogin.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NissanLogin_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NissanLogin_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NissanLogin] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NissanLogin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NissanLogin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NissanLogin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NissanLogin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NissanLogin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NissanLogin] SET ARITHABORT OFF 
GO
ALTER DATABASE [NissanLogin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NissanLogin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NissanLogin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NissanLogin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NissanLogin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NissanLogin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NissanLogin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NissanLogin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NissanLogin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NissanLogin] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NissanLogin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NissanLogin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NissanLogin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NissanLogin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NissanLogin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NissanLogin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NissanLogin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NissanLogin] SET RECOVERY FULL 
GO
ALTER DATABASE [NissanLogin] SET  MULTI_USER 
GO
ALTER DATABASE [NissanLogin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NissanLogin] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NissanLogin] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NissanLogin] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NissanLogin] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NissanLogin] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NissanLogin', N'ON'
GO
ALTER DATABASE [NissanLogin] SET QUERY_STORE = ON
GO
ALTER DATABASE [NissanLogin] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NissanLogin]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Category_id] [int] IDENTITY(1,1) NOT NULL,
	[Category_name] [varchar](100) NOT NULL,
	[Category_description] [varchar](300) NOT NULL,
	[Function_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Category_identifier] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceType]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceType](
	[D_type_id] [int] IDENTITY(1,1) NOT NULL,
	[D_type_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Device2] PRIMARY KEY CLUSTERED 
(
	[D_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Function](
	[Function_id] [int] IDENTITY(1,1) NOT NULL,
	[Function_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Function] PRIMARY KEY CLUSTERED 
(
	[Function_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[Inventory_id] [int] IDENTITY(1,1) NOT NULL,
	[D_type_id] [int] NOT NULL,
	[SerialNo] [varchar](100) NOT NULL,
	[PurchaseDate] [date] NOT NULL,
	[Location_id] [int] NOT NULL,
	[Version_id] [int] NOT NULL,
	[Model_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Inventory2] PRIMARY KEY CLUSTERED 
(
	[Inventory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LanSwitchDevice]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LanSwitchDevice](
	[Switch_device_id] [int] IDENTITY(1,1) NOT NULL,
	[Device] [varchar](50) NULL,
	[CPU_processing] [int] NOT NULL,
	[Temperature_level] [int] NOT NULL,
	[FAN_status] [bit] NOT NULL,
	[Bandwidth] [int] NOT NULL,
	[Power_source] [bit] NOT NULL,
	[LanSwitchDevice_data] [date] NOT NULL,
	[Plant_id] [int] NULL,
 CONSTRAINT [PK_LanSwitchDevice] PRIMARY KEY CLUSTERED 
(
	[Switch_device_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LanSwitchEndpoints]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LanSwitchEndpoints](
	[Endpoint_id] [int] IDENTITY(1,1) NOT NULL,
	[Device] [varchar](50) NULL,
	[Port] [varchar](50) NOT NULL,
	[Description_port] [varchar](50) NOT NULL,
	[Category_issue] [varchar](50) NOT NULL,
	[Description_issue] [varchar](100) NOT NULL,
	[LanSwitchEndpoints_data] [date] NOT NULL,
	[Plant_id] [int] NULL,
 CONSTRAINT [PK_LanSwitchEndpoints] PRIMARY KEY CLUSTERED 
(
	[Endpoint_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LanSwitchIssues]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LanSwitchIssues](
	[Issue_id] [int] IDENTITY(1,1) NOT NULL,
	[Device] [varchar](50) NULL,
	[Type_affectation] [varchar](100) NOT NULL,
	[Description_affectation] [varchar](100) NOT NULL,
	[Suggestion] [varchar](50) NOT NULL,
	[Criticality_level] [varchar](50) NOT NULL,
	[LanSwitchIssues_data] [date] NOT NULL,
	[Plant_id] [int] NULL,
 CONSTRAINT [PK_LanSwitchIssues] PRIMARY KEY CLUSTERED 
(
	[Issue_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Location_id] [int] IDENTITY(1,1) NOT NULL,
	[Location_description] [varchar](50) NOT NULL,
	[Plant_id] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Location_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Log_id] [int] IDENTITY(1,1) NOT NULL,
	[LogType_id] [int] NOT NULL,
	[User_id] [int] NOT NULL,
	[DateLog] [datetime] NOT NULL,
	[Message] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogType]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogType](
	[LogType_id] [int] IDENTITY(1,1) NOT NULL,
	[LogTypeDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LogType] PRIMARY KEY CLUSTERED 
(
	[LogType_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Model]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Model](
	[Model_id] [int] IDENTITY(1,1) NOT NULL,
	[Model_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Model] PRIMARY KEY CLUSTERED 
(
	[Model_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plant]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plant](
	[Plant_id] [int] IDENTITY(1,1) NOT NULL,
	[Plant_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Plant] PRIMARY KEY CLUSTERED 
(
	[Plant_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Security]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Security](
	[Security_id] [int] IDENTITY(1,1) NOT NULL,
	[SubCategory_id] [int] NOT NULL,
	[Plant_id] [int] NOT NULL,
	[Result] [bit] NOT NULL,
	[Comment] [varchar](100) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Security] PRIMARY KEY CLUSTERED 
(
	[Security_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategory_id] [int] IDENTITY(1,1) NOT NULL,
	[SubCategory_description] [varchar](300) NULL,
	[Active] [bit] NOT NULL,
	[Category_id] [int] NOT NULL,
 CONSTRAINT [PK_SubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastNamePaternal] [varchar](50) NOT NULL,
	[LastNameMaternal] [varchar](50) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailNormalized] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Privilege] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Plant_id] [int] NOT NULL,
	[IdUserNissan] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Version]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[Version_id] [int] IDENTITY(1,1) NOT NULL,
	[Version_description] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[EndOfSupport] [date] NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[Version_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionDT]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionDT](
	[idVersionDT] [int] IDENTITY(1,1) NOT NULL,
	[Version_id] [int] NOT NULL,
	[D_type_id] [int] NOT NULL,
 CONSTRAINT [PK_VersionDT] PRIMARY KEY CLUSTERED 
(
	[idVersionDT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WLC]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WLC](
	[WLC_id] [int] IDENTITY(1,1) NOT NULL,
	[Acces_Point] [varchar](50) NULL,
	[Number_connected_devices_2_4] [int] NOT NULL,
	[Number_connected_devices_5] [int] NOT NULL,
	[Canal_2_4] [int] NOT NULL,
	[Canal_5] [int] NOT NULL,
	[Frecuency_2_4] [int] NOT NULL,
	[Frecuency_5] [int] NOT NULL,
	[Number_SSID_propagated] [int] NOT NULL,
	[WLC_data] [date] NOT NULL,
	[Plant_id] [int] NULL,
 CONSTRAINT [PK_WLC] PRIMARY KEY CLUSTERED 
(
	[WLC_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Function] FOREIGN KEY([Function_id])
REFERENCES [dbo].[Function] ([Function_id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Function]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_DeviceType] FOREIGN KEY([D_type_id])
REFERENCES [dbo].[DeviceType] ([D_type_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_DeviceType]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Location] FOREIGN KEY([Location_id])
REFERENCES [dbo].[Location] ([Location_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Location]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Model] FOREIGN KEY([Model_id])
REFERENCES [dbo].[Model] ([Model_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Model]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Version] FOREIGN KEY([Version_id])
REFERENCES [dbo].[Version] ([Version_id])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Version]
GO
ALTER TABLE [dbo].[LanSwitchDevice]  WITH CHECK ADD  CONSTRAINT [FK_LanSwitchDevice_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[LanSwitchDevice] CHECK CONSTRAINT [FK_LanSwitchDevice_Plant]
GO
ALTER TABLE [dbo].[LanSwitchEndpoints]  WITH CHECK ADD  CONSTRAINT [FK_LanSwitchEndpoints_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[LanSwitchEndpoints] CHECK CONSTRAINT [FK_LanSwitchEndpoints_Plant]
GO
ALTER TABLE [dbo].[LanSwitchIssues]  WITH CHECK ADD  CONSTRAINT [FK_LanSwitchIssues_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[LanSwitchIssues] CHECK CONSTRAINT [FK_LanSwitchIssues_Plant]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Plant]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_LogType] FOREIGN KEY([LogType_id])
REFERENCES [dbo].[LogType] ([LogType_id])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_LogType]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_Users] FOREIGN KEY([User_id])
REFERENCES [dbo].[Users] ([User_id])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_Users]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_Plant]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_SubCategory] FOREIGN KEY([SubCategory_id])
REFERENCES [dbo].[SubCategory] ([SubCategory_id])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_SubCategory]
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD  CONSTRAINT [FK_SubCategory_Categories] FOREIGN KEY([Category_id])
REFERENCES [dbo].[Categories] ([Category_id])
GO
ALTER TABLE [dbo].[SubCategory] CHECK CONSTRAINT [FK_SubCategory_Categories]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Plant]
GO
ALTER TABLE [dbo].[VersionDT]  WITH CHECK ADD  CONSTRAINT [FK_VersionDT_DeviceType] FOREIGN KEY([D_type_id])
REFERENCES [dbo].[DeviceType] ([D_type_id])
GO
ALTER TABLE [dbo].[VersionDT] CHECK CONSTRAINT [FK_VersionDT_DeviceType]
GO
ALTER TABLE [dbo].[VersionDT]  WITH CHECK ADD  CONSTRAINT [FK_VersionDT_Version] FOREIGN KEY([Version_id])
REFERENCES [dbo].[Version] ([Version_id])
GO
ALTER TABLE [dbo].[VersionDT] CHECK CONSTRAINT [FK_VersionDT_Version]
GO
ALTER TABLE [dbo].[WLC]  WITH CHECK ADD  CONSTRAINT [FK_WLC_Plant] FOREIGN KEY([Plant_id])
REFERENCES [dbo].[Plant] ([Plant_id])
GO
ALTER TABLE [dbo].[WLC] CHECK CONSTRAINT [FK_WLC_Plant]
GO
/****** Object:  StoredProcedure [dbo].[AgingStatus]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgingStatus]
	--@Plan_idFilter int
AS
BEGIN
	
	SET NOCOUNT ON;

SELECT DT.D_type_description 'DeviceType' ,M.Model_description 'Model' ,V.Version_description 'Version'
	  ,L.Location_description 'Location',[SerialNo] 'Serial No.',I.PurchaseDate 'Purchase Date'
	  ,case
		when DATEDIFF (day, Getdate(), I.PurchaseDate)/365 >= 5  then 'OK'

		when DATEDIFF (day, Getdate(), I.PurchaseDate)/365 between 1 and 4 then 'EOL'
		else 'Obsolote'
	  end  as 'Status'
	  ,P.Plant_description 'Plant'
  FROM [Nissan].[dbo].[Inventory] I
  inner join [Nissan].[dbo].[DeviceType] DT on DT.D_type_id = I.D_type_id
  inner join [Nissan].[dbo].[Model] M on M.Model_id = I.Model_id
  inner join [Nissan].[dbo].[Version] V on V.Version_id = I.Version_id
  inner join [Nissan].[dbo].[Location] L on L.Location_id = I.Location_id
  inner join [Nissan].[dbo].[Plant] P on P.Plant_id = L.Plant_id 
  where I.Active = 1
END
GO
/****** Object:  StoredProcedure [dbo].[InsertTableSecurityByPlant]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTableSecurityByPlant]
	@Plan_idFilter int ,@Category_idFilter int
AS
BEGIN
	
	SET NOCOUNT ON;

	 DECLARE @count INT
	 Declare @Plant_id INT
	 Declare @Order int
	 Set @Order = 0
	 SELECT @count = COUNT(Plant_id) FROM [Nissan].[dbo].[Plant] where active = 1
	 WHILE @count !=@Order 
	 BEGIN
	 set @Plant_id =  (SELECT [Plant_id] FROM [Nissan].[dbo].[Plant] where active = 1 ORDER BY [Plant_id] OFFSET @Order ROWS  FETCH NEXT 1 ROWS ONLY)
	
		DECLARE @count2 INT
		Declare @SubCategory_id INT
		Declare @Order2 int
		Set @Order2 = 0
		SELECT @count2 = COUNT(SubCategory_id) FROM [Nissan].[dbo].[SubCategory] where active = 1
		WHILE @count2 != @Order2
		BEGIN
		set @SubCategory_id =  (SELECT SubCategory_id FROM [Nissan].[dbo].[SubCategory] where active = 1 ORDER BY SubCategory_id OFFSET @Order2 ROWS  FETCH NEXT 1 ROWS ONLY)
			if not exists (SELECT [SubCategory_id], [Plant_id] FROM [Nissan].[dbo].[Security] where  [SubCategory_id] = @SubCategory_id  and [Plant_id] = @Plant_id)
				Insert into [Nissan].[dbo].[Security] ([SubCategory_id] ,[Plant_id],[Result],[Comment],[Active])
				SELECT @SubCategory_id SubCategory_id, @Plant_id Plant_id, 0 Result, '' Comment , 1 Active
		set @Order2 = @Order2 +1
		END
	set @Order = @Order +1
    END

if exists (
	 SELECT
	  F.Function_id
	 ,F.Function_description as Function_name
	 ,C.Category_name
	 ,C.Category_id
	 ,S.Plant_id
	 ,S.SubCategory_id
	 ,S.[Security_id]
		,C.[Category_description]
	  ,SC.SubCategory_description
      ,S.[Result]
      ,S.[Comment]
	  ,P.Plant_description
  FROM [Nissan].[dbo].[Security] S
  inner join [Nissan].[dbo].[SubCategory] SC on SC.SubCategory_id = S.SubCategory_id
  inner join [Nissan].[dbo].[Categories] C on C.Category_id = SC.Category_id
  inner join [Nissan].[dbo].[Function] F on F.Function_id = C.Function_id
  inner join [Nissan].[dbo].[Plant] P on P.Plant_id = S.Plant_id
  where S.active = 1 and SC.active = 1 and C.Active = 1 and P.Active = 1
  and C.Category_id = @Category_idFilter
  and S.Plant_id = @Plan_idFilter)

   SELECT  
    F.Function_id
	 ,F.Function_description as Function_name
	 ,C.Category_name
	 ,C.Category_id
	 ,S.Plant_id
	,S.SubCategory_id
   ,S.[Security_id]
		,C.[Category_description]
	  ,SC.SubCategory_description
      ,S.[Result]
      ,S.[Comment]
	  ,P.Plant_description
  FROM [Nissan].[dbo].[Security] S
  inner join [Nissan].[dbo].[SubCategory] SC on SC.SubCategory_id = S.SubCategory_id
  inner join [Nissan].[dbo].[Categories] C on C.Category_id = SC.Category_id
  inner join [Nissan].[dbo].[Function] F on F.Function_id = C.Function_id
  inner join [Nissan].[dbo].[Plant] P on P.Plant_id = S.Plant_id
  where S.active = 1 and SC.active = 1 and C.Active = 1 and F.Active = 1 and P.Active = 1
  and C.Category_id = @Category_idFilter
  and S.Plant_id = @Plan_idFilter
  else
  Select 3 as Category_id, 0, 0, 0, 0, 0 
END
GO
/****** Object:  StoredProcedure [dbo].[OperativeSystemStatus]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OperativeSystemStatus]
	--@Plan_idFilter int
AS
BEGIN
	
	SET NOCOUNT ON;

SELECT DT.D_type_description 'DeviceType' ,M.Model_description 'Model' ,V.Version_description 'Version'
	  ,L.Location_description 'Location',[SerialNo] 'Serial No.',V.EndOfSupport 'End of Support'
	  ,case
		when DATEDIFF (day, Getdate(), V.EndOfSupport)/365 >= 3  then 'OK'
		when V.EndOfSupport is null then 'OK'
		when DATEDIFF (day, Getdate(), V.EndOfSupport)/365 between 0 and 2 then 'EOL'
		else 'Obsolote'
	  end  as 'Status'
	  ,P.Plant_description 'Plant'
  FROM [Nissan].[dbo].[Inventory] I
  inner join [Nissan].[dbo].[DeviceType] DT on DT.D_type_id = I.D_type_id
  inner join [Nissan].[dbo].[Model] M on M.Model_id = I.Model_id
  inner join [Nissan].[dbo].[Version] V on V.Version_id = I.Version_id
  inner join [Nissan].[dbo].[Location] L on L.Location_id = I.Location_id
  inner join [Nissan].[dbo].[Plant] P on P.Plant_id = L.Plant_id 
  where I.Active = 1



END
GO
/****** Object:  StoredProcedure [dbo].[ShowFunctionResult]    Script Date: 01/04/2024 08:53:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ShowFunctionResult] 
	@Plan_idFilter int 
AS
BEGIN
	
	SET NOCOUNT ON;
	Drop Table If Exists  #EY1
			Drop Table If Exists  #EY2
			Drop Table If Exists  #EY3
			Drop Table If Exists  #EY4

Select * into #EY1 from
	(select  F.Function_id, F.Function_description, C.Category_id, C.Category_name, C.Category_identifier
	from [Nissan].[dbo].[Function] F
	inner join [Nissan].[dbo].[Categories] C on C.Function_id = F.Function_id
	where F.Active = 1 and C.Active = 1) EY1

Select * into #EY2  from
	(select C.Category_id, S.Result from [Nissan].[dbo].[Categories] C
	left join [Nissan].[dbo].[SubCategory] SC on SC.Category_id = C.Category_id
	left  join [Nissan].[dbo].[Security] S on S.SubCategory_id = SC.SubCategory_id
	 where  SC.Active = 1 and S.Active = 1 and C.active = 1
	and S.Plant_id = @Plan_idFilter) EY2 

Select * into #EY3 from
	(select EY3.Function_id, EY3.Function_description as Function_name, EY3.Category_id, EY3.Category_name, EY3.Category_identifier, avg(cast(EY3.Result as float)) as Result
 from 
	(select #EY1.*,
	case when #EY2.Result is null then 0  
	else #EY2.Result end Result
	from #EY1 
	left join  #EY2 on #EY1.Category_id = #EY2.Category_id) EY3
	group by EY3.Function_id, EY3.Function_description, EY3.Category_id, EY3.Category_name, EY3.Category_identifier
	) as EY4

	Select * into #EY4 from
	(select #EY3.Function_id, #EY3.Function_name, round(avg(#EY3.Result),2) ResultFunction from #Ey3
	group by #EY3.Function_id, #EY3.Function_name) EY5


	select  #EY3.Function_id, #EY3.Function_name, #EY3.Category_id, #EY3.Category_name, #EY3.Category_identifier, 
	format(#EY3.Result,'P0') Result, FORMAT(#EY4.ResultFunction,'P0') ResultFunction
	from #EY3 inner join #EY4 on #EY3.Function_id = #EY4.Function_id
	order by #EY3.Function_id asc

			Drop Table If Exists  #EY1
			Drop Table If Exists  #EY2
			Drop Table If Exists  #EY3
			Drop Table If Exists  #EY4
END
GO
USE [master]
GO
ALTER DATABASE [NissanLogin] SET  READ_WRITE 
GO
