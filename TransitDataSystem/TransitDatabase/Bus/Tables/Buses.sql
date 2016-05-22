CREATE TABLE [Bus].[Buses]
(
	[BusID] INT NOT NULL PRIMARY KEY, 
    [RouteID] INT NOT NULL, 
    CONSTRAINT [FK_RouteID_ToRoutes] FOREIGN KEY ([RouteID]) REFERENCES [Bus].[Routes]([RouteID])
)
