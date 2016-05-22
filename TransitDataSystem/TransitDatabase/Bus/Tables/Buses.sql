﻿CREATE TABLE [Bus].[Buses]
(
	[BusID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [RouteID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_RouteID_ToRoutes] FOREIGN KEY ([RouteID]) REFERENCES [Bus].[Routes]([RouteID])
)
