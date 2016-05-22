CREATE TABLE [Bus].[Locations] (
    [LocationID]            UNIQUEIDENTIFIER CONSTRAINT [DF_Location] DEFAULT (newid()) NOT NULL,
    [LocationAddress]       VARCHAR (75)     NOT NULL,
    [LocationName]          VARCHAR (75)     NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

