﻿SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inventory](
	[AssetId] [int] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](25) NULL,
	[Description] [nvarchar](255) NULL,
	[Active] [bit] DEFAULT 1,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Inventory] PRIMARY KEY CLUSTERED (
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO