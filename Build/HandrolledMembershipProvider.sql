CREATE TABLE [dbo].[User](
	[ID] INT IDENTITY NOT NULL,
	[FirstName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[LastName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[EmailAddress] [varchar](150) COLLATE Latin1_General_CI_AS NOT NULL,
	[UserName] [varchar](150) COLLATE Latin1_General_CI_AS NOT NULL,
	[Password] [varchar](1024) COLLATE Latin1_General_CI_AS NOT NULL,
	[TwitterHandle] varchar(256) COLLATE Latin1_General_CI_AS NOT NULL,
	[RoleId] [int]
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[UserRoles](
	[RoleId] INT IDENTITY NOT NULL,
	[RoleName] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[RoleDescription] [varchar](50) COLLATE Latin1_General_CI_AS NOT NULL
	CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO UserRoles (RoleName, RoleDescription) VALUES ('Administrator', 'Admin Group');
INSERT INTO UserRoles (RoleName, RoleDescription) VALUES ('User', 'User Group');

