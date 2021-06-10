using Dapper;
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
    /// CounterController 的摘要描述
    /// </summary>
    public class CounterController : IHttpHandler
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string orgName = HttpUtility.UrlDecode(context.Request.QueryString["orgName"].ToString());
            IEnumerable<MonthModel> result = null;
            string sqlStr = "";
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (string.IsNullOrEmpty(orgName) || orgName == "全部")
                {
                    sqlStr = "Select * From view_worklogcount";
                    result = conn.Query<MonthModel>(sqlStr);
                    context.Response.Write(serializer.Serialize(result.Select(c => c.count)));
                }
                else
                {
                    List<int> lst = new List<int>();
                    for (int i = 1; i < 13; i++)
                    {
                        sqlStr = string.Format("Select SUBSTRING(CreateDate,6,2) as 月份, count From view_worklog Where CreateDate='{0}' and OrgName='{1}'", DateTime.Now.Year + "-" +i.ToString().PadLeft(2, '0'), orgName);
                        result = conn.Query<MonthModel>(sqlStr);
                        lst.Add(result.Select(c => c.count).FirstOrDefault());
                    }
                    context.Response.Write(serializer.Serialize(lst));
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class MonthModel
    {
        public string 月份 { get; set; }
        public int count { get; set; }
    }
}