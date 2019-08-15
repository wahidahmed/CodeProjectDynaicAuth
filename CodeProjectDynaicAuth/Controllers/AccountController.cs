using CodeProjectDynaicAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CodeProjectDynaicAuth.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account


        private DefaultConnection db = new DefaultConnection();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Membarship model, string returnUrl)
        {
            //sample data
            Dictionary<string, string> users = new Dictionary<string, string>();
            users.Add("admin", "admin-pass");

            string roles;
            bool isValid = db.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
            if (isValid)
            {
                var userid = db.User.Where(x => x.UserName == model.UserName).FirstOrDefault().Id;

                //my modifieng
                StringBuilder builer = new StringBuilder();

                var user = HttpContext.User.Identity.Name;
               
                var roleTableData = db.UserRole.ToList().Where(x => x.UserId == userid);
                var tableRole = roleTableData.Select(x => x.Role);
                foreach (string allRole in tableRole)
                {
                    string r = allRole;
                    builer.Append(",");
                    builer.Append(r);

                }
                builer.Remove(0, 1);
                roles = builer.ToString();
                //end of modifing

                Session["User"] = model.UserName;
                //roles = "admin;customer";

                // put the roles of the user in the Session            
                Session["Roles"] = roles;

                HttpContext.Items.Add("roles", roles);


                FormsAuthentication.SetAuthCookie(model.UserName, false);
                 returnUrl = Request.QueryString["ReturnUrl"] as string;

                //return RedirectToLocal(returnUrl);
                return RedirectToAction("Index", "Employees");
            }
            ModelState.AddModelError("", "invalid user");
            return View();
        }
    }
}