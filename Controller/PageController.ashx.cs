using Notes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Notes.Controller
{
    /// <summary>
    /// PageController 的摘要描述
    /// </summary>
    public class PageController : IHttpHandler, IReadOnlySessionState
    {
        SqlDatabase db = new SqlDatabase();
        private int pageSize = 10;//一頁顯示的資料
        private int pageNumber = 1;//頁碼
        private string sortName = "";//排序欄位
        private string sortOrder = "1";//asc or desc
        private string searchText = "";//關鍵字
        Users users = null;
        public void ProcessRequest(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Request["pageSize"]))
                pageSize = Convert.ToInt32(context.Request["pageSize"]);

            if (!string.IsNullOrEmpty(context.Request["pageNumber"]))
                pageNumber = Convert.ToInt32(context.Request["pageNumber"]);

            if (!string.IsNullOrEmpty(context.Request["sortName"]))
                sortName = (context.Request["sortName"]);

            if (!string.IsNullOrEmpty(context.Request["sortOrder"]))
            {
                sortOrder = (context.Request["sortOrder"]);
                if (sortOrder == "asc")
                    sortOrder = "1";
                else if (sortOrder == "desc")
                    sortOrder = "0";
            }
            string strWhere = "";
            try
            {
                users = ((List<Users>)context.Session["user"])[0];
                strWhere += string.Format("IsDeleted = 0 and UserId = {0}", users.Id);
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.Message); 
            }

            if (!string.IsNullOrEmpty(context.Request["searchText"]))
            {
                searchText = (context.Request["searchText"]);
                strWhere += string.Format(" (Tbody like '%{0}%')", searchText);
            }

            string json = LoadTableByPagination("template", "*", pageNumber, pageSize, Convert.ToInt32(sortOrder), strWhere, "Id");
            context.Response.Write(json);
            context.Response.End();
        }

        /// <summary>
        /// 呼叫分頁儲存過程，返回table的json資料
        /// </summary>
        /// <param name="table">資料庫表名</param>
        /// <param name="fields">查詢的欄位</param>
        /// <param name="pageNumber">當前頁碼</param>
        /// <param name="pageSize">一頁顯示的資料</param>
        /// <param name="orderSort">0降序，1升序</param>
        /// <param name="strWhere">查詢條件</param>
        /// <param name="orderName">排序欄位</param>
        /// <returns></returns>
        string LoadTableByPagination(string table, string fields, int pageNumber, int pageSize, int orderSort, string strWhere, string orderName)
        {
            string json = "";
            SqlParameter[] parameters = {new SqlParameter("@TABLE",SqlDbType.VarChar,50),
                new SqlParameter("@FEILDS",SqlDbType.VarChar,1000),
                new SqlParameter("@PAGE_INDEX",SqlDbType.Int,4),
                new SqlParameter("@PAGE_SIZE",SqlDbType.Int,4),
                 new SqlParameter("@ORDERTYPE",SqlDbType.Bit),
                  new SqlParameter("@ANDWHERE",SqlDbType.VarChar,1000),
                  new SqlParameter("@ORDERFEILD",SqlDbType.VarChar,100)
            };
            parameters[0].Value = table;//表格
            parameters[1].Value = fields;//欄位
            parameters[2].Value = pageNumber;
            parameters[3].Value = pageSize;
            parameters[4].Value = orderSort;//0是降序，1是升序
            parameters[5].Value = strWhere;//條件
            parameters[6].Value = orderName;//排序欄位
            DataSet dataSet = db.RunProcDataSet("pagination", parameters);
            if (dataSet != null)
            {
                DataTable dt = dataSet.Tables[0];
                //後臺返回的json資料必須包含total，和rows屬性，否則前臺沒資料
                if (dataSet.Tables[1] != null && dataSet.Tables[1].Rows.Count > 0)
                    json += "{\"total\":" + dataSet.Tables[1].Rows[0]["total"] + ",";
                if (dt != null & dt.Rows.Count > 0)
                    json += "\"rows\":" + DataTableToJson(dt) + "}";
            }
            return json;
        }

        #region Json&DataTable
        /// <summary> 
        /// DataTable轉為json 
        /// </summary> 
        /// <param name="dt">DataTable</param> 
        /// <returns>json資料</returns> 
        public static string DataTableToJson(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc]);
                }
                list.Add(result);
            }
            int recursionLimit = 100;
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            serialize.RecursionLimit = recursionLimit;
            serialize.MaxJsonLength = Int32.MaxValue;
            return serialize.Serialize(list);
        }
        #endregion Json&DataTable

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}