using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccountingNote.Auth;
using AccountingNote.DBSource;

namespace AccountingNote.SystemAdmin
{
    public partial class AccountingList : System.Web.UI.Page
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
            if(currentUser == null)
            {
                this.Session["UserLoginInfo"] = null;
                Response.Redirect("/Login.aspx");
                return;
            }

            // read accounting data
            var dt = AccountingManager.GetAccountingList(currentUser.user_id);
            if(dt.Rows.Count > 0)
            {
                // 取得收入總數
                int accountingAddAmount;
                var drAccAdd = AccountingManager.GetAccountingAddAmount(currentUser.user_id);
                if(drAccAdd != null)
                {
                    
                    if (!int.TryParse(drAccAdd["AccountingAddAmount"].ToString(), out accountingAddAmount))
                    {
                        accountingAddAmount = 0;
                    }
                } else
                {
                    accountingAddAmount = 0;
                }

                // 取得支出總數
                int accountingMinAmount;
                var drAccMin = AccountingManager.GetAccountingMinusAmount(currentUser.user_id);
                if (drAccMin != null)
                {
                    if (!int.TryParse(drAccMin["AccountingMinusAmount"].ToString(), out accountingMinAmount))
                    {
                        accountingMinAmount = 0;
                    }
                } else
                {
                    accountingMinAmount = 0;
                }
                
                this.lblAmount.Text = $"小計 {(accountingAddAmount - accountingMinAmount).ToString()} 元";
                this.gvAccountingList.DataSource = dt;
                this.gvAccountingList.DataBind();
            } else
            {
                this.lblAmount.Visible = false;
                this.gvAccountingList.Visible = false;
                this.plcNoData.Visible = true;
            }

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
        }

        protected void gvAccountingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var row = e.Row;

            if(row.RowType == DataControlRowType.DataRow)
            {
                Literal ltl = row.FindControl("ltActType") as Literal;
                Label lbl = row.FindControl("lblAmount") as Label;
                var dr = row.DataItem as DataRowView;
                int actType = dr.Row.Field<int>("ActType");
                lbl.Text = Convert.ToString(dr.Row.Field<int>("Amount"));

                if (actType == 0)
                {
                    ltl.Text = "支出";
                }
                else
                {
                    ltl.Text = "收入";
                }

                if(dr.Row.Field<int>("Amount") > 1500)
                {
                    lbl.ForeColor = Color.Red;
                }
            }
        }
    }
}