CREATE TABLE dbo.WorkLogEntries (
  WorkLogEntryKey INT IDENTITY(1,1) NOT NULL,
  HistoryStartTime DATETIME2(3) GENERATED ALWAYS AS ROW START NOT NULL,
  HistoryEndTime DATETIME2(3) GENERATED ALWAYS AS ROW END NOT NULL,
  EmployeeKey INT NOT NULL,
  ActivityKey INT NOT NULL,
  StartTime DATETIME2(0) NOT NULL,
  EndTime DATETIME2(0) NOT NULL,
  Comment VARCHAR(500) NOT NULL,
  PERIOD FOR SYSTEM_TIME (HistoryStartTime, HistoryEndTime),
  CONSTRAINT WorkLogEntries_PK
    PRIMARY KEY (WorkLogEntryKey),
  CONSTRAINT WorkItemEntries_Employees_FK
    FOREIGN KEY (EmployeeKey)
    REFERENCES dbo.Employees (EmployeeKey),
  CONSTRAINT WorkItemEntries_Activities_FK
    FOREIGN KEY (ActivityKey)
    REFERENCES dbo.Activities (ActivityKey)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.WorkLogEntries_History, DATA_CONSISTENCY_CHECK = ON));
GO
