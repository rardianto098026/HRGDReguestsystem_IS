using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRGDReguestsystem_IS.Models;
using System.Data.SqlClient;
using System.Data;
using HRGDReguestsystem_IS.Reposity;
using Annual_Compliance_Declaration.Repository;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using HRGDReguestsystem_IS.Repository;
using System.Data.OleDb;
using System.Configuration;

namespace HRGDReguestsystem_IS.Controllers
{
    public class ListGDController : Controller
    {
        // GET: ListGD
        public ActionResult Index(PagedListTR<ListGDModels> model = null, string Emp_ID = "", string Name = "", string submit = "")
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "ListGDController";

            ViewBag.menu = Session["menu"];

            model.emp_name = Session["UserID"].ToString();
            model.emp_numb = Session["EmployeeNumber"].ToString();
            string asf = Session["UserID"].ToString();
            string sd = Session["EmployeeNumber"].ToString();
            model.ENTITYY = DDL_Entity();
            model.FLAGG = DDL_Flag();

            //int maxRows = 10;
            model.Emp_ID = string.IsNullOrEmpty(model.Emp_ID) == true ? "" : model.Emp_ID;
            model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
            model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
            model.Flag = string.IsNullOrEmpty(model.Flag) == true ? "" : model.Flag;

            if (submit == "Download")
            {
                ViewBag.menu = Session["menu"];
                Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
                Session["controller"] = "ListGDController";

                model.No = string.IsNullOrEmpty(model.No) == true ? "" : model.No;
                model.Emp_ID = string.IsNullOrEmpty(model.Emp_ID) == true ? "" : model.Emp_ID;
                model.emp_name = string.IsNullOrEmpty(model.emp_name) == true ? "" : model.emp_name;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.f_name = string.IsNullOrEmpty(model.f_name) == true ? "" : model.f_name;
                model.l_name = string.IsNullOrEmpty(model.l_name) == true ? "" : model.l_name;
                model.Flag = string.IsNullOrEmpty(model.Flag) == true ? "" : model.Flag;

                string query = string.Empty;

                query = "exec dbo.[SP_TRN_GD_Req] '" + model.Name + "','" + model.Entity + "','" + model.Flag + "','" + Session["EmployeeNumber"].ToString() + "','" + Session["UserID"].ToString() + "' ";

                DataTable dt = Common.ExecuteQuery(query);

                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("No");
                dtReport.Columns.Add("Employee Id");
                dtReport.Columns.Add("Employee name");
                dtReport.Columns.Add("Terdaftar di YES System");
                dtReport.Columns.Add("Employee Status");
                dtReport.Columns.Add("Remarks");
                dtReport.Columns.Add("Entity");
                dtReport.Columns.Add("Unique ID");
                dtReport.Columns.Add("Personal Title");
                dtReport.Columns.Add("First Name");
                dtReport.Columns.Add("Last Name");
                dtReport.Columns.Add("Company");
                dtReport.Columns.Add("Site Address");
                dtReport.Columns.Add("Office Floor");
                dtReport.Columns.Add("City");
                dtReport.Columns.Add("Office Phone");
                dtReport.Columns.Add("Fax");
                dtReport.Columns.Add("Mobile");
                dtReport.Columns.Add("Email");
                dtReport.Columns.Add("Professional Family");
                dtReport.Columns.Add("Title");
                dtReport.Columns.Add("Department");
                dtReport.Columns.Add("Manager Name");
                dtReport.Columns.Add("Manager Email");
                dtReport.Columns.Add("Expired");
                dtReport.Columns.Add("Status");
                dtReport.Columns.Add("Created Date");
                //dtReport.Columns.Add("Created_by");
                dtReport.Columns.Add("Acknowledge Date");
                //dtReport.Columns.Add("Acknowledge_by");
                foreach (DataRow item in dt.Rows)
                {
                    var row = dtReport.NewRow();

                    row["No"] = item["No"].ToString();
                    row["Employee Id"] = item["Employee_Id"].ToString();
                    row["Employee name"] = item["Employee_Name"].ToString();
                    row["Terdaftar di YES System"] = item["Terdaftar_di_YES_System"].ToString();
                    row["Employee Status"] = item["Employee_Status"].ToString();
                    row["Remarks"] = item["Remarks"].ToString();
                    row["Entity"] = item["Entity"].ToString();
                    row["Unique ID"] = item["Unique_ID"].ToString();
                    row["Personal Title"] = item["Personal_Title"].ToString();
                    row["First Name"] = (item["First_Name"].ToString());
                    row["Last Name"] = item["Last_Name"].ToString();
                    row["Company"] = item["Company"].ToString();
                    row["Site Address"] = item["Site_Address"].ToString();
                    row["Office Floor"] = item["Office_Floor"].ToString();
                    row["City"] = item["City"].ToString();
                    row["Office Phone"] = item["Office_Phone"].ToString();
                    row["Fax"] = item["Fax"].ToString();
                    row["Mobile"] = item["Mobile"].ToString();
                    row["Email"] = item["Email"].ToString();
                    row["Professional Family"] = item["Professional_Family"].ToString();
                    row["Title"] = item["Title"].ToString();
                    row["Department"] = item["Department"].ToString();
                    row["Manager Name"] = item["Manager_Name"].ToString();
                    row["Manager Email"] = item["Manager_Email"].ToString();
                    row["Expired"] = item["Expired"].ToString();
                    row["Status"] = item["Flag"].ToString();
                    row["Created Date"] = item["Created_Date"].ToString();
                    //row["Created_by"] = item["Employee_Name"].ToString();
                    row["Acknowledge Date"] = item["Approval_Date"].ToString();
                    //row["Acknowledge_by"] = item["Employee_Name"].ToString();

                    dtReport.Rows.Add(row);
                }

                if (dt.Rows.Count > 0)
                {
                    var grid = new GridView();
                    grid.DataSource = dtReport;
                    grid.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    string filename = "Report HRGD Request System";
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
                    Response.ContentType = "application/ms-excel";

                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    foreach (GridViewRow r in grid.Rows)
                    {
                        if ((r.RowType == DataControlRowType.DataRow))
                        {
                            for (int columnIndex = 0; (columnIndex
                                        <= (r.Cells.Count - 1)); columnIndex++)
                            {
                                r.Cells[columnIndex].Attributes.Add("class", "text");
                            }

                        }

                    }

                    grid.RenderControl(htw);
                    string style = "<style> .text { mso-number-format:\\@; } </style> ";
                    Response.Write(style);

                    Response.Write(sw.ToString());
                    Response.End();
                    ModelState.Clear();

                }
                return RedirectToAction("Index", "ListGD");
            }
                model.Content = ListCompliance(model.Emp_ID, model.Name, model.Entity, model.Flag, model.emp_numb, model.emp_name);

            return View(model);
        }

        [HttpGet]
        public ActionResult Upload()
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
            Session["controller"] = "ListGDController";

            ViewBag.menu = Session["menu"];

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string submit)
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "ListGDController";

            ViewBag.menu = Session["menu"];

            string sheetName = null;

            if (submit == "Upload")
            {
                Helper helper = new Helper();
                try
                {
                    string filePath = string.Empty;
                    if (file != null)
                    {
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        filePath = path + Path.GetFileName(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        file.SaveAs(filePath);

                        string conString = string.Empty;
                        switch (extension)
                        {
                            case ".xls": //Excel 97-03.
                                conString = System.Configuration.ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                                break;
                            case ".xlsx": //Excel 07 and above.
                                conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                                break;
                        }

                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    sheetName = "UPLOAD REQUEST GD$";
                                    //sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();
                                    //if (sheetName.ToUpper() == "nyoba$")
                                    //{
                                    //Read Data from First Sheet.
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                    //}
                                }
                            }
                        }


                        SqlConnection connX = Common.GetConnection();
                        SqlCommand cmdX = new SqlCommand("SP_INSERT_UPLOADFILEGD", connX);
                        cmdX.CommandType = CommandType.StoredProcedure;
                        cmdX.Parameters.Add("@FILENAME", SqlDbType.VarChar).Value = file.FileName;
                        cmdX.Parameters.Add("@CREATEDBY", SqlDbType.VarChar).Value = Session["UserID"].ToString();

                        SqlDataAdapter da = new SqlDataAdapter(cmdX);
                        DataTable dtX = new DataTable();
                        connX.Open();
                        da.Fill(dtX);

                        int UploadID = Convert.ToInt32(dtX.Rows[0]["UPLOADID"].ToString());
                        connX.Close();
                        if (sheetName.ToUpper() == "UPLOAD REQUEST GD$")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i]["Expired"].ToString() == "")
                                {
                                    dt.Rows[i]["Expired"] = "1900-01-01";
                                }

                                if (dt.Rows[i]["Employee_Id"].ToString() != "")
                                {
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConSql"].ConnectionString))
                                    {
                                        using (SqlCommand cmd = new SqlCommand("[SP_UPLOAD_GD]", conn))
                                        {
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@EMP_ID", dt.Rows[i]["Employee_Id"].ToString());
                                            cmd.Parameters.AddWithValue("@TERDAFTAR", dt.Rows[i]["Terdaftar_di_YES_System"].ToString());
                                            cmd.Parameters.AddWithValue("@EMP_STATUS", dt.Rows[i]["Employee_Status"].ToString());
                                            cmd.Parameters.AddWithValue("@REMARKS", dt.Rows[i]["Remarks"].ToString());
                                            cmd.Parameters.AddWithValue("@UNIQUE", dt.Rows[i]["Unique_ID"].ToString());
                                            cmd.Parameters.AddWithValue("@PERS_TITLE", (dt.Rows[i]["Personal_Title"].ToString()));
                                            cmd.Parameters.AddWithValue("@F_NAME", (dt.Rows[i]["First_Name"].ToString()));
                                            cmd.Parameters.AddWithValue("@L_NAME", dt.Rows[i]["Last_Name"].ToString());

                                            cmd.Parameters.AddWithValue("@SITE", dt.Rows[i]["Site_Address"].ToString());
                                            cmd.Parameters.AddWithValue("@FLOOR", dt.Rows[i]["Office_Floor"].ToString());
                                            cmd.Parameters.AddWithValue("@CITY", dt.Rows[i]["City"].ToString());
                                            cmd.Parameters.AddWithValue("@MOBILE", dt.Rows[i]["Mobile"].ToString());
                                            cmd.Parameters.AddWithValue("@PROF_FAM", dt.Rows[i]["Professional_Family"].ToString());
                                            cmd.Parameters.AddWithValue("@TITLE", (dt.Rows[i]["Title"].ToString()));
                                            cmd.Parameters.AddWithValue("@DEPT", dt.Rows[i]["Department"].ToString());
                                            cmd.Parameters.AddWithValue("@MANAGER", dt.Rows[i]["Manager_Name"].ToString());
                                            cmd.Parameters.AddWithValue("@EXPIRED", Common.SqlDate2(dt.Rows[i]["Expired"].ToString()));
                                            cmd.Parameters.AddWithValue("@CREATED_BY", Session["UserID"].ToString());
                                            conn.Open();
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Wrong File Name or Sheet Name";
                        }
                        if (TempData["Error"] != null)
                        {
                            TempData["Success"] = "";
                        }
                        else
                        {
                            string message = "File uploaded succesfully.";
                            TempData["Success"] = message;
                            TempData["Error"] = "";
                        }

                    }
                    else
                    {
                        string message = "File upload can't be empty.";
                        TempData["Error"] = message;
                        TempData["Success"] = "";
                    }

                    ViewBag.menu = Session["menu"];
                    return View();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    TempData["Success"] = "";
                    ViewBag.menu = Session["menu"];
                    return View();
                }
            }
            else if (submit == "Back")
            {
                return RedirectToAction("Index", "ListGD");
            }

            return View();
        }

        public static List<ListGDModels> ListCompliance(string Emp_ID, string Name, string Entity, string Flag, string empNumb, string empName)
        {
            SqlConnection conn = Common.GetConnection();
            List<ListGDModels> model = new List<ListGDModels>();
            SqlCommand cmd = new SqlCommand("[SP_TRN_GD_Req]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = Entity;
            cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = Flag;
            cmd.Parameters.Add("@Emp_Num", SqlDbType.VarChar).Value = empNumb;
            cmd.Parameters.Add("@Emp_Name", SqlDbType.VarChar).Value = empName;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ListGDModels
                {
                    no = dr["no"].ToString(),
                    idx = dr["idx"].ToString(),
                    Emp_ID = dr["Employee_Id"].ToString(),
                    Name = dr["Employee_name"].ToString(),
                    Entity = dr["Entity"].ToString(),
                    Req_For = dr["First_Name"].ToString()+ " " +dr["Last_Name"].ToString(),
                    Flag = dr["Flag"].ToString()
                });
            }

            return model;

        }
        public ActionResult Admin(PagedListTR<ListGDModels> model = null, string Emp_ID = "", string Name = "", string submit = "")
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("LOGIN", "LOGINPAGE");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "ListGDController";

            ViewBag.menu = Session["menu"];

            model.emp_name = Session["EmployeeNumber"].ToString();
            model.emp_numb = Session["UserID"].ToString();
            model.ENTITYY = DDL_Entity();

            //int maxRows = 10;
            model.Emp_ID = string.IsNullOrEmpty(model.Emp_ID) == true ? "" : model.Emp_ID;
            model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
            model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
            model.Flag = string.IsNullOrEmpty(model.Flag) == true ? "" : model.Flag;

            if (submit == "Download")
            {
                ViewBag.menu = Session["menu"];
                Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
                Session["controller"] = "ListGDController";

                model.No = string.IsNullOrEmpty(model.No) == true ? "" : model.No;
                model.Emp_ID = string.IsNullOrEmpty(model.Emp_ID) == true ? "" : model.Emp_ID;
                model.emp_name = string.IsNullOrEmpty(model.emp_name) == true ? "" : model.emp_name;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.f_name = string.IsNullOrEmpty(model.f_name) == true ? "" : model.f_name;
                model.l_name = string.IsNullOrEmpty(model.l_name) == true ? "" : model.l_name;
                model.Flag = string.IsNullOrEmpty(model.Flag) == true ? "" : model.Flag;

                string query = string.Empty;

                query = "exec dbo.[SP_TRN_GD_Req_ADMIN] '" + model.Emp_ID.ToString() + "','" + model.Name + "','" + model.Entity  + "','" + model.Flag + "','download' ";

                DataTable dt = Common.ExecuteQuery(query);

                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("No");
                dtReport.Columns.Add("Employee Id");
                dtReport.Columns.Add("Employee name");
                dtReport.Columns.Add("Terdaftar di YES System");
                dtReport.Columns.Add("Employee Status");
                dtReport.Columns.Add("Remarks");
                dtReport.Columns.Add("Entity");
                dtReport.Columns.Add("Unique ID");
                dtReport.Columns.Add("Personal Title");
                dtReport.Columns.Add("First Name");
                dtReport.Columns.Add("Last Name");
                dtReport.Columns.Add("Company");
                dtReport.Columns.Add("Site Address");
                dtReport.Columns.Add("Office Floor");
                dtReport.Columns.Add("City");
                dtReport.Columns.Add("Office Phone");
                dtReport.Columns.Add("Fax");
                dtReport.Columns.Add("Mobile");
                dtReport.Columns.Add("Email");
                dtReport.Columns.Add("Professional Family");
                dtReport.Columns.Add("Title");
                dtReport.Columns.Add("Department");
                dtReport.Columns.Add("Manager Name");
                dtReport.Columns.Add("Manager Email");
                dtReport.Columns.Add("Expired");
                dtReport.Columns.Add("Status");
                dtReport.Columns.Add("Created Date");
                dtReport.Columns.Add("Created by");
                dtReport.Columns.Add("Acknowledge Date");
                dtReport.Columns.Add("Acknowledge by");

                foreach (DataRow item in dt.Rows)
                {
                    var row = dtReport.NewRow();

                    row["No"] = item["No"].ToString();
                    row["Employee Id"] = item["Employee_Id"].ToString();
                    row["Employee name"] = item["Employee_Name"].ToString();
                    row["Terdaftar di YES System"] = item["Terdaftar_di_YES_System"].ToString();
                    row["Employee Status"] = item["Employee_Status"].ToString();
                    row["Remarks"] = item["Remarks"].ToString();
                    row["Entity"] = item["Entity"].ToString();
                    row["Unique ID"] = item["Unique_ID"].ToString();
                    row["Personal Title"] = item["Personal_Title"].ToString();
                    row["First Name"] = (item["First_Name"].ToString());
                    row["Last Name"] = item["Last_Name"].ToString();
                    row["Company"] = item["Company"].ToString();
                    row["Site Address"] = item["Site_Address"].ToString();
                    row["Office Floor"] = item["Office_Floor"].ToString();
                    row["City"] = item["City"].ToString();
                    row["Office Phone"] = item["Office_Phone"].ToString();
                    row["Fax"] = item["Fax"].ToString();
                    row["Mobile"] = item["Mobile"].ToString();
                    row["Email"] = item["Email"].ToString();
                    row["Professional Family"] = item["Professional_Family"].ToString();
                    row["Title"] = item["Title"].ToString();
                    row["Department"] = item["Department"].ToString();
                    row["Manager Name"] = item["Manager_Name"].ToString();
                    row["Manager Email"] = item["Manager_Email"].ToString();
                    row["Expired"] = item["Expired"].ToString();
                    row["Status"] = item["Flag"].ToString();
                    row["Created Date"] = item["Created_Date"].ToString();
                    row["Created by"] = item["Created_by"].ToString();
                    row["Acknowledge Date"] = item["Approval_Date"].ToString();
                    row["Acknowledge by"] = item["Approval_by"].ToString();

                    dtReport.Rows.Add(row);
                }

                if (dt.Rows.Count > 0)
                {
                    var grid = new GridView();
                    grid.DataSource = dtReport;
                    grid.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    string filename = "Report HRGD Request System Admin";
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
                    Response.ContentType = "application/ms-excel";

                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    foreach (GridViewRow r in grid.Rows)
                    {
                        if ((r.RowType == DataControlRowType.DataRow))
                        {
                            for (int columnIndex = 0; (columnIndex
                                        <= (r.Cells.Count - 1)); columnIndex++)
                            {
                                r.Cells[columnIndex].Attributes.Add("class", "text");
                            }

                        }

                    }

                    grid.RenderControl(htw);
                    string style = "<style> .text { mso-number-format:\\@; } </style> ";
                    Response.Write(style);

                    Response.Write(sw.ToString());
                    Response.End();
                    ModelState.Clear();

                }
            }

            model.Content = ListComplianceAdmin(model.Emp_ID, model.Name, model.Entity, model.Flag);
            return View(model);
        }

        public static List<ListGDModels> ListComplianceAdmin(string Emp_ID, string Name, string Entity, string Flag)
        {
            SqlConnection conn = Common.GetConnection();
            List<ListGDModels> model = new List<ListGDModels>();
            SqlCommand cmd = new SqlCommand("[SP_TRN_GD_Req_ADMIN]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Emp_ID", SqlDbType.VarChar).Value = Emp_ID;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = Entity;
            cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = Flag;
            cmd.Parameters.Add("@History", SqlDbType.VarChar).Value = "";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ListGDModels
                {
                    no = dr["no"].ToString(),
                    idx = dr["idx"].ToString(),
                    Emp_ID = dr["Employee_Id"].ToString(),
                    Name = dr["Employee_name"].ToString(),
                    Entity = dr["Entity"].ToString(),
                    Req_For = dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString(),
                    Flag = dr["Flag"].ToString()
                });
            }

            return model;

        }

        public ActionResult AdminHistory(PagedListTR<ListGDModels> model = null, string Emp_ID = "", string Name = "", string submit = "")
        {
            string url = Request.Url.OriginalString;
            Session["url"] = url;

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DataSet ds = Get_Menu();
            Session["menu"] = ds.Tables[0];
            Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
            Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
            Session["controller"] = "ListGDController";

            ViewBag.menu = Session["menu"];

            model.emp_name = Session["EmployeeNumber"].ToString();
            model.emp_numb = Session["UserID"].ToString();
            model.ENTITYY = DDL_Entity();
            model.FLAGG = DDL_Flag_AdminHistory();

            //int maxRows = 10;
            model.Emp_ID = string.IsNullOrEmpty(model.Emp_ID) == true ? "" : model.Emp_ID;
            model.Name = string.IsNullOrEmpty(model.Name) == true ? "" : model.Name;
            model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
            model.Flag = string.IsNullOrEmpty(model.Flag) == true ? "" : model.Flag;

            if (submit == "Download")
            {
                ViewBag.menu = Session["menu"];
                Session["Role"] = Common.GetRole(Session["EmployeeNumber"].ToString());
                Session["EntityLogin"] = Common.GetEmployeeDetail(Session["EmployeeNumber"].ToString(), "Entity");
                Session["controller"] = "ListViewController";

                model.No = string.IsNullOrEmpty(model.No) == true ? "" : model.No;
                model.Emp_ID = string.IsNullOrEmpty(model.Emp_ID) == true ? "" : model.Emp_ID;
                model.emp_name = string.IsNullOrEmpty(model.emp_name) == true ? "" : model.emp_name;
                model.Entity = string.IsNullOrEmpty(model.Entity) == true ? "" : model.Entity;
                model.f_name = string.IsNullOrEmpty(model.f_name) == true ? "" : model.f_name;
                model.l_name = string.IsNullOrEmpty(model.l_name) == true ? "" : model.l_name;
                model.Flag = string.IsNullOrEmpty(model.Flag) == true ? "" : model.Flag;

                string query = string.Empty;
                //query = "exec dbo.[SP_COMMAND_GD] '" + Session["idx"].ToString() + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','SELECT'";
                query = "exec dbo.[SP_TRN_GD_Req_ADMIN] '" + model.Emp_ID.ToString() + "','" + model.Name + "','" + model.Entity + "','" + model.Flag + "','HISTORY' ";

                DataTable dt = Common.ExecuteQuery(query);

                DataTable dtReport = new DataTable();

                dtReport.Columns.Add("No");
                dtReport.Columns.Add("Employee Id");
                dtReport.Columns.Add("Employee name");
                dtReport.Columns.Add("Terdaftar di YES System");
                dtReport.Columns.Add("Employee Status");
                dtReport.Columns.Add("Remarks");
                dtReport.Columns.Add("Entity");
                dtReport.Columns.Add("Unique ID");
                dtReport.Columns.Add("Personal Title");
                dtReport.Columns.Add("First Name");
                dtReport.Columns.Add("Last Name");
                dtReport.Columns.Add("Company");
                dtReport.Columns.Add("Site Address");
                dtReport.Columns.Add("Office Floor");
                dtReport.Columns.Add("City");
                dtReport.Columns.Add("Office Phone");
                dtReport.Columns.Add("Fax");
                dtReport.Columns.Add("Mobile");
                dtReport.Columns.Add("Email");
                dtReport.Columns.Add("Professional Family");
                dtReport.Columns.Add("Title");
                dtReport.Columns.Add("Department");
                dtReport.Columns.Add("Manager Name");
                dtReport.Columns.Add("Manager Email");
                dtReport.Columns.Add("Expired");
                dtReport.Columns.Add("Remarks Admin");
                dtReport.Columns.Add("Status");
                dtReport.Columns.Add("Created Date");
                dtReport.Columns.Add("Created by");
                dtReport.Columns.Add("Acknowledge Date");
                dtReport.Columns.Add("Acknowledge by");

                foreach (DataRow item in dt.Rows)
                {
                    var row = dtReport.NewRow();

                    row["No"] = item["No"].ToString();
                    row["Employee Id"] = item["Employee_Id"].ToString();
                    row["Employee name"] = item["Employee_Name"].ToString();
                    row["Terdaftar di YES System"] = item["Terdaftar_di_YES_System"].ToString();
                    row["Employee Status"] = item["Employee_Status"].ToString();
                    row["Remarks"] = item["Remarks"].ToString();
                    row["Entity"] = item["Entity"].ToString();
                    row["Unique ID"] = item["Unique_ID"].ToString();
                    row["Personal Title"] = item["Personal_Title"].ToString();
                    row["First Name"] = (item["First_Name"].ToString());
                    row["Last Name"] = item["Last_Name"].ToString();
                    row["Company"] = item["Company"].ToString();
                    row["Site Address"] = item["Site_Address"].ToString();
                    row["Office Floor"] = item["Office_Floor"].ToString();
                    row["City"] = item["City"].ToString();
                    row["Office Phone"] = item["Office_Phone"].ToString();
                    row["Fax"] = item["Fax"].ToString();
                    row["Mobile"] = item["Mobile"].ToString();
                    row["Email"] = item["Email"].ToString();
                    row["Professional Family"] = item["Professional_Family"].ToString();
                    row["Title"] = item["Title"].ToString();
                    row["Department"] = item["Department"].ToString();
                    row["Manager Name"] = item["Manager_Name"].ToString();
                    row["Manager Email"] = item["Manager_Email"].ToString();
                    row["Expired"] = item["Expired"].ToString();
                    row["Remarks Admin"] = item["Remarks_Admin"].ToString();
                    row["Status"] = item["Flag"].ToString();
                    row["Created Date"] = item["Created_Date"].ToString();
                    row["Created by"] = item["Created_by"].ToString();
                    row["Acknowledge Date"] = item["Approval_Date"].ToString();
                    row["Acknowledge by"] = item["Approval_by"].ToString();

                    dtReport.Rows.Add(row);
                }

                if (dt.Rows.Count > 0)
                {
                    var grid = new GridView();
                    grid.DataSource = dtReport;
                    grid.DataBind();

                    Response.ClearContent();
                    Response.Buffer = true;
                    string filename = "Report HRGD Request System Admin History";
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
                    Response.ContentType = "application/ms-excel";

                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);

                    foreach (GridViewRow r in grid.Rows)
                    {
                        if ((r.RowType == DataControlRowType.DataRow))
                        {
                            for (int columnIndex = 0; (columnIndex
                                        <= (r.Cells.Count - 1)); columnIndex++)
                            {
                                r.Cells[columnIndex].Attributes.Add("class", "text");
                            }

                        }

                    }

                    grid.RenderControl(htw);
                    string style = "<style> .text { mso-number-format:\\@; } </style> ";
                    Response.Write(style);

                    Response.Write(sw.ToString());
                    Response.End();
                    ModelState.Clear();

                }
            }
            model.Content = ListComplianceAdminHistory(model.Emp_ID, model.Name, model.Entity, model.Flag);
            return View(model);
        }

        public static List<ListGDModels> ListComplianceAdminHistory(string Emp_ID, string Name, string Entity, string Flag)
        {
            SqlConnection conn = Common.GetConnection();
            List<ListGDModels> model = new List<ListGDModels>();
            SqlCommand cmd = new SqlCommand("[SP_TRN_GD_Req_ADMIN]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Emp_ID", SqlDbType.VarChar).Value = Emp_ID;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar).Value = Entity;
            cmd.Parameters.Add("@Flag", SqlDbType.VarChar).Value = Flag;
            cmd.Parameters.Add("@History", SqlDbType.VarChar).Value = "History";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Open();
            da.Fill(dt);
            conn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                model.Add(new ListGDModels
                {
                    no = dr["no"].ToString(),
                    idx = dr["idx"].ToString(),
                    Emp_ID = dr["Employee_Id"].ToString(),
                    Name = dr["Employee_name"].ToString(),
                    Entity = dr["Entity"].ToString(),
                    Req_For = dr["First_Name"].ToString() + " " + dr["Last_Name"].ToString(),
                    Flag = dr["Flag"].ToString()
                });
            }

            return model;

        }

      
        public ActionResult DeleteUser(string id, ListGDModels model)
        {
            try
            {
                Common.ExecuteNonQuery("dbo.[SP_COMMAND_GD] '" + id + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','','DELETE'");
                TempData["message"] = "<script>alert('Delete succesfully');</script>";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
                return RedirectToAction("Index");
            }
        }

        //public ActionResult UpdateUser(string id = "", PagedList<FORMModels> model = null)
        //{
        //    try
        //    {
        //        DataTable dt = Common.ExecuteQuery("dbo.[SP_COMMAND_GD] '" + id + "','','','','','','','','','','','','','','','','','','','','','','','','','','','','','SELECT'");
        //        if (dt.Rows.Count > 0)
        //        {
        //            Session["Emp_Status"] = dt.Rows[0]["Employee_Status"].ToString();
        //            model.EMPLOYEESTATUS = Session["Emp_Status"].ToString();

        //            model.idx = Convert.ToInt32(dt.Rows[0]["Idx"].ToString());
                    
        //            model.ENTITY = dt.Rows[0]["Entity"].ToString();
        //            model.TERDAFTAR = dt.Rows[0]["Terdaftar_di_YES_System"].ToString();
        //        }
        //        return RedirectToAction("Edit","RequestGD_IS");
        //    }
        //    catch (Exception)
        //    {
        //        TempData["message"] = "<script>alert('Delete unsuccesfully');</script>";
        //        return RedirectToAction("Index");
        //    }
        //}


        public JsonResult get_FLAG_LIST()
        {
            PagedListTR<ListGDModels> model = new PagedListTR<ListGDModels>();
            model.FLAGG = DDL_Flag();

            return Json(new SelectList(model.FLAGG, "VALUE", "TEXT", JsonRequestBehavior.AllowGet));
        }

        

        public JsonResult get_FLAG_LIST_AdminHistory()
        {
            PagedListTR<ListGDModels> model = new PagedListTR<ListGDModels>();
            model.FLAGG = DDL_Flag_AdminHistory();

            return Json(new SelectList(model.FLAGG, "VALUE", "TEXT", JsonRequestBehavior.AllowGet));
        }
        public JsonResult get_entity_LIST()
        {
            PagedListTR<ListGDModels> model = new PagedListTR<ListGDModels>();
            model.ENTITYY = DDL_Entity();

            return Json(new SelectList(model.ENTITYY, "VALUE", "TEXT", JsonRequestBehavior.AllowGet));
        }
        #region DDL
        private static List<SelectListItem> DDL_Entity()
        {
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec SP_ENTITY";
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
                            Value = dr["ShortEntity"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

        private static List<SelectListItem> DDL_Flag()
        {
            ListGDModels model = new ListGDModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [SP_SEL_FLAG] ''";
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
                            Text = dr["Flag"].ToString(),
                            Value = dr["Id_Flag"].ToString().ToUpper()
                        });
                    }
                }
                con.Close();
            }
            return item;
        }

       

        private static List<SelectListItem> DDL_Flag_AdminHistory()
        {
            ListGDModels model = new ListGDModels();
            SqlConnection con = Common.GetConnection();
            List<SelectListItem> item = new List<SelectListItem>();
            string query = "exec [SP_SEL_FLAG] 'AdminHistory'";
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
                            Text = dr["Flag"].ToString(),
                            Value = dr["Id_Flag"].ToString().ToUpper()
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