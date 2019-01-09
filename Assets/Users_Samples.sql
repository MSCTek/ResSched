USE [ResSchedDB]
GO

INSERT INTO [dbo].[User]
([ID],[InstallationId],[Name],[UserName],[Email],[LastLoginDate],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),null,'Superman','superman','super@man.com',null,1,0,'superman',getdate(),'superman',getdate())
GO

INSERT INTO [dbo].[User]
([ID],[InstallationId],[Name],[UserName],[Email],[LastLoginDate],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),null,'Spider Man','spiderman','spider@man.com',null,1,0,'spiderman',getdate(),'spiderman',getdate())
GO

INSERT INTO [dbo].[User]
([ID],[InstallationId],[Name],[UserName],[Email],[LastLoginDate],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),null,'Wonder Woman','wonderwoman','wonder@woman.com',null,1,0,'wonderwoman',getdate(),'wonderwoman',getdate())
GO

INSERT INTO [dbo].[User]
([ID],[InstallationId],[Name],[UserName],[Email],[LastLoginDate],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),null,'Thanos','thanos','thanos@badguy.com',null,1,0,'thanos',getdate(),'thanos',getdate())
GO

INSERT INTO [dbo].[User]
([ID],[InstallationId],[Name],[UserName],[Email],[LastLoginDate],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),null,'Robin Schroeder','rschroeder','robin@msctek.com',null,1,0,'rschroeder',getdate(),'rschroeder',getdate())
GO

INSERT INTO [dbo].[User]
([ID],[InstallationId],[Name],[UserName],[Email],[LastLoginDate],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),null,'Guest','guest','guest@guest.com',null,1,0,'guest',getdate(),'guest',getdate())
GO
