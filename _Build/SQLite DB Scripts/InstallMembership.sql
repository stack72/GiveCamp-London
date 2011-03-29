CREATE TABLE [aspnet_Applications]
(
	[ApplicationId] NVARCHAR (36) PRIMARY KEY NOT NULL,
	[ApplicationName] NVARCHAR (256) UNIQUE NOT NULL,
	[Description] NVARCHAR (256) NULL
);

CREATE TABLE [aspnet_Roles]
(
  [RoleId] NVARCHAR (36) PRIMARY KEY NOT NULL,
  [RoleName] NVARCHAR (256) NOT NULL,
  [LoweredRoleName] NVARCHAR (256) NOT NULL,
  [ApplicationId] NVARCHAR (36) NOT NULL
);

CREATE TABLE [aspnet_UsersInRoles]
(
  [UserId] NVARCHAR (36) NOT NULL,
  [RoleId] NVARCHAR (36) NOT NULL
);

CREATE TABLE [aspnet_Profile] (
 [UserId] NVARCHAR (36) UNIQUE NOT NULL,
 [LastUpdatedDate] TIMESTAMP NOT NULL,
 [PropertyNames] TEXT (6000) NOT NULL,
 [PropertyValuesString] TEXT (6000) NOT NULL
);

CREATE TABLE [aspnet_Users] (
[UserId] NVARCHAR (36) UNIQUE NOT NULL,
[Username] NVARCHAR (256) NOT NULL,
[LoweredUsername] NVARCHAR (256) NOT NULL,
[ApplicationId] NVARCHAR (36) NOT NULL,
[Email] NVARCHAR (256) NULL,
[LoweredEmail] NVARCHAR (256) NULL,
[Comment] NVARCHAR (3000) NULL,
[Password] NVARCHAR (128) NOT NULL,
[PasswordFormat] NVARCHAR (128) NOT NULL,
[PasswordSalt] NVARCHAR (128) NOT NULL,
[PasswordQuestion] NVARCHAR (256) NULL,
[PasswordAnswer] NVARCHAR (128) NULL,
[IsApproved] BOOL NOT NULL,
[IsAnonymous] BOOL  NOT NULL,
[LastActivityDate] TIMESTAMP  NOT NULL,
[LastLoginDate] TIMESTAMP NOT NULL,
[LastPasswordChangedDate] TIMESTAMP NOT NULL,
[CreateDate] TIMESTAMP  NOT NULL,
[IsLockedOut] BOOL NOT NULL,
[LastLockoutDate] TIMESTAMP NOT NULL,
[FailedPasswordAttemptCount] INTEGER NOT NULL,
[FailedPasswordAttemptWindowStart] TIMESTAMP NOT NULL,
[FailedPasswordAnswerAttemptCount] INTEGER NOT NULL,
[FailedPasswordAnswerAttemptWindowStart] TIMESTAMP NOT NULL
);

CREATE UNIQUE INDEX idxUsers ON [aspnet_Users] ( 'LoweredUsername' , 'ApplicationId' );

CREATE INDEX idxUsersAppId ON [aspnet_Users] ( 'ApplicationId' );

CREATE UNIQUE INDEX idxRoles ON [aspnet_Roles] ( 'LoweredRoleName' , 'ApplicationId' );

CREATE UNIQUE INDEX idxUsersInRoles ON [aspnet_UsersInRoles] ( 'UserId', 'RoleId');

CREATE UNIQUE INDEX idxProfile ON [aspnet_Profile] ( 'UserId' );