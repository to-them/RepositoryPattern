using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.DataModels
{
    /// <summary>
    /// Address - Person Profile
    /// </summary>
    public class Address
    {
        public int AddressID { get; set; }
        public int PersonID { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }
        public string PriorStreet1 { get; set; }
        public string PriorStreet2 { get; set; }
        public string PriorCity { get; set; }
        public string PriorProvince { get; set; }
        public string PriorPostal { get; set; }
        public string PriorCountry { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
