CREATE TABLE [Bus].[SpecialOnBoard] (
    [DetailsID]    INT  NOT NULL,
    [OnBoardID]       INT NOT NULL,
    [TagID]   INT     NOT NULL,
    [Count] TINYINT              NOT NULL,
    CONSTRAINT [PK_Details] PRIMARY KEY CLUSTERED ([DetailsID] ASC),
    CONSTRAINT [FK_OnBoardID_ToOnBoards] FOREIGN KEY ([OnBoardID]) REFERENCES [Bus].[OnBoards]([OnBoardID]), 
    CONSTRAINT [FK_TagId_ToTags] FOREIGN KEY ([TagID]) REFERENCES [Bus].[Tags]([TagID])
);

