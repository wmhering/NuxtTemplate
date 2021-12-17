﻿CREATE TABLE dbo.WorkItems (
  WorkItemKey INT IDENTITY(1,1) NOT NULL,
  CustomFields VARCHAR(MAX) NOT NULL,
  HistoryStartTime DATETIME2(3) GENERATED ALWAYS AS ROW START NOT NULL,
  HistoryEndTime DATETIME2(3) GENERATED ALWAYS AS ROW END NOT NULL,
  PERIOD FOR SYSTEM_TIME (HistoryStartTime, HistoryEndTime),
  CONSTRAINT WorkItems_PK
    PRIMARY KEY (WorkItemKey)
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = history.WorkItems));