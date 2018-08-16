using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementApp.Models.Contracts;
using VehicleManagementApp.Repository.Contracts;

namespace VehicleManagementApp.Models.Models
{
    public class Employee:IModel,IDeletable
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ContactNo { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [StringLength(255)]
        public string LicenceNo { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        public int DivisionId { get; set; }
        public Division Division { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }

        public int ThanaId { get; set; }
        public Thana Thana { get; set; }


        public bool IsDeleted { get; set; }
        public bool withDeleted()
        {
            return IsDeleted;
        }
    }
}
