/*
Navicat SQL Server Data Transfer

Source Server         : 10.1.32.13
Source Server Version : 105000
Source Host           : 10.1.32.13:1433
Source Database       : monitor_db
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 105000
File Encoding         : 65001

Date: 2019-08-08 13:30:00
*/


-- ----------------------------
-- Table structure for Behavior
-- ----------------------------
DROP TABLE [dbo].[Behavior]
GO
CREATE TABLE [dbo].[Behavior] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL DEFAULT NULL ,
[happenTime] datetime NULL DEFAULT NULL ,
[customerKey] varchar(255) NULL DEFAULT NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL DEFAULT NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL DEFAULT NULL ,
[updatedAt] datetime NULL DEFAULT NULL ,
[behaviorType] varchar(20) NULL DEFAULT NULL ,
[className] text NULL ,
[placeholder] text NULL ,
[inputValue] text NULL ,
[tagName] varchar(15) NULL DEFAULT NULL ,
[innerText] text NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for CustomerPV
-- ----------------------------
DROP TABLE [dbo].[CustomerPV]
GO
CREATE TABLE [dbo].[CustomerPV] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL ,
[happenTime] datetime NULL ,
[customerKey] varchar(255) NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[pageKey] varchar(255) NULL ,
[deviceName] varchar(100) NULL ,
[os] varchar(20) NULL ,
[browserName] varchar(20) NULL ,
[browserVersion] text NULL ,
[monitorIp] varchar(50) NULL ,
[country] varchar(20) NULL ,
[province] varchar(30) NULL ,
[city] varchar(30) NULL ,
[loadType] varchar(20) NULL ,
[loadTime] varchar(10) NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for ExtendBehavior
-- ----------------------------
DROP TABLE [dbo].[ExtendBehavior]
GO
CREATE TABLE [dbo].[ExtendBehavior] (
[keyId] uniqueidentifier NOT NULL ,
[userId] varchar(100) NULL ,
[behaviorType] varchar(50) NULL ,
[behaviorResult] varchar(50) NULL ,
[uploadType] varchar(30) NULL ,
[description] text NULL ,
[happenTime] datetime NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[webMonitorId] uniqueidentifier NULL ,
[completeUrl] text NULL 
)


GO

-- ----------------------------
-- Table structure for HttpLog
-- ----------------------------
DROP TABLE [dbo].[HttpLog]
GO
CREATE TABLE [dbo].[HttpLog] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL ,
[happenTime] datetime NULL ,
[customerKey] varchar(255) NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[httpUrl] text NULL ,
[simpleHttpUrl] text NULL ,
[status] varchar(20) NULL ,
[statusText] varchar(20) NULL ,
[statusResult] varchar(20) NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for JavascriptError
-- ----------------------------
DROP TABLE [dbo].[JavascriptError]
GO
CREATE TABLE [dbo].[JavascriptError] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL ,
[happenTime] datetime NULL ,
[customerKey] varchar(255) NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[pageKey] varchar(255) NULL ,
[deviceName] varchar(100) NULL ,
[os] varchar(20) NULL ,
[browserName] varchar(20) NULL ,
[browserVersion] text NULL ,
[monitorIp] varchar(50) NULL ,
[country] varchar(20) NULL ,
[province] varchar(30) NULL ,
[city] varchar(30) NULL ,
[errorMessage] text NULL ,
[errorStack] text NULL ,
[browserInfo] text NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for LoadPage
-- ----------------------------
DROP TABLE [dbo].[LoadPage]
GO
CREATE TABLE [dbo].[LoadPage] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL ,
[happenTime] datetime NULL ,
[customerKey] varchar(255) NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[loadPage] int NULL ,
[domReady] int NULL ,
[redirect] int NULL ,
[lookupDomain] int NULL ,
[ttfb] int NULL ,
[request] int NULL ,
[loadEvent] int NULL ,
[appcache] int NULL ,
[unloadEvent] int NULL ,
[CONNECT] int NULL ,
[loadType] varchar(20) NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for Project
-- ----------------------------
DROP TABLE [dbo].[Project]
GO
CREATE TABLE [dbo].[Project] (
[keyId] uniqueidentifier NOT NULL DEFAULT (newid()) ,
[projectName] varchar(20) NULL ,
[description] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[IsUse] bit NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Table structure for ResourceLoad
-- ----------------------------
DROP TABLE [dbo].[ResourceLoad]
GO
CREATE TABLE [dbo].[ResourceLoad] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL ,
[happenTime] datetime NULL ,
[customerKey] varchar(255) NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[sourceUrl] text NULL ,
[elementType] varchar(20) NULL ,
[status] varchar(1) NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for ScreenShot
-- ----------------------------
DROP TABLE [dbo].[ScreenShot]
GO
CREATE TABLE [dbo].[ScreenShot] (
[keyId] uniqueidentifier NOT NULL ,
[uploadType] varchar(20) NULL ,
[happenTime] datetime NULL ,
[customerKey] varchar(255) NULL ,
[simpleUrl] text NULL ,
[completeUrl] text NULL ,
[userId] varchar(255) NULL ,
[firstUserParam] text NULL ,
[secondUserParam] text NULL ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[description] text NULL ,
[imgType] varchar(20) NULL ,
[screenInfo] text NULL ,
[webMonitorId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Table structure for User
-- ----------------------------
DROP TABLE [dbo].[User]
GO
CREATE TABLE [dbo].[User] (
[DomainAccount] varchar(255) NOT NULL ,
[RealName] nvarchar(255) NOT NULL ,
[Description] text NULL ,
[IsUse] int NULL DEFAULT ((0)) ,
[createdAt] datetime NULL ,
[updatedAt] datetime NULL ,
[KeyId] uniqueidentifier NOT NULL DEFAULT (newid()) 
)


GO

-- ----------------------------
-- Indexes structure for table Behavior
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Behavior
-- ----------------------------
ALTER TABLE [dbo].[Behavior] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table CustomerPV
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table CustomerPV
-- ----------------------------
ALTER TABLE [dbo].[CustomerPV] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table ExtendBehavior
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ExtendBehavior
-- ----------------------------
ALTER TABLE [dbo].[ExtendBehavior] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table HttpLog
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table HttpLog
-- ----------------------------
ALTER TABLE [dbo].[HttpLog] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table JavascriptError
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table JavascriptError
-- ----------------------------
ALTER TABLE [dbo].[JavascriptError] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table LoadPage
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table LoadPage
-- ----------------------------
ALTER TABLE [dbo].[LoadPage] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table Project
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Project
-- ----------------------------
ALTER TABLE [dbo].[Project] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table ResourceLoad
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ResourceLoad
-- ----------------------------
ALTER TABLE [dbo].[ResourceLoad] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table ScreenShot
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ScreenShot
-- ----------------------------
ALTER TABLE [dbo].[ScreenShot] ADD PRIMARY KEY ([keyId])
GO

-- ----------------------------
-- Indexes structure for table User
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table User
-- ----------------------------
ALTER TABLE [dbo].[User] ADD PRIMARY KEY ([KeyId])
GO
