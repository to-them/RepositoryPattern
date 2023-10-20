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
    public class AddressServiceOracle : Base, IAddressService
    {
        #region :Declarations
        private readonly string _oraclePackageName = Resources.DataSources.PKG_PERSON_ADDRESS;
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file
        private string _query { get; set; }
        private string _storedProcedureName { get; set; }
        private bool _status = false;
        public string NewRecordId { get; set; }
        private OracleDataAdapter da = null;
        private DataTable dt = null;
        ILogger _logger = new Logger();

        public AddressServiceOracle()
        {
            _connString = this._connOraSQL;
            _logFolder = this._logFolderPath;
        }
        #endregion

        #region :CREATE
        public bool Create(Address obj)
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
        public Address Retrieve(string key)
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

        public string RetrieveStr(string key)
        {
            throw new NotImplementedException();
        }

        public List<Address> RetrieveAll()
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

        public string RetrieveAllStr()
        {
            throw new NotImplementedException();
        }

        public string ListToCSVFile(List<Address> ls)
        {
            throw new NotImplementedException();
        }

        public string ListToXML(List<Address> ls)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region :UPDATE
        public bool Update(Address obj, string key)
        {
            int id = Int32.Parse(key);
            
            try
            {
                //Copy current address to prior address b4 update
                CopyToPriorAddress(id, GetCurrentAddress(id));

                //Do update
                _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spUpdateAddress}";
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("person_id", OracleDbType.Int32, obj.PersonID, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_id", OracleDbType.Int32, id, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_street1", OracleDbType.Varchar2, obj.Street1, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_street2", OracleDbType.Varchar2, obj.Street2, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_city", OracleDbType.Varchar2, obj.City, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_province", OracleDbType.Varchar2, obj.Province, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_postal", OracleDbType.Varchar2, obj.Postal, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_country", OracleDbType.Varchar2, obj.Country, ParameterDirection.Input);
                    cmd.Parameters.Add("adr_updatedon", OracleDbType.Date, DateTime.Now, ParameterDirection.Input);

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

                _logger.Log($"RetrieveAll invoked", Common.LogType.Info.ToString());
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

        #region :OTHER
        private void CopyToPriorAddress(int address_id, DataTable dt)
        {            
            string street1="", street2="", city="", province="", postal="", country="";
            try
            {
                _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spCopyCurrentAddress}";
                if (dt.Rows.Count > 0)
                {
                    street1 = dt.Rows[0]["STREET1"].ToString();
                    street2 = dt.Rows[0]["STREET2"].ToString();
                    city = dt.Rows[0]["CITY"].ToString();
                    province = dt.Rows[0]["PROVINCE"].ToString();
                    postal = dt.Rows[0]["POSTAL"].ToString();
                    country = dt.Rows[0]["COUNTRY"].ToString();
                }

                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    OracleCommand cmd = new OracleCommand(_storedProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("adr_id", OracleDbType.Int32, address_id, ParameterDirection.Input);
                    cmd.Parameters.Add("pr_street1", OracleDbType.Varchar2, street1, ParameterDirection.Input);
                    cmd.Parameters.Add("pr_street2", OracleDbType.Varchar2, street2, ParameterDirection.Input);
                    cmd.Parameters.Add("pr_city", OracleDbType.Varchar2, city, ParameterDirection.Input);
                    cmd.Parameters.Add("pr_province", OracleDbType.Varchar2, province, ParameterDirection.Input);
                    cmd.Parameters.Add("pr_postal", OracleDbType.Varchar2, postal, ParameterDirection.Input);
                    cmd.Parameters.Add("pr_country", OracleDbType.Varchar2, country, ParameterDirection.Input);

                    conn.Open();

                    int recUpdated = 0;
                    try
                    {
                        recUpdated = cmd.ExecuteNonQuery();
                        _status = true;
                        _logger.Log($"Record id: {address_id} updated.", Common.LogType.Info.ToString());
                    }
                    catch (Exception x)
                    {
                        string err = x.Message;
                        _logger.Log($"Exception occured: {err}", Common.LogType.Error.ToString());
                        _status = false;
                    }

                }

                _logger.Log($"CopyToPriorAddress invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }
        }

        private DataTable GetCurrentAddress(int address_id)
        {            
            try
            {
                _storedProcedureName = $"{_oraclePackageName}.{Resources.DataSources.spGetAddress}";
                using (OracleConnection conn = new OracleConnection(_connString))
                {
                    da = new OracleDataAdapter(_storedProcedureName, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //In 
                    da.SelectCommand.Parameters.Add("adr_id", OracleDbType.Int32, address_id, ParameterDirection.Input);

                    //Out
                    da.SelectCommand.Parameters.Add(new OracleParameter("o_address", OracleDbType.RefCursor, ParameterDirection.Output));

                    //ds = new DataSet();
                    //da.Fill(ds);
                    ////Or
                    dt = new DataTable();
                    da.Fill(dt);

                }
                _logger.Log($"GetCurrentAddress invoked", Common.LogType.Info.ToString());
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                dt = null;
                _logger.Log($"Exception occured: {error}", Common.LogType.Error.ToString());
            }

            return dt;
        }
        #endregion
    }
}
