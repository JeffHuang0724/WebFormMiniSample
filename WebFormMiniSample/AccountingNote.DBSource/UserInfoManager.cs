using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class UserInfoManager
    {
        public static DataRow GetUserByAccount(string account)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = @"SELECT 
                                                                user_id, user_account, user_password, user_name, user_email
                                                            FROM user_info
                                                            WHERE user_account = @account";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@account", account));
            try
            {
                return DBHealper.ReadDataRow(connectionString, dbCommandString, list);
            } catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
