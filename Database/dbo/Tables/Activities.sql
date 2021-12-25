CREATE TABLE dbo.Activities (
  ActivityKey INT IDENTITY(1,1) NOT NULL,
  ShortName VARCHAR(50) NOT NULL,
  Description VARCHAR(500) NOT NULL,
  CONSTRAINT Activities_PK
    PRIMARY KEY (ActivityKey)
);
GO
