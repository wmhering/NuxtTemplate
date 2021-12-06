CREATE TABLE history.WorkItems (
  WorkItemsKey INT NOT NULL,
  [CustomFields] VARCHAR(MAX) NOT NULL,
  HistoryStartTime DATETIME2(3) NOT NULL,
  HistoryEndTime DATETIME2(3) NOT NULL
);
