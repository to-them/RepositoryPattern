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
    public class StudentsController : Controller
    {
        //While Creating an Instance of UnitOfWork, we need to specify the Actual Context Object
        private UnitOfWork<DemoDbEntities> _unitOfWork = new UnitOfWork<DemoDbEntities>();
        private GenericRepository<Student> _repo;
        public StudentsController()
        {
            //If you want to use Generic Repository with Unit of work
            this._repo = new GenericRepository<Student>(_unitOfWork);
        }

        // GET: Students
        public ActionResult Index()
        {
            var model = _repo.GetAll();
            return View(model);
        }

        // GET: Students/Details/5
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

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Student model)
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
                    return RedirectToAction("Index", "Students");
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //Log the exception and rollback the transaction
                _unitOfWork.Rollback();
            }
            return View();
        }

        // GET: Students/Edit/5
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
            //return View();
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Student model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _repo.Update(model);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                    //return RedirectToAction("Index", "Students");
                }
                else
                {
                    return View(model);
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Delete/5
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
            //return View();
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var rec = _repo.GetById(id);
            _repo.Delete(rec);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region :Without Unit of Work
        //private IGenericRepository<Student> _repo = null;
        //public StudentsController()
        //{
        //    this._repo = new GenericRepository<Student>();
        //}

        //public StudentsController(IGenericRepository<Student> repo)
        //{
        //    this._repo = repo;
        //}

        //// GET: Students
        //public ActionResult Index()
        //{
        //    var model = _repo.GetAll();
        //    return View(model);
        //}

        //// GET: Students/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student rec = _repo.GetById(id);
        //    if (rec == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rec);

        //    //return View();
        //}

        //// GET: Students/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Students/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection, Student model)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        if (ModelState.IsValid)
        //        {
        //            _repo.Insert(model);
        //            _repo.Save();
        //            return RedirectToAction("Index");
        //            //return RedirectToAction("Index", "Employees");
        //        }
        //        else
        //        {
        //            return View();
        //        }

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Students/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var rec = _repo.GetById(id);
        //    if (rec == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rec);
        //    //return View();
        //}

        //// POST: Students/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection, Student model)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here
        //        if (ModelState.IsValid)
        //        {
        //            _repo.Update(model);
        //            _repo.Save();
        //            return RedirectToAction("Index");
        //            //return RedirectToAction("Index", "Employees");
        //        }
        //        else
        //        {
        //            return View(model);
        //        }

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Students/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var rec = _repo.GetById(id);
        //    if (rec == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rec);
        //    //return View();
        //}

        //// POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    _repo.Delete(id);
        //    _repo.Save();
        //    return RedirectToAction("Index");
        //}
        #endregion

        #region :Default Actions
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        #endregion

    }
}
