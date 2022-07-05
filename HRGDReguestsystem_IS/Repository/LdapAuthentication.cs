using HRGDReguestsystem_IS.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web;

namespace HRGDReguestsystem_IS.Reposity
{
    public class LdapAuthentication
    {
        private String _path;
        private String _filterAttribute;

        public LdapAuthentication(String path)
        {
            _path = path;
        }

        public bool IsAuthenticated(String domain, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            try
            {	//Bind to the native AdsObject to force authentication.			
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public String GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(_path);
            search.Filter = "(cn=" + _filterAttribute + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();

                int propertyCount = result.Properties["memberOf"].Count;

                String dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (String)result.Properties["memberOf"][propertyCounter];

                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }

                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
        public String GetMail(String domain, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("mail");

            SearchResult result = search.FindOne();
            DirectoryEntry obUser = new DirectoryEntry(result.Path);

            string email = getUserProperty(obUser, "mail");

            return email;
        }
        private String getUserProperty(DirectoryEntry entry, String propName)
        {
            if (entry.Properties[propName] == null || entry.Properties[propName].Count == 0) return String.Empty;
            if (entry.Properties[propName][0] == null) return String.Empty;

            return entry.Properties[propName][0].ToString();
        }

        public string GetPropertyUser(String domain, String username, String pwd)
        {
            String domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
            string EmailEmpl = "";
            try
            { //Bind to the native AdsObject to force authentication.                    
                Object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");

                search.PropertiesToLoad.Add("company");
                search.PropertiesToLoad.Add("EMPLOYEENUMBER");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return "";
                }

                //Update the new path to the user in the directory.
                _path = result.Path;
                dynamic EmplNumber = "";
                dynamic Email = "";
                Email = result.Properties.Contains("mail");
                EmplNumber = result.Properties.Contains("EMPLOYEENUMBER");
                if (EmplNumber == false)
                {
                    DataTable dtSelectx = Common.ExecuteQuery("sp_SEL_EMPL_NUMBER'" + username + "'");
                    if (dtSelectx.Rows.Count > 0)
                    {
                        _filterAttribute = dtSelectx.Rows[0]["EmployeeID"].ToString();
                    }
                    else
                    {
                        if (Email == true)
                        {
                            EmailEmpl = (String)result.Properties["mail"][0];
                            DataTable dtSelectx2 = Common.ExecuteQuery("sp_SEL_EMPL_NUMBER_WITH_EMAIL'" + EmailEmpl + "'");
                            if (dtSelectx2.Rows.Count > 0)
                            {
                                _filterAttribute = dtSelectx2.Rows[0]["EmployeeID"].ToString();
                            }
                        }
                        else
                        {
                            DataTable dtSelectx3 = Common.ExecuteQuery("sp_SEL_EMPL_NUMBER'" + username + "'");
                            if (dtSelectx3.Rows.Count > 0)
                            {
                                EmailEmpl = dtSelectx3.Rows[0]["Email"].ToString();
                            }
                            else
                            {
                                _filterAttribute = (String)result.Properties["EMPLOYEENUMBER"][0];
                            }
                        }
                    }
                }
                else
                {
                    _filterAttribute = (String)result.Properties["EMPLOYEENUMBER"][0];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return _filterAttribute;
        }
    }
}