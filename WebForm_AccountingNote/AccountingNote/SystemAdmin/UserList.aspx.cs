using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 驗證登入
            if (!AuthManager.IsLogined())
            {
                Response.Redirect("/Login.aspx");
                return;
            }
    
            var currentUser = AuthManager.GetCurentUser();
            if (currentUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // 取得使用者列表，如為空則隱藏列表並顯示訊息
            var dt = UserInfoManager.GetUserList();
            if (dt.Rows.Count > 0)
            {
                var dtPaged = this.GetPageDataTable(dt);

                this.gvUserList.DataSource = dtPaged;
                this.gvUserList.DataBind();

                this.ucPager.TotalSize = dt.Rows.Count;
                this.ucPager.Bind();

            } else
            {
                this.ucPager.Visible = false;
                this.gvUserList.Visible = false;
                this.plcNoData.Visible = true;
            }
        }
        /// <summary>取得當前頁數 </summary>
        private int GetCurrentPage()
        {
            string pageText = Request.QueryString["page"];

            if (string.IsNullOrWhiteSpace(pageText))
                return 1;

            int intPage;
            if (!int.TryParse(pageText, out intPage))
                return 1;

            if (intPage <= 0)
                return 1;

            return intPage;
        }
        /// <summary> 取得分頁的資料列表  </summary>
        private DataTable GetPageDataTable(DataTable dt)
        {
            DataTable dtPaged = dt.Clone();

            int startIndex = (this.GetCurrentPage() - 1) * 10;
            int endIndex = (this.GetCurrentPage()) * 10;

            if (endIndex > dt.Rows.Count)
                endIndex = dt.Rows.Count;

            for (var i = startIndex; i < endIndex; i++)
            {
                DataRow dr = dt.Rows[i];
                var drNew = dtPaged.NewRow();
                foreach (DataColumn dc in dt.Columns)
                {
                    drNew[dc.ColumnName] = dr[dc];
                }
                dtPaged.Rows.Add(drNew);
            }
            return dtPaged;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/UserDetail.aspx");
        }

        protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Literal ltl = row.FindControl("ltUserLevel") as Literal;
                var dr = row.DataItem as DataRowView;
                int userLevel = dr.Row.Field<int>("UserLevel");

                if (userLevel == 0)
                {
                    ltl.Text = "管理員";
                }
                else
                {
                    ltl.Text = "一般會員";
                }
            }
        }
    }
}