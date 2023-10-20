using CRUDLibrary.DataModels;
using CRUDLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CRUDLibrary.Services
{
    public class StudentServiceMSSQL : Base, IStudentService
    {
        #region :Declarations
        private readonly string _tableName = Resources.DataSources.tblStudents;
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file
        private string _query { get; set; }
        private string _storedProcedureName { get; set; }
        private bool _status = false;
        public string NewRecordId { get; set; }
        private SqlDataAdapter da = null;
        private DataTable dt = null;

        ILogger _logger = new Logger();
        #endregion

        public StudentServiceMSSQL()
        {
            _connString = this._connMSSQL;
            _logFolder = this._logFolderPath;
        }

        #region :Create
        public bool Create(Student obj)
        {
            _storedProcedureName = Resources.DataSources.spCreateStudent;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                    cmd.Parameters.AddWithValue("@Gender", obj.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", obj.DateOfBirth);

                    //Set Output Parameter
                    SqlParameter outParameter = new SqlParameter
                    {
                        ParameterName = "@Id", //Parameter name defined in stored procedure
                        SqlDbType = SqlDbType.Int, //Data Type of Parameter
                        Direction = ParameterDirection.Output //Specify the parameter as ouput
                                                              //No need to specify the value property
                    };
                    //Add the parameter to the Parameters collection property of SqlCommand object
                    cmd.Parameters.Add(outParameter);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    _status = true;
                    NewRecordId = outParameter.Value.ToString();
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
            _storedProcedureName = Resources.DataSources.spGetStudents;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    da = new SqlDataAdapter(_query, conn);
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
            _storedProcedureName = Resources.DataSources.spGetStudentById;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    SqlCommand cmd = new SqlCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    
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
        
            _storedProcedureName = Resources.DataSources.spUpdateStudent;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    
                    SqlCommand cmd = new SqlCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
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
            
            _storedProcedureName = Resources.DataSources.spDeleteStudent;  //Common.StoredProcedures.spDeleteStudent.ToString();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connString))
                {
                    
                    SqlCommand cmd = new SqlCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
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
