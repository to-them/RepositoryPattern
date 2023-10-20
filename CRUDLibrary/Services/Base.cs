using CRUDLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Services
{
    /// <summary>
    /// Set SQLite connection string name to "sqliteConn" in the ConnectionString section in the client config file
    /// Set MySQL connection string name to "mysqlConn" in the ConnectionString section in the client config file 
    /// Set MSSQL connection string name to "mssqlConn" in the ConnectionString section in the client config file 
    /// Set MSAccess connection string name to "msaccessConn" in the ConnectionString section in the client config file
    /// Set MS Access Database folder path name to "msaccessDbFolderPath" in the AppSettings section in the client config file
    /// Set Oracle connection name to "oraTNSConn" in the AppSettings section in the client config file
    /// Set log folder path name to "logFolderPath" in the AppSettings section in the client config file
    /// </summary>
    
    public class Base
    {
        IEncDecUtility _serviceEncDecUtility;

        public string _connSQLite { get; set; }
        public string _connMySQL { get; set; }
        public string _connMSSQL { get; set; }
        public string _connMSAccess { get; set; }
        public string _msaccessDbFolderPath { get; set; }
        public string _connOraSQL { get; set; }
        public string _logFolderPath { get; set; }
        public Base()
        {
            _serviceEncDecUtility = new EncDecUtility();

            _connSQLite = _serviceEncDecUtility.Decrypt(ConfigurationManager.ConnectionStrings["sqliteConn"].ToString());
            _connMySQL = _serviceEncDecUtility.Decrypt(ConfigurationManager.ConnectionStrings["mysqlConn"].ToString());
            _connMSSQL = _serviceEncDecUtility.Decrypt(ConfigurationManager.ConnectionStrings["mssqlConn"].ToString());
            _connMSAccess = _serviceEncDecUtility.Decrypt(ConfigurationManager.ConnectionStrings["msaccessConn"].ToString());           
            _connOraSQL = _serviceEncDecUtility.Decrypt(ConfigurationManager.AppSettings["oraTNSConn"].ToString());
            _msaccessDbFolderPath = ConfigurationManager.AppSettings["msaccessDbFolderPath"].ToString();
            _logFolderPath = ConfigurationManager.AppSettings["logFolderPath"].ToString();
            
        }
    }

}
