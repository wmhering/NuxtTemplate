CREATE TABLE dbo.WorkItemCustomFields (
  WorkItemCustomFieldKey INT IDENTITY(1,1) NOT NULL,
  FieldCode VARCHAR(50) NOT NULL,
  DisplayName VARCHAR(50) NOT NULL,
  DataType VARCHAR(10) NOT NULL,
  IsUntracked BIT NOT NULL,
  IsInUse BIT NOT NULL,
  IsArchived BIT NOT NULL
)
