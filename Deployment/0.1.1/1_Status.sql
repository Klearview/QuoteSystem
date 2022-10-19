DROP TABLE IF EXISTS [dbo].[Status]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Status](
    [Id] [int] NOT NULL,
    [Name] [nvarchar](50) NOT NULL
);
GO

INSERT INTO [dbo].[Status] (Id, Name)
VALUES
(1, ''),
(2, 'Ready to Quote'),
(3, 'Ready for Customer'),
(4, 'Sent to Customer'),
(5, 'Booked'),
(6, 'Deleted'),
(7, 'Cancelled')
GO