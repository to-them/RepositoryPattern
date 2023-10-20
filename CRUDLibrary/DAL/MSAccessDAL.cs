using CRUDLibrary.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.DAL 
{
    public class MSAccessDAL : Base
    {
        private static string _connString { get; set; } //Base class will locate it in the config file
        private static string query { get; set; }
        private static DataTable dt = null;
        private static DataSet ds = null;
        public static OleDbCommand cmd = null;
        public static OleDbDataAdapter da = null;
        public static OleDbConnection conn = null;
        
        public MSAccessDAL()
        {
            _connString = this._connMSAccess;
        }

        // Get the data table containing the schema
        private DataTable getDbSchemas
        {
            get
            {
                using (conn = new OleDbConnection(_connString))
                {
                    conn.Open();
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    conn.Close();
                }

                return dt;
            }

        }

        public List<string> getDbTables
        {
            get
            {
                List<string> s = new List<string>();
                foreach (DataRow dr in getDbSchemas.Rows)
                {
                    string tbName = dr["TABLE_NAME"].ToString();
                    if (dr["TABLE_TYPE"].ToString() == "TABLE")
                    {
                        s.Add(tbName);
                        //Console.WriteLine(tbName);
                    }
                }

                return s;
            }
        }

        ///<summary>
        /// Perform: Read
        /// </summary>
        public DataTable getData(string Select_Stmt)
        {
            using (conn = new OleDbConnection(_connString))
            {
                da = new OleDbDataAdapter(Select_Stmt, conn);
                ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            return dt;
        }

        ///<summary>
        /// Perform: Read
        /// </summary>
        public DataSet getData_ds(string Select_Stmt)
        {
            using (conn = new OleDbConnection(_connString))
            {
                da = new OleDbDataAdapter(Select_Stmt, conn);
                ds = new DataSet();
                da.Fill(ds);
            }

            return ds;
        }
        /// <summary>
        /// Perform: Create, Update, Delete, and Truncate
        /// </summary>
        public bool ExecuteQuery(string query)
        {
            bool result = false;
            using (conn = new OleDbConnection(_connString))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                result = true;
            }
            conn.Close();
            return result;
        }
    }
}
