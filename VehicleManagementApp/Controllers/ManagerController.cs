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
        private IManagerManager managerManager;
        private IRequsitionManager _requsitionManager;
        private IEmployeeManager _employeeManager;
        private IVehicleManager vehicleManager;

        public ManagerController(IRequsitionManager _requsition, IEmployeeManager employee, IManagerManager manager,
            IVehicleManager vehicle)
        {
            this._employeeManager = employee;
            this._requsitionManager = _requsition;
            this.managerManager = manager;
            this.vehicleManager = vehicle;
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

        public ActionResult RequsitionAssign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var employee = _employeeManager.GetAll();
            var requsition = _requsitionManager.GetById((int)id);

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
            requsition.Status = "Assign";
            bool assign = _requsitionManager.Update(requsition);
            if (assign)
            {
                return RedirectToAction("Assign");
            }
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
            Manager manager = new Manager();

            ManagerViewModel managerVM = new ManagerViewModel();
            managerVM.Id = manager.Id;
            managerVM.RequsitionId = requsition.Id;
            managerVM.DriverNo = manager.DriverNo;

            var employees = _employeeManager.Get(c => c.IsDriver == true && c.IsDeleted == false);

            ViewBag.VehicleId = new SelectList(vehicleManager.GetAll(), "Id", "VehicleName", manager.VehicleId);
            ViewBag.EmployeeId = new SelectList(employees, "Id", "Name", manager.EmployeeId);

            ViewBag.DriverNo = new SelectListItem[] { new SelectListItem() { Value = "", Text = "Select..." } };

            managerVM.VehicleId = manager.VehicleId;
            return View(managerVM);
        }

        [HttpPost]
        public ActionResult Assign(ManagerViewModel managerViewModel)
        {
            Requsition requsition = new Requsition();
            Manager manager = new Manager();
            manager.Id = managerViewModel.Id;
            manager.DriverNo = managerViewModel.DriverNo;
            manager.RequsitionId = managerViewModel.RequsitionId;
            manager.EmployeeId = managerViewModel.EmployeeId;
            manager.VehicleId = managerViewModel.VehicleId;

            bool isSaved = managerManager.Add(manager);

            RequsitionAssign(managerViewModel.Id);

            if (isSaved)
            {
                TempData["msg"] = "Requsition Assign Successfully";
                return RedirectToAction("New");
            }
            
            return View();
        }

        public ActionResult AssignIndex()
        {
            Manager manager = new Manager();
            var employee = _employeeManager.GetAll();
            var vehicle = vehicleManager.GetAll();
            var managers = managerManager.GetAll();
            var requsition = _requsitionManager.GetAll();

            List<ManagerViewModel> managerViewModels = new List<ManagerViewModel>();
            foreach (var allData in managers)
            {
                var managerVM = new ManagerViewModel();
                managerVM.Employee = employee.Where(c => c.Id == allData.EmployeeId).FirstOrDefault();
                managerVM.Vehicle = vehicle.Where(c => c.Id == allData.VehicleId).FirstOrDefault();
                managerVM.Employee = employee.Where(c => c.Id == allData.EmployeeId).FirstOrDefault();
                managerVM.Requsition = requsition.Where(c => c.Id == allData.RequsitionId).FirstOrDefault();
                managerViewModels.Add(managerVM);
            }

            return View(managerViewModels);
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