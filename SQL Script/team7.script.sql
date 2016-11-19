CREATE TABLE [dbo].[Player] (
    [player_uid]      INT           IDENTITY (1, 1) NOT NULL,
    [player_username] NVARCHAR (50) NOT NULL,
    [player_password] NVARCHAR (50) NOT NULL,
    [player_email]    NVARCHAR (50) NOT NULL,
    [player_school]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([player_uid] ASC)
);
-----------------------------------------------------------------
CREATE TABLE [dbo].[Score] (
    [uid]          INT        NOT NULL,
    [Selection_no] INT        NOT NULL,
    [score]        INT        DEFAULT ((0)) NOT NULL,
    [Type]         BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([uid] ASC)
);
------------------------------------------------------------------
CREATE TABLE [dbo].[Game] (
    [Selection_no] INT         IDENTITY (1, 1) NOT NULL,
    [Information]  NCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Selection_no] ASC)
);


