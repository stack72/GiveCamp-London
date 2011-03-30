/*
general rules I've been using to do the conversion from SQL to CE:
remove all dbo.
replace varchar with nvarchar
replace any text columns with nvarchar(4000)
replace any nvarchar(max) columns with nvarchar(4000)
delimt all statements with semicolons, CE build splits on semicolons - may want to use a diff delimiter at some point
remove references to aspnet db, won't work until we convert that to CE as well somehow
*/

CREATE TABLE [Charity] (
[MembershipId] [uniqueidentifier] NOT NULL,
[CharityId] INTEGER IDENTITY PRIMARY KEY,
[CharityName] NVARCHAR(100) NOT NULL,
[BackgroundInformation] NVARCHAR(4000) NOT NULL,
[WorkRequested] NVARCHAR(4000) NOT NULL,
[OtherInfrastructure] NVARCHAR(1000)  NULL,
[OtherSupportSkills] NVARCHAR(1000)  NULL,
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
[Description] NVARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [FAQ] (
[FaqId] INTEGER  NOT NULL PRIMARY KEY,
[Question] NVARCHAR(4000)  NULL,
[Answer] INTEGER  NULL,
[PostDate] TIMESTAMP  NULL,
[AuthorId] INTEGER  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [JobRole] (
[JobRoleId] INTEGER IDENTITY PRIMARY KEY,
[Description] NVARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [Content] (
[ContentId] INT IDENTITY PRIMARY KEY,
[IsPublished] BIT DEFAULT 'false' NOT NULL,
[Title] NVARCHAR(500)  NOT NULL,
[ContentText] NVARCHAR(4000)  NOT NULL,
[Slug] NVARCHAR(500)  NULL,
[Tag] NVARCHAR(100) NULL,
[PostDate] DATETIME  NOT NULL,
[AuthorId] INTEGER  NULL
);

CREATE TABLE [Setting] (
[SettingId] INT IDENTITY PRIMARY KEY,
[Name] NVARCHAR(200)  NULL,
[Address] NVARCHAR(200)  NULL,
[City] NVARCHAR(200)  NULL,
[State] NVARCHAR(200)  NULL,
[Zip] NVARCHAR(200)  NULL,
[ContactName] NVARCHAR(50)  NULL,
[ContactEmail] NVARCHAR(100)  NULL,
[Version] INTEGER  NULL,
[TwitterTag] NVARCHAR(50) NULL,
[PublishCharities] BIT NOT NULL DEFAULT 0,
[PublishVolunteers] BIT NOT NULL DEFAULT 0
);

CREATE TABLE [Sponsor] (
[SponsorId] INTEGER  NOT NULL PRIMARY KEY,
[Name] NVARCHAR(100)  NOT NULL,
[Description] NVARCHAR(1000)  NOT NULL,
[Url] NVARCHAR(500)  NULL,
[ContactName] NVARCHAR(50)  NOT NULL,
[ContactEmail] NVARCHAR(100)  NOT NULL,
[LogoUri] NVARCHAR(250)  NOT NULL,
[IsActive] BIT DEFAULT 'false' NULL
);

CREATE TABLE [Technology] (
[TechnologyId] INTEGER  IDENTITY PRIMARY KEY,
[Description] NVARCHAR(30)  NULL,
[DisplayOrder] INTEGER  NULL
);

CREATE TABLE [Volunteer] (
[MembershipId] [uniqueidentifier] NOT NULL,
[VolunteerId] INT IDENTITY PRIMARY KEY,
[FirstName] NVARCHAR(30)  NOT NULL,
[LastName] NVARCHAR(30)  NOT NULL,
[TeamName] NVARCHAR(30)  NULL,
[Email] NVARCHAR(100) NOT NULL,
[PhoneNumber] NVARCHAR(50)  NOT NULL,
[IsStudent] BIT DEFAULT 'false' NOT NULL,
[JobDescription] NVARCHAR(100)  NOT NULL,
[HasLaptop] BIT DEFAULT 'false' NOT NULL,
[HasExtraLaptop] BIT DEFAULT 'false' NOT NULL,
[IsGoodGuiDesigner] BIT DEFAULT 'false' NOT NULL,
[ExperienceLevelId] INTEGER  NOT NULL,
[YearsOfExperience] INTEGER  NULL,
[DietaryNeeds] NVARCHAR(50)  NULL,
[TwitterHandle] NVARCHAR(50)  NULL,
[Bio] NVARCHAR(4000)  NULL,
[Comments] NVARCHAR(4000)  NULL,
[ShirtSize] NVARCHAR(3)  NULL,
[ShirtStyle] NVARCHAR(10)  NULL,
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
[DocumentId] INTEGER NOT NULL PRIMARY KEY,
[Type] NVARCHAR(50) NOT NULL,
[Name] NVARCHAR(150) NOT NULL,
[Description] NVARCHAR(500) NULL,
[OriginalFilename] NVARCHAR(500) NULL,
[MimeType] NVARCHAR(50) NULL,
[LocalFilename] NVARCHAR(500) NULL,
[UploadDate] DATETIME NOT NULL
);


/* ---------------------------------------------------------------------
	Home Controller Content
*/ ---------------------------------------------------------------------

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Home Page', 'home-index', 'blurb', GETDATE(), '<h2>Dallas GiveCamp 2011!</h2>
	
<p>We are gearing up for the Dallas GiveCamp! If you are a developer and want to help, sign up. If
you are a charity...you sign up too!</p>

<p>Lorizzle ipsizzle dolizzle sit amizzle, consectetuer adipiscing yo mamma. Nullam sapien velizzle, 
its fo rizzle volutpizzle, suscipit for sure, brizzle vizzle, its fo rizzle. Pellentesque we gonna 
chung tortizzle. Sed eros. Stuff fizzle dolor dapibus turpizzle tempizzle shizznit. pellentesque 
nibh et turpizzle. Vestibulum izzle tortor. Gangsta mammasay mammasa mamma oo sa rhoncus fo shizzle. 
Izzle the bizzle habitasse bow wow wow dictumst. Dang dapibizzle. Im in the shizzle we gonna chung 
urna, pretizzle eu, mattis mah nizzle, eleifend phat, nunc. Stuff suscipizzle. Integer sempizzle 
velit sizzle mofo.</p>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'About Page', 'home-about', 'blurb', GETDATE(), '<h2>About GiveCamp</h2>

    <p>GiveCamp is a weekend-long event where software developers, designers, 
    and database administrators donate their time to create custom software 
    for non-profit organizations. This custom software could be a new website 
    for the nonprofit organization, a small data-collection application to 
    keep track of members, or a application for the Red Cross that 
    automatically emails a blood donor three months after they’ve donated 
    blood to remind them that they are now eligible to donate again. The only 
    limitation is that the project should be scoped to be able to be completed 
    in a weekend. 
    </p>

    <p>
    During GiveCamp, developers are welcome to go home in the evenings or 
    camp out all weekend long. There are usually food and drink provided 
    at the event. There are sometimes even game systems set up for when you 
    and your need a little break!  Overall, it’s a great opportunity for 
    people to work together, developing new friendships, and doing something 
    important for their community.</p>

    <p>
    At GiveCamp, there is an expectation of “What Happens at GiveCamp, Stays 
    at GiveCamp”. Therefore, all source code must be turned over to the 
    charities at the end of the weekend (developers cannot ask for payment) 
    and the charities are responsible for maintaining the code moving forward 
    (charities cannot expect the developers to maintain the codebase). 
    </p>
    
	<h3>Media Kit</h3>

    <p>Help us get the word out about the Dallas GiveCamp.</p>

    <ul>
        <li><b>Print Flyer:</b> Having an in-person event, help get the word out by printing copies and handing them out at your event, or print them large and hang them up at your office. <a href="/Content/Files/HelpWantedFlyer.pptx">Get it here.</a></li>
        <li><b>PowerPoint Slide:</b> How about getting the word out by adding a Dallas GiveCamp slide to your next presentation. <a href="/Content/Files/HelpWantedSlide.pptx">Get it here.</a></li>
    </ul>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Frequently Asked Questions', 'home-faq', 'blurb', GETDATE(), '
		<h2>Frequently Asked Questions</h2>

		<p>Please read the FAQs for each specific role:</p>

		<ul>
		    <li><a href="/Home/FAQs/Charities">Charities</a></li>
		    <li><a href="/Home/FAQs/Developers">Developers</a></li>
		    <li><a href="/Home/FAQs/EventStaff">Event Staff</a></li>
		    <li><a href="/Home/FAQs/Sponsors">Sponsors</a></li>
		</ul>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
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
                    }).render().setUser("dallasgivecamp").start()
                </script>
            </div>
        </li>
    </ul>';


/* ---------------------------------------------------------------------
	Charity Content
*/ ---------------------------------------------------------------------

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Charity Page', 'charity-index', 'blurb', GETDATE(), '<h2>Information For Charities</h2>
	
<p>Below is a link to a form to fill out. Please read the FAQ prior to submitting the form. Keep in 
mind scoped work should be able to be accomplished in one weekend’s time.</p>

<p><a href="/charity/signup">Submit your proposal</a></p>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Charity Thank You', 'charity-thankyou', 'blurb', GETDATE(), '<h2>Thanks for Signing Up!</h2>
	
<p>We will get back to you soon. Need better text here, yo.</p>';


/* ---------------------------------------------------------------------
	Volunteer Content
*/ ---------------------------------------------------------------------

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'For Volunteers', 'volunteer-index', 'blurb', getdate(), 
'<h2>Information For Volunteers</h2>

<p>Volunteering for a GiveCamp event is an opportunity for you to use your skills to assist non-profit organizations reach their full potential.</p><br/>
<p>It is also an opportunity for you to grow as a professional, work with different people, be a mentor or be mentored. Please review the <a href="/Home/faq">FAQ</a> for more information.</p><br/>

<p><a href="/Volunteer/SignUp">Sign up to be a volunteer</a></p>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Volunteer Sign-up', 'volunteer-signup-index', 'blurb', getdate(), 
'<h2>Volunteer Sign-Up Form</h2>

<p>To volunteer for Give Camp, please fill out this form in it''s entirety. Any additional information about yourself can be included in the comments box at the bottom of the form. Thank you for participating in Give Camp!</p><br/>
<p>* = Required field</p><br/>
<p>It is also an opportunity for you to grow as a professional, work with different people, be a mentor or be mentored. Please review the <a href="/Home/faqs">FAQ</a> for more information.</p><br/>

<p><a href="/Volunteer/SignUp">Sign up to be a volunteer</a></p>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Volunteer Thank You', 'volunteer-thankyou', 'blurb', GETDATE(), '<h2>Thanks for Signing Up!</h2>
	
<p>We will get back to you soon. Need better text here, yo.</p>';

INSERT INTO Team (TeamName, IsApproved) SELECT 'The Fighting Vigilanties', 1;
INSERT INTO Team (TeamName, IsApproved) SELECT 'Pizzamongers', 0;
INSERT INTO Team (TeamName, IsApproved) SELECT 'Teh Coderz', 1;

INSERT INTO JobRole ([Description], DisplayOrder)
	SELECT 'Designer', 1
	union SELECT 'DBA', 1
	union SELECT 'Developer', 1
	union SELECT 'Business Analyst', 1
	union SELECT 'Project Manager', 1;
	
INSERT INTO ExperienceLevel (ExperienceLevelId, Description, DisplayOrder)
	SELECT 1, 'Beginner', 1;
INSERT INTO ExperienceLevel (ExperienceLevelId, Description, DisplayOrder)
	SELECT 2, 'Intermediate', 2;
INSERT INTO ExperienceLevel (ExperienceLevelId, Description, DisplayOrder)
	SELECT 3, 'Senior', 3;


INSERT INTO Technology ([Description], DisplayOrder)
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
	;

/* ---------------------------------------------------------------------
	Email Templates Content
*/ ---------------------------------------------------------------------

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Volunteers', 'welcomevolunteer-subject', 'email-template', getdate(), 
'@Model.FirstName Welcome to GiveCamp';


INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Volunteers', 'welcomevolunteer-body', 'email-template', getdate(), 
'<h2>Dear @Model.FirstName @Model.LastName,</h2> <br />

Welcome to @Model.SiteName. <br/>
Your UserName is: @Model.UserName <br/>';

INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Charity', 'welcomecharity-subject', 'email-template', getdate(), 
'$Name$ Welcome to GiveCamp';


INSERT INTO Content (IsPublished, Title, Slug, Tag, PostDate, ContentText)
	SELECT 1, 'Welcome Email Template - Charity', 'welcomecharity-body', 'email-template', getdate(), 
'<h2>Dear $Name$,</h2> <br />

We would like to welcome your charity $Name$ to $SiteName$. <br/>
Your UserName is: $UserName$ <br/>';
