CREATE TABLE [dbo].[Player] (
    [player_uid]      INT           IDENTITY (1, 1) NOT NULL,
    [player_username] NVARCHAR (50) NOT NULL,
    [player_password] NVARCHAR (50) NOT NULL,
    [player_email]    NVARCHAR (50) NOT NULL,
    [player_school]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([player_uid] ASC)
);


