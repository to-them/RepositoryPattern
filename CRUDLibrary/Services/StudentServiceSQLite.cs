using CRUDLibrary.DataModels;
using CRUDLibrary.Interfaces;
using CRUDLibrary.Servces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace CRUDLibrary.Services
{
    public class StudentServiceSQLite : Base, IStudentService
    {
        #region :Declarations
        private readonly string _tableName = Resources.DataSources.tblStudents;
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file
        private string _query { get; set; }
        private string _storedProcedureName { get; set; }
        private bool _status = false;
        public string NewRecordId { get; set; }
        private SQLiteDataAdapter da = null;
        private DataTable dt = null;

        ILogger _logger = new Logger();
        #endregion

        public StudentServiceSQLite()
        {
            _connString = this._connSQLite;
            _logFolder = this._logFolderPath;
        }

        #region :Create
        public bool Create(Student obj)
        {
            _query = $"Insert into {_tableName} values (@Name, @Email, @Mobile, @Gender, @DateOfBirth)";
            
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(_connString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    _status = true;
                    _logger.Log($"New record created.", Common.LogType.Info.ToString());
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
                using (SQLiteConnection conn = new SQLiteConnection(_connString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(_query, conn);
                    cmd.CommandType = CommandType.Text;
                    
                    conn.Open();
                    //SQLiteDataReader reader = cmd.ExecuteReader();
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

        public Student Retrieve(string key)
        {
            throw new NotImplementedException();
        }

        public string RetrieveAllStr()
        {
            throw new NotImplementedException();
        }

        public string RetrieveStr(string key)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        #endregion

        #region :Delete
        public bool Delete(string key)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
