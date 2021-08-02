using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                if (this.Request.QueryString["user_id"] == null)
                {
                    this.btnDelete.Visible = false;
                    this.btnChangePwd.Visible = false;
                    // 新增模式等級限制 為一般會員
                    this.ddlUserLevel.SelectedValue = "1";
                    this.ddlUserLevel.Enabled = false;
                }
                // 編輯模式
                else
                {
                    this.btnDelete.Visible = true;
                    this.btnChangePwd.Visible = true;
                    string userIdTxt = this.Request.QueryString["user_id"];

                    //確認登入者是否為本人或是管理員
                    if (string.Compare(currentUser.user_id, userIdTxt) != 0 && string.Compare(currentUser.user_level, "0") != 0)
                    {
                        this.txtUserName.Enabled = false;
                        this.txtUserEmail.Enabled = false;
                        this.ddlUserLevel.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.btnDelete.Enabled = false;
                        this.btnChangePwd.Enabled = false;
                    }

                    // 如為一般使用者，不得更改等級
                    if(string.Compare(currentUser.user_level, "0") != 0)
                    {
                        this.ddlUserLevel.Enabled = false;
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

            /* 新增模式
             * string userAccount = this.lblUserAccount.Text;
             * string levelTxt = this.lblUserLevel.Text;
             * int userLevel = (int)this.ddlUserLevel.SelectedValue;
            */

            // 修改模式
            string userName = this.txtUserName.Text;
            string userEmail = this.txtUserEmail.Text;


            // 新增模式
            if (string.IsNullOrWhiteSpace(this.Request.QueryString["user_id"]))
            {
                //UserInfoManager.CreateUserInfo(userAccount, userPassword, userName, userEmail, userLevel);
                Response.Redirect("/SystemAdmin/UserList.aspx");
            }

            // 更新模式
            else
            {
                string userIdTxt = this.Request.QueryString["user_id"];

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
            if (string.IsNullOrEmpty(this.Request.QueryString["user_id"]))
                return;

            if (UserInfoManager.DeleteUserInfo(this.Request.QueryString["user_id"]))
            {
                Response.Redirect("/SystemAdmin/UserList.aspx");
            }
            else
            {
                ltMsg.Text = "刪除失敗";
            }

        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/SystemAdmin/UserPassword.aspx?user_id={this.Request.QueryString["user_id"]}");
        }

        private bool CheckInpu(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            // chk Name
            if (string.IsNullOrWhiteSpace(this.txtUserName.Text))
            {
                msgList.Add("姓名為必填欄位");
            }
            // chk Email
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