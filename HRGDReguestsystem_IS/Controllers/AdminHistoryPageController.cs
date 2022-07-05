using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRGDReguestsystem_IS.Models;
using System.Data;
using Annual_Compliance_Declaration.Repository;
using System.Data.SqlClient;
using HRGDReguestsystem_IS.Repository;

namespace HRGDReguestsystem_IS.Controllers
{
    public class AdminHistoryPageController : Controller
    {
        // GET: AR
        public ActionResult AHP(PagedList<FORMModels> model = null, string submit = "", string terdaftar = "", string EXPIRED = "01/01/1900")
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("LOGINPAGE", "LOGIN");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "HomeController";


            ViewBag.menu = Session["menu"];
            //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            //Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "AdminHistoryPageController";

            // employee Status
            model.EMPLOYEESTATUSDDL = DDL_EMPLOYEESTATUS();
            var EmployeeStatus = DDL_EMPLOYEESTATUS();
            List<SelectListItem> EmployeeStatuslistItem = new List<SelectListItem>();
            foreach (var item in EmployeeStatus)
            {
                EmployeeStatuslistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.EmployeeStatusDDL = new SelectList(EmployeeStatuslistItem, "Value", "Text");
            // end employee status

            // entity
            model.ENTITYDDL = DDL_ENTITY();
            var Entity = DDL_ENTITY();
            List<SelectListItem> EntitylistItem = new List<SelectListItem>();
            foreach (var item in Entity)
            {
                EntitylistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.EntityDDL = new SelectList(EntitylistItem, "Value", "Text");
            // end entity

            // PersonalTitleID
            model.PERSONALTITLEDDL = DDL_PERSONALTITLE();
            var PersonalTitle = DDL_PERSONALTITLE();
            List<SelectListItem> PersonalTitlelistItem = new List<SelectListItem>();
            foreach (var item in PersonalTitle)
            {
                PersonalTitlelistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.PersonalTitleDDL = new SelectList(PersonalTitlelistItem, "Value", "Text");
            // end PersonalTitleID

            // Site Address ID
            model.SITEADDDDL = DDL_SITEADD();
            var SiteAdd = DDL_SITEADD();
            List<SelectListItem> SiteAddlistItem = new List<SelectListItem>();
            foreach (var item in SiteAdd)
            {
                SiteAddlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.SiteAddDDL = new SelectList(SiteAddlistItem, "Value", "Text");
            // End Site Address ID

            // CityID
            model.CITYDDL = DDL_CITY();
            var City = DDL_CITY();
            List<SelectListItem> CitylistItem = new List<SelectListItem>();
            foreach (var item in City)
            {
                CitylistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.CityDDL = new SelectList(CitylistItem, "Value", "Text");
            // end CityID

            // ProfessionalFamilyID
            model.PROFESSIONALFAMILYDDL = DDL_PROFESSIONALFAMILY();
            var ProfessionalFamily = DDL_PROFESSIONALFAMILY();
            List<SelectListItem> ProfessionalFamilyItem = new List<SelectListItem>();
            foreach (var item in ProfessionalFamily)
            {
                ProfessionalFamilyItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.ProfessionalFamilyDDL = new SelectList(ProfessionalFamilyItem, "Value", "Text");
            // end ProfessionalFamilyID

            // TitleID
            model.TITTLEDDL = DDL_TITLE();
            var Title = DDL_TITLE();
            List<SelectListItem> TitleItem = new List<SelectListItem>();
            foreach (var item in Title)
            {
                TitleItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.Title2DDL = new SelectList(TitleItem, "Value", "Text");
            // END TitleID

            // DepartmentID
            model.DEPARTDDL = DDL_DEPART();
            var Department = DDL_DEPART();
            List<SelectListItem> DepartmentlistItem = new List<SelectListItem>();
            foreach (var item in Department)
            {
                DepartmentlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.DepartmentDDL = new SelectList(DepartmentlistItem, "Value", "Text");
            // End DepartmentID

            //model.EMPLOYEESTATUS = string.IsNullOrEmpty(model.EMPLOYEESTATUS) == true ? "" : model.EMPLOYEESTATUS;
            //model.ENTITY = string.IsNullOrEmpty(model.ENTITY) == true ? "" : model.ENTITY;
            //model.PERSONALTITLE = string.IsNullOrEmpty(model.PERSONALTITLE) == true ? "" : model.PERSONALTITLE;
            //model.COMPANY = string.IsNullOrEmpty(model.COMPANY) == true ? "" : model.COMPANY;
            //model.SITEADD = string.IsNullOrEmpty(model.SITEADD) == true ? "" : model.SITEADD;
            //model.OFFICEFLOOR = string.IsNullOrEmpty(model.OFFICEFLOOR) == true ? "" : model.OFFICEFLOOR;
            //model.CITY = string.IsNullOrEmpty(model.CITY) == true ? "" : model.CITY;
            //model.OFFICEPHONE = string.IsNullOrEmpty(model.OFFICEPHONE) == true ? "" : model.OFFICEPHONE;
            //model.FAX = string.IsNullOrEmpty(model.FAX) == true ? "" : model.FAX;
            //model.PROFESSIONALFAMILY = string.IsNullOrEmpty(model.PROFESSIONALFAMILY) == true ? "" : model.PROFESSIONALFAMILY;
            //model.TITTLE = string.IsNullOrEmpty(model.TITTLE) == true ? "" : model.TITTLE;
            //model.DEPART = string.IsNullOrEmpty(model.DEPART) == true ? "" : model.DEPART;

            if (Convert.ToInt32(Request.QueryString["idx"]) == 0)
            {

            }
            else
            {
                Session["idx"] = Convert.ToInt32(Request.QueryString["idx"]);
                //Common.ExecuteQuery("dbo.[SP_REMARKS_ADMIN]'" + Session["idx"].ToString() + "','" + model.remarksadmin + "','VIEW'");
                DataTable dt = Common.ExecuteQuery("dbo.[SP_COMMAND_GD] '" + Session["idx"].ToString() + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','SELECT'");
                if (dt.Rows.Count > 0)
                {
                    model.idx = Convert.ToInt32(dt.Rows[0]["Idx"].ToString());
                    model.TERDAFTAR = dt.Rows[0]["Terdaftar_di_YES_System"].ToString();
                    model.EMPLOYEESTATUS = dt.Rows[0]["Employee_Status"].ToString();
                    model.EMPLOYEESTATUS_FreeText = dt.Rows[0]["Remarks"].ToString();
                    model.ENTITY = dt.Rows[0]["Entity"].ToString();
                    model.UNIQUEID = dt.Rows[0]["Unique_ID"].ToString();
                    model.PERSONALTITLE = dt.Rows[0]["Personal_Title"].ToString();
                    model.FIRSTNAME = dt.Rows[0]["First_Name"].ToString();
                    model.LASTNAME = dt.Rows[0]["Last_Name"].ToString();
                    model.COMPANY = dt.Rows[0]["Company"].ToString();
                    model.SITEADD = dt.Rows[0]["Site_Address"].ToString();
                    model.OFFICEFLOOR = dt.Rows[0]["Office_Floor"].ToString();
                    model.CITY = dt.Rows[0]["City"].ToString();
                    model.OFFICEPHONE = dt.Rows[0]["Office_Phone"].ToString();
                    model.FAX = dt.Rows[0]["Fax"].ToString();
                    model.MOBILE = dt.Rows[0]["Mobile"].ToString();
                    model.EMAIL = dt.Rows[0]["Email"].ToString();
                    model.PROFESSIONALFAMILY = dt.Rows[0]["Professional_Family"].ToString();
                    model.TITTLE = dt.Rows[0]["Title"].ToString();
                    model.DEPART = dt.Rows[0]["Department"].ToString();
                    model.MANAGERNAME = dt.Rows[0]["Manager_Name"].ToString();
                    model.MANAGEREMAIL = dt.Rows[0]["Manager_Email"].ToString();
                    model.EXPIRED = (Convert.ToDateTime(dt.Rows[0]["Expired"]).ToString("dd/MM/yyyy")).ToString();
                    model.remarksadmin = (dt.Rows[0]["Remarks_Admin"].ToString());
                }
                
            }



            if (submit == "back")
            {
                //Common.ExecuteQuery("dbo.[SP_REJECT_APPROVE] 'Approve', '" + Session["idx"].ToString() + "', '" + Session["EmployeeNumber"].ToString() + "'");
                return RedirectToAction("AdminHistory", "ListGD");
                //return Content("<script type='text/javascript'>alert('Data has been Approved');window.location.href = 'http://localhost:60534/ListGD/Admin';</script>");
                //return Content("<script type='text/javascript'>alert('Data has been Approved');window.location.href = 'http://wrbmdtapp01/HRGDRequest_UAT/ListGD/Admin';</script>");
            }
            else if (submit == "UPDATE")
            {
                DateTime EXPIREDUPDATE = DateTime.ParseExact(EXPIRED, "dd/MM/yyyy", null);
                Common.ExecuteNonQuery("dbo.[SP_UPDATE_EXPIRED] '" + Session["idx"].ToString() + "', '"+ EXPIREDUPDATE + "'");
                return Content("<script type='text/javascript'>alert('Update Data Success');window.location.href = 'http://wrbmdtapp01/HRGDRequest_UAT/ListGD/AdminHistory';</script>");
            }

            return View(model);
        }
        #region DDL
        private static List<SelectListItem> DDL_EMPLOYEESTATUS()
        {
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_EMPLOYEE_STATUS] '" + model.TERDAFTAR + "'";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "-- Choose --",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["EmployeeStatus_Desc"].ToString(),
                            Value = dr["EmployeeStatus_Desc"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_ENTITY()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC  [dbo].[SP_ENTITY]";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "-- Choose --",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["ShortEntity"].ToString(),
                            Value = dr["ShortEntity"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_PERSONALTITLE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_PERSONAL_TITLE]";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "-- Choose --",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["PersonalTitle_Desc"].ToString(),
                            Value = dr["PersonalTitle_Desc"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_COMPANY(string company)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_COMPANY] '" + company + "'";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["LongEntity"].ToString(),
                            Value = dr["LongEntity"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_SITEADD()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = " EXEC [dbo].[SP_SITEADD]";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "-- Choose --",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["SiteAddressDesc"].ToString(),
                            Value = dr["SiteAddressDesc"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_OFFICEFLOOR(string floor)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = " EXEC [dbo].[SP_FLOOR] '" + floor + "'";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Floor"].ToString(),
                            Value = dr["Floor"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_CITY()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_CITY]";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    //if (dr.HasRows)
                    //{
                    //    item.Add(new SelectListItem
                    //    {
                    //        Text = "",
                    //        Value = ""
                    //    });
                    //}
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_OFFICEPHONE(string officephone)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_OFFICEPHONE] '" + officephone + "'";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Telephone_Number"].ToString(),
                            Value = dr["Telephone_Number"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_FAX(string fax)
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_FAX] '" + fax + "'";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Fax"].ToString(),
                            Value = dr["Fax"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_PROFESSIONALFAMILY()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_PROFESSIONALFAMILY] ";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "-- Choose --",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["ProfessionalFamilyDesc"].ToString(),
                            Value = dr["ProfessionalFamilyDesc"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_TITLE()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC[dbo].[SP_TITLE_POSITIONNAME] ";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "-- Choose --",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Title_Position_Name"].ToString(),
                            Value = dr["Title_Position_Name"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_DEPART()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_DEPARTMENT] ";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            item.Add(new SelectListItem
                            {
                                Text = "-- Choose --",
                                Value = ""
                            });
                        }
                        item.Add(new SelectListItem
                        {
                            Text = dr["Department"].ToString(),
                            Value = dr["Department"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        #endregion
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
    }
}