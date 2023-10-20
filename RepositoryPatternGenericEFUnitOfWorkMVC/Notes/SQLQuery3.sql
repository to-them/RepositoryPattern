-- Create Employee Table
CREATE TABLE Employees
(
  EmployeeID INT PRIMARY KEY IDENTITY(1,1),
  Name VARCHAR(100),
  Gender VARCHAR(100),
  Salary INT,
  Dept VARCHAR(50)
)
GO
-- Populate some test data into Employee table
INSERT INTO Employees VALUES('Pranaya', 'Male', 10000, 'IT' )
INSERT INTO Employees VALUES('Anurag', 'Male', 15000, 'HR' )
INSERT INTO Employees VALUES('Priyanka', 'Female', 22000, 'HR' )
INSERT INTO Employees VALUES('Sambit', 'Male', 20000, 'IT' )
INSERT INTO Employees VALUES('Preety', 'Female', 25000, 'IT' )
INSERT INTO Employees VALUES('Hina', 'Female', 20000, 'HR' )
GO