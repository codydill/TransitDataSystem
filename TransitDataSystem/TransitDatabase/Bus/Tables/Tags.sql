CREATE TABLE [Bus].[Tags]
(
	[TagId] INT NOT NULL PRIMARY KEY, 
    [Description] NCHAR(75) NOT NULL, 
    [BeginDate] DATETIME2 NULL, 
    [EndDate] DATETIME2 NULL
)
