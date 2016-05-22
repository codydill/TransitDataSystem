CREATE TABLE [Bus].[OnBoard] (
    [OnBoardID]          UNIQUEIDENTIFIER CONSTRAINT [DF_Stop] DEFAULT (newid()) NOT NULL,
    [LocationID]      UNIQUEIDENTIFIER NOT NULL,
    [BusID]           UNIQUEIDENTIFIER NOT NULL,
    [OnBoardTimeStamp]   DATETIME2 (7)    NOT NULL,
    [YouthOnBoard]    INT              NULL,
    [AdultOnBoard]    INT              NULL,
    [SeniorOnBoard]   INT              NULL,
    [HandicapOnBoard] INT              NULL,
    CONSTRAINT [PK_OnBoard] PRIMARY KEY CLUSTERED ([OnBoardID] ASC),
    CONSTRAINT [FK_Bus_ToBuses] FOREIGN KEY ([BusID]) REFERENCES [Bus].[Buses] ([BusID]),
    CONSTRAINT [FK_Location_ToLocations] FOREIGN KEY ([LocationID]) REFERENCES [Bus].[Locations] ([LocationID]) 
);

