using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactNo { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        public string LicenceNo { get; set; }

        
        [Display(Name = "Department")]
        [Required]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Department> Departments { get; set; }


        [Display(Name = "Designation")]
        [Required]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }
        public IEnumerable<Designation> Designations { get; set; }
    }
}