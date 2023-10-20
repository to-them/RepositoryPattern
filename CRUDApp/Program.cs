using CRUDConsoleClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-=-=-=CRUD Async=-=-=-=-");
            StudentModels logic = new StudentModels();
            logic.ReadRecords();
            Console.WriteLine("-----------------------");
            logic.ReadRecord(8);
            logic.ReadRecord2(8);
            Console.WriteLine("-----------------------");
            logic.CreateRecord();
            logic.UpdateRecord(4);
            Console.WriteLine("-----------------------");
            //logic.DeleteRecord(5);
            //Console.WriteLine("-----------------------");
            logic.ReadRecords();

            Console.Write("\n Any key to Exit:");
            Console.ReadKey();
        }
    }
}
