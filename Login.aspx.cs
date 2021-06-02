using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using Notes.Models;

namespace Notes
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(account.Text) || string.IsNullOrEmpty(password.Text))
                return;
            string sqlstr = @"Select * From Users WHERE username = @username and password = @password";

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password.Text.Trim());
            string pwd = Convert.ToBase64String(bytes);

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var user = conn.Query<Users>(sqlstr,
                new
                {
                    username = account.Text.Trim(),
                    password = pwd
                }); ;
                if (user != null && user.Count() > 0)
                {
                    Session["user"] = user;
                    Response.Redirect("/WorkLog");
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('帳號或密碼錯誤!')", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            account.Text = "";
            password.Text = "";
        }
    }
}