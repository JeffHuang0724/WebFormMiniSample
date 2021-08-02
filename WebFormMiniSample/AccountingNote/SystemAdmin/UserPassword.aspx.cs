using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace AccountingNote.SystemAdmin
{
    public partial class UserPassword : System.Web.UI.Page
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

            if (!this.IsPostBack)
            {
                if (this.Request.QueryString["user_id"] == null)
                {
                    Response.Redirect("/SystemAdmin/UserList.aspx");
                }
                else
                {

                    string userIdTxt = this.Request.QueryString["user_id"];


                    //確認登入者是否為本人或是管理員
                    if (string.Compare(currentUser.user_id, userIdTxt) != 0 && string.Compare(currentUser.user_level, "0") != 0)
                    {
                        Response.Redirect("/SystemAdmin/UserList.aspx");
                    }


                    var drUserInfo = UserInfoManager.GetUserInfoByUserId(userIdTxt);
                    if (drUserInfo == null)
                    {
                        this.ltMsg.Text = "資料不存在";
                        this.btnSave.Visible = false;
                    }
                    else
                    {
                        this.lblUserAccount.Text = drUserInfo["Account"].ToString();
                    }
                }
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var alert1 = MessageBox.Show("你確定要變更密碼?", "訊息提示",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);
            if (alert1 == DialogResult.No)
            {
                return;
            }

            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br />", msgList);
                return;
            }

            UserInfoModel currentUser = AuthManager.GetCurentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string userIdTxt = this.Request.QueryString["user_id"];
            if (UserInfoManager.UpdateUserPwd(userIdTxt, this.txtNewPwd.Text))
            {
                var alertSuccess = MessageBox.Show("更新成功", "訊息提示",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                if (alertSuccess == DialogResult.OK)
                {
                    Response.Redirect($"/SystemAdmin/UserDetail.aspx?user_id={this.Request.QueryString["user_id"]}");
                }
            }
            else
            {
                var alertFailed = MessageBox.Show("更新失敗", "訊息提示",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                if (alertFailed == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            if (string.IsNullOrWhiteSpace(txtOldPwd.Text) || string.IsNullOrWhiteSpace(txtOldCommitPwd.Text) || string.IsNullOrWhiteSpace(txtNewPwd.Text))
            {
                msgList.Add("密碼不得為空，請重新確認");
            }
            // 確認原密碼以及確認密碼 兩者是否一致
            if (string.Compare(this.txtOldPwd.Text, this.txtOldCommitPwd.Text) != 0)
            {
                msgList.Add("密碼不一致，請重新確認");
            }
            else
            {
                // 確認與資料庫密碼是否相符
                var userDr = UserInfoManager.GetUserPassword(this.Request.QueryString["user_id"]);
                if (String.Compare(userDr["PWD"].ToString(), this.txtOldPwd.Text) != 0)
                {
                    msgList.Add("密碼不符，請重新確認");
                }
            }

            //確認新密碼是否介於8~16個字
            if (this.txtNewPwd.Text.Replace(" ", "").Length < 8 || this.txtNewPwd.Text.Replace(" ", "").Length > 16)
            {
                msgList.Add("密碼長度須為8~16個字，請重新確認");
            }
            errMsgList = msgList;
            if (msgList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}