using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.DataModels
{
    /// <summary>
    /// Dependent - Person Profile
    /// </summary>
    public class Dependent
    {
        public int DependentID { get; set; }
        public int PersonID { get; set; }
        public string Relationship { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Occupation { get; set; }
        public string SameAddress { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
