USE GiveCampLondon
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Charity]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Charity]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Team]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Team]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CharitySkills]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[CharitySkills]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CharityTechnologies]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[CharityTechnologies]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ExperienceLevel]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[ExperienceLevel]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FAQ]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[FAQ]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[JobRole]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[JobRole]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Page]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Page]
	
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Content]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Content]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Setting]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Setting]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Sponsor]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Sponsor]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Technology]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Technology]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Volunteer]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Volunteer]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[VolunteerJobRoles]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[VolunteerJobRoles]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[VolunteerTechnologies]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[VolunteerTechnologies]

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Documents]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[Documents]

GO



CREATE TABLE [Charity] (
[MembershipId] [uniqueidentifier] NOT NULL,
[CharityId] INTEGER IDENTITY PRIMARY KEY,
[CharityName] NVARCHAR(100) NOT NULL,
[BackgroundInformation] VARCHAR(4000) NOT NULL,
[WorkRequested] VARCHAR(4000) NOT NULL,
[OtherInfrastructure] VARCHAR(1000)  NULL,
[OtherSupportSkills] VARCHAR(1000)  NULL,
[Email] NVARCHAR(100) NOT NULL,
[Approved] BIT NOT NULL DEFAULT(0)
);

CREATE TABLE [Team] (
[TeamId] INT IDENTITY PRIMARY KEY,
[TeamName] NVARCHAR(100) NOT NULL,
[IsApproved] BIT NOT NULL DEFAULT(0)
);

CREATE TABLE [CharitySkills] (
[CharityId] INTEGER  NOT NULL,
[TechnologyId] INTEGER  NOT NULL,
PRIMARY KEY ([CharityId],[TechnologyId])
);

CREATE TABLE [CharityTechnologies] (
[CharityId] INTEGER NOT NULL,
[TechnologyId] INTEGER NOT NULL,
PRIMARY KEY ([CharityId],[TechnologyId])
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
[JobRoleId] INTEGER IDENTITY PRIMARY KEY,
[Description] VARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [Content] (
[ContentId] INT IDENTITY PRIMARY KEY,
[IsPublished] BIT DEFAULT 'false' NOT NULL,
[Title] VARCHAR(500)  NOT NULL,
[ContentText] NVARCHAR(MAX)  NOT NULL,
[Slug] VARCHAR(500)  NULL,
[Tag] VARCHAR(100) NULL,
[PostDate] DATETIME  NOT NULL,
[AuthorId] INTEGER  NULL
);

CREATE TABLE [Setting] (
[SettingId] INT IDENTITY PRIMARY KEY,
[Name] VARCHAR(200)  NULL,
[Address] VARCHAR(200)  NULL,
[City] VARCHAR(200)  NULL,
[State] VARCHAR(200)  NULL,
[Zip] VARCHAR(200)  NULL,
[ContactName] VARCHAR(50)  NULL,
[ContactEmail] VARCHAR(100)  NULL,
[Version] INTEGER  NULL,
[TwitterTag] VARCHAR(50) NULL,
[PublishCharities] BIT NOT NULL DEFAULT 0,
[PublishVolunteers] BIT NOT NULL DEFAULT 0
);

CREATE TABLE [Sponsor] (
[SponsorId] INTEGER  NOT NULL PRIMARY KEY,
[Name] VARCHAR(100)  NOT NULL,
[Description] VARCHAR(1000)  NOT NULL,
[Url] VARCHAR(500)  NULL,
[ContactName] VARCHAR(50)  NOT NULL,
[ContactEmail] VARCHAR(100)  NOT NULL,
[LogoUri] VARCHAR(250)  NOT NULL,
[IsActive] BIT DEFAULT 'false' NULL
);

CREATE TABLE [Technology] (
[TechnologyId] INTEGER  IDENTITY PRIMARY KEY,
[Description] VARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [Volunteer] (
[MembershipId] [uniqueidentifier] NOT NULL,
[VolunteerId] INT IDENTITY PRIMARY KEY,
[FirstName] VARCHAR(30)  NOT NULL,
[LastName] VARCHAR(30)  NOT NULL,
[TeamName] VARCHAR(30)  NULL,
[Email] VARCHAR(100) NOT NULL,
[PhoneNumber] VARCHAR(50)  NOT NULL,
[IsStudent] BIT DEFAULT 'false' NOT NULL,
[JobDescription] VARCHAR(100)  NOT NULL,
[HasLaptop] BIT DEFAULT 'false' NOT NULL,
[HasExtraLaptop] BIT DEFAULT 'false' NOT NULL,
[IsGoodGuiDesigner] BIT DEFAULT 'false' NOT NULL,
[ExperienceLevelId] INTEGER  NOT NULL,
[YearsOfExperience] INTEGER  NULL,
[DietaryNeeds] VARCHAR(50)  NULL,
[TwitterHandle] VARCHAR(50)  NULL,
[Bio] VARCHAR(4000)  NULL,
[Comments] VARCHAR(4000)  NULL,
[ShirtSize] VARCHAR(3)  NULL,
[ShirtStyle] VARCHAR(10)  NULL,
[TeamId] INT NULL
);

CREATE TABLE [VolunteerJobRoles] (
[VolunteerId] INTEGER NOT NULL,
[JobRoleId] INTEGER NOT NULL,
PRIMARY KEY ([VolunteerId],[JobRoleId])
);

CREATE TABLE [VolunteerTechnologies] (
[VolunteerId] INTEGER NOT NULL,
[TechnologyId] INTEGER NOT NULL,
PRIMARY KEY ([VolunteerId],[TechnologyId])
);

CREATE TABLE [Documents] (
[DocumentId] INTEGER IDENTITY PRIMARY KEY,
[Type] VARCHAR(50) NOT NULL,
[Name] VARCHAR(150) NOT NULL,
[Description] VARCHAR(500) NULL,
[OriginalFilename] VARCHAR(500) NULL,
[MimeType] VARCHAR(50) NULL,
[LocalFilename] VARCHAR(500) NULL,
[UploadDate] DATETIME NOT NULL
);

GO



/* ---------------------------------------------------------------------
	Home Controller Content
*/ ---------------------------------------------------------------------


INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Home Page', 'sidebar', 'blurb', GETDATE(), '<ul><li>
            <h3>Event Resources</h2>
            <ul>
                <li id="menuAgenda">
                    <a href="/Home/Agenda">Agenda</a></li>
                <li id="menuLocation">
                    <a href="/Home/Location">Location</a></li>
            </ul>
        </li>
    </ul>

    <ul>
        <li>
            <h2>@DallasGiveCamp</h2>
            <div>
                <script src="http://widgets.twimg.com/j/2/widget.js"></script>
                <script>
                    new TWTR.Widget({
                        version: 2,
                        type: "profile",
                        rpp: 4,
                        interval: 6000,
                        width: 250,
                        height: 300,
                        theme: {
                            shell: {
                                background: "#333333",
                                color: "#ffffff"
                            },
                            tweets: {
                                background: "#000000",
                                color: "#ffffff",
                                links: "#4aed05"
                            }
                        },
                        features: {
                            scrollbar: false,
                            loop: false,
                            live: false,
                            hashtags: true,
                            timestamp: true,
                            avatars: false,
                            behavior: "all"
                        }
                    }).render().setUser("dallasgivecamp").start();
                </script>
            </div>
        </li>
    </ul>'


/* ---------------------------------------------------------------------
	Charity Content
*/ ---------------------------------------------------------------------

INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Charity Page', 'charity-index', 'blurb', GETDATE(), '<h2>Information For Charities</h2>
	
<p>Below is a link to a form to fill out. Please read the FAQ prior to submitting the form. Keep in 
mind scoped work should be able to be accomplished in one weekend’s time.</p>

<p><a href="/charity/signup">Submit your proposal</a></p>'

INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Charity Thank You', 'charity-thankyou', 'blurb', GETDATE(), '<h2>Thanks for Signing Up!</h2>
	
<p>We will get back to you soon. Need better text here, yo.</p>'


/* ---------------------------------------------------------------------
	Volunteer Content
*/ ---------------------------------------------------------------------

INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Volunteer Sign-up', 'volunteer-signup-index', 'blurb', getdate(), 
'<h2>Volunteer Sign-Up Form</h2>

<p>To volunteer for Give Camp, please fill out this form in it''s entirety. Any additional information about yourself can be included in the comments box at the bottom of the form. Thank you for participating in Give Camp!</p><br/>
<p>* = Required field</p><br/>
<p>It is also an opportunity for you to grow as a professional, work with different people, be a mentor or be mentored. Please review the <a href="/Home/faqs">FAQ</a> for more information.</p><br/>

<p><a href="/Volunteer/SignUp">Sign up to be a volunteer</a></p>'

INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Volunteer Thank You', 'volunteer-thankyou', 'blurb', GETDATE(), '<h2>Thanks for Signing Up!</h2>
	
<p>We will get back to you soon. Need better text here, yo.</p>'

INSERT INTO dbo.Team (TeamName, IsApproved) SELECT 'The Fighting Vigilanties', 1
INSERT INTO dbo.Team (TeamName, IsApproved) SELECT 'Pizzamongers', 0
INSERT INTO dbo.Team (TeamName, IsApproved) SELECT 'Teh Coderz', 1

INSERT INTO dbo.JobRole ([Description], DisplayOrder)
	SELECT 'Designer', 1
	union SELECT 'DBA', 1
	union SELECT 'Developer', 1
	union SELECT 'Business Analyst', 1
	union SELECT 'Project Manager', 1
	
INSERT INTO dbo.ExperienceLevel (ExperienceLevelId, Description, DisplayOrder)
	SELECT 1, 'Beginner', 1
INSERT INTO dbo.ExperienceLevel (ExperienceLevelId, Description, DisplayOrder)
	SELECT 2, 'Intermediate', 2
INSERT INTO dbo.ExperienceLevel (ExperienceLevelId, Description, DisplayOrder)
	SELECT 3, 'Senior', 3


INSERT INTO dbo.Technology ([Description], DisplayOrder)
	SELECT 'ASP', 1
	UNION SELECT 'ASP.NET', 1
	UNION SELECT 'C#', 1
	UNION SELECT 'Visual Basic', 1
	UNION SELECT 'HTML', 1
	UNION SELECT 'JavaScript', 1
	UNION SELECT 'CSS', 1
	UNION SELECT 'DotNetNuke', 1
	UNION SELECT 'Photoshop', 1
	UNION SELECT 'ColdFusion', 1
	UNION SELECT 'Flex', 1
	UNION SELECT 'Flash', 1
	UNION SELECT 'Silverlight', 1
	UNION SELECT 'PHP', 1
	UNION SELECT 'Ruby', 1
	UNION SELECT 'Java', 1
	UNION SELECT 'Joomla', 1
	UNION SELECT 'Sitefinity', 1
	UNION SELECT 'Drupal', 1
	UNION SELECT 'PHP-Nuke', 1
	UNION SELECT 'Umbraco', 1
	UNION SELECT 'WordPress', 1
	UNION SELECT 'MovableType', 1
	UNION SELECT 'Community Server', 1

DECLARE @appId UNIQUEIDENTIFIER, @membershipId UNIQUEIDENTIFIER, @username VARCHAR(100)
SET @appId = (select ApplicationId from aspnet_Applications where ApplicationName = 'GiveCampLondon')

DECLARE @adminId UNIQUEIDENTIFIER
SET @adminId = (SELECT TOP 1 UserId FROM dbo.aspnet_Users)

DELETE FROM dbo.aspnet_Membership WHERE UserId != @adminId
DELETE FROM dbo.aspnet_UsersInRoles WHERE UserId != @adminId
DELETE FROM dbo.aspnet_Users WHERE UserId != @adminId


SET @username = 'jangofett'

INSERT INTO dbo.aspnet_Users
           ([ApplicationId]
           ,[UserId]
           ,[UserName]
           ,[LoweredUserName]
           ,[MobileAlias]
           ,[IsAnonymous]
           ,[LastActivityDate])
     VALUES
           (@appId
           ,NEWID()
           ,@username
           ,@username
           ,null
           ,0
           ,GETDATE())

SET @membershipId = (select UserId from aspnet_Users where UserName = @username)

INSERT INTO Volunteer (MembershipId, FirstName, LastName, Email, PhoneNumber, JobDescription, ExperienceLevelId)
	SELECT @membershipId, 'Jango', 'Fett', 'jango.fett@mtfbwy.com', '2145896587', 'Assassin', 3
	
SET @username = 'bobafett'

INSERT INTO dbo.aspnet_Users
           ([ApplicationId]
           ,[UserId]
           ,[UserName]
           ,[LoweredUserName]
           ,[MobileAlias]
           ,[IsAnonymous]
           ,[LastActivityDate])
     VALUES
           (@appId
           ,NEWID()
           ,@username
           ,@username
           ,null
           ,0
           ,GETDATE())

SET @membershipId = (select UserId from aspnet_Users where UserName = @username)

INSERT INTO Volunteer (MembershipId, FirstName, LastName, Email, PhoneNumber, JobDescription, ExperienceLevelId)
	SELECT @membershipId, 'Boba', 'Fett', 'boba.fett@mtfbwy.com', '2145896587', 'Mercenary', 2
	
SET @username = 'hansolo'

INSERT INTO dbo.aspnet_Users
           ([ApplicationId]
           ,[UserId]
           ,[UserName]
           ,[LoweredUserName]
           ,[MobileAlias]
           ,[IsAnonymous]
           ,[LastActivityDate])
     VALUES
           (@appId
           ,NEWID()
           ,@username
           ,@username
           ,null
           ,0
           ,GETDATE())

SET @membershipId = (select UserId from aspnet_Users where UserName = @username)

INSERT INTO Volunteer (MembershipId, FirstName, LastName, Email, PhoneNumber, JobDescription, ExperienceLevelId)
	SELECT @membershipId, 'Han', 'Solo', 'han.solo@mtfbwy.com', '2145896587', 'Smuggler', 3
/* ---------------------------------------------------------------------
	Email Templates Content
*/ ---------------------------------------------------------------------

INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Volunteers', 'welcomevolunteer-subject', 'email-template', getdate(), 
'$FirstName$ Welcome to GiveCamp'


INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Volunteers', 'welcomevolunteer-body', 'email-template', getdate(), 
'<h2>Dear $FirstName$ $LastName$,</h2> <br />

Welcome to $SiteName$. <br/>
Your UserName is: $UserName$ <br/>'

INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Charity', 'welcomecharity-subject', 'email-template', getdate(), 
'$Name$ Welcome to GiveCamp'


INSERT INTO dbo.Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Charity', 'welcomecharity-body', 'email-template', getdate(), 
'<h2>Dear $Name$,</h2> <br />

We would like to welcome your charity $Name$ to $SiteName$. <br/>
Your UserName is: $UserName$ <br/>'


