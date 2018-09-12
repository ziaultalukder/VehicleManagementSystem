using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.ViewModels
{
    public class RequsitionViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Form { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Journey Start")]
        public DateTime JourneyStart { get; set; }

        [Required]
        [Display(Name = "Journey End")]
        public DateTime JouneyEnd { get; set; }

        public string Status { get; set; }


        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }


        [Display(Name = "Vehicle Name")]
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }

        [Display(Name = "Vehicle Name")]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public IEnumerable<Manager> Managers { get; set; }

        public int EmployeeViewModelId { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }

        public int CommentViewModelId { get; set; }
        public CommentViewModel CommentViewModel { get; set; }
        
    }
}