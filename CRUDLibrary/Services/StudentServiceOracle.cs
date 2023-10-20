using CRUDLibrary.DataModels;
using CRUDLibrary.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CRUDLibrary.Services
{
    public class StudentServiceOracle : Base, IStudentService
    {
        #region :Declarations
        private readonly string _tableName = Resources.DataSources.tblStudents; //Common.TableName.Students.ToString();
        private readonly string _oraclePackageName = Resources.DataSources.pkg_students2; //Common.OraclePackageName;
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file
        private string _query { get; set; }
        private string _storedProcedureName { get; set; }
        private bool _status = false;
        public string NewRecordId { get; set; }
        private OracleDataAdapter da = null;
        private DataTable dt = null;        

        ILogger _logger = new Logger();
        #endregion

        public StudentServiceOracle()
        {
            _connString = this._connOraSQL;
            _logFolder = this._logFolderPath;
        }

        #region :Using Package and Stored Procedure

        #region :Create        
        public bool Create(Student obj)
        {
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spCreateStudent}";
            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {   
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("stud_name", OracleDbType.Varchar2, obj.Name, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_email", OracleDbType.Varchar2, obj.Email, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_mobile", OracleDbType.Varchar2, obj.Mobile, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_gender", OracleDbType.Varchar2, obj.Gender, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_dob", OracleDbType.Date, obj.DateOfBirth, ParameterDirection.Input);
                    cmd.Parameters.Add(new OracleParameter("o_stud_id", OracleDbType.Int32, ParameterDirection.Output));

                    int recCreated = 0;
                    try
                    {
                        conn.Open();
                        recCreated = cmd.ExecuteNonQuery();
                        _status = true;

                        object id = cmd.Parameters["o_stud_id"].Value;
                        NewRecordId = id.ToString();
                        _logger.Log($"New record id:{NewRecordId} created.", Common.LogType.Info.ToString());
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                        _logger.Log($"Exception occured: {err}", Common.LogType.Error.ToString());
                        _status = false;
                    }

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
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spGetStudents}";
           
            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("o_students", OracleDbType.RefCursor, ParameterDirection.Output));
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        //Reader to List Object
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

                    conn.Close();
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
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spGetStudents}";

            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    da = new OracleDataAdapter(_storedProcedureName, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add(new OracleParameter("o_students", OracleDbType.RefCursor, ParameterDirection.Output));

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
            Int32 id = Int32.Parse(key);
            Student student = null;
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spGetStudent}";

            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    da = new OracleDataAdapter(_storedProcedureName, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("stud_id", OracleDbType.Int32, id, ParameterDirection.Input);
                    da.SelectCommand.Parameters.Add(new OracleParameter("o_student", OracleDbType.RefCursor, ParameterDirection.Output));

                    //ds = new DataSet();
                    //da.Fill(ds);
                    ////Or
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            student = new Student();
                            student.Id = Convert.ToInt32(dr["Id"]);
                            student.Name = dr["Name"].ToString();
                            student.Email = dr["Email"].ToString();
                            student.Mobile = dr["Mobile"].ToString();
                            student.Gender = dr["Gender"].ToString();
                            student.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
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

            return student;
        }

        public Student Retrieve2(string key)
        {
            Student student = null;
            try
            {
                student = RetrieveAll().FirstOrDefault(x => x.Id == Int32.Parse(key));
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
                string error = ex.ToString();
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

        public int getEmployeeTitleCount(string job_title)
        {
            int n = 0;
            _storedProcedureName = Resources.DataSources.count_emp_by_title; //"count_emp_by_title";
            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("pin_title", OracleDbType.Varchar2, job_title, ParameterDirection.Input);
                    cmd.Parameters.Add("pout_count", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    int recCreated = 0;
                    try
                    {
                        conn.Open();
                        recCreated = cmd.ExecuteNonQuery();
                        _status = true;
                        n = Int32.Parse(cmd.Parameters["pout_count"].Value.ToString()); 
                        _logger.Log($"Employees with job title {job_title}:{n} was found.", Common.LogType.Info.ToString());
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                        _logger.Log($"Exception occured: {err}", Common.LogType.Error.ToString());
                        _status = false;
                    }

                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
                _status = false;
            }
            return n;
        }
        #endregion

        #region :Update
        public bool Update(Student obj, string key)
        {
            int id = Int32.Parse(key);
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spUpdateStudent}";
            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("stud_id", OracleDbType.Int32, id, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_name", OracleDbType.Varchar2, obj.Name, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_email", OracleDbType.Varchar2, obj.Email, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_mobile", OracleDbType.Varchar2, obj.Mobile, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_gender", OracleDbType.Varchar2, obj.Gender, ParameterDirection.Input);
                    cmd.Parameters.Add("stud_dob", OracleDbType.Date, obj.DateOfBirth, ParameterDirection.Input);
                    
                    conn.Open();

                    int recUpdated = 0;
                    try
                    {
                        recUpdated = cmd.ExecuteNonQuery();
                        _status = true;
                        _logger.Log($"Record id: {key} updated.", Common.LogType.Info.ToString());
                    }
                    catch (Exception x)
                    {
                        string err = x.Message;
                        _logger.Log($"Exception occured: {err}", Common.LogType.Error.ToString());
                        _status = false;
                    }

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
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spDeleteStudent}";
            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("stud_id", OracleDbType.Int32, id, ParameterDirection.Input);
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

        #endregion
    }
}
