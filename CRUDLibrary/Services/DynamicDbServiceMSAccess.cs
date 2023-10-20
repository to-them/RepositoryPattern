using CRUDLibrary.DataModels;
using CRUDLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Services
{
    public class DynamicDbServiceMSAccess : Base
    {
        private string _dataFolderPath { get; set; }
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file

        ILogger _logger = new Logger();
        DAL.MSAccessDAL dal = new DAL.MSAccessDAL();
        /// <summary>
        /// Check for MS Access database and if not exists, create the db and its corresponding tables with default data
        /// </summary>
        public DynamicDbServiceMSAccess()
        {
            _dataFolderPath = this._msaccessDbFolderPath;
            _connString = this._connMSAccess;
            _logFolder = this._logFolderPath;

            //Create file directory if it does not exist
            DirectoryInfo dir = new DirectoryInfo(_dataFolderPath);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        //Create Access database if not Exists
        #region :Create Access Database
        public void CreateAccessDb()
        {
            //Split the connection String:
            //[0] = Provider
            //[0] = Data Source
            string[] arrConnStr = _connString.Split(';');

            //Split the Data Source:
            //[0] = Data Source
            //[1] = file path
            string[] arrDataSource = arrConnStr[1].Split('=');
            string dataFilePath = arrDataSource[1];

            if (!File.Exists(dataFilePath))
            {
                //Requires: ADOX dll i.e. Microsoft ADO Ext. 2.8 for DDL and Security from COM Libries
                ADOX.Catalog cat = new ADOX.Catalog();
                ADOX.Table table = new ADOX.Table();

                //Create the table and it's fields. 
                string tableName = Resources.DataSources.tblTable1; //Common.TableName.Table1.ToString();
                table.Name = tableName;
                table.Columns.Append("Id");
                table.Columns.Append("FirstName");
                table.Columns.Append("LastName");

                try
                {
                    cat.Create(_connString);
                    cat.Tables.Append(table);

                    //Now Close the database
                    //Requires adodb dll from Assemblies > Entensions Libraries
                    ADODB.Connection con = cat.ActiveConnection as ADODB.Connection;
                    if (con != null)
                        con.Close();

                }
                catch (Exception ex)
                {
                    string err = ex.ToString();
                }
                cat = null;
            }

        }
        #endregion

        #region :Create Students Table
        public void CreateStudentsTable()
        {
            string tableName = "Students";
            //Create Table
            if (!isTableExist(tableName))
            {
                try
                {
                    String createSQL = "CREATE TABLE " + tableName + "(" +
                    "Id INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL, " +
                    "[Name] varchar(100), " +
                    "[Email] varchar(50), " +
                    "Mobile varchar(50), " +
                    "Gender varchar(20), " +                    
                    "DateOfBirth DateTime " +
                    ")";

                    dal.ExecuteQuery(createSQL);

                    //Seed the table
                    SeedStudentsTable(tableName);
                }
                catch (System.Exception ex)
                {
                    string err = ex.ToString();
                }

            }

        }

        private void SeedStudentsTable(string tableName)
        {
            try
            {
                string query = $"INSERT INTO {tableName}([Name], [Email], Mobile, Gender, DateOfBirth) " +
                $"VALUES('Some Name', 'some@email.com', '109-989-7766', 'Male', '{DateTime.UtcNow.ToString()}')";

                dal.ExecuteQuery(query);
            }
            catch (System.Exception ex)
            {
                string err = ex.ToString();
            }

        }
        #endregion

        #region :Create Employees Table
        public void CreateEmployeesTable()
        {
            string tableName = "Employees";
            //Create Table
            if (!isTableExist(tableName))
            {
                try
                {
                    String createSQL = "CREATE TABLE " + tableName + "(" +
                    "EmployeeID INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL, " +
                    "FirstName varchar(30), " +
                    "LastName varchar(30), " +
                    "Title varchar(50), " +
                    "AccessID int, " +
                    "Passwd varchar(16), " +
                    "UserName varchar(15), " +
                    "HMPhone varchar(12), " +
                    "OtherPhone varchar(12) " +
                    ")";

                    dal.ExecuteQuery(createSQL);

                    //Seed the table
                    SeedEmployeesTable(tableName);
                }
                catch (System.Exception ex)
                {
                    string err = ex.ToString();
                }

            }

        }

        private void SeedEmployeesTable(string tableName)
        {
            try
            {
                string query = $"INSERT INTO {tableName}(FirstName, LastName, Title, AccessID, Passwd, UserName, HMPhone, OtherPhone) " +
                $"VALUES('Admin', 'Admin', 'Administrator', 4, 'administrator', 'admin', '220-987-9988', '220-987-9988')";

                dal.ExecuteQuery(query);
            }
            catch (System.Exception ex)
            {
                string err = ex.ToString();
            }

        }
        #endregion

        #region :TEST TABLE - Template
        public void CreateTestTable()
        {
            string tableName = "Test";
            //Create Table
            if (!isTableExist(tableName))
            {
                try
                {
                    String createSQL = "CREATE TABLE " + tableName + "(" +
                    "ID INTEGER IDENTITY(1,1) PRIMARY KEY NOT NULL, " +
                    "[Name] varchar(50), " +
                    "Quantity int, " +
                    "Comments nText, " +
                    "CreatedOn DateTime, " +
                    "Amount money," +
                    "Completed bit" +
                    ")";

                    dal.ExecuteQuery(createSQL);

                    //Seed the table
                    SeedTestTable(tableName);
                }
                catch (System.Exception ex)
                {
                    string err = ex.ToString();
                }

            }

        }

        private void SeedTestTable(string tableName)
        {
            try
            {
                string query = $"INSERT INTO {tableName}([Name], Quantity, Comments, CreatedOn, Amount, Completed) " +
                $"VALUES('SomeName', 3,'Initial Comments', '{DateTime.UtcNow.ToString()}', 2.05, true)";

                dal.ExecuteQuery(query);
            }
            catch (System.Exception ex)
            {
                string err = ex.ToString();
            }

        }
        #endregion

        #region :METHODS
        //Check if table exist
        private bool isTableExist(string tablename)
        {
            bool result = false;
            if (dal.getDbTables.Count > 0)
            {
                foreach (string tbName in dal.getDbTables)
                {
                    if (tbName.ToLower().Equals(tablename.ToLower()))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
