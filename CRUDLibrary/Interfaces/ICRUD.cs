using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Interfaces
{
    /// <summary>
    /// Generic CRUD Interface
    /// </summary>
    /// <typeparam name="T">Table Model</typeparam>
    public interface ICRUD<T>
    {
        /// <summary>
        /// Create new
        /// </summary>
        /// <param name="obj">New object to be created</param>
        /// <returns>Returns true if success</returns>
        bool Create(T obj);

        /// <summary>
        /// Get one row data - Object
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns row data if found</returns>
        T Retrieve(string key);

        /// <summary>
        /// Get one row data - String
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns row data if found</returns>
        string RetrieveStr(string key);

        /// <summary>
        /// Get all rows data
        /// </summary>
        /// <returns>Returns all found rows else will return the seeded row</returns>
        List<T> RetrieveAll();

        /// <summary>
        /// Get all rows data - String
        /// </summary>
        /// <returns>Returns all found rows else will return the seeded row</returns>
        string RetrieveAllStr();

        /// <summary>
        /// Ex: List<bel_Memo> memo = bal_Memo.getMemos();
        /// string str = ListToCSVFile(memo);
        /// </summary>
        /// <param name="ls">List object to be converted</param>
        /// <returns>Return csv string of the converted list</returns>
        string ListToCSVFile(List<T> ls);

        /// <summary>
        /// Ex: List<bel_Memo> memo = bal_Memo.getMemos();
        /// string str = ListToXML(memo);
        /// </summary>
        /// <param name="ls">List object to be converted</param>
        /// <returns>Return xml string of the converted list</returns>
        string ListToXML(List<T> ls);

        /// <summary>
        /// Edit row data by primary key
        /// </summary>
        /// <param name="obj">Object to be updated</param>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns true if success</returns>
        bool Update(T obj, string key);

        /// <summary>
        /// Delete row data by primary key
        /// </summary>
        /// <param name="key">Primary key value</param>
        /// <returns>Returns true if success</returns>
        bool Delete(string key);
    }
}
