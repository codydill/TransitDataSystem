CREATE TABLE [Bus].[SpecialOnBoard] (
    [SpecialID]    INT  NOT NULL,
    [OnBoardID]       INT NOT NULL,
    [SpecialTag]   VARCHAR (75)     NOT NULL,
    [SpecialCount] TINYINT              NULL,
    CONSTRAINT [PK_Special] PRIMARY KEY CLUSTERED ([SpecialID] ASC),
    CONSTRAINT [FK_OnBoardID_ToOnBoard] FOREIGN KEY ([OnBoardID]) REFERENCES [Bus].[OnBoard] ([OnBoardID])
);

