CREATE TABLE [Charity] (
[CharityId] INTEGER  NOT NULL PRIMARY KEY,
[CharityName] VARCHAR(100)  NULL,
[BackgroundInformation] VARCHAR(4000)  NULL,
[WorkRequested] VARCHAR(4000)  NULL,
[OtherInfrastructure] VARCHAR(1000)  NULL,
[OtherSupportSkills] VARCHAR(1000)  NULL
);

CREATE TABLE [CharitySkills] (
[CharityId] INTEGER  NOT NULL,
[TechnologyId] INTEGER  NOT NULL,
PRIMARY KEY ([CharityId],[TechnologyId])
);

CREATE TABLE [CharityTechnologies] (
[CharitId] INTEGER  NULL,
[TechnologyId] INTEGER  NULL,
PRIMARY KEY ([CharitId],[TechnologyId])
);

CREATE TABLE [ExperienceLevel] (
[ExperienceLevelId] INTEGER  PRIMARY KEY NOT NULL,
[Description] VARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [FAQ] (
[FaqId] INTEGER  NOT NULL PRIMARY KEY,
[Question] TEXT  NULL,
[Answer] INTEGER  NULL,
[PostDate] TIMESTAMP  NULL,
[AuthorId] INTEGER  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [JobRole] (
[JobRoleId] INTEGER  NULL PRIMARY KEY,
[Description] VARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [Page] (
[PageId] INTEGER  NOT NULL PRIMARY KEY,
[IsPublished] BOOLEAN DEFAULT 'false' NOT NULL,
[Title] VARCHAR(500)  NOT NULL,
[Content] TEXT  NOT NULL,
[Slug] VARCHAR(500)  NULL,
[PostDate] TIMESTAMP  NOT NULL,
[AuthorId] INTEGER  NOT NULL
);

CREATE TABLE [Setting] (
[Name] VARCHAR(200)  NULL,
[Address] VARCHAR(200)  NULL,
[City] VARCHAR(200)  NULL,
[State] VARCHAR(200)  NULL,
[Zip] VARCHAR(200)  NULL,
[ContactName] VARCHAR(50)  NULL,
[ContactEmail] VARCHAR(100)  NULL,
[Version] INTEGER(50)  NULL
);

CREATE TABLE [Sponsor] (
[SponsorId] INTEGER  NOT NULL PRIMARY KEY,
[Name] VARCHAR(100)  NOT NULL,
[Description] VARCHAR(1000)  NOT NULL,
[Url] VARCHAR(500)  NULL,
[ContactName] VARCHAR(50)  NOT NULL,
[ContactEmail] VARCHAR(100)  NOT NULL,
[LogoUri] VARCHAR(250)  NOT NULL,
[IsActive] BOOLEAN DEFAULT 'false' NULL
);

CREATE TABLE [Technology] (
[TechnologyId] INTEGER  NOT NULL PRIMARY KEY,
[Description] VARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [Volunteer] (
[VolunteerId] INTEGER  NOT NULL PRIMARY KEY,
[FirstName] VARCHAR(30)  NOT NULL,
[LastName] VARCHAR(30)  NOT NULL,
[TeamName] VARCHAR(30)  NULL,
[EmailAddress] VARCHAR(255)  NOT NULL,
[PhoneNumber] VARCHAR(50)  NOT NULL,
[Student] BOOLEAN DEFAULT 'false' NOT NULL,
[JobDescription] VARCHAR(100)  NOT NULL,
[HasLaptop] BOOLEAN DEFAULT 'false' NOT NULL,
[HasExtraLaptop] BOOLEAN DEFAULT 'false' NOT NULL,
[GoodGuiDesigner] BOOLEAN DEFAULT 'false' NOT NULL,
[ExperienceLevelId] INTEGER  NOT NULL,
[YearsOfExperience] INTEGER  NULL,
[DietaryNeeds] VARCHAR(50)  NULL,
[TwitterHandle] VARCHAR(50)  NULL,
[Bio] VARCHAR(4000)  NULL,
[Comments] VARCHAR(4000)  NULL,
[ShirtSize] VARCHAR(3)  NULL,
[ShirtStyle] VARCHAR(10)  NULL
);

CREATE TABLE [VolunteerJobRoles] (
[VolunteerId] INTEGER  NULL,
[JobRoleId] INTEGER  NULL,
PRIMARY KEY ([VolunteerId],[JobRoleId])
);

CREATE TABLE [VolunteerTechnologies] (
[VolunteerId] INTEGER  NULL,
[Technology] INTEGER  NULL,
PRIMARY KEY ([VolunteerId],[Technology])
);

CREATE TABLE [aspnet_Applications]
(
	[ApplicationId] NVARCHAR (36) PRIMARY KEY NOT NULL,
	[ApplicationName] NVARCHAR (256) UNIQUE NOT NULL,
	[Description] NVARCHAR (256) NULL
);

CREATE TABLE [aspnet_Profile] (
 [UserId] NVARCHAR (36) UNIQUE NOT NULL,
 [LastUpdatedDate] TIMESTAMP NOT NULL,
 [PropertyNames] TEXT (6000) NOT NULL,
 [PropertyValuesString] TEXT (6000) NOT NULL
);

CREATE TABLE [aspnet_Roles]
(
  [RoleId] NVARCHAR (36) PRIMARY KEY NOT NULL,
  [RoleName] NVARCHAR (256) NOT NULL,
  [LoweredRoleName] NVARCHAR (256) NOT NULL,
  [ApplicationId] NVARCHAR (36) NOT NULL
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

CREATE TABLE [aspnet_UsersInRoles]
(
  [UserId] NVARCHAR (36) NOT NULL,
  [RoleId] NVARCHAR (36) NOT NULL
);

CREATE UNIQUE INDEX [idxCharity] ON [Charity](
[CharityName]  ASC
);

CREATE UNIQUE INDEX [idxExperienceLevel] ON [ExperienceLevel](
[Description]  ASC,
[DisplayOrder]  ASC
);

CREATE INDEX [idxPage] ON [Page](
[IsPublished]  ASC,
[Title]  ASC,
[Slug]  ASC,
[AuthorId]  ASC,
[PostDate]  ASC
);

CREATE UNIQUE INDEX idxProfile ON [aspnet_Profile] ( 'UserId' );

CREATE UNIQUE INDEX idxRoles ON [aspnet_Roles] ( 'LoweredRoleName' , 'ApplicationId' );

CREATE INDEX idxUsersAppId ON [aspnet_Users] ( 'ApplicationId' );

CREATE UNIQUE INDEX idxUsers ON [aspnet_Users] ( 'LoweredUsername' , 'ApplicationId' );

CREATE UNIQUE INDEX idxUsersInRoles ON [aspnet_UsersInRoles] ( 'UserId', 'RoleId');
