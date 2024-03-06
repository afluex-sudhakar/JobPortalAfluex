
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/01/2021 11:04:59
-- Generated from EDMX file: D:\workspace\afluex\Repositories\careermitra\careermitra\source\Data\CareerMitra.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [JobPortaldb];
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
IF OBJECT_ID(N'[dbo].[FK_UserUserLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogs] DROP CONSTRAINT [FK_UserUserLog];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserDocument]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDocuments] DROP CONSTRAINT [FK_UserUserDocument];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserExperience]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserExperiences] DROP CONSTRAINT [FK_UserUserExperience];
GO
IF OBJECT_ID(N'[dbo].[FK_UserResume]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Resumes] DROP CONSTRAINT [FK_UserResume];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserEnrollmentProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEnrollmentPrograms] DROP CONSTRAINT [FK_UserUserEnrollmentProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_UserEnrollmentProgram1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EnrollmentPrograms] DROP CONSTRAINT [FK_UserEnrollmentProgram1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNotifications] DROP CONSTRAINT [FK_UserUserNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseEducationDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EducationDetails] DROP CONSTRAINT [FK_CourseEducationDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseJobQualification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobQualifications] DROP CONSTRAINT [FK_CourseJobQualification];
GO
IF OBJECT_ID(N'[dbo].[FK_CityUserDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDetails] DROP CONSTRAINT [FK_CityUserDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_CityJobLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobLocations] DROP CONSTRAINT [FK_CityJobLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_SkillJobSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobSkills] DROP CONSTRAINT [FK_SkillJobSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_CategoryJob];
GO
IF OBJECT_ID(N'[dbo].[FK_JobJobSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobSkills] DROP CONSTRAINT [FK_JobJobSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_JobJobQualification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobQualifications] DROP CONSTRAINT [FK_JobJobQualification];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRoleJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_JobRoleJob];
GO
IF OBJECT_ID(N'[dbo].[FK_JobRoleUserExperience]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserExperiences] DROP CONSTRAINT [FK_JobRoleUserExperience];
GO
IF OBJECT_ID(N'[dbo].[FK_JobTypeJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_JobTypeJob];
GO
IF OBJECT_ID(N'[dbo].[FK_EnrollmentProgramUserEnrollmentProgram]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEnrollmentPrograms] DROP CONSTRAINT [FK_EnrollmentProgramUserEnrollmentProgram];
GO
IF OBJECT_ID(N'[dbo].[FK_EnrollmentProgramTrainingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrainingMaterials] DROP CONSTRAINT [FK_EnrollmentProgramTrainingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_TrainingMaterialUserTrainingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTrainingMaterials] DROP CONSTRAINT [FK_TrainingMaterialUserTrainingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_NotificationUserNotification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserNotifications] DROP CONSTRAINT [FK_NotificationUserNotification];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserTrainingMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTrainingMaterials] DROP CONSTRAINT [FK_UserUserTrainingMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDevices] DROP CONSTRAINT [FK_UserUserDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserJobs] DROP CONSTRAINT [FK_UserUserJob];
GO
IF OBJECT_ID(N'[dbo].[FK_JobUserJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserJobs] DROP CONSTRAINT [FK_JobUserJob];
GO
IF OBJECT_ID(N'[dbo].[FK_UserJob1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_UserJob1];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSkills] DROP CONSTRAINT [FK_UserUserSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_SkillUserSkill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserSkills] DROP CONSTRAINT [FK_SkillUserSkill];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserEducation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEducations] DROP CONSTRAINT [FK_UserUserEducation];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseUserEducation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEducations] DROP CONSTRAINT [FK_CourseUserEducation];
GO
IF OBJECT_ID(N'[dbo].[FK_StateCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_StateCity];
GO
IF OBJECT_ID(N'[dbo].[FK_Pincode]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_Pincode];
GO
IF OBJECT_ID(N'[dbo].[FK_CityPincodeMaster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PincodeMasters] DROP CONSTRAINT [FK_CityPincodeMaster];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentJob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Jobs] DROP CONSTRAINT [FK_DepartmentJob];
GO
IF OBJECT_ID(N'[dbo].[FK_DepartmentDepartmentCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentCategories] DROP CONSTRAINT [FK_DepartmentDepartmentCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryDepartmentCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DepartmentCategories] DROP CONSTRAINT [FK_CategoryDepartmentCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ChatChatMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_ChatChatMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_UserChat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chats] DROP CONSTRAINT [FK_UserChat];
GO
IF OBJECT_ID(N'[dbo].[FK_UserChat1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chats] DROP CONSTRAINT [FK_UserChat1];
GO
IF OBJECT_ID(N'[dbo].[FK_JobChat]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Chats] DROP CONSTRAINT [FK_JobChat];
GO
IF OBJECT_ID(N'[dbo].[FK_UserChatMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_UserChatMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_UserChatMessage1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ChatMessages] DROP CONSTRAINT [FK_UserChatMessage1];
GO
IF OBJECT_ID(N'[dbo].[FK_PincodeMasterJobLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobLocations] DROP CONSTRAINT [FK_PincodeMasterJobLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_JobJobLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobLocations] DROP CONSTRAINT [FK_JobJobLocation];
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
IF OBJECT_ID(N'[dbo].[UserJobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserJobs];
GO
IF OBJECT_ID(N'[dbo].[UserSkills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSkills];
GO
IF OBJECT_ID(N'[dbo].[NewsLetters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsLetters];
GO
IF OBJECT_ID(N'[dbo].[StaticContents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaticContents];
GO
IF OBJECT_ID(N'[dbo].[UserEducations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEducations];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[PincodeMasters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PincodeMasters];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[DepartmentCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DepartmentCategories];
GO
IF OBJECT_ID(N'[dbo].[Chats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Chats];
GO
IF OBJECT_ID(N'[dbo].[ChatMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChatMessages];
GO
IF OBJECT_ID(N'[dbo].[Feedbacks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedbacks];
GO
IF OBJECT_ID(N'[dbo].[EmailMasters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailMasters];
GO
IF OBJECT_ID(N'[dbo].[SMSMasters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SMSMasters];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [TemporaryPassword] nvarchar(max)  NOT NULL,
    [IsChangePassword] bit  NULL,
    [IsVerified] bit  NOT NULL,
    [IsProfileUpdate] bit  NULL,
    [IsDeleted] bit  NOT NULL,
    [IsBlocked] bit  NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [RoleId] int  NULL,
    [OTP] nvarchar(max)  NULL,
    [OTPExpireAt] datetime  NULL,
    [Mode] nvarchar(max)  NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [UserId] int  NOT NULL
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
    [Designation] nvarchar(max)  NULL,
    [ContactPersonName] nvarchar(max)  NULL,
    [NoOfEmployees] int  NULL,
    [About] nvarchar(max)  NULL,
    [CityId] int  NULL,
    [Logo] nvarchar(max)  NULL,
    [CompanyType] nvarchar(max)  NULL,
    [Lat] nvarchar(max)  NULL,
    [Long] nvarchar(max)  NULL
);
GO

-- Creating table 'UserLogs'
CREATE TABLE [dbo].[UserLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [CreatedAt] datetime  NOT NULL,
    [Remark] nvarchar(max)  NOT NULL,
    [DeviceType] nvarchar(max)  NOT NULL,
    [DeviceId] nvarchar(max)  NOT NULL,
    [OS] nvarchar(max)  NOT NULL,
    [IP] nvarchar(max)  NOT NULL,
    [UserAgent] nvarchar(max)  NOT NULL,
    [Domain] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [Lat] nvarchar(max)  NULL,
    [Lng] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [DeviceOtherInfo] nvarchar(max)  NULL,
    [Data] nvarchar(max)  NULL,
    [Error] nvarchar(max)  NULL
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
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [StateId] int  NULL
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
    [SalaryMin] decimal(18,2)  NULL,
    [SalaryMax] decimal(18,2)  NULL,
    [IsMonthly] bit  NOT NULL,
    [ExperienceMin] nvarchar(max)  NOT NULL,
    [ExperienceMax] nvarchar(max)  NOT NULL,
    [IsPublishd] bit  NOT NULL,
    [PostedDate] datetime  NOT NULL,
    [LastDate] datetime  NULL,
    [IsVerified] bit  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [NoOfVacancies] int  NULL,
    [CategoryId] int  NULL,
    [JobRoleId] int  NULL,
    [JobTypeId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [UserId] int  NULL,
    [DepartmentId] int  NULL
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
    [CityId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [PincodeMasterId] int  NULL,
    [JobId] int  NULL
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
    [DeletedAt] datetime  NULL,
    [IsBlocked] bit  NULL
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
    [YearFrom] nvarchar(max)  NOT NULL,
    [YearTo] nvarchar(max)  NOT NULL,
    [JobRoleId] int  NULL,
    [Designation] nvarchar(max)  NULL,
    [DesignationH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [IsCurrent] bit  NOT NULL,
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
    [Image] nvarchar(max)  NULL,
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
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [UserId] int  NULL
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
    [DeletedAt] datetime  NULL,
    [Image] nvarchar(max)  NULL
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

-- Creating table 'UserJobs'
CREATE TABLE [dbo].[UserJobs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [JobId] int  NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserSkills'
CREATE TABLE [dbo].[UserSkills] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NULL,
    [SkillId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'NewsLetters'
CREATE TABLE [dbo].[NewsLetters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'StaticContents'
CREATE TABLE [dbo].[StaticContents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL,
    [TitleH] nvarchar(max)  NULL,
    [ShortDescription] nvarchar(max)  NULL,
    [ShortDescriptionH] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [DescriptionH] nvarchar(max)  NULL,
    [IsPublishd] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [PageName] nvarchar(max)  NULL
);
GO

-- Creating table 'UserEducations'
CREATE TABLE [dbo].[UserEducations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CollegeName] nvarchar(max)  NULL,
    [YearOfPassing] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [UserId] int  NULL,
    [CourseId] int  NULL
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

-- Creating table 'PincodeMasters'
CREATE TABLE [dbo].[PincodeMasters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [PinCode] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [CityId] int  NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [NameH] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'DepartmentCategories'
CREATE TABLE [dbo].[DepartmentCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DepartmentId] int  NULL,
    [CategoryId] int  NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Chats'
CREATE TABLE [dbo].[Chats] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Employer] int  NULL,
    [JobSeeker] int  NULL,
    [JobId] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'ChatMessages'
CREATE TABLE [dbo].[ChatMessages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ChatId] int  NULL,
    [Employer] int  NULL,
    [JobSeeker] int  NULL,
    [IsDeleted] bit  NOT NULL,
    [IsUnRead] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Message] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [Mobile] nvarchar(max)  NULL,
    [Message] nvarchar(max)  NULL,
    [Medium] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'EmailMasters'
CREATE TABLE [dbo].[EmailMasters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [To] nvarchar(max)  NULL,
    [From] nvarchar(max)  NULL,
    [Subject] nvarchar(max)  NULL,
    [Body] nvarchar(max)  NULL,
    [IsTemplate] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedAt] datetime  NOT NULL,
    [ModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL
);
GO

-- Creating table 'SMSMasters'
CREATE TABLE [dbo].[SMSMasters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [To] nvarchar(max)  NULL,
    [From] nvarchar(max)  NULL,
    [Subject] nvarchar(max)  NULL,
    [Body] nvarchar(max)  NULL,
    [IsTemplate] bit  NOT NULL,
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

-- Creating primary key on [Id] in table 'UserJobs'
ALTER TABLE [dbo].[UserJobs]
ADD CONSTRAINT [PK_UserJobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSkills'
ALTER TABLE [dbo].[UserSkills]
ADD CONSTRAINT [PK_UserSkills]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewsLetters'
ALTER TABLE [dbo].[NewsLetters]
ADD CONSTRAINT [PK_NewsLetters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaticContents'
ALTER TABLE [dbo].[StaticContents]
ADD CONSTRAINT [PK_StaticContents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserEducations'
ALTER TABLE [dbo].[UserEducations]
ADD CONSTRAINT [PK_UserEducations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PincodeMasters'
ALTER TABLE [dbo].[PincodeMasters]
ADD CONSTRAINT [PK_PincodeMasters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DepartmentCategories'
ALTER TABLE [dbo].[DepartmentCategories]
ADD CONSTRAINT [PK_DepartmentCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [PK_Chats]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [PK_ChatMessages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailMasters'
ALTER TABLE [dbo].[EmailMasters]
ADD CONSTRAINT [PK_EmailMasters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SMSMasters'
ALTER TABLE [dbo].[SMSMasters]
ADD CONSTRAINT [PK_SMSMasters]
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

-- Creating foreign key on [UserId] in table 'UserTrainingMaterials'
ALTER TABLE [dbo].[UserTrainingMaterials]
ADD CONSTRAINT [FK_UserUserTrainingMaterial]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserTrainingMaterial'
CREATE INDEX [IX_FK_UserUserTrainingMaterial]
ON [dbo].[UserTrainingMaterials]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserDevices'
ALTER TABLE [dbo].[UserDevices]
ADD CONSTRAINT [FK_UserUserDevice]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserDevice'
CREATE INDEX [IX_FK_UserUserDevice]
ON [dbo].[UserDevices]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserJobs'
ALTER TABLE [dbo].[UserJobs]
ADD CONSTRAINT [FK_UserUserJob]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserJob'
CREATE INDEX [IX_FK_UserUserJob]
ON [dbo].[UserJobs]
    ([UserId]);
GO

-- Creating foreign key on [JobId] in table 'UserJobs'
ALTER TABLE [dbo].[UserJobs]
ADD CONSTRAINT [FK_JobUserJob]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobUserJob'
CREATE INDEX [IX_FK_JobUserJob]
ON [dbo].[UserJobs]
    ([JobId]);
GO

-- Creating foreign key on [UserId] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_UserJob1]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserJob1'
CREATE INDEX [IX_FK_UserJob1]
ON [dbo].[Jobs]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserSkills'
ALTER TABLE [dbo].[UserSkills]
ADD CONSTRAINT [FK_UserUserSkill]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserSkill'
CREATE INDEX [IX_FK_UserUserSkill]
ON [dbo].[UserSkills]
    ([UserId]);
GO

-- Creating foreign key on [SkillId] in table 'UserSkills'
ALTER TABLE [dbo].[UserSkills]
ADD CONSTRAINT [FK_SkillUserSkill]
    FOREIGN KEY ([SkillId])
    REFERENCES [dbo].[Skills]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SkillUserSkill'
CREATE INDEX [IX_FK_SkillUserSkill]
ON [dbo].[UserSkills]
    ([SkillId]);
GO

-- Creating foreign key on [UserId] in table 'UserEducations'
ALTER TABLE [dbo].[UserEducations]
ADD CONSTRAINT [FK_UserUserEducation]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserEducation'
CREATE INDEX [IX_FK_UserUserEducation]
ON [dbo].[UserEducations]
    ([UserId]);
GO

-- Creating foreign key on [CourseId] in table 'UserEducations'
ALTER TABLE [dbo].[UserEducations]
ADD CONSTRAINT [FK_CourseUserEducation]
    FOREIGN KEY ([CourseId])
    REFERENCES [dbo].[Courses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseUserEducation'
CREATE INDEX [IX_FK_CourseUserEducation]
ON [dbo].[UserEducations]
    ([CourseId]);
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

-- Creating foreign key on [UserId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [FK_Pincode]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pincode'
CREATE INDEX [IX_FK_Pincode]
ON [dbo].[Roles]
    ([UserId]);
GO

-- Creating foreign key on [CityId] in table 'PincodeMasters'
ALTER TABLE [dbo].[PincodeMasters]
ADD CONSTRAINT [FK_CityPincodeMaster]
    FOREIGN KEY ([CityId])
    REFERENCES [dbo].[Cities]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityPincodeMaster'
CREATE INDEX [IX_FK_CityPincodeMaster]
ON [dbo].[PincodeMasters]
    ([CityId]);
GO

-- Creating foreign key on [DepartmentId] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [FK_DepartmentJob]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentJob'
CREATE INDEX [IX_FK_DepartmentJob]
ON [dbo].[Jobs]
    ([DepartmentId]);
GO

-- Creating foreign key on [DepartmentId] in table 'DepartmentCategories'
ALTER TABLE [dbo].[DepartmentCategories]
ADD CONSTRAINT [FK_DepartmentDepartmentCategory]
    FOREIGN KEY ([DepartmentId])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DepartmentDepartmentCategory'
CREATE INDEX [IX_FK_DepartmentDepartmentCategory]
ON [dbo].[DepartmentCategories]
    ([DepartmentId]);
GO

-- Creating foreign key on [CategoryId] in table 'DepartmentCategories'
ALTER TABLE [dbo].[DepartmentCategories]
ADD CONSTRAINT [FK_CategoryDepartmentCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryDepartmentCategory'
CREATE INDEX [IX_FK_CategoryDepartmentCategory]
ON [dbo].[DepartmentCategories]
    ([CategoryId]);
GO

-- Creating foreign key on [ChatId] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [FK_ChatChatMessage]
    FOREIGN KEY ([ChatId])
    REFERENCES [dbo].[Chats]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ChatChatMessage'
CREATE INDEX [IX_FK_ChatChatMessage]
ON [dbo].[ChatMessages]
    ([ChatId]);
GO

-- Creating foreign key on [Employer] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_UserChat]
    FOREIGN KEY ([Employer])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChat'
CREATE INDEX [IX_FK_UserChat]
ON [dbo].[Chats]
    ([Employer]);
GO

-- Creating foreign key on [JobSeeker] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_UserChat1]
    FOREIGN KEY ([JobSeeker])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChat1'
CREATE INDEX [IX_FK_UserChat1]
ON [dbo].[Chats]
    ([JobSeeker]);
GO

-- Creating foreign key on [JobId] in table 'Chats'
ALTER TABLE [dbo].[Chats]
ADD CONSTRAINT [FK_JobChat]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobChat'
CREATE INDEX [IX_FK_JobChat]
ON [dbo].[Chats]
    ([JobId]);
GO

-- Creating foreign key on [Employer] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [FK_UserChatMessage]
    FOREIGN KEY ([Employer])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChatMessage'
CREATE INDEX [IX_FK_UserChatMessage]
ON [dbo].[ChatMessages]
    ([Employer]);
GO

-- Creating foreign key on [JobSeeker] in table 'ChatMessages'
ALTER TABLE [dbo].[ChatMessages]
ADD CONSTRAINT [FK_UserChatMessage1]
    FOREIGN KEY ([JobSeeker])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserChatMessage1'
CREATE INDEX [IX_FK_UserChatMessage1]
ON [dbo].[ChatMessages]
    ([JobSeeker]);
GO

-- Creating foreign key on [PincodeMasterId] in table 'JobLocations'
ALTER TABLE [dbo].[JobLocations]
ADD CONSTRAINT [FK_PincodeMasterJobLocation]
    FOREIGN KEY ([PincodeMasterId])
    REFERENCES [dbo].[PincodeMasters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PincodeMasterJobLocation'
CREATE INDEX [IX_FK_PincodeMasterJobLocation]
ON [dbo].[JobLocations]
    ([PincodeMasterId]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------