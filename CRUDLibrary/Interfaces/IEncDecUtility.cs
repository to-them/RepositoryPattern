using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDLibrary.Interfaces
{
    public interface IEncDecUtility
    {
        string Encrypt(string str);
        string Decrypt(string str);
    }
}
