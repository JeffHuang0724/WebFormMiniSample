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

            // read user data
            var dt = UserInfoManager.GetUserList();
            if (dt.Rows.Count > 0)
            {
                this.gvUserList.DataSource = dt;
                this.gvUserList.DataBind();
            } else
            {
                this.gvUserList.Visible = false;
                this.plcNoData.Visible = true;
            }
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