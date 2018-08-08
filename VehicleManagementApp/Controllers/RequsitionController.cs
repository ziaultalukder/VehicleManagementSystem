using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementApp.BLL.Contracts;
using VehicleManagementApp.Models.Models;
using VehicleManagementApp.ViewModels;

namespace VehicleManagementApp.Controllers
{
    public class RequsitionController : Controller
    {
        // GET: Requsition
        private IRequsitionManager _requsitionManager;
        private IEmployeeManager _employeeManager;
        private IRequsitionStatusManager _requsitionStatusManager;
        private ICommentManager _commentManager;

        public RequsitionController(IRequsitionManager requsition, IEmployeeManager employee, IRequsitionStatusManager requsitionStatus, ICommentManager comment)
        {
            this._requsitionManager = requsition;
            this._employeeManager = employee;
            this._requsitionStatusManager = requsitionStatus;
            this._commentManager = comment;
        }
        public ActionResult Index()
        {
            var requsition = _requsitionManager.GetAll();
            var employee = _employeeManager.GetAll();
            var requstionStatus = _requsitionStatusManager.GetAll();

            List<RequsitionViewModel> requsitionViewList = new List<RequsitionViewModel>();
            foreach (var allRequsition in requsition)
            {
                var requsitionVM = new RequsitionViewModel();
                requsitionVM.Id = allRequsition.Id;
                requsitionVM.Form = allRequsition.Form;
                requsitionVM.To = allRequsition.To;
                requsitionVM.Description = allRequsition.Description;
                requsitionVM.JourneyStart = allRequsition.JourneyStart;
                requsitionVM.JouneyEnd = allRequsition.JouneyEnd;
                requsitionVM.Employee = employee.Where(x => x.Id == allRequsition.EmployeeId).FirstOrDefault();
                requsitionVM.RequsitionStatus =
                    requstionStatus.Where(x => x.Id == allRequsition.RequsitionStatusId).FirstOrDefault();
                requsitionViewList.Add(requsitionVM);
            }
            return View(requsitionViewList);
        }

        // GET: Requsition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Requsition requsition = _requsitionManager.GetById((int) id);
            return View();
        }

        // GET: Requsition/Create
        public ActionResult Create()
        {
            var employee = _employeeManager.GetAll();
            var requstionStatus = _requsitionStatusManager.GetAll();

            RequsitionViewModel requsitionVM = new RequsitionViewModel();
            requsitionVM.Employees = employee;
            requsitionVM.RequsitionStatuses = requstionStatus;
            return View(requsitionVM);
        }

        // POST: Requsition/Create
        [HttpPost]
        public ActionResult Create(RequsitionViewModel requsitionVM)
        {
            try
            {
                Requsition requsition = new Requsition();
                requsition.Form = requsitionVM.Form;
                requsition.To = requsitionVM.To;
                requsition.Description = requsitionVM.Description;
                requsition.JourneyStart = requsitionVM.JourneyStart;
                requsition.JouneyEnd = requsitionVM.JouneyEnd;
                requsition.EmployeeId = requsitionVM.EmployeeId;
                requsition.RequsitionStatusId = requsitionVM.RequsitionStatusId;
                requsition.CommentId = requsitionVM.CommentId;

                bool isSaved = _requsitionManager.Add(requsition);
                if (isSaved)
                {
                    TempData["msg"] = "Requsition Send Successfully";
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requsition/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Requsition requsition = _requsitionManager.GetById((int) id);
            RequsitionViewModel requsitionView = new RequsitionViewModel();
            requsitionView.Id = requsition.Id;
            requsitionView.Form = requsition.Form;
            requsitionView.To = requsition.To;
            requsitionView.Description = requsition.Description;
            requsitionView.JourneyStart = requsition.JourneyStart;
            requsitionView.JouneyEnd = requsition.JouneyEnd;
            return View(requsitionView);
        }

        // POST: Requsition/Edit/5
        [HttpPost]
        public ActionResult Edit(RequsitionViewModel requsitionVM)
        {
            try
            {
                Requsition requsition = new Requsition();
                requsition.Id = requsitionVM.Id;
                requsition.Form = requsitionVM.Form;
                requsition.To = requsitionVM.To;
                requsition.JourneyStart = requsitionVM.JourneyStart;
                requsition.JouneyEnd = requsitionVM.JouneyEnd;
                requsition.EmployeeId = requsitionVM.EmployeeId;
                requsition.RequsitionStatusId = requsitionVM.RequsitionStatusId;
                requsition.CommentId = requsitionVM.CommentId;
                _requsitionManager.Add(requsition);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Requsition/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Requsition requsition = _requsitionManager.GetById((int) id);
            bool isRemove = _requsitionManager.Remove(requsition);
            if (isRemove)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Requsition/Delete/5
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
    }
}
