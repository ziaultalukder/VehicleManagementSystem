using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.ViewModels
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string VehicleName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; } 
    }
}