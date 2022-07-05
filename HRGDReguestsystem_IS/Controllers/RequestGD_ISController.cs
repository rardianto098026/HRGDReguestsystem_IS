using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRGDReguestsystem_IS.Reposity;
using HRGDReguestsystem_IS.Models;
using Annual_Compliance_Declaration.Repository;

using System.Data;
using HRGDReguestsystem_IS.Repository;

namespace HRGDReguestsystem_IS.Controllers
{
    public class RequestGD_ISController : Controller
    {
        // GET: RequestGD_IS

        public ActionResult Edit(PagedList<FORMModels> model = null, string Submit = "", string terdaftar = "", string Emp_Status = "", string entity = "", string EXPIRED = "01/01/1900")
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
            Session["controller"] = "RequestGD_ISController";

            

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
            model.EMPLOYEESTATUSDDL = DDL_EMPLOYEESTATUS(terdaftar);
            var EmployeeStatus = DDL_EMPLOYEESTATUS(terdaftar);
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
            model.PROFESSIONALFAMILYDDL = DDL_PROFFAMILY();
            var ProfessionalFamily = DDL_PROFFAMILY();
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
            model.DEPARTDDL = DDL_DEPARTMENT();
            var Department = DDL_DEPARTMENT();
            List<SelectListItem> DepartmentlistItem = new List<SelectListItem>();
            foreach (var item in Department)
            {
                DepartmentlistItem.Add(new SelectListItem { Text = item.Text, Value = item.Value.ToString() });
            }
            ViewBag.DepartmentDDL = new SelectList(DepartmentlistItem, "Value", "Text");

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

            //model.TERDAFTAR = txtterdaftar;
            //model.EMPLOYEESTATUSDDL = DDL_EMPLOYEESTATUS();
            //model.ENTITYDDL = DDL_ENTITY();
            //model.PERSONALTITLEDDL = DDL_PERSONALTITLE();
            //model.COMPANYDDL = DDL_COMPANY(model.ENTITY);
            //model.SITEADDDDL = DDL_SITEADD();
            //model.OFFICEFLOORDDL = DDL_OFFICEFLOOR(model.ENTITY);
            //model.CITYDDL = DDL_CITY();
            //model.OFFICEPHONEDDL = DDL_OFFICEPHONE(model.ENTITY);
            //model.FAXDDL = DDL_FAX(model.ENTITY);
            //model.PROFESSIONALFAMILYDDL = DDL_PROFFAMILY();
            //model.TITTLEDDL = DDL_TITLE();
            //model.DEPARTDDL = DDL_DEPARTMENT(); 
            try
            {
                DateTime date = DateTime.ParseExact(EXPIRED, "dd/MM/yyyy", null);

                if (Convert.ToInt32(Request.QueryString["idx"]) == 0)
                {

                }
                else
                {
                    Session["idx"] = Convert.ToInt32(Request.QueryString["idx"]);

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
                        ViewBag.Company = model.COMPANY;
                        ViewBag.SITEADD = model.SITEADD;
                    }
                }

                



                if (Submit == "UPDATE")
                {


                    if (model.EMPLOYEESTATUS == "- Please Select -" || model.EMPLOYEESTATUS == "" || model.EMPLOYEESTATUS == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose Employee Status');</script>";
                    }
                    else if (model.ENTITY == "- Please Select -" || model.ENTITY == "" || model.ENTITY == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose Entity');</script>";
                    }
                    else if (model.UNIQUEID == "- Please Select -" || model.UNIQUEID == "" || model.UNIQUEID == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Unique ID');</script>";
                    }
                    else if (model.PERSONALTITLE == "- Please Select -" || model.PERSONALTITLE == "" || model.PERSONALTITLE == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose Personal Title');</script>";
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
                        TempData["messageRequest"] = "<script>alert('Please Choose Site Address');</script>";
                    }
                    else if (model.CITY == "- Please Select -" || model.CITY == "" || model.CITY == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose City');</script>";
                    }
                    else if (model.MOBILE == "- Please Select -" || model.MOBILE == "" || model.MOBILE == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Fill The Mobile');</script>";
                    }
                    else if (model.PROFESSIONALFAMILY == "- Please Select -" || model.PROFESSIONALFAMILY == "" || model.PROFESSIONALFAMILY == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose Professional Family');</script>";
                    }
                    else if (model.TITTLE == "- Please Select -" || model.TITTLE == "" || model.TITTLE == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose Title/Position Name');</script>";
                    }
                    else if (model.DEPART == "- Please Select -" || model.DEPART == "" || model.DEPART == null)
                    {
                        TempData["messageRequest"] = "<script>alert('Please Choose Departement');</script>";
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
                        string id = Session["Userid"].ToString();
                        string name = Session["EmployeeNumber"].ToString();
                        // Common.ExecuteNonQuery("dbo.[SP_COMMAND_GD] '','" + Session["EmployeeNumber"].ToString() + "','" + Session["UserID"].ToString() + "','" + model.TERDAFTAR + "','" + model.EMPLOYEESTATUS +" - " + model.EMPLOYEESTATUS_FreeText + "','" + model.ENTITY + "','" + model.UNIQUEID + "','" + model.PERSONALTITLE + "','" + model.FIRSTNAME + "','" + model.LASTNAME + "','" + model.COMPANY + "','" + model.SITEADD + "','" + model.OFFICEFLOOR + "','" + model.CITY + "','" + model.OFFICEPHONE + "','" + model.FAX + "','" + model.MOBILE + "','" + model.EMAIL + "','" + model.PROFESSIONALFAMILY + "','" + model.TITTLE + "','" + model.DEPART + "','" + model.MANAGERNAME + "','" + model.MANAGEREMAIL + "','" + date + "','0','','" + Session["UserID"].ToString() + "','','','INSERT'");
                        Common.ExecuteNonQuery("dbo.[SP_COMMAND_GD] '" + Session["idx"].ToString() + "','" + Session["EmployeeNumber"].ToString() + "','" + Session["UserID"].ToString() + "','" + model.TERDAFTAR + "','" + model.EMPLOYEESTATUS + "','" + model.EMPLOYEESTATUS_FreeText + "','" + model.ENTITY + "','" + model.UNIQUEID + "','" + model.PERSONALTITLE + "','" + model.FIRSTNAME + "','" + model.LASTNAME + "','" + model.COMPANY + "','" + model.SITEADD + "','" + model.OFFICEFLOOR + "','" + model.CITY + "','" + model.OFFICEPHONE + "','" + model.FAX + "','" + model.MOBILE + "','" + model.EMAIL + "','" + model.PROFESSIONALFAMILY + "','" + model.TITTLE + "','" + model.DEPART + "','" + model.MANAGERNAME + "','" + model.MANAGEREMAIL + "','" + date + "','0','','" + Session["UserID"].ToString() + "','','','" + Session["UserID"].ToString() + "','UPDATE'");
                        Common.ExecuteNonQuery("dbo.[SP_EMAILNOTIF_UPDATE] 'UPDATE','" + Session["idx"].ToString() + "','"+ Session["UserID"].ToString() + "','"+model.ENTITY+"','"+ Session["EmployeeNumber"].ToString() + "','"+model.FIRSTNAME+"','"+model.LASTNAME+"','"+ Session["UserID"].ToString() + "'");
                        //TempData["messageRequest"] = "<script>alert('Update Data Success');</script>";
                        //Response.Write("<script>alert('Save Data Success');</script>");
                        return Content("<script type='text/javascript'>alert('Update Data Success');window.location.href = 'http://wrbmdtapp01/HRGDRequest_UAT/ListGD';</script>");
                        //return Content("<script type='text/javascript'>alert('Update Data Success');window.location.href = 'http://localhost:60534/ListGD';</script>");
                        //return RedirectToAction("window.location = '" + Url.Action("Index", "ListGD") + "'");
                        
                    }
                         
                }
                else if (Submit == "CANCEL")
                {
                    return RedirectToAction("Index", "ListGD");
                }
            }
            catch (Exception e)
            {
                TempData["messageRequest"] = "<script>alert('Invalid date format. Please enter the date in the format dd/mm/yyyy');</script>";
            }

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
                    //if (dr.HasRows)
                    //{
                    //    item.Add(new SelectListItem
                    //    {
                    //        Text = "- Please Select -",
                    //        Value = ""
                    //    });
                    //}
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_EMPLOYEE_STATUS '"+ terdaftar +"'";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = con;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (terdaftar == "Yes")
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_ENTITY";
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
                    //        Text = " --Choose-- ",
                    //        Value = ""
                    //    });
                    //}
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = " exec SP_PERSONAL_TITLE";
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
                    //        Text = " --Choose-- ",
                    //        Value = ""
                    //    });
                    //}
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = " exec SP_COMPANY '" + company + "'";
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
                    //if (dr.HasRows)
                    //{
                    //    item.Add(new SelectListItem
                    //    {
                    //        Text = "- Please Select -",
                    //        Value = ""
                    //    });
                    //}
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_FLOOR'" + floor + "'";
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
                            Text = dr["FLOOR"].ToString(),
                            Value = dr["FLOOR"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_CITY()
        {
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_CITY";
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
                            Text = dr["KOTAMADYA"].ToString(),
                            Value = dr["KOTAMADYA"].ToString()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }
        private static List<SelectListItem> DDL_OFFICEPHONE(string phone)
        {
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_OFFICEPHONE'" + phone + "'";
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_FAX'" + fax + "'";
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

        private static List<SelectListItem> DDL_PROFFAMILY()
        {
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_PROFESSIONALFAMILY";
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
                            Text = " --Choose-- ",
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
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_TITLE_POSITIONNAME";
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
                            Text = " --Choose-- ",
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
        private static List<SelectListItem> DDL_DEPARTMENT()
        {
            FORMModels model = new FORMModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_DEPARTMENT";
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
                            Text = " --Choose-- ",
                            Value = ""
                        });
                    }
                    while (dr.Read())
                    {
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

        public JsonResult getentity()
        {
            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.ENTITYDDL = DDL_ENTITY();
            return Json(new SelectList(model.ENTITYDDL, "Value", "Text", JsonRequestBehavior.AllowGet));
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
        public JsonResult getphone(string phone)
        {
            PagedList<FORMModels> model = new PagedList<FORMModels>();
            model.OFFICEPHONEDDL = DDL_OFFICEPHONE(phone);
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

        public JsonResult GetOfficeFloor(string id)
        {
            //if (PolicyNo != "")
            //{

            FORMModels model = new FORMModels();
            DataTable dt = Common.ExecuteQuery("SP_Get_OnChange '" + id + "'");
            if (dt.Rows.Count > 0)
            {
                var record = dt;
                var result = new
                {
                    //OfficePhone = dt.Rows[0]["Telephone_Number"].ToString(),
                    //Fax = dt.Rows[0]["Fax"].ToString(),
                    Company = dt.Rows[0]["LongEntity"].ToString(),
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
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

        public JsonResult Get2(string entity)
        {
            FORMModels model = new FORMModels();
            DataTable dt = Common.ExecuteQuery("SP_Get_OnChange '" + entity + "'");
            if (dt.Rows.Count > 0)
            {
                var record = dt;
                var result = new
                {
                    OfficeFloor = dt.Rows[0]["Floor"].ToString(),
                    Fax = dt.Rows[0]["Fax"].ToString(),
                    OfficePhone = dt.Rows[0]["Telephone_Number"].ToString()
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            //}
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

}