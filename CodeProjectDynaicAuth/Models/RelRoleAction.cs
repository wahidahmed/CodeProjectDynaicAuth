using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeProjectDynaicAuth.Models
{
    [Table("RelRoleAction")]
    public class RelRoleAction
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? ConActionId { get; set; }
    }
}