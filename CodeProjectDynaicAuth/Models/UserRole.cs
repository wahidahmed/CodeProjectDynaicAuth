using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeProjectDynaicAuth.Models
{
    [Table("UserRole")]
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}