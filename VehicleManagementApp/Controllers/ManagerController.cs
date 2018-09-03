using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleManagementApp.BLL.Contracts;
using VehicleManagementApp.Models.Models;
using VehicleManagementApp.Repository.Contracts;
using VehicleManagementApp.ViewModels;

namespace VehicleManagementApp.Controllers
{
    public class ManagerController : Controller
    {
        private IManagerRepository managerRepository;
        private IRequsitionManager _requsitionManager;
        private IEmployeeManager _employeeManager;
        private IVehicleRepository vehicleRepository;

        public ManagerController(IRequsitionManager _requsition, IEmployeeManager employee, IManagerRepository manager,
            IVehicleRepository vehicle)
        {
            this._employeeManager = employee;
            this._requsitionManager = _requsition;
            this.managerRepository = manager;
            this.vehicleRepository = vehicle;
        }

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            Requsition requsitions = new Requsition();
            var employee = _employeeManager.GetAll();
            var requsition = _requsitionManager.GetAllByNull(requsitions.Status = null);

            List<RequsitionViewModel> requsitionViewModels = new List<RequsitionViewModel>();
            foreach (var data in requsition)
            {
                var requsitionVM = new RequsitionViewModel()
                {
                    Id = data.Id,
                    Form = data.Form,
                    To = data.To,
                    Description = data.Description,
                    JourneyStart = data.JourneyStart,
                    JouneyEnd = data.JouneyEnd,
                    Employee = employee.Where(x => x.Id == data.EmployeeId).FirstOrDefault()
                };
                requsitionViewModels.Add(requsitionVM);
            }
            return View(requsitionViewModels);
        }

        //details
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _employeeManager.GetAll();
            var requsition = _requsitionManager.GetById((int) id);

            RequsitionViewModel requsitionVM = new RequsitionViewModel()
            {
                Id = requsition.Id,
                Form = requsition.Form,
                To = requsition.To,
                Description = requsition.Description,
                JourneyStart = requsition.JourneyStart,
                JouneyEnd = requsition.JouneyEnd,
                Employee = employee.Where(x => x.Id == requsition.EmployeeId).FirstOrDefault()
            };
            requsition.Status = "Seen";
            _requsitionManager.Update(requsition);
            return View(requsitionVM);
        }

        [HttpGet]
        public ActionResult Assign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requsition requsition = _requsitionManager.GetById((int) id);
            var employee = _employeeManager.GetAll();
            var vehicle = vehicleRepository.GetAll();
            Manager manager = new Manager();

            ManagerViewModel managerVM = new ManagerViewModel();
            managerVM.Id = manager.Id;
            managerVM.RequsitionId = requsition.Id;
            //managerVM.EmployeeId = requsition.EmployeeId;

            ViewBag.VehicleId = new SelectList(vehicleRepository.GetAll(), "Id", "VehicleName", manager.VehicleId);
            ViewBag.EmployeeId = new SelectList(_employeeManager.GetAll(), "Id", "Name", manager.EmployeeId);

            managerVM.VehicleId = manager.VehicleId;
            return View(managerVM);
        }

        [HttpPost]
        public ActionResult Assign(ManagerViewModel managerViewModel)
        {
            Manager manager = new Manager();
            manager.Id = managerViewModel.Id;
            manager.RequsitionId = managerViewModel.RequsitionId;
            manager.EmployeeId = managerViewModel.EmployeeId;
            manager.VehicleId = managerViewModel.VehicleId;

            bool isSaved = managerRepository.Add(manager);
            if (isSaved)
            {
                TempData["msg"] = "Requsition Assign Successfully";
                return RedirectToAction("New");
            }
            return View();
        }

        public ActionResult AssignIndex()
        {
            var manager = managerRepository.GetAll();
            return View();
        }

        public ActionResult Complete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requsition requsition = _requsitionManager.GetById((int) id);
            if (requsition.JouneyEnd == requsition.JourneyStart)
            {
                return RedirectToAction("Complete");
            }
            return View();
        }

        public ActionResult OnProgress()
        {
            Requsition requsition = new Requsition();
            var data = _requsitionManager.GetAllBySeen(requsition.Status = "Seen");
            var employee = _employeeManager.GetAll();
            List<RequsitionViewModel> requsitionViewModels = new List<RequsitionViewModel>();
            foreach (var allRequsition in data)
            {
                var requsitionVM = new RequsitionViewModel();
                requsitionVM.Id = allRequsition.Id;
                requsitionVM.Form = allRequsition.Form;
                requsitionVM.To = allRequsition.To;
                requsitionVM.Description = allRequsition.Description;
                requsitionVM.JourneyStart = allRequsition.JourneyStart;
                requsitionVM.JouneyEnd = allRequsition.JouneyEnd;
                requsitionVM.Employee = employee.Where(x => x.Id == allRequsition.EmployeeId).FirstOrDefault();

                requsitionViewModels.Add(requsitionVM);
            }
            return View(requsitionViewModels);
        }
    
    }
}