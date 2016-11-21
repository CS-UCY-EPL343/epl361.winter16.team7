
--Azure SQL Server Script
CREATE TABLE [dbo].[Player] (
    [player_uid]      INT           IDENTITY (1, 1) NOT NULL,
    [player_username] NVARCHAR (50) NOT NULL,
    [player_password] NVARCHAR (50) NOT NULL,
    [player_email]    NVARCHAR (50) NOT NULL,
    [player_school]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([player_uid] ASC)
);
SET IDENTITY_INSERT [dbo].[Player] ON
INSERT INTO [dbo].[Player] ([player_uid], [player_username], [player_password], [player_email], [player_school]) VALUES (1, N'loukas', N'kodikos', N'email', N'sxolio')
INSERT INTO [dbo].[Player] ([player_uid], [player_username], [player_password], [player_email], [player_school]) VALUES (2, N'nkolas', N'npassword', N'nemail', N'nsxolio')
INSERT INTO [dbo].[Player] ([player_uid], [player_username], [player_password], [player_email], [player_school]) VALUES (3, N'test', N'test', N'test', N'test')
SET IDENTITY_INSERT [dbo].[Player] OFF

CREATE TABLE [dbo].[Game] (
    [Selection_no] INT         IDENTITY (1, 1) NOT NULL,
    [Information]  NCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Selection_no] ASC)
);

SET IDENTITY_INSERT [dbo].[Game] ON
INSERT INTO [dbo].[Game] ([Selection_no], [Information]) VALUES (1, N'Mission1                                                                                            ')
INSERT INTO [dbo].[Game] ([Selection_no], [Information]) VALUES (2, N'Mission2                                                                                            ')
INSERT INTO [dbo].[Game] ([Selection_no], [Information]) VALUES (3, N'Mission3                                                                                            ')
SET IDENTITY_INSERT [dbo].[Game] OFF


CREATE TABLE [dbo].[Score] (
    [uid]          INT NOT NULL,
    [Selection_no] INT NOT NULL,
    [score]        INT DEFAULT ((0)) NOT NULL,
    [Type]         BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Selection_no],[uid]), 
    CONSTRAINT [FK_Score_Game] FOREIGN KEY ([Selection_no]) REFERENCES [Game]([Selection_no]), 
    CONSTRAINT [FK_Score_Player] FOREIGN KEY ([uid]) REFERENCES [Player]([player_uid])
);

INSERT INTO [dbo].[Score] ([uid], [Selection_no], [score], [Type]) VALUES (1, 1, 55, 1)
INSERT INTO [dbo].[Score] ([uid], [Selection_no], [score], [Type]) VALUES (2, 1, 0, 0)
INSERT INTO [dbo].[Score] ([uid], [Selection_no], [score], [Type]) VALUES (3, 1, 24, 1)
INSERT INTO [dbo].[Score] ([uid], [Selection_no], [score], [Type]) VALUES (1, 2, 0, 0)
INSERT INTO [dbo].[Score] ([uid], [Selection_no], [score], [Type]) VALUES (3, 2, 51, 1)
INSERT INTO [dbo].[Score] ([uid], [Selection_no], [score], [Type]) VALUES (3, 3, 44, 1)
