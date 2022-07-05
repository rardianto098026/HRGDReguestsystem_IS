using Annual_Compliance_Declaration.Repository;
using HRGDReguestsystem_IS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRGDReguestsystem_IS.Reposity;
using HRGDReguestsystem_IS.Repository;

namespace HRGDReguestsystem_IS.Controllers
{

    public class LOGINController : Controller
    {
        [HttpGet]
        public ActionResult LOGINPAGE()
        {
            LOGINGDModels model = new LOGINGDModels();
            return View(model);
        }

      

        [HttpPost]
        // GET: LOGIN
        //public ActionResult LOGINPAGE()
        //{
        //    LOGINGDModels model = new LOGINGDModels();
        //    return View(model);
        //}
        public ActionResult LOGINPAGE(LOGINGDModels model, string Submit, string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                String adPath = ConfigurationManager.AppSettings["LDAPPath"].ToString();
                String domain = ConfigurationManager.AppSettings["LDAPDomain"].ToString();
                LdapAuthentication adAuth = new LdapAuthentication(adPath);
                String LocalHostaddress = HttpContext.Request.UserHostAddress;
                String Ip_Local = LocalHostaddress.Replace(".", "").Replace("::", "").Trim();

                if (true == adAuth.IsAuthenticated(domain, model.username, model.password))
                {
                    Session["EmployeeNumber"] = adAuth.GetPropertyUser(domain, model.username, model.password);

                    string asf = Session["EmployeeNumber"].ToString();
                    DataTable dtCheck = Common.ExecuteQuery("SP_LOGIN_LDAP'" + Session["EmployeeNumber"].ToString() + "'");
                    if (dtCheck.Rows.Count > 0)
                    {
                        Session["Free"] = "a";
                        Session["UserID"] = dtCheck.Rows[0]["EmployeeName"].ToString();
                        string nama = Session["UserID"].ToString();
                        return RedirectToAction("IndexUser", "Home");
                        //}
                        //else
                        //{
                        //    Response.Write("<script>alert('Invalid Username or Password');</script>");
                        //}
                    }

                    else
                    {
                        Response.Write("<script>alert('Invalid Username or Password');</script>");
                    }
                }

            }

            catch (Exception ex)
            {
                Response.Write("<script>alert('Invalid Username or Password');</script>");
                //throw new Exception("Error authenticating user. " + ex.Message);
            }
            return View(model);
        }
    }
}