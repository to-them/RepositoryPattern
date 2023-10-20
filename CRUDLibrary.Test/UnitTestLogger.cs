using CRUDLibrary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTestCRUDLibrary.Test
{
    [TestClass]
    public class UnitTestLogger
    {
        Logger _service = new Logger();
        bool _result = false;
        private string _logFile = @"C:\Temp\CrudAppLog\Test\Log_2022-12.txt";

        [TestMethod]
        public void TestMethodDefaultLog()
        {
            _service.Log("Unit test default log", "");
            if (File.Exists(_logFile))
                _result = true;

            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodInfoLog()
        {
            _service.Log("Unit test info log", Utilities.LogType.Info.ToString());
            if (File.Exists(_logFile))
                _result = true;

            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodErrorLog()
        {
            _service.Log("Unit test error log", Utilities.LogType.Error.ToString());
            if (File.Exists(_logFile))
                _result = true;

            Assert.IsTrue(_result);
        }

        [TestMethod]
        public void TestMethodWarningLog()
        {
            _service.Log("Unit test warning log", Utilities.LogType.Warning.ToString());
            if (File.Exists(_logFile))
                _result = true;

            Assert.IsTrue(_result);
        }
    }
}
