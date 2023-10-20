using CRUDLibrary.DataModels;
using CRUDLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CRUDLibrary.Services
{
    public class StudentServiceMSAccess : Base, IStudentService
    {
        #region :Declarations
        private readonly string _tableName = Resources.DataSources.tblStudents; 
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file
        private string _query { get; set; }
        private string _storedProcedureName { get; set; }
        private bool _status = false;
        public int NewRecordId { get; set; }
        private OleDbDataAdapter da = null;
        private DataTable dt = null;

        ILogger _logger = new Logger();
        DynamicDbServiceMSAccess _service = new DynamicDbServiceMSAccess();
        #endregion

        public StudentServiceMSAccess()
        {
            _connString = this._connMSAccess;
            _logFolder = this._logFolderPath;

            RunDatabaseCheck();
        }

        //Create MS Access database and corrensponding tables if not exists
        private void RunDatabaseCheck()
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
                //Create Database
                _service.CreateAccessDb();

                //Create Tables
                _service.CreateEmployeesTable();
                _service.CreateStudentsTable();
                _service.CreateTestTable();
            }
                
        }

        #region :Create
        public bool Create(Student obj)
        {
            _query = $"Insert into {_tableName}(Name, Email, Mobile, Gender, DateOfBirth) values (@Name, @Email, @Mobile, @Gender, @DateOfBirth)";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_connString))
                {
                    OleDbCommand cmd = new OleDbCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    _status = true;

                    //Log the latest entry
                    cmd.CommandText = "SELECT @@IDENTITY";
                    NewRecordId = (int)cmd.ExecuteScalar();
                    _logger.Log($"New record id: {NewRecordId} created.", Common.LogType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
                _status = false;
            }

            return _status;
        }
        #endregion

        #region :Read
        public List<Student> RetrieveAll()
        {
            List<Student> ls = new List<Student>();
            _query = $"Select * from {_tableName}";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_connString))
                {
                    OleDbCommand cmd = new OleDbCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student();
                            student.Id = Convert.ToInt32(reader["Id"]);
                            student.Name = reader["Name"].ToString();
                            student.Email = reader["Email"].ToString();
                            student.Mobile = reader["Mobile"].ToString();
                            student.Gender = reader["Gender"].ToString();
                            student.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                            ls.Add(student);
                        }
                    }
                }

                _logger.Log($"RetrieveAll invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }

            return ls;
        }

        public List<Student> RetrieveAll2()
        {
            List<Student> ls = new List<Student>();
            _query = _query = $"Select * from {_tableName}";

            try
            {
                using (OleDbConnection conn = new OleDbConnection(_connString))
                {
                    da = new OleDbDataAdapter(_query, conn);
                    da.SelectCommand.CommandType = CommandType.Text;

                    //ds = new DataSet();
                    //da.Fill(ds);
                    ////Or
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Student student = new Student();
                            student.Id = Convert.ToInt32(dr["Id"]);
                            student.Name = dr["Name"].ToString();
                            student.Email = dr["Email"].ToString();
                            student.Mobile = dr["Mobile"].ToString();
                            student.Gender = dr["Gender"].ToString();
                            student.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
                            ls.Add(student);
                        }
                    }

                }

                _logger.Log($"RetrieveAll2 invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }
            return ls;
        }

        public Student Retrieve(string key)
        {
            Student student = new Student();
            int id = Int32.Parse(key);
            _query = $"Select * from {_tableName} where Id={id}";
           
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_connString))
                {
                    OleDbCommand cmd = new OleDbCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    //SqlDataReader reader = cmd.ExecuteReader();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Id = Convert.ToInt32(reader["Id"]);
                            student.Name = reader["Name"].ToString();
                            student.Email = reader["Email"].ToString();
                            student.Mobile = reader["Mobile"].ToString();
                            student.Gender = reader["Gender"].ToString();
                            student.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        }
                    }
                    
                }

                _logger.Log($"Retrieve invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }

            return student;
        }

        public string RetrieveAllStr()
        {
            string s = string.Empty;
            try
            {                
                List<Student> ls = new List<Student>();
                if (ls != null)
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    s = ser.Serialize(ls);
                }
                
                _logger.Log($"RetrieveAllStr invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.InnerException.Message;
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }

            return s;

        }

        public string RetrieveStr(string key)
        {
            string s = string.Empty;
            int id = Int32.Parse(key);
            try
            {              
                var student = RetrieveAll().FirstOrDefault(x => x.Id == id);
                if (student != null)
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    s = ser.Serialize(student);
                }
                
                _logger.Log($"RetrieveStr invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }

            return s;
        }

        public string ListToCSVFile(List<Student> ls)
        {
            throw new NotImplementedException();
        }

        public string ListToXML(List<Student> ls)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region :Update
        public bool Update(Student obj, string key)
        {
            int id = Int32.Parse(key);
            _query = $"Update {_tableName} set Name=@Name, Email=@Email, Mobile=@Mobile, " +
                $"Gender=@Gender, DateOfBirth=@DateOfBirth Where Id={id}";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_connString))
                {
                    OleDbCommand cmd = new OleDbCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    _status = true;
                    _logger.Log($"Record id: {key} updated.", Common.LogType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
                _status = false;
            }

            return _status;
        }
        #endregion

        #region :Delete
        public bool Delete(string key)
        {
            int id = Int32.Parse(key);
            _query = $"Delete from {_tableName} where Id={id}";
            try
            {
                using (OleDbConnection conn = new OleDbConnection(_connString))
                {
                    OleDbCommand cmd = new OleDbCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    _status = true;

                    _logger.Log($"Record id: {key} deleted.", Common.LogType.Info.ToString());
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
                _status = false;
            }

            return _status;
        }
        #endregion

    }
}
