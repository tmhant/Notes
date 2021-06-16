using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace Notes.WebService
{
    /// <summary>
    ///WebService 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public string[] GetCompletionList(string prefixText, int count)
        {
            //連線字串
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

            ArrayList array = new ArrayList();//儲存撈出來的字串集合

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                DataSet ds = new DataSet();
                
                string selectStr = @"SELECT Top (" + count + ") Name FROM WorkLog Where Name Like '" + prefixText + "%' Group by Name Order by Name ASC";
                SqlDataAdapter da = new SqlDataAdapter(selectStr, conn);
                conn.Open();
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    array.Add(dr["Name"].ToString());
                }

            }

            return (string[])array.ToArray(typeof(string));

        }

        public string[] GetOrgList(string prefixText, int count)
        {
            //連線字串
            string connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();

            ArrayList array = new ArrayList();//儲存撈出來的字串集合

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                DataSet ds = new DataSet();

                string selectStr = @"SELECT Top (" + count + ") OrgName FROM Org Where OrgName Like '" + prefixText + "%' Group by Name Order by Name ASC";
                SqlDataAdapter da = new SqlDataAdapter(selectStr, conn);
                conn.Open();
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    array.Add(dr["Name"].ToString());
                }

            }

            return (string[])array.ToArray(typeof(string));
        }
    }
}
