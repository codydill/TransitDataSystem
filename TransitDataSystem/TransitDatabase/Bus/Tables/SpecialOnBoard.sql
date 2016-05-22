CREATE TABLE [Bus].[SpecialOnBoard] (
    [SpecialID]    UNIQUEIDENTIFIER CONSTRAINT [DF_Special] DEFAULT (newid()) NOT NULL,
    [OnBoardID]       UNIQUEIDENTIFIER NOT NULL,
    [SpecialTag]   VARCHAR (75)     NOT NULL,
    [SpecialCount] INT              NULL,
    CONSTRAINT [PK_Special] PRIMARY KEY CLUSTERED ([SpecialID] ASC),
    CONSTRAINT [FK_OnBoardID_ToOnBoard] FOREIGN KEY ([OnBoardID]) REFERENCES [Bus].[OnBoard] ([OnBoardID])
);

