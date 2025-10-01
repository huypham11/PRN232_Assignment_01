-- Create ActivityLog table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ActivityLog' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[ActivityLog](
        [ActivityID] [int] IDENTITY(1,1) NOT NULL,
        [UserID] [smallint] NOT NULL,
        [Action] [nvarchar](50) NOT NULL,
        [EntityType] [nvarchar](100) NOT NULL,
        [EntityId] [nvarchar](100) NOT NULL,
        [Description] [nvarchar](500) NOT NULL,
        [Timestamp] [datetime] NOT NULL,
        [OldValues] [nvarchar](max) NULL,
        [NewValues] [nvarchar](max) NULL,
        [IpAddress] [nvarchar](100) NULL,
        [UserAgent] [nvarchar](500) NULL,
        CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
        CONSTRAINT [FK_ActivityLog_SystemAccount] FOREIGN KEY([UserID]) REFERENCES [dbo].[SystemAccount] ([AccountID]) ON DELETE CASCADE
    )
END

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ActivityLog_UserID')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ActivityLog_UserID] ON [dbo].[ActivityLog]([UserID] ASC)
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ActivityLog_Timestamp')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ActivityLog_Timestamp] ON [dbo].[ActivityLog]([Timestamp] DESC)
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ActivityLog_EntityType')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ActivityLog_EntityType] ON [dbo].[ActivityLog]([EntityType] ASC)
END

-- Insert some sample data (optional)
INSERT INTO [dbo].[ActivityLog] ([UserID], [Action], [EntityType], [EntityId], [Description], [Timestamp])
VALUES 
    (1, 'CREATE', 'SystemAccount', '1', 'System initialized with admin account', GETDATE()),
    (1, 'VIEW', 'ActivityLog', 'ALL', 'Accessed activity log system for the first time', GETDATE())