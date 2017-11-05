CREATE TABLE [dbo].[ToDoItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(100) NULL, 
    [IsCompleted] BIT NULL
)
