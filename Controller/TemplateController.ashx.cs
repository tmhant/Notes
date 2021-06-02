using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Notes.Controller
{
    /// <summary>
    /// TemplateController 的摘要描述
    /// </summary>
    public class TemplateController : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = HttpUtility.UrlDecode(context.Request.QueryString["Id"].ToString());
            string tbody = HttpUtility.UrlDecode(context.Request.QueryString["tbody"].ToString());
            string IsDeleted = HttpUtility.UrlDecode(context.Request.QueryString["IsDeleted"].ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            int result = 0;
            if (!string.IsNullOrEmpty(id))
            {
                string sqlStr = "";
                bool del = false;
                bool.TryParse(IsDeleted, out del);
                if(del)
                    sqlStr = "Update Template set IsDeleted = 1 Where Id=@Id";
                else
                    sqlStr = "Update Template set Tbody = @Tbody Where Id=@Id";
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    result = conn.Execute(sqlStr, new { Id = id, Tbody = tbody});
                }
            }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}