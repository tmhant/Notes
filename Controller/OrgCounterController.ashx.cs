using Dapper;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Notes.Controller
{
    /// <summary>
    /// OrgCounterController 的摘要描述
    /// </summary>
    public class OrgCounterController : IHttpHandler
    {

        string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            IEnumerable<Orgs> orgModel = null;
            IEnumerable<MonthModel> result = null;
            string sqlStr = "";
            List<OrgModel> lst = new List<OrgModel>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlStr = "select * from Org where Id <> 0";
                orgModel = conn.Query<Orgs>(sqlStr).ToList();
                foreach (var item in orgModel)
                {
                    sqlStr = string.Format("Select OrgName, sum(count) as count From view_worklog Where SUBSTRING(CreateDate, 0, 5)='{0}' and OrgName='{1}' group by OrgName", DateTime.Now.Year, item.OrgName);
                    result = conn.Query<MonthModel>(sqlStr);
                    lst.Add(new OrgModel()
                    {
                        OrgName = item.OrgName,
                        count = result.Select(c => c.count).FirstOrDefault()
                    });
                }
            }
            context.Response.Write(serializer.Serialize(lst));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class OrgModel
    {
        public string OrgName { get; set; }
        public int count { get; set; }
    }
}