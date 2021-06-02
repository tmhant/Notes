using Dapper;
using Notes.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NPOI.XWPF.UserModel;
using System.IO;
using System.Web.UI;
using System.Linq;

namespace Notes
{
    public partial class WorkLog : System.Web.UI.Page
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
                        SqlDataSource1.SelectCommand = string.Format("Select * From WorkLog Where IsDeleted = 0 and UserId = {0} and CreateDate = convert(varchar, getdate(), 111)", users.Id);
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            PopWorkLog.Visible = true;
            btnNew.Visible = false;
            btnSave.Visible = true;
            btnReset.Visible = true;
            btnDelete.Visible = false;
            Reset();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PopWorkLog.Visible = false;
            btnNew.Visible = true;
            btnSave.Visible = false;
            btnReset.Visible = false;
            btnDelete.Visible = false;
            string sqlStr = "";
            users = ((List<Users>)Session["user"])[0];
            if (string.IsNullOrEmpty(hd_id.Value))
            {
                sqlStr = @"Insert Into WorkLog(UserId,UserName,OrgId,OrgName,Name,Question,DealWithId,DealWithName,DealWith,Remark)
                                Values(@UserId,@UserName,@OrgId,@OrgName,@Name,@Question,@DealWithId,@DealWithName,@DealWith,@Remark)";
            }
            else
            {
                sqlStr = @"Update WorkLog set OrgId=@OrgId, OrgName=@OrgName, Name=@Name, Question=@Question,
                            DealWithId=@DealWithId, DealWithName=@DealWithName, DealWith=@DealWith, Remark=@Remark, UpdateDate=getdate()
                            Where Id=@Id";
            }
            WorkLogs workLogs = new WorkLogs
            {
                Id = string.IsNullOrEmpty(hd_id.Value) ? 0 : int.Parse(hd_id.Value),
                UserId = users == null ? 0 : int.Parse(users.Id),
                UserName = users == null ? "" : users.firstName,
                OrgId = int.Parse(ddlOrg.SelectedItem.Value),
                OrgName = ddlOrg.SelectedItem.Text,
                Name = txtName.Text,
                Question = txtQuestion.Text,
                DealWithId = int.Parse(ddlDealwith.SelectedItem.Value),
                DealWithName = ddlDealwith.SelectedItem.Text,
                DealWith = txtDealwith.Text,
                Remark = txtRemark.Text
            };
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var worklog = conn.ExecuteScalar<WorkLogs>(sqlStr, workLogs);
                SqlDataSource1.SelectCommand = string.Format("Select * From WorkLog Where IsDeleted = 0 and UserId = {0} and CreateDate = convert(varchar, getdate(), 111)", users.Id);
                gv.DataSourceID = "SqlDataSource1";
                gv.DataBind();
            }
            
            if (chkQuestion.Checked)
            {
                users = ((List<Users>)Session["user"])[0];
                sqlStr = @"Insert Into Template(Catalog,Tbody,UserId)Values(@Catalog,@Tbody,@UserId)";
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var t = conn.ExecuteScalar<WorkLogs>(sqlStr, new
                    {
                        Catalog = 1,
                        Tbody = txtQuestion.Text.Trim(),
                        UserId = users.Id
                    });
                }
            }
            Reset();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            PopWorkLog.Visible = false;
            btnNew.Visible = true;
            btnSave.Visible = false;
            btnReset.Visible = false;
            btnDelete.Visible = false;
            Reset();
        }

        private void Reset()
        {
            hd_id.Value = "";
            txtName.Text = "";
            txtQuestion.Text = "";
            chkQuestion.Checked = false;
            txtDealwith.Text = "";
            txtRemark.Text = "";
            ddlOrg.ClearSelection();
            ddlDealwith.ClearSelection();
        }

        protected void gw_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            PopWorkLog.Visible = true;
            btnNew.Visible = false;
            btnSave.Visible = true;
            btnReset.Visible = true;
            btnDelete.Visible = true;
            int a = e.NewSelectedIndex;
            GridViewRow row = gv.Rows[e.NewSelectedIndex];
            hd_id.Value = row.Cells[1].Text.Trim();
            ddlOrg.SelectedValue = row.Cells[2].Text.Trim();
            txtName.Text = row.Cells[4].Text.Trim().Replace("&nbsp;","");
            txtQuestion.Text = row.Cells[5].Text.Trim();
            ddlDealwith.SelectedValue = row.Cells[6].Text.Trim();
            txtDealwith.Text = row.Cells[8].Text.Trim();
            txtRemark.Text = row.Cells[9].Text.Trim().Replace("&nbsp;","");
        }

        protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                //要隱藏的欄位    
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[6].Visible = false;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string Org0 = "", Org1 ="", Org2 = "", Org3 = "", Org4 = "", Org5 = "", Org6 = "", Org7 = "", Org8 = "", Org9 = "";
                string Name0 = "", Name1 = "", Name2 = "", Name3 = "", Name4 = "", Name5 = "", Name6 = "", Name7 = "", Name8 = "", Name9 = "";
                string Q0 = "", Q1 = "", Q2 = "", Q3 = "", Q4 = "", Q5 = "", Q6 = "", Q7 = "", Q8 = "", Q9 = "";
                string D0 = "", D1 = "", D2 = "", D3 = "", D4 = "", D5 = "", D6 = "", D7 = "", D8 = "", D9 = "";
                string R0 = "", R1 = "", R2 = "", R3 = "", R4 = "", R5 = "", R6 = "", R7 = "", R8 = "", R9 = "";
                users = ((List<Users>)Session["user"])[0];
                string strSql = string.Format("Select Id, UserId, UserName, OrgId, OrgName, Name, Question, DealWithName, DealWith, Remark, IsDeleted From WorkLog Where UserId = {0} and CreateDate = convert(varchar, getdate(), 111)", users.Id);
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var worklog = conn.Query<WorkLogs>(strSql).ToList();
                    if (worklog.Count == 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('目前沒有資料!')", true);
                        return;
                    }
                    int i = 0;
                    foreach (var item in worklog)
                    {
                        switch (i) {
                            case 0:
                                Org0 = item.OrgName;
                                Name0 = item.Name;
                                Q0 = item.Question;
                                D0 = item.DealWith;
                                R0 = item.Remark;
                                break;
                            case 1:
                                Org1 = item.OrgName;
                                Name1 = item.Name;
                                Q1 = item.Question;
                                D1 = item.DealWith;
                                R1 = item.Remark;
                                break;
                            case 2:
                                Org2 = item.OrgName;
                                Name2 = item.Name;
                                Q2 = item.Question;
                                D2 = item.DealWith;
                                R2 = item.Remark;
                                break;
                            case 3:
                                Org3 = item.OrgName;
                                Name3 = item.Name;
                                Q3 = item.Question;
                                D3 = item.DealWith;
                                R3 = item.Remark;
                                break;
                            case 4:
                                Org4 = item.OrgName;
                                Name4 = item.Name;
                                Q4 = item.Question;
                                D4 = item.DealWith;
                                R4 = item.Remark;
                                break;
                            case 5:
                                Org5 = item.OrgName;
                                Name5 = item.Name;
                                Q5 = item.Question;
                                D5 = item.DealWith;
                                R5 = item.Remark;
                                break;
                            case 6:
                                Org6 = item.OrgName;
                                Name6 = item.Name;
                                Q6 = item.Question;
                                D6 = item.DealWith;
                                R6 = item.Remark;
                                break;
                            case 7:
                                Org7 = item.OrgName;
                                Name7 = item.Name;
                                Q7 = item.Question;
                                D7 = item.DealWith;
                                R7 = item.Remark;
                                break;
                            case 8:
                                Org8 = item.OrgName;
                                Name8 = item.Name;
                                Q8 = item.Question;
                                D8 = item.DealWith;
                                R8 = item.Remark;
                                break;
                            case 9:
                                Org9 = item.OrgName;
                                Name9 = item.Name;
                                Q9 = item.Question;
                                D9 = item.DealWith;
                                R9 = item.Remark;
                                break;
                        }
                        i++;
                    }
                }
                using (FileStream stream = File.OpenRead(Server.MapPath("~/工程人員日報表.docx")))
                {
                    XWPFDocument doc = new XWPFDocument(stream);
                    var allTables = doc.Tables;
                    foreach(var table in allTables)
                    {
                        var rows = table.Rows;
                        foreach (var row in rows)
                        {
                            var cells = row.GetTableCells();
                            foreach(var cell in cells)
                            {
                                foreach (var para in cell.Paragraphs)
                                {
                                    string oldtext = para.ParagraphText;
                                    if (oldtext == "" || oldtext.Contains("\n"))
                                        continue;
                                    string temptext = para.ParagraphText;
                                    if (temptext.Contains("${e}"))
                                        temptext = temptext.Replace("${e}", users.firstName);
                                    if (temptext.Contains("#yy"))
                                        temptext = temptext.Replace("#yy", DateTime.Now.Year.ToString());
                                    if (temptext.Contains("#mm"))
                                        temptext = temptext.Replace("#mm", DateTime.Now.Month.ToString());
                                    if (temptext.Contains("#dd"))
                                        temptext = temptext.Replace("#dd", DateTime.Now.Day.ToString());
                                    if (temptext.Contains("#w"))
                                        temptext = temptext.Replace("#w", transWeek(DateTime.Now.DayOfWeek.ToString()));
                                    
                                    if (temptext.Contains("#Org0"))
                                        temptext = temptext.Replace("#Org0", Org0);
                                    if (temptext.Contains("#Org1"))
                                        temptext = temptext.Replace("#Org1", Org1);
                                    if (temptext.Contains("#Org2"))
                                        temptext = temptext.Replace("#Org2", Org2);
                                    if (temptext.Contains("#Org3"))
                                        temptext = temptext.Replace("#Org3", Org3);
                                    if (temptext.Contains("#Org4"))
                                        temptext = temptext.Replace("#Org4", Org4);
                                    if (temptext.Contains("#Org5"))
                                        temptext = temptext.Replace("#Org5", Org5);
                                    if (temptext.Contains("#Org6"))
                                        temptext = temptext.Replace("#Org6", Org6);
                                    if (temptext.Contains("#Org7"))
                                        temptext = temptext.Replace("#Org7", Org7);
                                    if (temptext.Contains("#Org8"))
                                        temptext = temptext.Replace("#Org8", Org8);
                                    if (temptext.Contains("#Org9"))
                                        temptext = temptext.Replace("#Org9", Org9);
                                    if (temptext.Contains("#Name0"))
                                        temptext = temptext.Replace("#Name0", Name0);
                                    if (temptext.Contains("#Name1"))
                                        temptext = temptext.Replace("#Name1", Name1);
                                    if (temptext.Contains("#Name2"))
                                        temptext = temptext.Replace("#Name2", Name2);
                                    if (temptext.Contains("#Name3"))
                                        temptext = temptext.Replace("#Name3", Name3);
                                    if (temptext.Contains("#Name4"))
                                        temptext = temptext.Replace("#Name4", Name4);
                                    if (temptext.Contains("#Name5"))
                                        temptext = temptext.Replace("#Name5", Name5);
                                    if (temptext.Contains("#Name6"))
                                        temptext = temptext.Replace("#Name6", Name6);
                                    if (temptext.Contains("#Name7"))
                                        temptext = temptext.Replace("#Name7", Name7);
                                    if (temptext.Contains("#Name8"))
                                        temptext = temptext.Replace("#Name8", Name8);
                                    if (temptext.Contains("#Name9"))
                                        temptext = temptext.Replace("#Name9", Name9);
                                    if (temptext.Contains("#Q0"))
                                        temptext = temptext.Replace("#Q0", Q0);
                                    if (temptext.Contains("#Q1"))
                                        temptext = temptext.Replace("#Q1", Q1);
                                    if (temptext.Contains("#Q2"))
                                        temptext = temptext.Replace("#Q2", Q2);
                                    if (temptext.Contains("#Q3"))
                                        temptext = temptext.Replace("#Q3", Q3);
                                    if (temptext.Contains("#Q4"))
                                        temptext = temptext.Replace("#Q4", Q4);
                                    if (temptext.Contains("#Q5"))
                                        temptext = temptext.Replace("#Q5", Q5);
                                    if (temptext.Contains("#Q6"))
                                        temptext = temptext.Replace("#Q6", Q6);
                                    if (temptext.Contains("#Q7"))
                                        temptext = temptext.Replace("#Q7", Q7);
                                    if (temptext.Contains("#Q8"))
                                        temptext = temptext.Replace("#Q8", Q8);
                                    if (temptext.Contains("#Q9"))
                                        temptext = temptext.Replace("#Q9", Q9);
                                    if (temptext.Contains("#D0"))
                                        temptext = temptext.Replace("#D0", D0);
                                    if (temptext.Contains("#D1"))
                                        temptext = temptext.Replace("#D1", D1);
                                    if (temptext.Contains("#D2"))
                                        temptext = temptext.Replace("#D2", D2);
                                    if (temptext.Contains("#D3"))
                                        temptext = temptext.Replace("#D3", D3);
                                    if (temptext.Contains("#D4"))
                                        temptext = temptext.Replace("#D4", D4);
                                    if (temptext.Contains("#D5"))
                                        temptext = temptext.Replace("#D5", D5);
                                    if (temptext.Contains("#D6"))
                                        temptext = temptext.Replace("#D6", D6);
                                    if (temptext.Contains("#D7"))
                                        temptext = temptext.Replace("#D7", D7);
                                    if (temptext.Contains("#D8"))
                                        temptext = temptext.Replace("#D8", D8);
                                    if (temptext.Contains("#D9"))
                                        temptext = temptext.Replace("#D9", D9);
                                    if (temptext.Contains("#R0"))
                                        temptext = temptext.Replace("#R0", R0);
                                    if (temptext.Contains("#R1"))
                                        temptext = temptext.Replace("#R1", R1);
                                    if (temptext.Contains("#R2"))
                                        temptext = temptext.Replace("#R2", R2);
                                    if (temptext.Contains("#R3"))
                                        temptext = temptext.Replace("#R3", R3);
                                    if (temptext.Contains("#R4"))
                                        temptext = temptext.Replace("#R4", R4);
                                    if (temptext.Contains("#R5"))
                                        temptext = temptext.Replace("#R5", R5);
                                    if (temptext.Contains("#R6"))
                                        temptext = temptext.Replace("#R6", R6);
                                    if (temptext.Contains("#R7"))
                                        temptext = temptext.Replace("#R7", R7);
                                    if (temptext.Contains("#R8"))
                                        temptext = temptext.Replace("#R8", R8);
                                    if (temptext.Contains("#R9"))
                                        temptext = temptext.Replace("#R9", R9);
                                    
                                    para.ReplaceText(oldtext, temptext);
                                }
                            }
                        }
                    }

                    DirectoryInfo info = new DirectoryInfo(@"c:\\tmp");
                    if (!info.Exists)
                        info.Create();
                    FileStream output = new FileStream(@"c:\\tmp\\工程人員日報表"+DateTime.Now.ToString("MM月dd日")+".docx", FileMode.Create);

                    doc.Write(output);
                    output.Close();
                    output.Dispose();

                    ScriptManager.RegisterClientScriptBlock(this, GetType(),"alertMessage", @"alert('列印完成!請至C:\\tmp資料夾下讀取檔案!')", true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        private string transWeek(string w)
        {
            string retvalue = "";
            switch (w)
            {
                case "Monday":
                    retvalue = "一";
                    break;
                case "Tuesday":
                    retvalue = "二";
                    break;
                case "Wednesday":
                    retvalue = "三";
                    break;
                case "Thursday":
                    retvalue = "四";
                    break;
                case "Friday":
                    retvalue = "五";
                    break;
                case "Saturday":
                    retvalue = "六";
                    break;
                case "Sunday":
                    retvalue = "日";
                    break;

            }
            return retvalue;
        }
    }
}