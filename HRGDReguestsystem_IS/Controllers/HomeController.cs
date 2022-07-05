using HRGDReguestsystem_IS.Models;
using HRGDReguestsystem_IS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HRGDReguestsystem_IS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexUser()
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["Userid"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "HomeController";

            ViewBag.menu = Session["menu"];
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public DataSet Get_Menu()
        {

            SqlCommand com = new SqlCommand("exec [sp_Get_Menu_Parent] '" + Session["EmployeeNumber"] + "'", Common.GetConnection());

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);


            return ds;

        }

        public DataSet Get_SubMenu(string ParentID)

        {

            SqlCommand com = new SqlCommand("exec [sp_Get_SubMenu] '" + Session["EmployeeNumber"] + "',@ParentID", Common.GetConnection());

            com.Parameters.AddWithValue("@ParentID", ParentID);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }


        public void get_Submenu(string catid)

        {

            DataSet ds = Get_SubMenu(catid);

            List<MenuViewModels.SubMenu> submenulist = new List<MenuViewModels.SubMenu>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                submenulist.Add(new MenuViewModels.SubMenu
                {

                    MenuID = dr["MenuID"].ToString(),

                    MenuName = dr["MenuName"].ToString(),

                    ActionName = dr["ActionName"].ToString(),

                    ControllerName = dr["ControllerName"].ToString()

                });

            }

            Session["submenu"] = submenulist;

        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LOGINPAGE", "LOGIN");
        }

        public ActionResult beranda()
        {
            //FormsAuthentication.SignOut();
            Session.Abandon();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("IndexUser", "Home");
        }

    }
}