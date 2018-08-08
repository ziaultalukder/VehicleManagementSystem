using System;
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
        [DataType(DataType.Date)]
        public DateTime JourneyStart { get; set; }

        [Required]
        [Display(Name = "Journey End")]
        [DataType(DataType.Date)]
        public DateTime JouneyEnd { get; set; }


        [Display(Name = "Employee")]
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<Employee> Employees { get; set; }


        public int? CommentId { get; set; }
        public Comment Comment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }

        [Display(Name = "Status")]
        public int? RequsitionStatusId { get; set; }
        public RequsitionStatus RequsitionStatus { get; set; }
        public IEnumerable<RequsitionStatus> RequsitionStatuses { get; set; }
    }
}