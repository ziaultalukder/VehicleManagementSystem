using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagementApp.Models.Models;

namespace VehicleManagementApp.Repository.DatabaseContext
{
    public class VehicleDatabaseContext:DbContext
    {
        public DbSet<Organaization> Organaizations { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<User> Users { get; set; }
        public DbSet<Requsition> Requsitions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RequsitionStatus> RequsitionStatuses { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Thana> Thanas { get; set; }
    }
}
