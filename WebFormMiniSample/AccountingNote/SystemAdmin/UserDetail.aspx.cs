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
    public partial class UserDetail : System.Web.UI.Page
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
                // 新增模式
                if (this.Request.QueryString["UID"] == null)
                {
                    // 移除帳號 Label 
                    this.pnlAccount.Controls.Remove(this.lblUserAccount);
                    this.btnDelete.Visible = false;
                    this.btnChangePwd.Visible = false;
                    // 新增模式等級限制 為一般會員
                    this.ddlUserLevel.SelectedValue = "1";
                    
                }
                // 編輯模式
                else
                {
                    // 移除帳號 TextBox 
                    this.pnlAccount.Controls.Remove(this.txtUserAccount);
                    this.btnDelete.Visible = true;
                    this.btnChangePwd.Visible = true;
                    string userIdTxt = this.Request.QueryString["UID"];

                    //確認登入者是否為本人
                    if (string.Compare(currentUser.user_id, userIdTxt) != 0)
                    {
                        this.txtUserName.Enabled = false;
                        this.txtUserEmail.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnDelete.Enabled = false;
                        this.btnChangePwd.Enabled = false;
                    }

                    var drUserInfo = UserInfoManager.GetUserInfoByUserId(userIdTxt);
                    if (drUserInfo == null)
                    {
                        this.ltMsg.Text = "資料不存在";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                    }
                    else
                    {
                        this.lblUserAccount.Text = drUserInfo["Account"].ToString();
                        this.txtUserName.Text = drUserInfo["Name"].ToString();
                        this.txtUserEmail.Text = drUserInfo["Email"].ToString();
                        this.ddlUserLevel.SelectedValue = drUserInfo["UserLevel"].ToString();
                        this.lblUserCreateTime.Text = drUserInfo["CreateDate"].ToString();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            // Check Input
            if (!this.CheckInpu(out msgList))
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

            // 取得資訊
            string userAccount = this.txtUserAccount.Text;
            string userName = this.txtUserName.Text;
            string userEmail = this.txtUserEmail.Text;
            // 如果UserLevel 無法轉型，一律強制為一般使用者(1)
            int userLevel;
            if(!int.TryParse(this.ddlUserLevel.SelectedValue, out userLevel))
            {
                userLevel = 1;
            }

            // 新增模式
            if (string.IsNullOrWhiteSpace(this.Request.QueryString["UID"]))
            {
                UserInfoManager.CreateUserInfo(userAccount, userName, userEmail, userLevel);
                Response.Redirect("/SystemAdmin/UserList.aspx");
            }

            // 更新模式
            else
            {
                string userIdTxt = this.Request.QueryString["UID"];
                if (UserInfoManager.UpdateUserInfo(userIdTxt, userName, userEmail))
                {
                    Response.Redirect("/SystemAdmin/UserList.aspx");
                }
                else
                {
                    ltMsg.Text = "更新失敗";
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Request.QueryString["UID"]))
                return;

            var alertSuccess = MessageBox.Show("確定要刪除此帳號嗎", "警告提示",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
            if (alertSuccess == DialogResult.OK)
            {
                if (UserInfoManager.DeleteUserInfo(this.Request.QueryString["UID"]))
                {
                    Response.Redirect("/SystemAdmin/UserList.aspx");
                }
                else
                {
                    ltMsg.Text = "刪除失敗";
                }
            }
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/SystemAdmin/UserPassword.aspx?UID={this.Request.QueryString["UID"]}");
        }

        private bool CheckInpu(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            // check Account
            if (string.IsNullOrWhiteSpace(this.txtUserAccount.Text))
            {
                msgList.Add("帳號為必填欄位");
            }
            // check Name
            if (string.IsNullOrWhiteSpace(this.txtUserName.Text))
            {
                msgList.Add("姓名為必填欄位");
            }
            // check Email
            if (string.IsNullOrWhiteSpace(this.txtUserEmail.Text))
            {
                msgList.Add("Email為必填欄位");
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