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

            
            if (this.IsPostBack)
            {
                if (this.Request.QueryString["UID"] == null)
                {
                    // 移除帳號 Label  改顯示帳號 TextBox
                    this.pnlAccount.Controls.Remove(this.lblUserAccount);
                    this.pnlAccount.Controls.Add(this.txtUserAccount);
                }
                else
                {
                    // 移除帳號 TextBox 改顯示帳號 帳號 Label
                    this.pnlAccount.Controls.Remove(this.txtUserAccount);
                    this.pnlAccount.Controls.Add(this.lblUserAccount);
                }
            }
            

            if (!this.IsPostBack)
            {
                // 新增模式
                if (this.Request.QueryString["UID"] == null)
                {
                    this.pnlAccount.Controls.Remove(this.lblUserAccount);
                    this.pnlAccount.Controls.Add(this.txtUserAccount);
                    this.btnDelete.Visible = false;
                    this.btnChangePwd.Visible = false;
                    // 新增模式等級限制 為一般會員
                    this.lblUserLevel.Text = "一般會員";
                    this.lblUserCreateTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                // 編輯模式
                else
                {
                    this.pnlAccount.Controls.Remove(this.txtUserAccount);
                    this.pnlAccount.Controls.Add(this.lblUserAccount);
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
                    // 取得使用者資訊
                    var drUserInfo = UserInfoManager.GetUserInfoByUserId(userIdTxt);
                    if (drUserInfo == null)
                    {
                        this.plcHasData.Visible = false;
                        this.ltMsg.Text = "資料不存在";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
                        this.btnChangePwd.Visible = false;
                    }
                    else
                    {
                        this.plcHasData.Visible = true;
                        this.lblUserAccount.Text = drUserInfo["Account"].ToString();
                        this.txtUserName.Text = drUserInfo["Name"].ToString();
                        this.txtUserEmail.Text = drUserInfo["Email"].ToString();
                        // 會員等級
                        if (drUserInfo["UserLevel"].ToString() == "0")
                        {
                            lblUserLevel.Text = "管理員";
                        }
                        else
                        {
                            lblUserLevel.Text = "一般會員";
                        }
                        this.lblUserCreateTime.Text = Convert.ToDateTime(drUserInfo["CreateDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> msgList = new List<string>();
            // Check Input
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

            // 取得資訊
            string userAccount = this.txtUserAccount.Text;
            string userName = this.txtUserName.Text;
            string userEmail = this.txtUserEmail.Text;
            // UserLevel 轉型
            int userLevel;
            if (this.lblUserLevel.Text == "管理員")
            {
                userLevel = 0;
            }
            else
            {
                userLevel = 1;
            }
            DateTime createDate = Convert.ToDateTime(this.lblUserCreateTime.Text);


            // 新增模式
            if (string.IsNullOrWhiteSpace(this.Request.QueryString["UID"]))
            {
                UserInfoManager.CreateUserInfo(userAccount, userName, userEmail, userLevel, createDate);
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
            // 提示訊息
            var alertSuccess = MessageBox.Show("若刪除帳號，流水帳資料也會一併刪除！ 確定要刪除嗎？", "警告提示",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);
            if (alertSuccess == DialogResult.Yes)
            {
                if (UserInfoManager.DeleteUserInfo(this.Request.QueryString["UID"]))
                {
                    // 抓取使用者在AccountingNote 筆數，如為0則直接跳轉頁面，不然則進行刪除流水帳資訊的動作
                    var dt = AccountingManager.GetAccountingList(this.Request.QueryString["UID"]);
                    if(dt.Rows.Count == 0)
                    {
                        Response.Redirect("/SystemAdmin/UserList.aspx");
                    } else
                    {
                        if (AccountingManager.DeleteAccountingByUserId(this.Request.QueryString["UID"]))
                        {
                            Response.Redirect("/SystemAdmin/UserList.aspx");
                        } else
                        {
                            ltMsg.Text = "流水帳刪除失敗";
                        }
                    }
                }
                else
                {
                    ltMsg.Text = "會員資料刪除失敗";
                }
            }
        }

        protected void btnChangePwd_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/SystemAdmin/UserPassword.aspx?UID={this.Request.QueryString["UID"]}");
        }
        /// <summary>確認檢核Input內容 </summary>
        private bool CheckInput(out List<string> errMsgList)
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