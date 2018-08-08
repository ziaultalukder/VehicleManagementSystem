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
    public class EmployeeController : Controller
    {
        // GET: Employee
        private IEmployeeManager _employeeManager;
        private IDepartmentManager _departmentManager;
        private IDesignationManager _designationManager;

        public EmployeeController(IEmployeeManager employee, IDepartmentManager department, IDesignationManager designation)
        {
            this._employeeManager = employee;
            this._departmentManager = department;
            this._designationManager = designation;
        }

        public ActionResult Index()
        {
            var department = _departmentManager.GetAll();
            var designation = _designationManager.GetAll();
            var employee = _employeeManager.GetAll();

            List<EmployeeViewModel> employeeViewList = new List<EmployeeViewModel>();
            foreach (var emploeedata in employee)
            {
                var employeeVM = new EmployeeViewModel();
                employeeVM.Id = emploeedata.Id;
                employeeVM.Name = emploeedata.Name;
                employeeVM.ContactNo = emploeedata.ContactNo;
                employeeVM.Email = emploeedata.Email;
                employeeVM.Address = emploeedata.Address;
                employeeVM.LicenceNo = emploeedata.LicenceNo;
                employeeVM.Department = department.Where(x => x.Id == emploeedata.DepartmentId).FirstOrDefault();
                employeeVM.Designation = designation.Where(x => x.Id == emploeedata.DesignationId).FirstOrDefault();

                employeeViewList.Add(employeeVM);
            }
            return View(employeeViewList);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var department = _departmentManager.GetAll();
            var designation = _designationManager.GetAll();
            EmployeeViewModel employeeVM = new EmployeeViewModel();
            employeeVM.Departments = department;
            employeeVM.Designations = designation;
            return View(employeeVM);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeVM)
        {
            try
            {
                Employee employee = new Employee();
                employee.Name = employeeVM.Name;
                employee.ContactNo = employeeVM.Address;
                employee.Email = employeeVM.Email;
                employee.Address = employeeVM.Address;
                employee.LicenceNo = employeeVM.LicenceNo;
                employee.DepartmentId = employeeVM.DepartmentId;
                employee.DesignationId = employeeVM.DesignationId;

                bool isSaved = _employeeManager.Add(employee);
                if (isSaved)
                {
                    TempData["msg"] = "Employee Save Successfully";
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Employee employee = _employeeManager.GetById((int)id);

            EmployeeViewModel employeeVM = new EmployeeViewModel();
            employeeVM.Id = employee.Id;
            employeeVM.Name = employee.Name;
            employeeVM.ContactNo = employee.ContactNo;
            employeeVM.Email = employee.Email;
            employeeVM.Address = employee.Address;
            employeeVM.LicenceNo = employee.LicenceNo;
            employeeVM.DepartmentId = employee.DepartmentId;
            employeeVM.DesignationId = employee.DesignationId;

            ViewBag.DepartmentId = new SelectList(_departmentManager.GetAll(),"Id","Name", employee.DepartmentId);
            ViewBag.DesignationId = new SelectList(_designationManager.GetAll(),"Id","Name", employee.DesignationId);
            return View(employeeVM);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employeeVM)
        {
            try
            {
                Employee employee = new Employee();
                employee.Id = employeeVM.Id;
                employee.Name = employeeVM.Name;
                employee.ContactNo = employeeVM.ContactNo;
                employee.Email = employeeVM.Email;
                employee.Address = employeeVM.Address;
                employee.LicenceNo = employeeVM.LicenceNo;
                employee.DepartmentId = employeeVM.DepartmentId;
                employee.DesignationId = employeeVM.DesignationId;
                _employeeManager.Update(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Employee employee = _employeeManager.GetById((int)id);
            _employeeManager.Remove(employee);
            return View();
        }

        // POST: Employee/Delete/5
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
