using Dapper;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft;
using System.Web.Script.Serialization;

namespace Notes.Controller
{
    /// <summary>
    /// OrgController 的摘要描述
    /// </summary>
    public class OrgController : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            IEnumerable<Orgs> result = null;
            string sqlStr = "Select * From Org Where Id <> 0";
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                result = conn.Query<Orgs>(sqlStr);
            }
            List<string> orgs = result.Select(c => c.OrgName).ToList();

            var serializer = new JavaScriptSerializer();
            List<jsonOrg> jsonOrg = new List<jsonOrg>();
            foreach (var item in orgs)
            {
                jsonOrg json = new jsonOrg()
                {
                    text = item,
                    selectedIcon = "bi-check-lg"
                };
                jsonOrg.Add(json);
            }

            context.Response.Write(serializer.Serialize(jsonOrg));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class jsonOrg
    {
        public string text { get; set; }
        public string selectedIcon { get; set; }
    }
}