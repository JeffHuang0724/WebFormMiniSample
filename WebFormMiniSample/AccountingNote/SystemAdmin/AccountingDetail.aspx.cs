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
    public partial class AccountingDetail : System.Web.UI.Page
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
                if (this.Request.QueryString["list_id"] == null)
                {
                    this.btnDelete.Visible = false;
                }
                // 編輯模式
                else
                {
                    this.btnDelete.Visible = true;

                    string listIdTxt = this.Request.QueryString["list_id"];
                    int list_id;
                    if (int.TryParse(listIdTxt, out list_id))
                    {
                        var drAccounting = AccountingManager.GetAccountingByListId(list_id, currentUser.user_id);
                        if (drAccounting == null)
                        {
                            this.ltMsg.Text = "資料不存在";
                            this.btnSave.Visible = false;
                            this.btnDelete.Visible = false;
                        }
                        else
                        {
                            this.ddlActType.SelectedValue = drAccounting["act_type"].ToString();
                            this.txtAmount.Text = drAccounting["amount"].ToString();
                            this.txtCaption.Text = drAccounting["caption"].ToString();
                            this.txtDesc.Text = drAccounting["description"].ToString();
                        }
                    }
                    else
                    {
                        this.ltMsg.Text = "ID is required";
                        this.btnSave.Visible = false;
                        this.btnDelete.Visible = false;
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
            if(currentUser == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }

            string userId = currentUser.user_id;
            int actType = Convert.ToInt32(this.ddlActType.SelectedValue);
            int amount = Convert.ToInt32(this.txtAmount.Text);
            string caption = this.txtCaption.Text;
            string desc = this.txtDesc.Text;

            // 新增模式
            if (string.IsNullOrWhiteSpace(this.Request.QueryString["list_id"]))
            {
                AccountingManager.CreateAccountingList(userId, caption, amount, actType, desc);
                Response.Redirect("/SystemAdmin/AccountingList.aspx");
            }
            // 更新模式
            else
            {
                string listIdTxt = this.Request.QueryString["list_id"];
                int list_id;
                if (int.TryParse(listIdTxt, out list_id))
                {
                    if (AccountingManager.UpdateAccountingList(list_id, userId, caption, amount, actType, desc))
                    {
                        Response.Redirect("/SystemAdmin/AccountingList.aspx");
                    }
                    else
                    {
                        ltMsg.Text = "更新失敗";
                    }
                }
            }
        }

        private bool CheckInpu(out List<string> errMsgList)
        {
            List<string> msgList = new List<string>();
            // chk ActType
            if (this.ddlActType.SelectedValue != "0" && this.ddlActType.SelectedValue != "1")
            {
                msgList.Add("Type must be 0 or 1.");
            }
            // chk Amount
            if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
            {
                msgList.Add("Amount is required.");
            }
            else
            {
                int tempInt;
                if (!int.TryParse(this.txtAmount.Text, out tempInt))
                {
                    msgList.Add("Amount must be a  number.");
                }
                if (tempInt < 0 || tempInt > 1000000)
                {
                    msgList.Add("Amount must between 0 and 1,000,000.");
                }
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Request.QueryString["list_id"]))
                return;

            int listId;
            if (int.TryParse(this.Request.QueryString["list_id"], out listId))
            {
                if (AccountingManager.DeleteAccountingByListId(this.Request.QueryString["list_id"]))
                {
                    Response.Redirect("/SystemAdmin/AccountingList.aspx");
                }
                else
                {
                    ltMsg.Text = "Delete Failed";
                }
            }
        }
    }
}