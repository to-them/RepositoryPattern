-- Create Student Table
CREATE TABLE Students
(
  StudentID INT PRIMARY KEY IDENTITY(1,1),
  [Name] VARCHAR(100),
  Gender VARCHAR(100),
  Course VARCHAR(50)
)
GO
-- Seed the table
INSERT INTO Students VALUES('Test Student', 'Male', 'History')
GO