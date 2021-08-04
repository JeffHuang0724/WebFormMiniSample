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
    public partial class UserInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
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
                    Response.Redirect("/Login.aspx");
                    return;
                }

                this.ltAccount.Text = currentUser.user_account;
                this.ltName.Text = currentUser.user_name;
                this.ltEmail.Text = currentUser.user_email;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthManager.Logout();
            Response.Redirect("/Login.aspx");
        }
    }
}