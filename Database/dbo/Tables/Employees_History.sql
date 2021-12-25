CREATE TABLE dbo.Employees_History (
  EmployeeKey INT NOT NULL,
  HistoryStartTime DATETIME2(3) NOT NULL,
  HistoryEndTime DATETIME2(3) NOT NULL,
  Change_EmployeeKey INT NULL,
  SamAccountName VARCHAR(50) NOT NULL,
  UserPrincipalName VARCHAR(50) NOT NULL,
  Email VARCHAR(50) NULL,
  LastName VARCHAR(50) NULL,
  FirstName VARCHAR(50) NULL,
  IsSupervisor BIT NOT NULL,
  IsDirector BIT NOT NULL,
  IsProvisional BIT NULL
);
GO
CREATE CLUSTERED COLUMNSTORE INDEX Employees_History_IX
  ON dbo.Employees_History;
GO
