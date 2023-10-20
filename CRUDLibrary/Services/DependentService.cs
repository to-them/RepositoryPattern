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
    public class DependentService : Base, IDependentService
    {
        #region :Declarations
        private readonly string _oraclePackageName = Resources.DataSources.PKG_PERSON_DEPENDENT;
        private string _connString { get; set; } //Base class will locate it in the config file
        private string _logFolder { get; set; } //Base class will locate it in the config file
        private string _query { get; set; }
        private string _storedProcedureName { get; set; }
        private bool _status = false;
        public string NewRecordId { get; set; }
        private OracleDataAdapter da = null;
        private DataTable dt = null;

        ILogger _logger = new Logger();

        public DependentService()
        {
            _connString = this._connOraSQL;
            _logFolder = this._logFolderPath;
        }
        #endregion

        #region :CREATE
        public bool Create(Dependent obj)
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
        public Dependent Retrieve(string key)
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

        public List<Dependent> RetrieveAll()
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

        public string ListToCSVFile(List<Dependent> ls)
        {
            throw new NotImplementedException();
        }

        public string ListToXML(List<Dependent> ls)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region :UPDATE
        public bool Update(Dependent obj, string key)
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
