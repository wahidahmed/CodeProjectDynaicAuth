using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeProjectDynaicAuth.Models
{
    public class DefaultConnection : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<ControllerAction> ControllerAction { get; set; }
        public DbSet<RelRoleAction> RelRoleAction { get; set; }
    }
}