
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/01/2020 12:14:28
-- Generated from EDMX file: D:\workspace\afluex\Repositories\careermitra\careermitra\source\Data\CareerDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [careermitradb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_RoleUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_UserUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEducationDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EducationDetails] DROP CONSTRAINT [FK_UserEducationDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseEducationDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EducationDetails] DROP CONSTRAINT [FK_CourseEducationDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CityUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_CityUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogs] DROP CONSTRAINT [FK_UserUserLog];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentTypeUserDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDocuments] DROP CONSTRAINT [FK_DocumentTypeUserDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDocuments] DROP CONSTRAINT [FK_UserUserDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_JobJobSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobSkills] DROP CONSTRAINT [FK_JobJobSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_SkillJobSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobSkills] DROP CONSTRAINT [FK_SkillJobSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_JobJobQualification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobQualifications] DROP CONSTRAINT [FK_JobJobQualification];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseJobQualification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobQualifications] DROP CONSTRAINT [FK_CourseJobQualification];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_CategoryJob];
GO
IF OBJECT_ID(N'[dbo].[FK_JobJobLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobLocations] DROP CONSTRAINT [FK_JobJobLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_CityJobLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobLocations] DROP CONSTRAINT [FK_CityJobLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRoleJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_JobRoleJob];
GO
IF OBJECT_ID(N'[dbo].[FK_JobTypeJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_JobTypeJob];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserExperience]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserExperiences] DROP CONSTRAINT [FK_UserUserExperience];
GO
IF OBJECT_ID(N'[dbo].[FK_UserResume]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Resumes] DROP CONSTRAINT [FK_UserResume];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRoleUserExperience]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserExperiences] DROP CONSTRAINT [FK_JobRoleUserExperience];
GO
IF OBJECT_ID(N'[dbo].[FK_EnrollmentProgramUserEnrollmentProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEnrollmentPrograms] DROP CONSTRAINT [FK_EnrollmentProgramUserEnrollmentProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserEnrollmentProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEnrollmentPrograms] DROP CONSTRAINT [FK_UserUserEnrollmentProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEnrollmentProgram1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnrollmentPrograms] DROP CONSTRAINT [FK_UserEnrollmentProgram1];
GO
IF OBJECT_ID(N'[dbo].[FK_EnrollmentProgramTrainingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingMaterials] DROP CONSTRAINT [FK_EnrollmentProgramTrainingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_TrainingMaterialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingMaterials] DROP CONSTRAINT [FK_TrainingMaterialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_TrainingMaterialUserTrainingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTrainingMaterials] DROP CONSTRAINT [FK_TrainingMaterialUserTrainingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTrainingMaterialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTrainingMaterials] DROP CONSTRAINT [FK_UserTrainingMaterialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDeviceUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDevices] DROP CONSTRAINT [FK_UserDeviceUser];
GO
IF OBJECT_ID(N'[dbo].[FK_NotificationUserNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNotifications] DROP CONSTRAINT [FK_NotificationUserNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_UserDeviceUserNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNotifications] DROP CONSTRAINT [FK_UserDeviceUserNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNotifications] DROP CONSTRAINT [FK_UserUserNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_StateCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_StateCity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[UserDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDetails];
GO
IF OBJECT_ID(N'[dbo].[UserLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogs];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[Cities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cities];
GO
IF OBJECT_ID(N'[dbo].[EducationDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EducationDetails];
GO
IF OBJECT_ID(N'[dbo].[UserDocuments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDocuments];
GO
IF OBJECT_ID(N'[dbo].[DocumentTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocumentTypes];
GO
IF OBJECT_ID(N'[dbo].[Skills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skills];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Jobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jobs];
GO
IF OBJECT_ID(N'[dbo].[JobSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobSkills];
GO
IF OBJECT_ID(N'[dbo].[JobQualifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobQualifications];
GO
IF OBJECT_ID(N'[dbo].[JobLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobLocations];
GO
IF OBJECT_ID(N'[dbo].[JobRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobRoles];
GO
IF OBJECT_ID(N'[dbo].[JobTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobTypes];
GO
IF OBJECT_ID(N'[dbo].[UserExperiences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserExperiences];
GO
IF OBJECT_ID(N'[dbo].[Resumes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resumes];
GO
IF OBJECT_ID(N'[dbo].[EnrollmentPrograms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnrollmentPrograms];
GO
IF OBJECT_ID(N'[dbo].[UserEnrollmentPrograms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEnrollmentPrograms];
GO
IF OBJECT_ID(N'[dbo].[TrainingMaterials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainingMaterials];
GO
IF OBJECT_ID(N'[dbo].[UserTrainingMaterials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTrainingMaterials];
GO
IF OBJECT_ID(N'[dbo].[UserDevices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDevices];
GO
IF OBJECT_ID(N'[dbo].[Notifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notifications];
GO
IF OBJECT_ID(N'[dbo].[UserNotifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserNotifications];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [IsChangePassword] bit  NULL,
    [IsVerified] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [RoleId] int  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'UserDetails'
CREATE TABLE [dbo].[UserDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [Email] nvarchar(max)  NULL,
    [Mobile] nvarchar(20)  NULL,
    [Mobile2] nvarchar(20)  NULL,
    [Gender] nvarchar(max)  NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [FatherName] nvarchar(max)  NULL,
    [MotherName] nvarchar(max)  NULL,
    [SpouseName] nvarchar(max)  NULL,
    [HusbandName] nvarchar(max)  NULL,
    [DOB] datetime  NULL,
    [Age] int  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [PinCode] int  NULL,
    [State] nvarchar(max)  NULL,
    [Photo] nvarchar(max)  NULL,
    [CompanyName] nvarchar(max)  NULL,
    [ContactPersonName] nvarchar(max)  NULL,
    [OfficialEmailId] nvarchar(max)  NULL,
    [NoOfEmployees] int  NULL,
    [About] nvarchar(max)  NULL,
    [CityId] int  NULL,
    [Logo] nvarchar(max)  NULL
);
GO

-- Creating table 'UserLogs'
CREATE TABLE [dbo].[UserLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [CreatedAt] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(max)  NOT NULL,
    [DeviceType] nvarchar(max)  NOT NULL,
    [DeviceId] nvarchar(max)  NOT NULL,
    [OS] nvarchar(max)  NOT NULL,
    [IP] nvarchar(max)  NOT NULL,
    [UserAgent] nvarchar(max)  NOT NULL,
    [Domain] nvarchar(max)  NOT NULL,
    [IsDeleted] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'Cities'
CREATE TABLE [dbo].[Cities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [StateId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'EducationDetails'
CREATE TABLE [dbo].[EducationDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [CourseId] int  NULL,
    [YearFrom] nvarchar(max)  NULL,
    [YearTo] nvarchar(max)  NULL,
    [MarksObt] nvarchar(max)  NULL,
    [TotalMarks] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'UserDocuments'
CREATE TABLE [dbo].[UserDocuments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DocumentTypeId] int  NULL,
    [Attachment] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'DocumentTypes'
CREATE TABLE [dbo].[DocumentTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'Skills'
CREATE TABLE [dbo].[Skills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'Jobs'
CREATE TABLE [dbo].[Jobs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL,
    [TitleH] nvarchar(max)  NULL,
    [ShortDescription] nvarchar(max)  NULL,
    [ShortDescriptionH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [SalaryMin] nvarchar(max)  NULL,
    [SalaryMax] nvarchar(max)  NULL,
    [IsMonthly] bit  NOT NULL,
    [ExperienceMin] nvarchar(max)  NOT NULL,
    [ExperienceMax] nvarchar(max)  NOT NULL,
    [IsPublishd] bit  NOT NULL,
    [PostedDate] datetime  NOT NULL,
    [LastDate] datetime  NULL,
    [IsVerified] bit  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [CategoryId] int  NULL,
    [JobRoleId] int  NULL,
    [JobTypeId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'JobSkills'
CREATE TABLE [dbo].[JobSkills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobId] int  NULL,
    [SkillId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'JobQualifications'
CREATE TABLE [dbo].[JobQualifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobId] int  NULL,
    [CourseId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'JobLocations'
CREATE TABLE [dbo].[JobLocations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [JobId] int  NULL,
    [CityId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'JobRoles'
CREATE TABLE [dbo].[JobRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'JobTypes'
CREATE TABLE [dbo].[JobTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'UserExperiences'
CREATE TABLE [dbo].[UserExperiences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [CompanyName] nvarchar(max)  NULL,
    [CompanyNameH] nvarchar(max)  NULL,
    [DateFrom] datetime  NOT NULL,
    [DateTo] datetime  NOT NULL,
    [JobRoleId] int  NULL,
    [Title] nvarchar(max)  NULL,
    [TitleH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'Resumes'
CREATE TABLE [dbo].[Resumes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [Attachment] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'EnrollmentPrograms'
CREATE TABLE [dbo].[EnrollmentPrograms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL,
    [TitleH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [UserId] int  NULL,
    [DateStart] datetime  NULL,
    [DateEnd] datetime  NULL,
    [LastDate] datetime  NULL,
    [PublishDate] datetime  NULL,
    [Duration] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'UserEnrollmentPrograms'
CREATE TABLE [dbo].[UserEnrollmentPrograms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EnrollmentProgramId] int  NULL,
    [UserId] int  NULL,
    [IsApproved] bit  NOT NULL,
    [IsCompleted] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'TrainingMaterials'
CREATE TABLE [dbo].[TrainingMaterials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EnrollmentProgramId] int  NULL,
    [UserId] int  NULL,
    [Title] nvarchar(max)  NULL,
    [TitleH] nvarchar(max)  NULL,
    [ShortDescription] nvarchar(max)  NULL,
    [ShortDescriptionH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [Link] nvarchar(max)  NULL,
    [Attachment] nvarchar(max)  NULL,
    [Type] nvarchar(max)  NULL,
    [PublishDate] datetime  NOT NULL,
    [IsPublished] bit  NOT NULL,
    [Image] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'UserTrainingMaterials'
CREATE TABLE [dbo].[UserTrainingMaterials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TrainingMaterialId] int  NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'UserDevices'
CREATE TABLE [dbo].[UserDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeviceId] nvarchar(max)  NULL,
    [MobileNo] nvarchar(max)  NULL,
    [DeviceType] nvarchar(max)  NULL,
    [FCMId] nvarchar(max)  NULL,
    [UserId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'Notifications'
CREATE TABLE [dbo].[Notifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL,
    [TitleH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [Link] nvarchar(max)  NULL,
    [Logo] nvarchar(max)  NULL,
    [IsSchedule] bit  NOT NULL,
    [DateFrom] datetime  NULL,
    [DateTo] datetime  NULL,
    [Status] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'UserNotifications'
CREATE TABLE [dbo].[UserNotifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NotificationId] int  NULL,
    [UserDeviceId] int  NULL,
    [UserId] int  NULL,
    [Medium] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [PK_UserDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserLogs'
ALTER TABLE [dbo].[UserLogs]
ADD CONSTRAINT [PK_UserLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [PK_Cities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EducationDetails'
ALTER TABLE [dbo].[EducationDetails]
ADD CONSTRAINT [PK_EducationDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDocuments'
ALTER TABLE [dbo].[UserDocuments]
ADD CONSTRAINT [PK_UserDocuments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocumentTypes'
ALTER TABLE [dbo].[DocumentTypes]
ADD CONSTRAINT [PK_DocumentTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Skills'
ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [PK_Skills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [PK_Jobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobSkills'
ALTER TABLE [dbo].[JobSkills]
ADD CONSTRAINT [PK_JobSkills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobQualifications'
ALTER TABLE [dbo].[JobQualifications]
ADD CONSTRAINT [PK_JobQualifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobLocations'
ALTER TABLE [dbo].[JobLocations]
ADD CONSTRAINT [PK_JobLocations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobRoles'
ALTER TABLE [dbo].[JobRoles]
ADD CONSTRAINT [PK_JobRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobTypes'
ALTER TABLE [dbo].[JobTypes]
ADD CONSTRAINT [PK_JobTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserExperiences'
ALTER TABLE [dbo].[UserExperiences]
ADD CONSTRAINT [PK_UserExperiences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Resumes'
ALTER TABLE [dbo].[Resumes]
ADD CONSTRAINT [PK_Resumes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnrollmentPrograms'
ALTER TABLE [dbo].[EnrollmentPrograms]
ADD CONSTRAINT [PK_EnrollmentPrograms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserEnrollmentPrograms'
ALTER TABLE [dbo].[UserEnrollmentPrograms]
ADD CONSTRAINT [PK_UserEnrollmentPrograms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TrainingMaterials'
ALTER TABLE [dbo].[TrainingMaterials]
ADD CONSTRAINT [PK_TrainingMaterials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserTrainingMaterials'
ALTER TABLE [dbo].[UserTrainingMaterials]
ADD CONSTRAINT [PK_UserTrainingMaterials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserDevices'
ALTER TABLE [dbo].[UserDevices]
ADD CONSTRAINT [PK_UserDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Notifications'
ALTER TABLE [dbo].[Notifications]
ADD CONSTRAINT [PK_Notifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserNotifications'
ALTER TABLE [dbo].[UserNotifications]
ADD CONSTRAINT [PK_UserNotifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_UserUserDetail]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserDetail'
CREATE INDEX [IX_FK_UserUserDetail]
ON [dbo].[UserDetails]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'EducationDetails'
ALTER TABLE [dbo].[EducationDetails]
ADD CONSTRAINT [FK_UserEducationDetail]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEducationDetail'
CREATE INDEX [IX_FK_UserEducationDetail]
ON [dbo].[EducationDetails]
    ([UserId]);
GO

-- Creating foreign key on [CourseId] in table 'EducationDetails'
ALTER TABLE [dbo].[EducationDetails]
ADD CONSTRAINT [FK_CourseEducationDetail]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseEducationDetail'
CREATE INDEX [IX_FK_CourseEducationDetail]
ON [dbo].[EducationDetails]
    ([CourseId]);
GO

-- Creating foreign key on [CityId] in table 'UserDetails'
ALTER TABLE [dbo].[UserDetails]
ADD CONSTRAINT [FK_CityUserDetail]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityUserDetail'
CREATE INDEX [IX_FK_CityUserDetail]
ON [dbo].[UserDetails]
    ([CityId]);
GO

-- Creating foreign key on [UserId] in table 'UserLogs'
ALTER TABLE [dbo].[UserLogs]
ADD CONSTRAINT [FK_UserUserLog]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserLog'
CREATE INDEX [IX_FK_UserUserLog]
ON [dbo].[UserLogs]
    ([UserId]);
GO

-- Creating foreign key on [DocumentTypeId] in table 'UserDocuments'
ALTER TABLE [dbo].[UserDocuments]
ADD CONSTRAINT [FK_DocumentTypeUserDocument]
    FOREIGN KEY ([DocumentTypeId])
    REFERENCES [dbo].[DocumentTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentTypeUserDocument'
CREATE INDEX [IX_FK_DocumentTypeUserDocument]
ON [dbo].[UserDocuments]
    ([DocumentTypeId]);
GO

-- Creating foreign key on [UserId] in table 'UserDocuments'
ALTER TABLE [dbo].[UserDocuments]
ADD CONSTRAINT [FK_UserUserDocument]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserDocument'
CREATE INDEX [IX_FK_UserUserDocument]
ON [dbo].[UserDocuments]
    ([UserId]);
GO

-- Creating foreign key on [JobId] in table 'JobSkills'
ALTER TABLE [dbo].[JobSkills]
ADD CONSTRAINT [FK_JobJobSkill]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobJobSkill'
CREATE INDEX [IX_FK_JobJobSkill]
ON [dbo].[JobSkills]
    ([JobId]);
GO

-- Creating foreign key on [SkillId] in table 'JobSkills'
ALTER TABLE [dbo].[JobSkills]
ADD CONSTRAINT [FK_SkillJobSkill]
    FOREIGN KEY ([SkillId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SkillJobSkill'
CREATE INDEX [IX_FK_SkillJobSkill]
ON [dbo].[JobSkills]
    ([SkillId]);
GO

-- Creating foreign key on [JobId] in table 'JobQualifications'
ALTER TABLE [dbo].[JobQualifications]
ADD CONSTRAINT [FK_JobJobQualification]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobJobQualification'
CREATE INDEX [IX_FK_JobJobQualification]
ON [dbo].[JobQualifications]
    ([JobId]);
GO

-- Creating foreign key on [CourseId] in table 'JobQualifications'
ALTER TABLE [dbo].[JobQualifications]
ADD CONSTRAINT [FK_CourseJobQualification]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseJobQualification'
CREATE INDEX [IX_FK_CourseJobQualification]
ON [dbo].[JobQualifications]
    ([CourseId]);
GO

-- Creating foreign key on [CategoryId] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_CategoryJob]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryJob'
CREATE INDEX [IX_FK_CategoryJob]
ON [dbo].[Jobs]
    ([CategoryId]);
GO

-- Creating foreign key on [JobId] in table 'JobLocations'
ALTER TABLE [dbo].[JobLocations]
ADD CONSTRAINT [FK_JobJobLocation]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobJobLocation'
CREATE INDEX [IX_FK_JobJobLocation]
ON [dbo].[JobLocations]
    ([JobId]);
GO

-- Creating foreign key on [CityId] in table 'JobLocations'
ALTER TABLE [dbo].[JobLocations]
ADD CONSTRAINT [FK_CityJobLocation]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityJobLocation'
CREATE INDEX [IX_FK_CityJobLocation]
ON [dbo].[JobLocations]
    ([CityId]);
GO

-- Creating foreign key on [JobRoleId] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_JobRoleJob]
    FOREIGN KEY ([JobRoleId])
    REFERENCES [dbo].[JobRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobRoleJob'
CREATE INDEX [IX_FK_JobRoleJob]
ON [dbo].[Jobs]
    ([JobRoleId]);
GO

-- Creating foreign key on [JobTypeId] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_JobTypeJob]
    FOREIGN KEY ([JobTypeId])
    REFERENCES [dbo].[JobTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobTypeJob'
CREATE INDEX [IX_FK_JobTypeJob]
ON [dbo].[Jobs]
    ([JobTypeId]);
GO

-- Creating foreign key on [UserId] in table 'UserExperiences'
ALTER TABLE [dbo].[UserExperiences]
ADD CONSTRAINT [FK_UserUserExperience]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserExperience'
CREATE INDEX [IX_FK_UserUserExperience]
ON [dbo].[UserExperiences]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'Resumes'
ALTER TABLE [dbo].[Resumes]
ADD CONSTRAINT [FK_UserResume]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserResume'
CREATE INDEX [IX_FK_UserResume]
ON [dbo].[Resumes]
    ([UserId]);
GO

-- Creating foreign key on [JobRoleId] in table 'UserExperiences'
ALTER TABLE [dbo].[UserExperiences]
ADD CONSTRAINT [FK_JobRoleUserExperience]
    FOREIGN KEY ([JobRoleId])
    REFERENCES [dbo].[JobRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobRoleUserExperience'
CREATE INDEX [IX_FK_JobRoleUserExperience]
ON [dbo].[UserExperiences]
    ([JobRoleId]);
GO

-- Creating foreign key on [EnrollmentProgramId] in table 'UserEnrollmentPrograms'
ALTER TABLE [dbo].[UserEnrollmentPrograms]
ADD CONSTRAINT [FK_EnrollmentProgramUserEnrollmentProgram]
    FOREIGN KEY ([EnrollmentProgramId])
    REFERENCES [dbo].[EnrollmentPrograms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EnrollmentProgramUserEnrollmentProgram'
CREATE INDEX [IX_FK_EnrollmentProgramUserEnrollmentProgram]
ON [dbo].[UserEnrollmentPrograms]
    ([EnrollmentProgramId]);
GO

-- Creating foreign key on [UserId] in table 'UserEnrollmentPrograms'
ALTER TABLE [dbo].[UserEnrollmentPrograms]
ADD CONSTRAINT [FK_UserUserEnrollmentProgram]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserEnrollmentProgram'
CREATE INDEX [IX_FK_UserUserEnrollmentProgram]
ON [dbo].[UserEnrollmentPrograms]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'EnrollmentPrograms'
ALTER TABLE [dbo].[EnrollmentPrograms]
ADD CONSTRAINT [FK_UserEnrollmentProgram1]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserEnrollmentProgram1'
CREATE INDEX [IX_FK_UserEnrollmentProgram1]
ON [dbo].[EnrollmentPrograms]
    ([UserId]);
GO

-- Creating foreign key on [EnrollmentProgramId] in table 'TrainingMaterials'
ALTER TABLE [dbo].[TrainingMaterials]
ADD CONSTRAINT [FK_EnrollmentProgramTrainingMaterial]
    FOREIGN KEY ([EnrollmentProgramId])
    REFERENCES [dbo].[EnrollmentPrograms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EnrollmentProgramTrainingMaterial'
CREATE INDEX [IX_FK_EnrollmentProgramTrainingMaterial]
ON [dbo].[TrainingMaterials]
    ([EnrollmentProgramId]);
GO

-- Creating foreign key on [UserId] in table 'TrainingMaterials'
ALTER TABLE [dbo].[TrainingMaterials]
ADD CONSTRAINT [FK_TrainingMaterialUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainingMaterialUser'
CREATE INDEX [IX_FK_TrainingMaterialUser]
ON [dbo].[TrainingMaterials]
    ([UserId]);
GO

-- Creating foreign key on [TrainingMaterialId] in table 'UserTrainingMaterials'
ALTER TABLE [dbo].[UserTrainingMaterials]
ADD CONSTRAINT [FK_TrainingMaterialUserTrainingMaterial]
    FOREIGN KEY ([TrainingMaterialId])
    REFERENCES [dbo].[TrainingMaterials]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrainingMaterialUserTrainingMaterial'
CREATE INDEX [IX_FK_TrainingMaterialUserTrainingMaterial]
ON [dbo].[UserTrainingMaterials]
    ([TrainingMaterialId]);
GO

-- Creating foreign key on [UserId] in table 'UserTrainingMaterials'
ALTER TABLE [dbo].[UserTrainingMaterials]
ADD CONSTRAINT [FK_UserTrainingMaterialUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTrainingMaterialUser'
CREATE INDEX [IX_FK_UserTrainingMaterialUser]
ON [dbo].[UserTrainingMaterials]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserDevices'
ALTER TABLE [dbo].[UserDevices]
ADD CONSTRAINT [FK_UserDeviceUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDeviceUser'
CREATE INDEX [IX_FK_UserDeviceUser]
ON [dbo].[UserDevices]
    ([UserId]);
GO

-- Creating foreign key on [NotificationId] in table 'UserNotifications'
ALTER TABLE [dbo].[UserNotifications]
ADD CONSTRAINT [FK_NotificationUserNotification]
    FOREIGN KEY ([NotificationId])
    REFERENCES [dbo].[Notifications]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NotificationUserNotification'
CREATE INDEX [IX_FK_NotificationUserNotification]
ON [dbo].[UserNotifications]
    ([NotificationId]);
GO

-- Creating foreign key on [UserDeviceId] in table 'UserNotifications'
ALTER TABLE [dbo].[UserNotifications]
ADD CONSTRAINT [FK_UserDeviceUserNotification]
    FOREIGN KEY ([UserDeviceId])
    REFERENCES [dbo].[UserDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserDeviceUserNotification'
CREATE INDEX [IX_FK_UserDeviceUserNotification]
ON [dbo].[UserNotifications]
    ([UserDeviceId]);
GO

-- Creating foreign key on [UserId] in table 'UserNotifications'
ALTER TABLE [dbo].[UserNotifications]
ADD CONSTRAINT [FK_UserUserNotification]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserNotification'
CREATE INDEX [IX_FK_UserUserNotification]
ON [dbo].[UserNotifications]
    ([UserId]);
GO

-- Creating foreign key on [StateId] in table 'Cities'
ALTER TABLE [dbo].[Cities]
ADD CONSTRAINT [FK_StateCity]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StateCity'
CREATE INDEX [IX_FK_StateCity]
ON [dbo].[Cities]
    ([StateId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------