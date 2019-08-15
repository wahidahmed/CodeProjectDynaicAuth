using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeProjectDynaicAuth.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
    }
}