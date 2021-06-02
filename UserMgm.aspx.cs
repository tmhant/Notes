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
    public partial class UserMgm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        private Users users;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                    Response.Redirect("/Login");
                else
                {
                    users = ((List<Users>)Session["user"])[0];
                    SqlDataSource1.SelectCommand = users.username == "admin" ? string.Format("Select * From Users Where IsDeleted = 0") : string.Format("Select * From Users Where IsDeleted = 0 and Id={0}", users.Id);
                    gv.DataSourceID = "SqlDataSource1";
                    gv.DataBind();
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Pop.Visible = true;
            btnNew.Visible = false;
            btnSave.Visible = true;
            btnReset.Visible = true;
            btnDelete.Visible = false;
            Reset();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Pop.Visible = false;
            btnNew.Visible = true;
            btnSave.Visible = false;
            btnReset.Visible = false;
            btnDelete.Visible = false;
            users = ((List<Users>)Session["user"])[0];
            string sqlStr = "";
            if (string.IsNullOrEmpty(hd_id.Value))
            {
                sqlStr = @"Insert Into Users(username,password,firstName,lastName,CreateUser)
                                Values(@username,@password,@firstName,@lastName,@CreateUser)";
            }
            else
            {
                sqlStr = @"Update Users set username=@username, password=@password, firstName=@firstName, lastName=@lastName,
                            UpdateUser=@UpdateUser, UpdateDate=getdate() Where Id = @Id";
            }
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password.Text.Trim());

            //編成 Base64 字串
            string pwd = Convert.ToBase64String(bytes);
            Users u = new Users
            {
                Id = string.IsNullOrEmpty(hd_id.Value) ? "" : hd_id.Value,
                username = username.Text.Trim(),
                password = pwd,
                firstName = firstName.Text.Trim(),
                lastName = lastName.Text.Trim(),
                CreateUser = users.username,
                UpdateUser = users.username
            };
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var user = conn.ExecuteScalar<Users>(sqlStr, u);
                SqlDataSource1.SelectCommand = username.Text.Trim() == "admin" ? string.Format("Select * From Users Where IsDeleted = 0") : string.Format("Select * From Users Where IsDeleted = 0 and Id={0}", hd_id.Value);
                gv.DataSourceID = "SqlDataSource1";
                gv.DataBind();
            }
            Reset();
        }

        private void Reset()
        {
            username.Text = "";
            password.Text = "";
            firstName.Text = "";
            lastName.Text = "";
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Pop.Visible = false;
            btnNew.Visible = true;
            btnSave.Visible = false;
            btnReset.Visible = false;
            btnDelete.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Pop.Visible = false;
            btnNew.Visible = true;
            btnSave.Visible = false;
            btnReset.Visible = false;
            btnDelete.Visible = false;
        }

        protected void gv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Pop.Visible = true;
            btnNew.Visible = false;
            btnSave.Visible = true;
            btnReset.Visible = true;
            btnDelete.Visible = Session["user"] != null && ((List<Notes.Models.Users>)Session["user"])[0].username == "admin" ? true : false;
            int a = e.NewSelectedIndex;
            GridViewRow row = gv.Rows[e.NewSelectedIndex];
            hd_id.Value = row.Cells[0].Text.Trim();
            username.Text = row.Cells[1].Text.Trim();
            string pwd = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(row.Cells[2].Text.Trim()));
            password.Text = pwd;
            lastName.Text = row.Cells[3].Text.Trim().Replace("&nbsp;","");
            firstName.Text = row.Cells[4].Text.Trim().Replace("&nbsp;", "");
        }
    }
}