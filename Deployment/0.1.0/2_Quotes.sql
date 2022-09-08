/****** Object:  Table [dbo].[Quotes]    Script Date: 2022-07-09 2:05:35 PM ******/
DROP TABLE IF EXISTS [dbo].[Quotes]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Quotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EstimateXML] [xml] NULL,
	[CustomerXML] [xml] NULL,
	[QuoteInfoXML] [xml] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](256) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](256) NULL,
	[SentAt] [datetime2](7) NULL,
	[SentBy] [nvarchar](256) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
