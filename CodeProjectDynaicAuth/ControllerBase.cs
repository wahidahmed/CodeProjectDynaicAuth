using CodeProjectDynaicAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CodeProjectDynaicAuth
{
    public class ControllerBase : Controller
    {
        private DefaultConnection db = new DefaultConnection();
        private string ActionKey;

        //sample data for the roles of the application
        Dictionary<string, List<string>> AllRoles =
                   new Dictionary<string, List<string>>();

       
        public ControllerBase()
        {
            List<string> allId = new List<string>();
            var roleTableData = db.UserRole.ToList();
            var tableRole = roleTableData.Select(x => x.Role);
            foreach (var allRole in roleTableData)
            {
                
                string r = allRole.Role;
          
                var contAct = db.RelRoleAction.ToList().Where(x=>x.RoleId==allRole.Id);
                foreach(var i in contAct)
                {
                    var controller = db.ControllerAction.Where(x => x.Id == i.ConActionId).FirstOrDefault().Controller;
                    var action = db.ControllerAction.Where(x => x.Id == i.ConActionId).FirstOrDefault().Action;
                    string con_act = controller + "-" + action;
                    allId.Add(con_act);
                }
                AllRoles.Add(r, allId);

            }
        }
        protected void initRoles()
        {
            var roleTableData = db.UserRole.ToList();
            var tableRole = roleTableData.Select(x => x.Role);
            foreach (string allRole in tableRole)
            {
                string r = allRole;
                AllRoles.Add(r, new List<string>() {  });

            }
            //      AllRoles.Add("role1", new List<string>() { "Controller1-View",
            //"Controller1-Create", "Controller1-Edit", "Controller1-Delete" });
            //      AllRoles.Add("role2", new List<string>() { "Controller1-View", "Controller1-Create" });
            //      AllRoles.Add("role3", new List<string>() { "Controller1-View" });
        }
        //sample data for the pages that need authorization
        List<string> NeedAuthenticationActions =
          new List<string>() { "Employees-Edit", "Employees-Delete" };

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionKey = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                       "-" + filterContext.ActionDescriptor.ActionName;

            string role = Session["Roles"].ToString();//getting the current role

            if (NeedAuthenticationActions.Any(s => s.Equals(ActionKey, StringComparison.OrdinalIgnoreCase)))
            {
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    string redirectUrl = string.Format("?returnUrl={0}",
                            filterContext.HttpContext.Request.Url.PathAndQuery);
                    filterContext.HttpContext.Response.Redirect(FormsAuthentication.LoginUrl + redirectUrl, true);
                }
                else //check role
                {
                    if (!AllRoles[role].Contains(ActionKey))
                    {
                        filterContext.HttpContext.Response.Redirect("~/NoAccess", true);
                    }
                }
            }
        }
    }
}