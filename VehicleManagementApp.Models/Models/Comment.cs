using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementApp.Models.Contracts;
using VehicleManagementApp.Repository.Contracts;

namespace VehicleManagementApp.Models.Models
{
    public class Comment:IModel,IDeletable
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public int? CommntId { get; set; }
        public Comment Commnt { get; set; }

        public int RequsitionId { get; set; }
        public Requsition Requsition { get; set; }

        public bool IsDeleted { get; set; }
        public bool withDeleted()
        {
            return IsDeleted;
        }
    }
}
