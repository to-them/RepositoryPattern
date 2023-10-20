using CRUDLibrary.DataModels;
using CRUDLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDConsoleClient.Models
{
    public class StudentModels
    {
        //This MSSQL tested good - 12.16.22
        //StudentServiceMSSQL _service = new StudentServiceMSSQL();

        //This Oracle tested good - 12.17.22
        StudentServiceOracle _service = new StudentServiceOracle();

        #region :Create
        public void CreateRecord()
        {
            Student obj = new Student()
            {
                Name = Utilities.GenerateTempPwd(8),
                Email = $"new{Utilities.GenerateTempPwd(4)}@email.com",
                Mobile = "720-777-9898",
                Gender = "Male-N",
                //DateOfBirth = DateTime.Now
                DateOfBirth = Convert.ToDateTime("3/20/2000")
            };

            bool status = _service.Create(obj);
            if (status)
                Console.WriteLine($"New record id:{_service.NewRecordId} added");
            else
                Console.WriteLine("Unable to add record!");
        }
        #endregion

        #region :Read
        public void ReadRecords()
        {
            List<Student> ls = _service.RetrieveAll();
            if(ls.Count > 0)
            {
                foreach (Student t in ls)
                {
                    Console.WriteLine($"{t.Id}, {t.Name}, {t.Email}, {t.Mobile}, {t.Gender}, {t.DateOfBirth} \n");
                    //Console.WriteLine("ID:{0} Name:{1} Gender:{2} Dob:{3}", t.Id, t.Name, t.Gender, t.DateOfBirth);
                    //Thread.Sleep(2000);
                }
            }
            else
            {
                Console.WriteLine("No record was returned!");
            }
            
        }

        public void ReadRecord(int id)
        {
            Student rec = _service.Retrieve(id.ToString());
            if (rec != null)
            {
                Console.WriteLine($"{rec.Id}, {rec.Name}, {rec.Email}, {rec.Mobile}, {rec.Gender}, {rec.DateOfBirth} \n");
            }
            else
            {
                Console.WriteLine(" No data was returned!");
            }
        }
        public void ReadRecord2(int id)
        {
            var rec = _service.RetrieveAll().FirstOrDefault(x => x.Id == id);
            if (rec != null)
            {
                Console.WriteLine($"{rec.Id}, {rec.Name}, {rec.Email}, {rec.Mobile}, {rec.Gender}, {rec.DateOfBirth} \n");
            }
            else
            {
                Console.WriteLine(" No data was returned!");
            }
        }
        #endregion

        #region :Update
        public void UpdateRecord(int id)
        {
            //var rec = _service.RetrieveAll().FirstOrDefault(x => x.Id == id);
            var rec = _service.Retrieve(id.ToString());
            if (rec != null)
            {
                Student obj = new Student()
                {
                    Name = Utilities.GenerateTempPwd(9),
                    Email = $"edit{Utilities.GenerateTempPwd(4)}@email.com",
                    Mobile = "777-777-7777",
                    Gender = "EditG",
                    DateOfBirth = Convert.ToDateTime("3/20/1979"),
                    Id = id
                };

                bool status = _service.Update(obj, id.ToString());
                if (status)
                    Console.WriteLine($"Record id:{id} updated");
                else
                    Console.WriteLine("Unable to update record!");
            }

        }
        #endregion

        #region :Delete
        public void DeleteRecord(int id)
        {
            //var rec = _service.RetrieveAll().FirstOrDefault(x => x.Id == id);
            var rec = _service.Retrieve(id.ToString());
            if (rec != null)
            {
                bool status = _service.Delete(id.ToString());
                if (status)
                    Console.WriteLine($"Record id:{id} deleted");
                else
                    Console.WriteLine("Unable to delete record!");
            }

        }
        #endregion
    }
}
