using RepositoryPatternGenericEFUnitOfWorkMVC.DAL;
using RepositoryPatternGenericEFUnitOfWorkMVC.GenericRepository;
using RepositoryPatternGenericEFUnitOfWorkMVC.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPatternGenericEFUnitOfWorkMVC.Controllers
{
    public class EmployeesController : Controller
    {
        //While Creating an Instance of UnitOfWork, we need to specify the Actual Context Object
        private UnitOfWork<DemoDbEntities> _unitOfWork = new UnitOfWork<DemoDbEntities>();
        private GenericRepository<Employee> _repo;
        //private IEmployeeRepository employeeRepository;

        public EmployeesController()
        {
            //If you want to use Generic Repository with Unit of work
            _repo = new GenericRepository<Employee>(_unitOfWork);
            
            //If you want to use a Specific Repository with Unit of work
            //employeeRepository = new EmployeeRepository(unitOfWork);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = _repo.GetAll();
            return View(model);
        }
        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rec = _repo.GetById(id);
            if (rec == null)
            {
                return HttpNotFound();
            }
            return View(rec);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee model)
        {
            try
            {
                //First, Begin the Transaction
                _unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    //Do the Database Operation
                    _repo.Insert(model);
                    //Call the Save Method to call the Context Class Save Changes Method
                    _unitOfWork.Save();
                    //Do Some Other Tasks with the Database
                    //If everything is working then commit the transaction else rollback the transaction
                    _unitOfWork.Commit();
                    return RedirectToAction("Index", "Employees");
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //Log the exception and rollback the transaction
                _unitOfWork.Rollback();
            }
            return View();

            //Without Unit of Work
            //if (ModelState.IsValid)
            //{
            //    _repo.Insert(model);
            //    _repo.Save();
            //    return RedirectToAction("Index", "Employees");
            //}
            //return View();
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rec = _repo.GetById(id);
            if (rec == null)
            {
                return HttpNotFound();
            }
            return View(rec);

            //Employee model = repository.GetById(EmployeeId);
            //return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(model);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Employees");
            }
            else
            {
                return View(model);
            }

            //Without Unit of Work
            //if (ModelState.IsValid)
            //{
            //    _repo.Update(model);
            //    _repo.Save();
            //    return RedirectToAction("Index", "Employees");
            //}
            //else
            //{
            //    return View(model);
            //}
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rec = _repo.GetById(id);
            if (rec == null)
            {
                return HttpNotFound();
            }
            return View(rec);
        }
        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rec = _repo.GetById(id);
            _repo.Delete(rec);
            _unitOfWork.Save();
            return RedirectToAction("Index", "Employees");

            //Without Unit of Work
            //_repo.Delete(id);
            //_repo.Save();
            //return RedirectToAction("Index", "Employees");
        }

        #region :Without Unit of Work
        //private IGenericRepository<Employee> _repo = null;
        //public EmployeesController()
        //{
        //    this._repo = new GenericRepository<Employee>();
        //}

        //public EmployeesController(IGenericRepository<Employee> repo)
        //{
        //    this._repo = repo;
        //}
        #endregion

        /*
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
