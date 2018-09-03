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
        [Display(Name = "Vehicle Name")]
        public string VehicleName { get; set; }
        [Required]
        [Display(Name = "Vehicle Model")]
        public string VModel { get; set; }
        [Required]
        [Display(Name = "Vehicle Registration No")]
        public string VRegistrationNo { get; set; }
        [Required]
        [Display(Name = "Vehicle Chesis No")]
        public string VChesisNo { get; set; }
        [Required]
        [Display(Name = "Vehicle Capacity")]
        public string VCapacity { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Vehicle Type")]
        public int VehicleTypeId { get; set; }
        public VehicleType VehicleType { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; } 
    }
}