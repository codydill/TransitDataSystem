CREATE TABLE [Bus].[OnBoard] (
    [OnBoardID]          INT CONSTRAINT [DF_Stop] DEFAULT (newid()) NOT NULL,
    [LocationID]      INT NOT NULL,
    [BusID]           INT NOT NULL,
    [OnBoardTimeStamp]   DATETIME2 (7)    NOT NULL,
    [YouthOnBoard]    TINYINT              NULL,
    [AdultOnBoard]    TINYINT              NULL,
    [SeniorOnBoard]   TINYINT              NULL,
    [HandicapOnBoard] TINYINT              NULL,
    CONSTRAINT [PK_OnBoard] PRIMARY KEY CLUSTERED ([OnBoardID] ASC),
    CONSTRAINT [FK_Bus_ToBuses] FOREIGN KEY ([BusID]) REFERENCES [Bus].[Buses] ([BusID]),
    CONSTRAINT [FK_Location_ToLocations] FOREIGN KEY ([LocationID]) REFERENCES [Bus].[Locations] ([LocationID]) 
);

