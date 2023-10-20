using CRUDLibrary.DataModels;
using CRUDLibrary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestCRUDLibrary.Test
{
    [TestClass]
    public class UnitTestStudentServiceOracle
    {
        StudentServiceOracle _service = new StudentServiceOracle();
        bool _result = false;
        private readonly int _readId = 10;
        private readonly int _updateId = 20;
        private readonly int _deleteId = 123;

        #region :Create
        //Using query script
        [TestMethod]
        public void TestMethodCreate()
        {
            Student obj = new Student()
            {
                Name = Utilities.GenerateTempPwd(12),
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
        public void TestMethodRetrieveAll2()
        {
            List<Student> ls = _service.RetrieveAll2();
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

        [TestMethod]
        public void TestMethodRetrieve2()
        {
            var rec = _service.Retrieve2(_readId.ToString());
            if (rec != null)
            {
                _result = true;
            }

            Assert.IsTrue(_result);
        }

        //
        [TestMethod]
        public void TestMethodgetEmployeeTitleCount()
        {
            int n = _service.getEmployeeTitleCount("Programmer");
            if (n > 0)
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
