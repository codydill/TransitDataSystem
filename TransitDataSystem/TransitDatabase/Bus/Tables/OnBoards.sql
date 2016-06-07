CREATE TABLE [Bus].[OnBoards] (
    [OnBoardID]          INT  NOT NULL,
    [LocationID]      INT NOT NULL,
    [BusID]           INT NOT NULL,
    [OnBoardTimeStamp]   DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_OnBoard] PRIMARY KEY CLUSTERED ([OnBoardID] ASC),
    CONSTRAINT [FK_Bus_ToBuses] FOREIGN KEY ([BusID]) REFERENCES [Bus].[Buses] ([BusID]),
    CONSTRAINT [FK_Location_ToLocations] FOREIGN KEY ([LocationID]) REFERENCES [Bus].[Locations] ([LocationID]) 
);

