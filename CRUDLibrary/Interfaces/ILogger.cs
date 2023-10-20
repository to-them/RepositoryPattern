using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Call this in any desire location
        /// </summary>
        /// <param name="message">Desire message</param>
        /// <param name="log_type">Either: Info, Warning, Error, ""</param>
        void Log(string message, string log_type);
    }
}
