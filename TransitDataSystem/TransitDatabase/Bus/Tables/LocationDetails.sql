CREATE TABLE [Bus].[LocationDetails] (
    [LocationID]            UNIQUEIDENTIFIER CONSTRAINT [DF_Location] DEFAULT (newid()) NOT NULL,
    [RouteID]               UNIQUEIDENTIFIER NOT NULL,
    [LocationAddress]       VARCHAR (75)     NOT NULL,
    [LocationName]          VARCHAR (75)     NULL,
    [PositionNumberInRoute] INT              NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

