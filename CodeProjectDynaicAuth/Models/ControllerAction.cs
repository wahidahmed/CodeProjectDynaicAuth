using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeProjectDynaicAuth.Models
{
    [Table("ControllerAction")]
    public class ControllerAction
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}