CREATE TABLE [Bus].[OnBoard] (
    [StopID]          UNIQUEIDENTIFIER CONSTRAINT [DF_Stop] DEFAULT (newid()) NOT NULL,
    [LocationID]      UNIQUEIDENTIFIER NOT NULL,
    [BusID]           UNIQUEIDENTIFIER NOT NULL,
    [StopTimeStamp]   DATETIME2 (7)    NOT NULL,
    [YouthOnBoard]    INT              NULL,
    [AdultOnBoard]    INT              NULL,
    [SeniorOnBoard]   INT              NULL,
    [HandicapOnBoard] INT              NULL,
    CONSTRAINT [PK_Stop] PRIMARY KEY CLUSTERED ([StopID] ASC),
    CONSTRAINT [FK_Bus] FOREIGN KEY ([BusID]) REFERENCES [Bus].[RouteDetails] ([BusID]),
    CONSTRAINT [FK_Location] FOREIGN KEY ([LocationID]) REFERENCES [Bus].[LocationDetails] ([LocationID]), 
    CONSTRAINT [CK_Youth] CHECK (YouthOnBoard >= 0), 
    CONSTRAINT [CK_Adult] CHECK (AdultOnBoard >= 0), 
    CONSTRAINT [CK_Senior] CHECK (SeniorOnBoard >= 0), 
    CONSTRAINT [CK_Handicap] CHECK (HandicapOnBoard >= 0)
);

