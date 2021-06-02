using Dapper;
using Notes.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Notes
{
    public partial class WorkLogMgm : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        private Users users;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["user"] == null)
                        Response.Redirect("/Login");
                    else
                    {
                        users = ((List<Users>)Session["user"])[0];
                        SqlDataSource1.SelectCommand = string.Format("Select * From WorkLog Where UserId = {0} and CreateDate = convert(varchar, getdate(), 111)", users.Id);
                        gv.DataSourceID = "SqlDataSource1";
                        gv.DataBind();
                    }
                }
                catch
                {
                    Response.Redirect("/Login");
                }
            }
        }

        protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //要隱藏的欄位    
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[8].Visible = false;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                users = ((List<Users>)Session["user"])[0];
                using (var conn = new SqlConnection(connectionString))
                {
                    SqlDataSource1.SelectCommand = "Select * From WorkLog Where IsDeleted = 0";
                    if (ddlUser.SelectedItem.Value != "1")
                        SqlDataSource1.SelectCommand += " And UserId = " + ddlUser.SelectedItem.Value;
                    if (ddlOrg.SelectedItem.Value != "0")
                        SqlDataSource1.SelectCommand += " And OrgId = " + ddlOrg.SelectedItem.Value;
                    if (!string.IsNullOrEmpty(txtSdate.Text))
                        SqlDataSource1.SelectCommand += " And CreateDate >= convert(varchar, '" + txtSdate.Text + "', 111)";
                    if (!string.IsNullOrEmpty(txtEdate.Text))
                        SqlDataSource1.SelectCommand += " And CreateDate <= convert(varchar, '" + txtEdate.Text + "', 111)";
                    gv.DataSourceID = "SqlDataSource1";
                    gv.DataBind();
                }
            }
            catch
            {
                Response.Redirect("/Login");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlUser.ClearSelection();
            ddlOrg.ClearSelection();
            txtSdate.Text = "";
            txtEdate.Text = "";
        }
    }
}