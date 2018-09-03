using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementApp.Models.Contracts;
using VehicleManagementApp.Repository.Contracts;

namespace VehicleManagementApp.Models.Models
{
    public class Manager:IDeletable,IModel
    {
        public int Id { get; set; }
        public int RequsitionId { get; set; }
        public Requsition Requsition { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool IsDeleted { get; set; }
        public bool withDeleted()
        {
            return IsDeleted;
        }
    }
}
