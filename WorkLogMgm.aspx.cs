using Dapper;
using Notes.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;

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
                        Query();
                        if (users.username != "admin")
                        {
                            SqlDataSource3.SelectCommand = string.Format("Select * From Users Where Id={0}", users.Id);
                            ddlUser.DataSourceID = "SqlDataSource3";
                            ddlUser.DataBind();
                            //SqlDataSource1.SelectCommand = string.Format("Select * From WorkLog Where UserId={0}", users.Id);
                            //gv.DataSourceID = "SqlDataSource1";
                            //gv.DataBind();
                        }
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
            Query();
        }

        private void Query()
        {
            try
            {
                users = ((List<Users>)Session["user"])[0];
                using (var conn = new SqlConnection(connectionString))
                {
                    gv.AllowPaging = false;
                    SqlDataSource1.SelectCommand = "Select * From WorkLog Where IsDeleted = 0";
                    if (ddlUser.SelectedItem != null && ddlUser.SelectedItem.Value != "1")
                        SqlDataSource1.SelectCommand += " And UserId = " + ddlUser.SelectedItem.Value;
                    if (ddlUser.SelectedItem == null && users.Id != "1")
                        SqlDataSource1.SelectCommand += " And UserId = " + users.Id;
                    if (!string.IsNullOrEmpty(txtName.Text))
                        SqlDataSource1.SelectCommand += " And Name = '" + txtName.Text.Trim() + "'";
                    if (ddlOrg.SelectedItem != null && ddlOrg.SelectedItem.Value != "0")
                        SqlDataSource1.SelectCommand += " And OrgId = " + ddlOrg.SelectedItem.Value;
                    if (!string.IsNullOrEmpty(txtSdate.Text))
                        SqlDataSource1.SelectCommand += " And CreateDate >= convert(varchar, '" + txtSdate.Text + "', 111)";
                    if (!string.IsNullOrEmpty(txtEdate.Text))
                        SqlDataSource1.SelectCommand += " And CreateDate <= convert(varchar, '" + txtEdate.Text + "', 111)";
                    gv.DataSourceID = "SqlDataSource1";
                    gv.DataBind();

                    total.Text = gv.Rows.Count.ToString()+ "筆";
                    gv.AllowPaging = true;

                }
            }
            catch(Exception ex)
            {
                Response.Redirect("/Login");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlUser.ClearSelection();
            ddlOrg.ClearSelection();
            txtName.Text = "";
            txtSdate.Text = "";
            txtEdate.Text = "";
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            gv.PageSize = int.Parse(ddl.SelectedItem.Value);
        }

        protected void gv_PageIndexChanged(object sender, EventArgs e)
        {
            Query();
        }
    }
}