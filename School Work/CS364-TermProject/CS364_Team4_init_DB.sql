-- Team 4: Ryan Hanson, Emily Webb, Jose Angulo; CS 364; Project: App and Data Design
USE master
GO

/****** Object:  Database SubscriptionServices     ******/
IF DB_ID('SubscriptionServices') IS NOT NULL
	DROP DATABASE SubscriptionServices
GO

CREATE DATABASE SubscriptionServices
GO 

USE [SubscriptionServices]
GO

/****** Object:  Table [dbo].[Users]    ******/
CREATE TABLE dbo.Users
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [FirstName]             VARCHAR(128) NULL,
 [LastName]              VARCHAR(128) NULL,
 [UserName]              VARCHAR(255) UNIQUE NOT NULL,
 [AspNetUserID]          NVARCHAR(128) NULL,
 [Email]                 VARCHAR(255) UNIQUE NOT NULL
);
GO

/****** Object:  Table [dbo].[Companies]    ******/
CREATE TABLE dbo.Companies
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [Name]                  VARCHAR(128) UNIQUE NOT NULL,
 [URL]                   VARCHAR(255) NULL

);
GO

/****** Object:  Table [dbo].[Subscriptions]    ******/
CREATE TABLE dbo.Subscriptions
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [MonthlyPrice]          MONEY NOT NULL,
 [SubscriptionTier]      VARCHAR(50) DEFAULT 'Standard',
 [CompanyID]             INT NOT NULL FOREIGN KEY REFERENCES dbo.Companies(ID)
);
GO

/****** Object:  Table [dbo].[UserSubscriptions]    ******/
CREATE TABLE dbo.UserSubscriptions
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [SubscriptionID]        INT NOT NULL FOREIGN KEY REFERENCES dbo.Subscriptions(ID),
 [UserID]                INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(ID), 
 [StartDate]             DATE NOT NULL,
 [EndDate]               DATE NULL
);
GO

/****** Object:  Table [dbo].[UserTotalStats]    ******/
CREATE TABLE dbo.UserTotalStats
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [UserID]                INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(ID), 
 [TotalSubYearlyCost]    MONEY NULL,
 [AllSubRunningTotal]    MONEY NULL
);
GO

/****** Object:  Table [dbo].[UserSubStats]    ******/
CREATE TABLE dbo.UserSubStats
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [UserSubID]             INT NOT NULL FOREIGN KEY REFERENCES dbo.UserSubscriptions(ID), 
 [YearlyCost]            MONEY NULL,
 [RunningTotal]          MONEY NULL
);
GO

/****** Object:  Table [dbo].[CompanyStats]    ******/
CREATE TABLE dbo.CompanyStats
([ID]                    INT PRIMARY KEY IDENTITY(1,1),
 [CompanyID]             INT NOT NULL FOREIGN KEY REFERENCES dbo.Companies(ID), 
 [YearlyEarnings]        MONEY NULL,
 [ActiveSubCount]        INT NULL,
 [CancelledSubCount]     INT NULL
);
GO

/****** Data Inserts *****/
USE [SubscriptionServices]
GO


SET IDENTITY_INSERT dbo.Users ON;  

INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(1,'Ryan', 'Hanson', 'RyanH', 'ryanh@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(2,'Emily', 'Webb', 'EmilyW', 'emilyw@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(3,'Jose', 'Angulo', 'JoseA', 'josea@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(4,'Elon', 'Musk', 'ElonM', 'elonm@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(5,'Bill', 'Gates', 'BillG', 'billg@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(6,'Steve', 'Jobs', 'SteveJ', 'stevej@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(7,'Joe', 'Rogan', 'JoeR', 'joer@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(8,'Damian', 'Lillard', 'DamianL', 'damianl@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(9,'Stephen', 'Curry', 'StephenC', 'stephenc@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(10,'Luke', 'Skywalker', 'LukeS', 'lukes@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(11,'Darth', 'Vader', 'DarthV', 'darthvader@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(12,'Angus', 'Young', 'AngusY', 'angusacdc@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(13,'Ozzy', 'Osbourne', 'OzzyO', 'ozzyo@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(14,'Freddy', 'Mercury', 'FreddyM', 'freddym@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(15,'Robert', 'Plant', 'RobertP', 'robertp@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(16,'Jimmy', 'Page', 'JimmyP', 'jimmyp@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(17,'Mick', 'Mars', 'MickM', 'mickmars@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(18,'Nikki', 'Sixx', 'Nikki6', 'nikki6@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(19,'Tommy', 'Lee', 'TommyL', 'tommyl@gmail.com')
INSERT INTO dbo.Users([ID],[FirstName], [LastName], [UserName], [Email]) VALUES(20,'Vince', 'Neil', 'VinceN', 'vinceneil@gmail.com')

SET IDENTITY_INSERT dbo.Users OFF;  

SET IDENTITY_INSERT dbo.Companies ON;  

INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(1,'Netflix', 'https://www.netflix.com')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(2,'Hulu', 'https://www.hulu.com/welcome')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(3,'HBO Max', 'https://www.hbomax.com')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(4,'Disney+', 'https://www.disneyplus.com')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(5,'Paramount Plus', 'https://www.paramountplus.com')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(6,'Amazon', 'https://www.amazon.com/amazonprime?tag=mh0b-20&hvadid=78409042974208&hvqmt=e&hvbmt=be&hvdev=c&ref=pd_sl_34qfrygf2j_e')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(7,'Apple Music', 'https://www.apple.com/apple-music/')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(8,'Xbox Live', 'https://support.xbox.com/en-US/help/subscriptions-billing/browse')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(9,'Playstation', 'https://www.playstation.com/en-us/ps-plus/')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(10,'Apple iCloud', 'https://support.apple.com/en-us/HT201238')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(11,'Microsoft OneDrive', 'https://www.microsoft.com/en-us/microsoft-365/onedrive/compare-onedrive-plans?activetab=tab%3aprimaryr1')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(12,'ESPN+', 'https://plus.espn.com/?ex_cid=DSS-Search-Google-71700000068495554-&s_kwcid=AL!8468!3!!e!!s!!espn%20plus&cid=DSS-Search-Google-71700000068495554-&msclkid=6a48f35569d81a5bfe489753039656e9&gclid=6a48f35569d81a5bfe489753039656e9&gclsrc=3p.ds')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(13,'Youtube', 'https://www.youtube.com/premium')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(14,'Peacock', 'https://www.peacocktv.com')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(15,'Spotify', 'https://www.spotify.com/us/premium/')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(16,'Pandora', 'https://www.pandora.com/plans')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(17,'Apple TV+', 'https://www.apple.com/apple-tv-plus/')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(18,'DoorDash', 'https://help.doordash.com/consumers/s/article/What-is-DashPass?language=en_US#WhatisDashPass')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(19,'Uber Eats', 'https://www.ubereats.com/uber-one')
INSERT INTO dbo.Companies([ID],[Name], [URL]) VALUES(20,'Tinder', 'https://tinder.com/feature/subscription-tiers')

SET IDENTITY_INSERT dbo.Companies OFF;  

SET IDENTITY_INSERT dbo.Subscriptions ON;  

INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(1, 6.99, 'Basic with ads', 1)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(2, 9.99, 'Basic', 1)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(3, 15.49, 'Standard', 1)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(4, 19.99, 'Premium', 1)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(5, 13.49, 'Tinder Plus', 20)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(6, 22.49, 'Tinder Gold', 20)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(7, 26.99, 'Tinder Platinum', 20)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(8, 7.99, 'Hulu Ad-Supported', 2)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(9, 14.99, 'Hulu No Ads', 2)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(10, 14.99, 'Game Pass Ultimate', 8)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(11, 9.99, 'PC Game Pass', 8)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(12, 9.99, 'Game Pass for Console', 8)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(13, 9.99, 'Xbox Live Gold', 8)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(14, 6.99, 'Disney+ Basic', 4)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(15, 9.99, 'Disney+ Hulu Bundle', 4)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(16, 10.99, 'Apple Music Standard', 7)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(17, 14.99, 'Prime Monthly', 6)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(18, 7.49, 'Prime Student Monthly', 6)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(19, 6.99, 'Prime Qualified Gov. Assistance Monthly', 6)
INSERT INTO dbo.Subscriptions([ID],[MonthlyPrice], [SubscriptionTier], [CompanyID]) VALUES(20, 9.99, 'Spotify Premium', 15)

SET IDENTITY_INSERT dbo.Subscriptions OFF;  

SET IDENTITY_INSERT dbo.UserSubscriptions ON;  

INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(1, 1, 1, '20160523')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(2, 1, 2, '20141004')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(3, 1, 3, '20181219')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(4, 16, 1, '20150208')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(5, 8, 1, '20170317')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(6, 18, 1, '20200122')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(7, 18, 2, '20190408')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(8, 18, 3, '20210728')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(9, 1, 13, '20150924')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(10, 20, 13, '20161201')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(11, 1, 17, '20180921')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(12, 15, 8, '20201106')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(13, 15, 9, '20191210')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(14, 4, 12, '20210502')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(15, 13, 10, '20100820')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(16, 3, 11, '20110318')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(17, 4, 18, '20210102')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(18, 16, 15, '20160202')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(19, 7, 16, '20210904')
INSERT INTO dbo.UserSubscriptions([ID],[SubscriptionID], [UserID], [StartDate]) VALUES(20, 6, 19, '20200429')

SET IDENTITY_INSERT dbo.UserSubscriptions OFF;  

SET IDENTITY_INSERT dbo.UserTotalStats ON;  

INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(1, 1, 401.52, 2499.11)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(2, 2, 173.76, 1058.02)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(3, 3, 173.76, 506.29)
INSERT INTO dbo.UserTotalStats([ID],[UserID]) VALUES(4, 4)
INSERT INTO dbo.UserTotalStats([ID],[UserID]) VALUES(5, 5)
INSERT INTO dbo.UserTotalStats([ID],[UserID]) VALUES(6, 6)
INSERT INTO dbo.UserTotalStats([ID],[UserID]) VALUES(7, 7)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(8, 8, 119.88, 279.72)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(9, 9, 119.88, 389.61)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(10, 10, 119.88, 1508.49)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(11, 11, 185.88, 2230.56)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(12, 12, 239.88, 439.78)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(13, 13, 203.76, 1378.35)
INSERT INTO dbo.UserTotalStats([ID],[UserID]) VALUES(14, 14)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(15, 15, 131.88, 934.15)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(16, 16, 323.88, 485.82)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(17, 17, 83.88, 377.46)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(18, 18, 239.88, 519.74)
INSERT INTO dbo.UserTotalStats([ID],[UserID], [TotalSubYearlyCost], [AllSubRunningTotal]) VALUES(19, 19, 269.88, 787.15)
INSERT INTO dbo.UserTotalStats([ID],[UserID]) VALUES(20, 20)

SET IDENTITY_INSERT dbo.UserTotalStats OFF; 

SET IDENTITY_INSERT dbo.UserSubStats ON;  

INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(1, 1, 83.88, 573.18)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(2, 2, 83.88, 705.99)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(3, 3, 83.88, 356.49)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(4, 4, 131.88, 1066.03)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(5, 5, 95.88, 575.28)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(6, 6, 89.88, 284.62)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(7, 7, 89.88, 352.03)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(8, 8, 89.88, 149.80)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(9, 9, 83.88, 629.10)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(10, 10, 119.88, 749.25)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(11, 11, 83.88, 377.46)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(12, 12, 119.88, 279.72)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(13, 13, 119.88, 389.61)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(14, 14, 239.88, 439.78)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(15, 15, 119.88, 1508.49)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(16, 16, 185.88, 2230.56)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(17, 17, 239.88, 519.74)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(18, 18, 131.88, 934.15)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(19, 19, 323.88, 485.82)
INSERT INTO dbo.UserSubStats([ID],[UserSubID], [YearlyCost], [RunningTotal]) VALUES(20, 20, 269.88, 787.15)

SET IDENTITY_INSERT dbo.UserSubStats OFF; 

SET IDENTITY_INSERT dbo.CompanyStats ON;  

INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(1, 1, 1085.04, 8, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(2, 2, 95.88, 1, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(3, 3)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(4, 4, 239.76, 2, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(5, 5)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(6, 6, 269.64, 3, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(7, 7, 263.76, 2, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(8, 8, 119.88, 1, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(9, 9)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(10, 10)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(11, 11)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(12, 12)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(13, 13)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(14, 14)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(15, 15, 119.88, 1, 0)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(16, 16)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(17, 17)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(18, 18)
INSERT INTO dbo.CompanyStats([ID],[CompanyID]) VALUES(19, 19)
INSERT INTO dbo.CompanyStats([ID],[CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount]) VALUES(20, 20, 593.76, 2, 0)

SET IDENTITY_INSERT dbo.CompanyStats OFF; 

GO;

/******* The Following is a script found at: https://stackoverflow.com/questions/28636511/how-to-create-asp-net-identity-tables-inside-existing-database by user: Mohammed Osman in order to setup tables for ASP.NET Identity (With the added columns: [NormalizedUserName], [NormalizedEmail], [LockoutEnd], [ConcurrencyStamp] in [AspNetUsers] table) ******/
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 15-Mar-17 10:27:06 PM ******/

USE SubscriptionServices;

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[AspNetRoles](

    [Id] [nvarchar](128) NOT NULL,

    [Name] [nvarchar](256) NOT NULL,

CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED

(

    [Id] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]



GO

/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 15-Mar-17 10:27:06 PM ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[AspNetUserClaims](

    [Id] [int] IDENTITY(1,1) NOT NULL,

    [UserId] [nvarchar](128) NOT NULL,

    [ClaimType] [nvarchar](max) NULL,

    [ClaimValue] [nvarchar](max) NULL,

CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED

(

    [Id] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



GO

/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 15-Mar-17 10:27:06 PM ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[AspNetUserLogins](

    [LoginProvider] [nvarchar](128) NOT NULL,

    [ProviderKey] [nvarchar](128) NOT NULL,

    [UserId] [nvarchar](128) NOT NULL,

CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED

(

    [LoginProvider] ASC,

    [ProviderKey] ASC,

    [UserId] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]



GO

/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 15-Mar-17 10:27:06 PM ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[AspNetUserRoles](

    [UserId] [nvarchar](128) NOT NULL,

    [RoleId] [nvarchar](128) NOT NULL,

CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED

(

    [UserId] ASC,

    [RoleId] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]



GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 15-Mar-17 10:27:06 PM ******/

SET ANSI_NULLS ON

GO

SET QUOTED_IDENTIFIER ON

GO

CREATE TABLE [dbo].[AspNetUsers](

    [Id] [nvarchar](128) NOT NULL,

    [Email] [nvarchar](256) NULL,

    [EmailConfirmed] [bit] NOT NULL,

    [PasswordHash] [nvarchar](max) NULL,

    [SecurityStamp] [nvarchar](max) NULL,

    [PhoneNumber] [nvarchar](max) NULL,

    [PhoneNumberConfirmed] [bit] NOT NULL,

    [TwoFactorEnabled] [bit] NOT NULL,

    [LockoutEndDateUtc] [datetime] NULL,

    [LockoutEnabled] [bit] NOT NULL,

    [AccessFailedCount] [int] NOT NULL,

    [UserName] [nvarchar](256) NOT NULL,

    [NormalizedUserName] NVARCHAR(256) NULL,

    [NormalizedEmail] NVARCHAR(256) NULL,

    [LockoutEnd] DATETIME NULL,

    [ConcurrencyStamp] NVARCHAR(256) NULL,

CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED

(

    [Id] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])

REFERENCES [dbo].[AspNetUsers] ([Id])

ON DELETE CASCADE

GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]

GO

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])

REFERENCES [dbo].[AspNetUsers] ([Id])

ON DELETE CASCADE

GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]

GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])

REFERENCES [dbo].[AspNetRoles] ([Id])

ON DELETE CASCADE

GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]

GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])

REFERENCES [dbo].[AspNetUsers] ([Id])

ON DELETE CASCADE

GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]

GO;

/****** END OF ASPNET IDENTITY SCRIPT ******/

/****** Function to get the running total spent on a subscription ******/
CREATE FUNCTION fnGetSubRunningTotal (@UserSubID INT)
    RETURNS MONEY
BEGIN
    DECLARE @RunningTotal MONEY
    IF (SELECT EndDate 
        FROM UserSubscriptions
        WHERE ID = @UserSubID) IS NULL
        SET @RunningTotal = (SELECT ((DATEDIFF(MONTH, us.StartDate, GETDATE())) * s.MonthlyPrice) AS RunningTotal
                FROM UserSubscriptions AS us
                INNER JOIN Subscriptions AS s
                ON s.ID = us.SubscriptionID
                WHERE us.ID = @UserSubID);
    ELSE
        SET @RunningTotal = (SELECT ((DATEDIFF(MONTH, us.StartDate, us.EndDate)) * s.MonthlyPrice) AS RunningTotal
                FROM UserSubscriptions AS us
                INNER JOIN Subscriptions AS s
                ON s.ID = us.SubscriptionID
                WHERE us.ID = @UserSubID);
    RETURN @RunningTotal
END;

GO;

/****** Function to get the yearly cost of a subscription ******/
CREATE FUNCTION fnGetYearlyCost (@SubID INT)
    RETURNS MONEY
BEGIN
    RETURN (SELECT (s.MonthlyPrice * 12) 
            FROM Subscriptions AS s
            WHERE s.ID = @SubID);
END;

GO;

/****** Function to get the sum of all the running totals spent on subscriptions for a user ******/
CREATE FUNCTION fnGetAllSubRunningTotal (@UserID INT)
    RETURNS MONEY
BEGIN
    DECLARE @UserRunningTotal MONEY
    SET @UserRunningTotal = (SELECT SUM(uss.RunningTotal)
        FROM UserSubscriptions AS us
        INNER JOIN UserSubStats AS uss
        ON us.ID = uss.UserSubID
        WHERE us.UserID = @UserID AND uss.RunningTotal IS NOT NULL)
    RETURN @UserRunningTotal
END;

GO;

/****** Function to get the sum of all the yearly subscription costs for a user ******/
CREATE FUNCTION fnGetTotalSubYearlyCost (@UserID INT)
    RETURNS MONEY
BEGIN
    DECLARE @UserTotalYearly MONEY
    SET @UserTotalYearly = (SELECT SUM(uss.YearlyCost)
        FROM UserSubscriptions AS us
        INNER JOIN UserSubStats AS uss
        ON us.ID = uss.UserSubID
        WHERE us.UserID = @UserID AND uss.YearlyCost IS NOT NULL)
    RETURN @UserTotalYearly
END;

GO;

/****** Function to get the sum of all the yearly subscription earnings for a company ******/
CREATE FUNCTION fnGetYearlyEarnings (@CompanyID INT)
    RETURNS MONEY
BEGIN
    DECLARE @YearlyEarnings MONEY
    SET @YearlyEarnings = (SELECT SUM(uss.YearlyCost) AS TotalYearlyCost 
        FROM UserSubStats AS uss
        INNER JOIN UserSubscriptions AS us
        ON uss.UserSubID = us.ID
        INNER JOIN Subscriptions AS s
        ON us.SubscriptionID = s.ID
        INNER JOIN Companies AS c
        ON s.CompanyID = c.ID
        WHERE c.ID = @CompanyID AND YearlyCost IS NOT NULL AND us.EndDate IS NULL)
    RETURN @YearlyEarnings
END;

GO;

/****** Function to get the sum of all the active subscribers for a company ******/
CREATE FUNCTION fnGetActiveSubCount (@CompanyID INT)
    RETURNS INT
BEGIN
    DECLARE @ActiveSubCount INT
    SET @ActiveSubCount = (SELECT COUNT(us.UserID) 
        FROM UserSubscriptions AS us
        INNER JOIN Subscriptions AS s
        ON us.SubscriptionID = s.ID
        INNER JOIN Companies AS c
        ON s.CompanyID = c.ID
        WHERE c.ID = @CompanyID AND us.EndDate IS NULL)
    RETURN @ActiveSubCount
END;

GO;

/****** Function to get the sum of all the cancelled subscriptions for a company ******/
CREATE FUNCTION fnGetCancelledSubCount (@CompanyID INT)
    RETURNS INT
BEGIN
    DECLARE @CancelledSubCount INT
    SET @CancelledSubCount = (SELECT COUNT(us.UserID) 
        FROM UserSubscriptions AS us
        INNER JOIN Subscriptions AS s
        ON us.SubscriptionID = s.ID
        INNER JOIN Companies AS c
        ON s.CompanyID = c.ID
        WHERE c.ID = @CompanyID AND us.EndDate IS NOT NULL)
    RETURN @CancelledSubCount
END;

GO;

/****** Trigger to insert UserSubStats table's 'RunningTotal' and 'YearlyCost' ******/
CREATE TRIGGER UserSubStats_INSERT
ON UserSubscriptions
AFTER INSERT
AS
BEGIN
    INSERT INTO UserSubStats([UserSubID], [YearlyCost], [RunningTotal] )
    SELECT i.ID, dbo.fnGetYearlyCost(i.SubscriptionID), dbo.fnGetSubRunningTotal(i.ID)
    FROM INSERTED AS i
END;

GO;

/****** Trigger to update UserSubStats table's 'RunningTotal' and 'YearlyCost' ******/
CREATE TRIGGER UserSubStats_UPDATE
ON UserSubscriptions
AFTER UPDATE
AS
BEGIN
    UPDATE UserSubStats
    SET YearlyCost = dbo.fnGetYearlyCost(i.SubscriptionID), RunningTotal = dbo.fnGetSubRunningTotal(i.ID)
    FROM INSERTED AS i
    WHERE i.ID = UserSubStats.UserSubID
END;

GO;

/****** Trigger to insert UserTotalStats table's 'AllSubRunningTotal' and 'TotalSubYearlyCost' ******/
CREATE TRIGGER UserTotalStats_INSERT
ON UserSubscriptions
AFTER INSERT
AS
BEGIN
    IF EXISTS (SELECT UserTotalStats.UserID 
                FROM UserTotalStats
                INNER JOIN INSERTED AS i
                ON i.UserID = UserTotalStats.UserID
                WHERE UserTotalStats.UserID = i.UserID)
        BEGIN
        UPDATE UserTotalStats
        SET TotalSubYearlyCost = dbo.fnGetTotalSubYearlyCost(i.UserID), AllSubRunningTotal = dbo.fnGetAllSubRunningTotal(i.UserID)
        FROM INSERTED AS i
        WHERE i.UserID = UserTotalStats.UserID
        END;
    ELSE
        BEGIN
        INSERT INTO UserTotalStats([UserID], [TotalSubYearlyCost], [AllSubRunningTotal] )
        SELECT i.UserID, dbo.fnGetTotalSubYearlyCost(i.UserID), dbo.fnGetAllSubRunningTotal(i.UserID)
        FROM INSERTED AS i
        END;
END;

GO;

/****** Trigger to update UserTotalStats table's 'AllSubRunningTotal' and 'TotalSubYearlyCost' ******/
CREATE TRIGGER UserTotalStats_UPDATE
ON UserSubscriptions
AFTER UPDATE
AS
BEGIN
    UPDATE UserTotalStats
    SET TotalSubYearlyCost = dbo.fnGetTotalSubYearlyCost(i.UserID), AllSubRunningTotal = dbo.fnGetAllSubRunningTotal(i.UserID)
    FROM INSERTED AS i
    WHERE i.UserID = UserTotalStats.UserID
END;

GO;

/****** Trigger to handle a deletion on UserSubscriptions table ******/
CREATE TRIGGER UserSubscriptions_DELETE
ON UserSubscriptions
INSTEAD OF DELETE
AS
BEGIN
    DELETE UserSubStats
    WHERE UserSubID IN (SELECT ID FROM DELETED);

    UPDATE UserTotalStats 
    SET TotalSubYearlyCost = dbo.fnGetTotalSubYearlyCost(d.UserID), AllSubRunningTotal = dbo.fnGetAllSubRunningTotal(d.UserID)
    FROM DELETED AS d
    WHERE d.UserID = UserTotalStats.UserID;

    DELETE UserSubscriptions
    WHERE ID IN (SELECT ID FROM DELETED);

    UPDATE CompanyStats
    SET YearlyEarnings = dbo.fnGetYearlyEarnings(s.CompanyID), ActiveSubCount = dbo.fnGetActiveSubCount(s.CompanyID), CancelledSubCount = dbo.fnGetCancelledSubCount(s.CompanyID)
    FROM DELETED AS d
    INNER JOIN Subscriptions AS s
    ON s.ID = d.SubscriptionID
    WHERE s.CompanyID = CompanyStats.CompanyID

END;

GO;

/****** Trigger to insert CompanyStats table's 'YearlyEarnings', 'ActiveSubCount', and 'CancelledSubCount' ******/
CREATE TRIGGER CompanyStats_INSERT
ON UserSubscriptions
AFTER INSERT
AS
BEGIN
    IF EXISTS (SELECT cs.CompanyID 
                FROM CompanyStats AS cs
                INNER JOIN Subscriptions AS s
                ON s.CompanyID = cs.CompanyID
                INNER JOIN INSERTED AS i
                ON i.SubscriptionID = s.ID
                WHERE cs.CompanyID = s.CompanyID AND i.SubscriptionID = s.ID)
        BEGIN
        UPDATE CompanyStats
        SET YearlyEarnings = dbo.fnGetYearlyEarnings(s.CompanyID), ActiveSubCount = dbo.fnGetActiveSubCount(s.CompanyID), CancelledSubCount = dbo.fnGetCancelledSubCount(s.CompanyID)
        FROM INSERTED AS i
        INNER JOIN Subscriptions AS s
        ON s.ID = i.SubscriptionID
        WHERE s.CompanyID = CompanyStats.CompanyID
        END;
    ELSE
        BEGIN
        INSERT INTO CompanyStats([CompanyID], [YearlyEarnings], [ActiveSubCount], [CancelledSubCount] )
        SELECT s.CompanyID, dbo.fnGetYearlyEarnings(s.CompanyID), dbo.fnGetActiveSubCount(s.CompanyID), dbo.fnGetCancelledSubCount(s.CompanyID)
        FROM INSERTED AS i
        INNER JOIN Subscriptions AS s
        ON s.ID = i.SubscriptionID
        END;
END;

GO;

/****** Trigger to update CompanyStats table's 'YearlyEarnings', 'ActiveSubCount', and 'CancelledSubCount' ******/
CREATE TRIGGER CompanyStats_UPDATE
ON UserSubscriptions
AFTER UPDATE
AS
BEGIN
        UPDATE CompanyStats
        SET YearlyEarnings = dbo.fnGetYearlyEarnings(s.CompanyID), ActiveSubCount = dbo.fnGetActiveSubCount(s.CompanyID), CancelledSubCount = dbo.fnGetCancelledSubCount(s.CompanyID)
        FROM INSERTED AS i
        INNER JOIN Subscriptions AS s
        ON s.ID = i.SubscriptionID
        WHERE s.CompanyID = CompanyStats.CompanyID
END;

GO;

/****** Trigger to update/insert Users from AspNetUsers inserts ******/
CREATE TRIGGER Users_INSERT
ON [dbo].[AspNetUsers]
AFTER INSERT
AS
BEGIN
    IF EXISTS (SELECT u.Email 
                FROM Users AS u
                INNER JOIN INSERTED AS i
                ON i.Email = u.Email)
        BEGIN
        UPDATE Users
        SET AspNetUserID = i.Id
        FROM INSERTED AS i
        WHERE Users.Email = i.Email
        END;
    ELSE
        BEGIN
        INSERT INTO Users([UserName], [AspNetUserID], [Email])
        SELECT i.Email, i.Id, i.Email 
        FROM INSERTED AS i
        END;
END;

GO;

