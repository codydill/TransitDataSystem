CREATE TABLE [Bus].[SpecialOnBoard] (
    [SpecialID]    UNIQUEIDENTIFIER CONSTRAINT [DF_Special] DEFAULT (newid()) NOT NULL,
    [StopID]       UNIQUEIDENTIFIER NOT NULL,
    [SpecialTag]   VARCHAR (75)     NOT NULL,
    [SpecialCount] INT              NOT NULL,
    CONSTRAINT [PK_Special] PRIMARY KEY CLUSTERED ([SpecialID] ASC),
    CONSTRAINT [FK_Stop] FOREIGN KEY ([StopID]) REFERENCES [Bus].[OnBoard] ([StopID])
);

