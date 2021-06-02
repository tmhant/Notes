using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace Notes.Controller
{
    /// <summary>
    /// UserController 的摘要描述
    /// </summary>
    public class UserController : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string id = HttpUtility.UrlDecode(context.Request.QueryString["Id"].ToString());
            string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            int result = 0;
            if (!string.IsNullOrEmpty(id))
            {
                string sqlStr = "Update Users set IsDeleted = 1 Where Id=@Id";
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    result = conn.Execute(sqlStr, new { id = id });
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