using CRUDLibrary.DataModels;
using CRUDLibrary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestCRUDLibrary.Test
{
    [TestClass]
    public class UnitTestStudentServiceMSSQL
    {
        StudentServiceMSSQL _service = new StudentServiceMSSQL();
        bool _result = false;
        private readonly int _readId = 120;
        private readonly int _updateId = 123;
        //private readonly int _deleteId = 113;

        #region :Create
        [TestMethod]
        public void TestMethodCreate()
        {
            Student obj = new Student()
            {
                Name = Utilities.GenerateTempPwd(8),
                Email = $"Create{Utilities.GenerateTempPwd(4)}@unittest.com",
                Mobile = "201-898-9898",
                Gender = "Male",
                DateOfBirth = Convert.ToDateTime("3/20/2003")
            };

            _result = _service.Create(obj);
            Assert.IsTrue(_result);
        }
        #endregion

        #region :Read
        [TestMethod]
        public void TestMethodRetrieveAll()
        {
            List<Student> ls = _service.RetrieveAll();
            if (ls.Count > 0)
            {
                _result = true;
            }
        
            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodRetrieve()
        {
            var rec = _service.Retrieve(_readId.ToString());
            if (rec != null)
            {
                _result = true;
            }

            Assert.IsTrue(_result);
        }
        #endregion

        #region :Update
        [TestMethod]
        public void TestMethodUpdate()
        {
            var rec = _service.Retrieve(_updateId.ToString());
            if(rec != null)
            {
                Student obj = new Student()
                {
                    Name = Utilities.GenerateTempPwd(8),
                    Email = $"Update{Utilities.GenerateTempPwd(4)}@unittest.com",
                    Mobile = "201-898-5545",
                    Gender = "Female",
                    DateOfBirth = Convert.ToDateTime("3/20/2011"),
                    Id = _updateId
                };

                _result = _service.Update(obj, _updateId.ToString());
            }
            
            Assert.IsTrue(_result);
        }
        #endregion

        #region :Delete
        //[TestMethod]
        //public void TestMethodDelete()
        //{
        //    var rec = _service.Retrieve(_deleteId.ToString());
        //    if (rec != null)
        //    {
        //        _result = _service.Delete(_deleteId.ToString());
        //    }

        //    Assert.IsTrue(_result);
        //}
        #endregion
    }
}
