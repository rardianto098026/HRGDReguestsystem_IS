using Annual_Compliance_Declaration.Repository;
using HRGDReguestsystem_IS.Models;
using HRGDReguestsystem_IS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRGDReguestsystem_IS.Controllers
{
    public class FORMController : Controller
    {
        // GET: FORM
        public ActionResult FORMREQUEST(PagedList<FORMModels> model = null, string submit = "", string terdaftar = "", string EXPIRED = "01/01/1900", string TERDAFTAR = "", string entity = "")
        {
            
            //string free = Session["Free"].ToString();
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("LOGINPAGE", "LOGIN");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["Userid"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "HomeController";


            ViewBag.menu = Session["menu"];
            //Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            //Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "FORMController";

            //model.OFFICEFLOORDDL = DDL_OFFICEFLOOR(ViewBag.Entity);

            // terdaftar
            model.TERDAFTARDDL = DDL_TERDAFTAR();
            var Terdaftar = DDL_TERDAFTAR();
            List<SelectListItem> TerdaftarlistItem = new List<SelectListItem>();
            foreach (var item in Terdaftar)
            {
                TerdaftarlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.TerdaftarDDL = new SelectList(TerdaftarlistItem, "Value", "Text");
            // end terdaftar

            // employee Status
            model.EMPLOYEESTATUSDDL = DDL_EMPLOYEESTATUS(TERDAFTAR);
            var EmployeeStatus = DDL_EMPLOYEESTATUS(TERDAFTAR);
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

            // OfficeFloor
            model.OFFICEFLOORDDL = DDL_OFFICEFLOOR(entity);
            var OfficeFloor = DDL_OFFICEFLOOR(entity);
            List<SelectListItem> OfficeFloorlistItem = new List<SelectListItem>();
            foreach (var item in OfficeFloor)
            {
                OfficeFloorlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.OFFICEFLOORDDL = new SelectList(OfficeFloorlistItem, "Value", "Text");
            // end OfficeFloorID

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
            //model.TITTLEDDL = DDL_TITLE();
            //var Title = DDL_TITLE();
            //List<SelectListItem> TitleItem = new List<SelectListItem>();
            //foreach (var item in Title)
            //{
            //    TitleItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            //}
            //ViewBag.Title2DDL = new SelectList(TitleItem, "Value", "Text");
            // END TitleID

            // DepartmentID
            //model.DEPARTDDL = DDL_DEPART();
            //var Department = DDL_DEPART();
            //List<SelectListItem> DepartmentlistItem = new List<SelectListItem>();
            //foreach (var item in Department)
            //{
            //    DepartmentlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            //}
            //ViewBag.DepartmentDDL = new SelectList(DepartmentlistItem, "Value", "Text");
            // End DepartmentID

            // MANAGERNAME
            model.MANAGERNAMEDDL = DDL_MANAGERNAME();
            var managername = DDL_MANAGERNAME();
            List<SelectListItem> managernamelistItem = new List<SelectListItem>();
            foreach (var item in managername)
            {
                managernamelistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.managernameDDL = new SelectList(managernamelistItem, "Value", "Text");
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
            //model.DOSEN = string.IsNullOrEmpty(model.DOSEN) == true ? "" : model.DOSEN;


            //model.EMPLOYEESTATUSDDL = DDL_EMPLOYEESTATUS();
            //model.ENTITYDDL = DDL_ENTITY();
            //model.PERSONALTITLEDDL = DDL_PERSONALTITLE();
            //model.COMPANYDDL = DDL_COMPANY(model.ENTITY);
            //model.SITEADDDDL = DDL_SITEADD();
            //model.OFFICEFLOORDDL = DDL_OFFICEFLOOR(model.ENTITY);
            //model.CITYDDL = DDL_CITY();
            //model.OFFICEPHONEDDL = DDL_OFFICEPHONE(model.ENTITY);
            //model.FAXDDL = DDL_FAX(model.ENTITY);
            //model.PROFESSIONALFAMILYDDL = DDL_PROFESSIONALFAMILY();
            //model.TITTLEDDL = DDL_TITLE();
            //model.DEPARTDDL = DDL_DEPART();
            //model.TERDAFTAR = terdaftar;
            //DateTime exp = Convert.ToDateTime(EXPIRED);
            //DateTime date = DateTime.ParseExact(EXPIRED, "dd/MM/yyyy", null);
            try
            {
                DateTime date = DateTime.ParseExact(EXPIRED, "dd/MM/yyyy", null);
                
                if (submit == "INSERT")
                {
                    if (model.TERDAFTAR == "- Please Select -" || model.TERDAFTAR == "" || model.TERDAFTAR == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Terdaftar di System YES');</script>";
                    }
                    else if (model.EMPLOYEESTATUS == "- Please Select -" || model.EMPLOYEESTATUS == "" || model.EMPLOYEESTATUS == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Employee Status');</script>";
                    }
                    else if (model.ENTITY == "- Please Select -" || model.ENTITY == "" || model.ENTITY == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Entity');</script>";
                    }
                    else if (model.UNIQUEID == "- Please Select -" || model.UNIQUEID == "" || model.UNIQUEID == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Unique ID');</script>";
                    }
                    else if (model.PERSONALTITLE == "- Please Select -" || model.PERSONALTITLE == "" || model.PERSONALTITLE == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Personal Title');</script>";
                    }
                    else if (model.FIRSTNAME == "- Please Select -" || model.FIRSTNAME == "" || model.FIRSTNAME == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The First Name');</script>";
                    }
                    else if (model.LASTNAME == "- Please Select -" || model.LASTNAME == "" || model.LASTNAME == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Last Name');</script>";
                    }
                    else if (model.SITEADD == "- Please Select -" || model.SITEADD == "" || model.SITEADD == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Site Address');</script>";
                    }
                    //else if (model.SITEADD == "AXA Tower - Jakarta" && model.OFFICEFLOOR == "" || model.OFFICEFLOOR == null)
                    //{
                    //    TempData["messageRequest"] = "<script>alert('Please Fill The Office Floor');</script>";
                    //}
                    else if (model.CITY == "- Please Select -" || model.CITY == "" || model.CITY == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The City');</script>";
                    }
                    else if (model.MOBILE == "- Please Select -" || model.MOBILE == "" || model.MOBILE == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Mobile');</script>";
                    }
                    else if (model.PROFESSIONALFAMILY == "- Please Select -" || model.PROFESSIONALFAMILY == "" || model.PROFESSIONALFAMILY == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Professional Family');</script>";
                    }
                    else if (model.TITTLE == "- Please Select -" || model.TITTLE == "" || model.TITTLE == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Title/Position Name');</script>";
                    }
                    else if (model.DEPART == "- Please Select -" || model.DEPART == "" || model.DEPART == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Departement');</script>";
                    }
                    else if (model.MANAGERNAME == "- Please Select -" || model.MANAGERNAME == "" || model.MANAGERNAME == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Manager Name');</script>";
                    }
                    else if (model.MANAGEREMAIL == "- Please Select -" || model.MANAGEREMAIL == "" || model.MANAGEREMAIL == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Manager E-mail');</script>";
                    }
                    else if (date == null || date <= (DateTime.Now.AddDays(+30)))
                    {
                        string exp2 = Convert.ToString(model.EXPIRED);
                        TempData["messageRequest"] = "<script>alert('Please Check the End Date. Minimum Contract is 1 Month.');</script>";
                    }
                    else
                    {
                        Common.ExecuteNonQuery("dbo.[SP_COMMAND_GD] '','" + Session["EmployeeNumber"].ToString() + "','" + Session["UserID"].ToString() + "','" + model.TERDAFTAR + "','" + model.EMPLOYEESTATUS + "','" + model.EMPLOYEESTATUS_FreeText + "','" + model.ENTITY + "','" + model.UNIQUEID + "','" + model.PERSONALTITLE + "','" + model.FIRSTNAME + "','" + model.LASTNAME + "','" + model.COMPANY + "','" + model.SITEADD + "','" + model.OFFICEFLOOR + "','" + model.CITY + "','" + model.OFFICEPHONE + "','" + model.FAX + "','" + model.MOBILE + "','" + model.EMAIL + "','" + model.PROFESSIONALFAMILY + "','" + model.TITTLE + "','" + model.DEPART + "','" + model.MANAGERNAME + "','" + model.MANAGEREMAIL + "','" + date + "','0','','" + Session["UserID"].ToString() + "','','','','INSERT'");
                        //Common.ExecuteNonQuery("dbo.[SP_EMAIL_REQUEST] 'submit'");
                        //TempData["messageRequest"] = "<script>alert('Save Data Success');</script>";
                        //Response.Write("<script>alert('Save Data Success');</script>");
                        //return RedirectToAction("FORMREQUEST", "FORM");
                        //ModelState.Clear();
                        //return Content("<script type='text/javascript'>alert('Save Data Success');window.location.href = 'http://wrbmdtapp01/HRGDRequest_UAT/Home/IndexUser';</script>");
                        return Content("<script type='text/javascript'>alert('Save Data Success');window.location.href = 'http://wrbmdtapp01/HRGDRequest_UAT/FORM/FORMREQUEST';</script>");
                        // return Content("<script type='text/javascript'>alert('Save Data Success');window.location.href = 'http://localhost:60534/Home/IndexUser';</script>");
                        //return Content("<script type='text/javascript'>alert('Save Data Success');window.location.href = 'http://localhost:60534/FORM/FORMREQUEST';</script>");
                    }



                }
                //else if (submit == "next")
                //{
                //    return RedirectToAction("Index", "ListGD");
                //}
            }
            catch (Exception e)
            {
                TempData["messageRequest"] = "<script>alert('Invalid date format. Please enter the date in the format dd/mm/yyyy');</script>";
               
            }


            //ModelState.Clear();
            return View(model);
        }

        #region DDL

        private static List<SelectListItem> DDL_TERDAFTAR()
        {
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_TERDAFTAR";
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
                            Text = "- Please Select -",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
                        item.Add(new SelectListItem
                        {
                            Text = dr["Terdaftar_di_YES_System"].ToString(),
                            Value = dr["Terdaftar_di_YES_System"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_EMPLOYEESTATUS(string terdaftar)
        {
            PagedList<FORMModels> model = new PagedList<FORMModels>();
            
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_EMPLOYEE_STATUS] '"+ terdaftar + "'";

            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if(terdaftar == "Yes")
                    {

                    }
                    else
                    {
                        if (dr.HasRows)
                        {
                            item.Add(new SelectListItem
                            {
                                Text = "- Please Select -",
                                Value = ""
                            });
                        }
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
                            Text = "- Please Select -",
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
                            Text = "- Please Select -",
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
                            Text = "- Please Select -",
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
                    if (dr.HasRows)
                    {
                        item.Add(new SelectListItem
                        {
                            Text = "- Please Select -",
                            Value = ""
                        });
                    }
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
                            Text = "- Please Select -",
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
        //private static List<SelectListItem> DDL_TITLE()
        //{
        //    SqlConnection con = Common.GetConnection();
        //    List<SelectListItem> item = new List<SelectListItem>();
        //    string query = "EXEC[dbo].[SP_TITLE_POSITIONNAME] ";

        //    using (SqlCommand cmd = new SqlCommand(query))
        //    {
        //        cmd.Connection = con;
        //        con.Open();

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            if (dr.HasRows)
        //            {
        //                item.Add(new SelectListItem
        //                {
        //                    Text = "- Please Select -",
        //                    Value = ""
        //                });
        //            }
        //            while (dr.Read())
        //            {
        //                item.Add(new SelectListItem
        //                {
        //                    Text = dr["Title_Position_Name"].ToString(),
        //                    Value = dr["Title_Position_Name"].ToString()
        //                });
        //            }
        //        }
        //        con.Close();
        //    }
        //    return item;
        //}
        //private static List<SelectListItem> DDL_DEPART()
        //{
        //    SqlConnection con = Common.GetConnection();
        //    List<SelectListItem> item = new List<SelectListItem>();
        //    string query = "EXEC [dbo].[SP_DEPARTMENT] ";

        //    using (SqlCommand cmd = new SqlCommand(query))
        //    {
        //        cmd.Connection = con;
        //        con.Open();

        //        using (SqlDataReader dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                if (dr.HasRows)
        //                {
        //                    item.Add(new SelectListItem
        //                    {
        //                        Text = "- Please Select -",
        //                        Value = ""
        //                    });
        //                }
        //                item.Add(new SelectListItem
        //                {
        //                    Text = dr["Department"].ToString(),
        //                    Value = dr["Department"].ToString()
        //                });
        //            }
        //        }
        //        con.Close();
        //    }
        //    return item;
        //}

        private static List<SelectListItem> DDL_MANAGERNAME()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "EXEC [dbo].[SP_MANAGER] ";

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
                            //item.Add(new SelectListItem
                            //{
                            //    Text = "",
                            //    Value = ""
                            //});
                        }
                        item.Add(new SelectListItem
                        {
                            Text = dr["EmployeeName_Manager"].ToString(),
                            Value = dr["EmployeeName_Manager"].ToString()
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
        public JsonResult getcompany(string company)
        {

            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.COMPANYDDL = DDL_COMPANY(company);
            return Json(new SelectList(model.COMPANYDDL, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult getfloor(string officefloor)
        {

            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.OFFICEFLOORDDL = DDL_OFFICEFLOOR(officefloor);
            return Json(new SelectList(model.OFFICEFLOORDDL, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult getofficephone(string officephone)
        {

            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.OFFICEPHONEDDL = DDL_OFFICEPHONE(officephone);
            return Json(new SelectList(model.OFFICEPHONEDDL, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult getfax(string fax)
        {

            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.FAXDDL = DDL_FAX(fax);
            return Json(new SelectList(model.FAXDDL, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult get_yesno(string terdaftar)
        {
            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.EMPLOYEESTATUSDDL = DDL_EMPLOYEESTATUS(terdaftar);
            return Json(new SelectList(model.EMPLOYEESTATUSDDL, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        public JsonResult Get2(string entity)
        {
            FORMModels model = new FORMModels();
            DataTable dt = Common.ExecuteQuery("SP_Get_OnChange '" + entity + "'");
            if (dt.Rows.Count > 0)
            {
                var record = dt;
                var result = new
                {
                    Fax = dt.Rows[0]["Fax"].ToString(),
                    OfficePhone = dt.Rows[0]["Telephone_Number"].ToString()
                };

                
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //}
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetOfficeFloor(string id)
        {
            //if (PolicyNo != "")
            //{

            FORMModels model = new FORMModels();
            DataTable dt = Common.ExecuteQuery("SP_Get_OnChange '" + id + "'");
            //DataTable dt2 = Common.ExecuteQuery("[SP_FLOOR] '"+ id +"' ");
            if (dt.Rows.Count > 0)
            {
                var record = dt;
                var result = new
                {
                    //OfficeFloor = dt.Rows[0]["Floor"].ToString(),
                    //OfficePhone = dt.Rows[0]["Telephone_Number"].ToString(),
                    //Fax = dt.Rows[0]["Fax"].ToString(),
                    Company = dt.Rows[0]["LongEntity"].ToString(),
                    
            };
                
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //if (dt2.Rows.Count > 0)
            //{
            //    var record = dt;
            //    var result = new
            //    {
            //        //SiteAdd = "- Please Select -"
            //    };

            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}
        
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getmanageremail(string id)
        {
            //if (PolicyNo != "")
            //{

            FORMModels model = new FORMModels();
            DataTable dt = Common.ExecuteQuery("SP_MANAGEREMAIL '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                var record = dt;
                var result = new
                {
                   
                    manageremail = dt.Rows[0]["Email_Manager"].ToString(),
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //}
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult showEmp_Status()
        {
            Session["Free"] = "Free";
            string free = Session["Free"].ToString();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}