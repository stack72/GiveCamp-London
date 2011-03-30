USE GiveCampStarterKit
GO
--Username: admin Password: p@ssw0rd
DECLARE @appId uniqueidentifier
DECLARE @roleId uniqueidentifier
DECLARE @userId uniqueidentifier

DELETE FROM aspnet_UsersInRoles
DELETE FROM aspnet_Roles
DELETE FROM aspnet_Membership
DELETE FROM aspnet_Users
DELETE FROM aspnet_Applications


INSERT INTO [GiveCampStarterKit].[dbo].[aspnet_Applications]
           ([ApplicationName]
           ,[LoweredApplicationName]
           ,[ApplicationId]
           ,[Description])
     VALUES
           ('GiveCampStarterKit'
           ,'givecampstarterkit'
           ,'621c868a-39ad-4f63-80c9-02e1f9e4b160'
           ,'Givecamp Starter Kit')


SET @appId = (select ApplicationId from aspnet_Applications where ApplicationName = 'GiveCampStarterKit')

INSERT INTO [GiveCampStarterKit].[dbo].[aspnet_Roles]
           ([ApplicationId]
           ,[RoleId]
           ,[RoleName]
           ,[LoweredRoleName]
           ,[Description])
     VALUES
           (@appId
           ,'dceb57af-d1d3-4cb2-a462-e3543f24c317'
           ,'Administrator'
           ,'administrator'
           ,null)

INSERT INTO [GiveCampStarterKit].[dbo].[aspnet_Roles]
           ([ApplicationId]
           ,[RoleId]
           ,[RoleName]
           ,[LoweredRoleName]
           ,[Description])
     VALUES
           (@appId
           ,'78139D22-5707-4A60-9124-052EFD6DD81A'
           ,'Charity'
           ,'charity'
           ,null)

SET @roleId = (select RoleId from aspnet_Roles where RoleName = 'Administrator')


INSERT INTO [GiveCampStarterKit].[dbo].[aspnet_Users]
           ([ApplicationId]
           ,[UserId]
           ,[UserName]
           ,[LoweredUserName]
           ,[MobileAlias]
           ,[IsAnonymous]
           ,[LastActivityDate])
     VALUES
           (@appId
           ,'5fd0b705-0d89-4ca1-9304-d8f92d3c917b'
           ,'admin'
           ,'admin'
           ,null
           ,0
           ,GETDATE())


SET @userId = (select UserId from aspnet_Users where UserName = 'admin')


INSERT INTO [GiveCampStarterKit].[dbo].[aspnet_Membership]
           ([ApplicationId]
           ,[UserId]
           ,[Password]
           ,[PasswordFormat]
           ,[PasswordSalt]
           ,[MobilePIN]
           ,[Email]
           ,[LoweredEmail]
           ,[PasswordQuestion]
           ,[PasswordAnswer]
           ,[IsApproved]
           ,[IsLockedOut]
           ,[CreateDate]
           ,[LastLoginDate]
           ,[LastPasswordChangedDate]
           ,[LastLockoutDate]
           ,[FailedPasswordAttemptCount]
           ,[FailedPasswordAttemptWindowStart]
           ,[FailedPasswordAnswerAttemptCount]
           ,[FailedPasswordAnswerAttemptWindowStart]
           ,[Comment])
     VALUES
           (@appId
           ,@userId
           ,'v/4kjZujtopKl+HELJTIWpK38iY='
           ,1
           ,'I1n3NmWdn5OuPBmwF5zwuA=='
           ,null
           ,'admin@admin.com'
           ,'admin@admin.com'
           ,null
           ,null
           ,1
           ,0
           ,GETDATE()
           ,GETDATE()
           ,GETDATE()
           ,GETDATE()
           ,0
           ,'1754-01-01 00:00:00.000'
           ,0
           ,'1754-01-01 00:00:00.000'
           ,null)


INSERT INTO [GiveCampStarterKit].[dbo].[aspnet_UsersInRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (@userId
           ,@roleId)
GO





