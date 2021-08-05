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
                if (this.Request.QueryString["UID"] == null)
                {
                    Response.Redirect("/SystemAdmin/UserList.aspx");
                }
                else
                {

                    string userIdTxt = this.Request.QueryString["UID"];


                    //確認登入者是否為本人
                    if (string.Compare(currentUser.user_id, userIdTxt) != 0)
                    {
                        Response.Redirect("/SystemAdmin/UserList.aspx");
                    }

                    // 取得會員帳號，並顯示
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
            // 訊息提示
            var alert1 = MessageBox.Show("你確定要變更密碼?", "訊息提示",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);
            if (alert1 == DialogResult.No)
            {
                return;
            }

            // 新增List、確認輸入內容，並將錯誤訊息顯示
            List<string> msgList = new List<string>();
            if (!this.CheckInput(out msgList))
            {
                this.ltMsg.Text = string.Join("<br />", msgList);
                return;
            }

            // 確認是否為本人
            UserInfoModel currentUser = AuthManager.GetCurentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            // 取得會員ID ，藉以更新密碼，並將訊息提示給使用者
            string userIdTxt = this.Request.QueryString["UID"];
            if (UserInfoManager.UpdateUserPwd(userIdTxt, this.txtNewPwd.Text))
            {
                var alertSuccess = MessageBox.Show("更新成功", "訊息提示",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
                if (alertSuccess == DialogResult.OK)
                {
                    Response.Redirect($"/SystemAdmin/UserDetail.aspx?UID={this.Request.QueryString["UID"]}");
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
        /// <summary>確認檢核Input 內容，回傳布林值以及錯誤訊息</summary>
        private bool CheckInput(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            //確認欄位是否為空
            if (string.IsNullOrWhiteSpace(txtOldPwd.Text) || string.IsNullOrWhiteSpace(txtOldCommitPwd.Text) || string.IsNullOrWhiteSpace(txtNewPwd.Text))
            {
                msgList.Add("密碼不得為空，請重新確認");
                errMsgList = msgList;
                return false;
            }
            // 確認原密碼以及確認密碼 兩者是否一致
            if (string.Compare(this.txtOldPwd.Text, this.txtOldCommitPwd.Text) != 0)
            {
                msgList.Add("密碼不一致，請重新確認");
                errMsgList = msgList;
                return false;
            }
            else
            {
                // 確認與資料庫密碼是否相符
                var userDr = UserInfoManager.GetUserPassword(this.Request.QueryString["UID"]);
                if (String.Compare(userDr["PWD"].ToString(), this.txtOldPwd.Text) != 0)
                {
                    msgList.Add("密碼不符，請重新確認");
                    errMsgList = msgList;
                    return false;
                }
            }

            //確認新密碼是否介於8~16個字
            if (this.txtNewPwd.Text.Replace(" ", "").Length < 8 || this.txtNewPwd.Text.Replace(" ", "").Length > 16)
            {
                msgList.Add("密碼長度須為8~16個字，請重新確認");
                errMsgList = msgList;
                return false;
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