using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 取得Accounting 資訊
            DataRow drAccounting = AccountingManager.GetAccountingDefaultInfo();
            if (drAccounting == null)
            {
                this.lblFirstAccountingTime.Text = "尚無資料";
                this.lblLastAccountingTime.Text = "尚無資料";
                this.lblAccountingCount.Text = "尚無資料";
            } else
            {
                this.lblFirstAccountingTime.Text = Convert.ToDateTime(drAccounting["OldestDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                this.lblLastAccountingTime.Text = Convert.ToDateTime(drAccounting["NewestDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                this.lblAccountingCount.Text = $"共 {drAccounting["AccountingCount"].ToString()} 筆";
            }

            // 取得會員數
            DataRow drUserCount =  UserInfoManager.GetUserCount();
            if(drUserCount["UserCount"] == null)
            {
                this.lblUserCount.Text = "尚無資料";
            }else
            {
                this.lblUserCount.Text = $"共 {drUserCount["UserCount"].ToString()} 人";
            }
            
        }
    }
}