CREATE TABLE dbo.WorkLogEntries_History (
  WorkLogEntryKey INT NOT NULL,
  HistoryStartTime DATETIME2(3) NOT NULL,
  HistoryEndTime DATETIME2(3) NOT NULL,
  EmployeeKey INT NOT NULL,
  ActivityKey INT NOT NULL,
  StartTime DATETIME2(0) NOT NULL,
  EndTime DATETIME2(0) NOT NULL,
  Comment VARCHAR(500) NOT NULL
);
GO
CREATE CLUSTERED COLUMNSTORE INDEX WorkLogEntries_History_PK
  ON dbo.WorkLogEntries_History;
GO
