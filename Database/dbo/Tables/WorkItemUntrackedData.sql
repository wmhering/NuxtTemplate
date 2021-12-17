CREATE TABLE dbo.WorkItemUntrackedData (
  WorkItemKey INT NOT NULL,
  [CustomFields] VARCHAR(MAX) NOT NULL,
  CONSTRAINT WorkItemUntrackedData_PK
    PRIMARY KEY (WorkItemKey),
  CONSTRAINT WorkItemUntrackedDate_WorkItem_FK
    FOREIGN KEY (WorkItemKey)
    REFERENCES dbo.WorkItems (WorkItemKey)
);
