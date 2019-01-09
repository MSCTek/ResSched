USE [ResSchedDB]
GO

INSERT INTO [dbo].[Resource]
([ID],[Name],[Description],[ImageLink],[ImageLinkThumb],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),'#1 Red Fox Meeting Room','10 people max, bow window','https://github.com/robintschroeder/ResSched/blob/master/Assets/Red_Fox_500x500.png?raw=true'
,'https://github.com/robintschroeder/ResSched/blob/master/Assets/Red_Fox_500x500.png?raw=true',1,0,'spiderman',getdate(),'spiderman',getdate())
GO

INSERT INTO [dbo].[Resource]
([ID],[Name],[Description],[ImageLink],[ImageLinkThumb],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),'#2 Gray Fox Meeting Room','5 people max','https://github.com/robintschroeder/ResSched/blob/master/Assets/Gray_Fox_500x500.png?raw=true'
,'https://github.com/robintschroeder/ResSched/blob/master/Assets/Gray_Fox_500x500.png?raw=true',1,0,'spiderman',getdate(),'spiderman',getdate())
GO

INSERT INTO [dbo].[Resource]
([ID],[Name],[Description],[ImageLink],[ImageLinkThumb],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),'#3 Artic Fox Meeting Room','5 people max','https://github.com/robintschroeder/ResSched/blob/master/Assets/Arctic_Fox_500x500.png?raw=true'
,'https://github.com/robintschroeder/ResSched/blob/master/Assets/Arctic_Fox_500x500.png?raw=true',1,0,'spiderman',getdate(),'spiderman',getdate())
GO

INSERT INTO [dbo].[Resource]
([ID],[Name],[Description],[ImageLink],[ImageLinkThumb],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),'Fox-Carve','3D Carving Machine','https://github.com/robintschroeder/ResSched/blob/master/Assets/XCarve.png?raw=true'
,'https://github.com/robintschroeder/ResSched/blob/master/Assets/XCarve.png?raw=true',1,0,'spiderman',getdate(),'spiderman',getdate())
GO

INSERT INTO [dbo].[Resource]
([ID],[Name],[Description],[ImageLink],[ImageLinkThumb],[IsActive],[IsDeleted],[CreatedBy],[CreatedDate],[UpdatedBy],[UpdatedDate])
VALUES
(NEWID(),'Media Recording Room','Sound proof media room','https://github.com/robintschroeder/ResSched/blob/master/Assets/XCarve.png?raw=true'
,'https://github.com/robintschroeder/ResSched/blob/master/Assets/XCarve.png?raw=true',1,0,'spiderman',getdate(),'spiderman',getdate())
GO

