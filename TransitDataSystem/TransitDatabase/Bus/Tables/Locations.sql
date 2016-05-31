CREATE TABLE [Bus].[Locations] (
    [LocationID]            INT  NOT NULL,
    [LocationAddress]       VARCHAR (75)     NULL,
    [LocationName]          VARCHAR (75)     NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

