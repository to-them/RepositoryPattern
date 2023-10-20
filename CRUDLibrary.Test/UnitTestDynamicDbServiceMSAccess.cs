using CRUDLibrary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestCRUDLibrary.Test
{
    [TestClass]
    public class UnitTestDynamicDbServiceMSAccess
    {
        DynamicDbServiceMSAccess _service = new DynamicDbServiceMSAccess();
        bool _result = false;

        [TestMethod]
        public void TestMethodCreateAccessDb()
        {
            try
            {
                _service.CreateAccessDb();
                _result = true;
            }
            catch (System.Exception ex)
            {
                string err = ex.Message;
                _result = false;
            }

            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodCreateTestTable()
        {           
            try
            {
                _service.CreateTestTable();
                _result = true;
            }
            catch (System.Exception ex)
            {
                string err = ex.Message;
                _result = false;
            }

            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodCreateStudentsTable()
        {
            try
            {
                _service.CreateStudentsTable();
                _result = true;
            }
            catch (System.Exception ex)
            {
                string err = ex.Message;
                _result = false;
            }

            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodCreateEmployeesTable()
        {
            try
            {
                _service.CreateEmployeesTable();
                _result = true;
            }
            catch (System.Exception ex)
            {
                string err = ex.Message;
                _result = false;
            }

            Assert.IsTrue(_result);
        }
    }
}
