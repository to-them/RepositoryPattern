using CRUDLibrary.DataModels;
using CRUDLibrary.Interfaces;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Services
{
    /// <summary>
    /// Person Profile Member
    /// </summary>
    public class PersonServiceOracle : Base, IPersonService
    {
        #region :Declarations
        private readonly string _oraclePackageName = Resources.DataSources.PKG_PERSON_PROFILE; 
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

        public PersonServiceOracle()
        {
            _connString = this._connOraSQL;
            _logFolder = this._logFolderPath;
        }

        #region :CREATE
        public bool Create(Person obj)
        {
            try
            {
                _logger.Log($"RetrieveAll invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }
            throw new NotImplementedException();
        }
        #endregion

        #region :READ
        public string ListToCSVFile(List<Person> ls)
        {
            throw new NotImplementedException();
        }

        public string ListToXML(List<Person> ls)
        {
            throw new NotImplementedException();
        }

        public Person Retrieve(string key)
        {
            Int32 id = Int32.Parse(key);
            Person obj = null;
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spGetPerson}";

            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    da = new OracleDataAdapter(_storedProcedureName, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //In
                    da.SelectCommand.Parameters.Add("per_id", OracleDbType.Int32, id, ParameterDirection.Input);

                    //Out
                    da.SelectCommand.Parameters.Add(new OracleParameter("o_person", OracleDbType.RefCursor, ParameterDirection.Output));

                    //ds = new DataSet();
                    //da.Fill(ds);
                    ////Or
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            obj = new Person();
                            obj.PersonID = Convert.ToInt32(dr["PersonID"]);
                            obj.FirstName = dr["FirstName"].ToString();
                            obj.LastName = dr["LastName"].ToString();
                            obj.Phone = dr["Phone"].ToString();
                            obj.Email = dr["Email"].ToString();
                            obj.UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]);
                            obj.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
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
            throw new NotImplementedException();
            
        }

        public List<Person> RetrieveAll()
        {
            List<Person> ls = new List<Person>();
            _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spGetPersons}";

            try
            {
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    da = new OracleDataAdapter(_storedProcedureName, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //Out
                    da.SelectCommand.Parameters.Add(new OracleParameter("o_persons", OracleDbType.RefCursor, ParameterDirection.Output));

                    //ds = new DataSet();
                    //da.Fill(ds);
                    ////Or
                    dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Person obj = new Person();
                            obj.PersonID = Convert.ToInt32(dr["PersonID"]);
                            obj.FirstName = dr["FirstName"].ToString();
                            obj.LastName = dr["LastName"].ToString();
                            obj.Phone = dr["Phone"].ToString();
                            obj.Email = dr["Email"].ToString();
                            obj.UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]);
                            obj.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                            ls.Add(obj);
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

        public string RetrieveAllStr()
        {
            throw new NotImplementedException();
        }

        public string RetrieveStr(string key)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region :UPDATE
        public bool Update(Person obj, string key)
        {
            try
            {
                _logger.Log($"RetrieveAll invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }
            throw new NotImplementedException();
        }
        #endregion

        #region :DELETE
        public bool Delete(string key)
        {
            try
            {
                _logger.Log($"RetrieveAll invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }
            throw new NotImplementedException();
        }
        #endregion

    }
}
