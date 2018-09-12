using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleManagementApp.BLL.Contracts;
using VehicleManagementApp.Models.Models;
using VehicleManagementApp.Repository.Migrations;
using VehicleManagementApp.ViewModels;
using Requsition = VehicleManagementApp.Models.Models.Requsition;

namespace VehicleManagementApp.Controllers
{
    public class RequsitionController : Controller
    {
        // GET: Requsition
        private IRequsitionManager _requsitionManager;
        private IEmployeeManager _employeeManager;
        private IRequsitionStatusManager _requsitionStatusManager;
        private IManagerManager _managerManager;
        private IVehicleManager vehicleManager;

        private ICommentManager commentManager;

        public RequsitionController(IRequsitionManager requsition, IEmployeeManager employee,
            IRequsitionStatusManager requsitionStatus, IManagerManager manager, IVehicleManager vehicle,
            ICommentManager comment)
        {
            this._requsitionManager = requsition;
            this._employeeManager = employee;
            this._requsitionStatusManager = requsitionStatus;
            this._managerManager = manager;
            this.vehicleManager = vehicle;
            this.commentManager = comment;
        }
        
        public ActionResult Index()
        {

            GetRequsitionComplete();

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
                requsitionVM.Status = allRequsition.Status;
                requsitionViewList.Add(requsitionVM);
            }
            return View(requsitionViewList);
        }

        private void GetRequsitionComplete()
        {
            var requsition = _requsitionManager.GetAll();
            foreach (var allRequest in requsition)
            {
                var today = DateTime.Now;
                if (allRequest.JouneyEnd < today)
                {
                    allRequest.Status = "Complete";
                    _requsitionManager.Update(allRequest);
                }
            }
        }

        // GET: Requsition/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Requsition requsition = _requsitionManager.GetById((int)id);
            var employee = _employeeManager.Get(c => c.IsDriver == true && c.IsDeleted == false);
            var manager = _managerManager.GetAll();

            RequsitionViewModel requsitionViewModel = new RequsitionViewModel();
            requsitionViewModel.Id = requsition.Id;
            requsitionViewModel.Form = requsition.Form;
            requsitionViewModel.To = requsition.To;
            requsitionViewModel.Description = requsition.Description;
            requsitionViewModel.Employee = employee.Where(c => c.Id == requsition.EmployeeId).FirstOrDefault();
            requsitionViewModel.Manager = manager.Where(c => c.RequsitionId == requsition.Id).FirstOrDefault();

            if (requsition == null)
            {
                return HttpNotFound();
            }
            return View(requsitionViewModel);
        }


        // GET: Requsition/Create
        public ActionResult Create()
        {
            var employees = _employeeManager.Get(c => c.IsDriver == false && c.IsDeleted == false);
            RequsitionViewModel requsitionVM = new RequsitionViewModel();
            requsitionVM.Employees = employees;
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
            requsitionView.EmployeeId = requsition.EmployeeId;

            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "Name", requsition.EmployeeId);

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
                requsition.Description = requsitionVM.Description;
                requsition.JourneyStart = requsitionVM.JourneyStart;
                requsition.JouneyEnd = requsitionVM.JouneyEnd;
                requsition.EmployeeId = requsitionVM.EmployeeId;

                _requsitionManager.Update(requsition);

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

        public ActionResult CreateComment(RequsitionViewModel requsitionViewModel)
        {
            Comment comment = new Comment();
            comment.RequsitionId = requsitionViewModel.Id;
            comment.Comments = requsitionViewModel.CommentViewModel.Comments;
            bool isSaved = commentManager.Add(comment);
            if (isSaved)
            {
                return RedirectToAction("Details");
            }
            return View();
        }
    }
}
