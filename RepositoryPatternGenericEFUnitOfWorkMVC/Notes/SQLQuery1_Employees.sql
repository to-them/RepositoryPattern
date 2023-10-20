-- Create Employee Table
CREATE TABLE Employees
(
  EmployeeID INT PRIMARY KEY IDENTITY(1,1),
  [Name] VARCHAR(100),
  Gender VARCHAR(100),
  Salary decimal(18,2),
  Dept VARCHAR(50)
)
GO
-- Seed the table
INSERT INTO Employees VALUES('Test Name', 'Male', 10000, 'Sales' )
GO