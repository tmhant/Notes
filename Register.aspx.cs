using Dapper;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Notes
{
    public partial class Register : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnReset_Click(object sender, EventArgs e)
        {
            account.Text = "";
            password.Text = "";
            repassword.Text = "";
            firstName.Text = "";
            lastName.Text = "";
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password.Text.Trim());

            //編成 Base64 字串
            string pwd = Convert.ToBase64String(bytes);
            Users user = new Users()
            {
                username = account.Text.Trim(),
                password = pwd,
                firstName = firstName.Text.Trim(),
                lastName = lastName.Text.Trim()
            };
            string sqlStr = @"Insert Into Users(username,password,firstName,lastName)Values(@username,@password,@firstName,@lastName)";
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                int u = conn.Execute(sqlStr, user);
                if (u > 0)
                    Response.Redirect("/Login");
            }
        }
    }
}