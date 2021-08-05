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
        /// <summary> 創建會員資訊 </summary>
        public static void CreateUserInfo(string user_account, string user_name, string user_email, int user_level, DateTime create_date)
        {
            // <<<<< check input >>>>>
            if (user_level < 0 || user_level > 1)
            {
                throw new ArgumentException("act_type must between 0 and 1.");
            }
            // <<<<< check input >>>>>

            string connectionString = DBHealper.GetConnectionString();
            string queryString = $@"INSERT INTO [dbo].[UserInfo]
                                                               (ID
                                                               ,Account
                                                               ,PWD
                                                               ,Name
                                                               ,Email
                                                               ,UserLevel
                                                               ,CreateDate)
                                                 VALUES
                                                                (@user_id
                                                               ,@user_account
                                                               ,@user_password
                                                               ,@user_name
                                                               ,@user_email
                                                               ,@user_level
                                                               ,@user_create_date);";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", Guid.NewGuid()));
            list.Add(new SqlParameter("@user_account", user_account));
            //密碼預設為12345
            list.Add(new SqlParameter("@user_password", "12345"));
            list.Add(new SqlParameter("@user_name", user_name));
            list.Add(new SqlParameter("@user_email", user_email));
            list.Add(new SqlParameter("@user_level", user_level));
            list.Add(new SqlParameter("@user_create_date", create_date));
            try
            {
                DBHealper.CreateData(connectionString, queryString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        /// <summary> 更新會員資訊 </summary>
        public static bool UpdateUserInfo(string user_id, string user_name, string user_email)
        {
            string connectionString = DBHealper.GetConnectionString();
            string queryString = $@" UPDATE [dbo].[UserInfo]
                                                     SET
                                                               Name = @user_name
                                                               ,Email = @user_email
                                                      WHERE
                                                               ID = @user_id;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));
            list.Add(new SqlParameter("@user_name", user_name));
            list.Add(new SqlParameter("@user_email", user_email));
            try
            {
                return DBHealper.ModifyData(connectionString, queryString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        /// <summary> 刪除會員資訊 </summary>
        public static bool DeleteUserInfo(string user_id)
        {
            string connectionString = DBHealper.GetConnectionString();
            string queryString = $@"DELETE 
                                                    FROM UserInfo 
                                                    WHERE ID = @user_id;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));
            try
            {
                return DBHealper.ModifyData(connectionString, queryString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        /// <summary> 取得總會員人數 </summary>
        public static DataRow GetUserCount()
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = @"SELECT 
                                                            COUNT(ID) AS UserCount
                                                            FROM UserInfo;";

            List<SqlParameter> list = new List<SqlParameter>();
            try
            {
                return DBHealper.ReadDataRow(connectionString, dbCommandString, list);
            } catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary> 取得總會員資訊(Account) </summary>
        public static DataRow GetUserByAccount(string account)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = $@"SELECT 
                                                                ID 
                                                                ,Account
                                                                ,PWD
                                                                ,Name
                                                                ,Email
                                                                ,UserLevel
                                                                ,CreateDate
                                                            FROM UserINfo
                                                            WHERE Account = @account;";

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
        /// <summary> 取得總會員資訊(ID) </summary>
        public static DataRow GetUserInfoByUserId(string user_id)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = $@"SELECT 
                                                                  Account
                                                                 ,Name
                                                                  ,Email
                                                                  ,UserLevel
                                                                  ,CreateDate
                                                            FROM UserInfo
                                                            WHERE ID = @user_id;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));
            try
            {
                return DBHealper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary> 取得全部會員資訊(UserList) </summary>
        public static DataTable GetUserList()
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = $@"SELECT
                                                                    ID
                                                                    ,Account
                                                                    ,Name
                                                                    ,Email
                                                                    ,UserLevel
                                                                    ,CreateDate
                                                               FROM UserInfo
                                                               ORDER BY CreateDate DESC;";

            List<SqlParameter> list = new List<SqlParameter>();
            try
            {
                return DBHealper.ReadDataTable(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary> 取得會員密碼 </summary>
        public static DataRow GetUserPassword(string user_id)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = $@"SELECT 
                                                                    PWD
                                                                FROM UserInfo
                                                                WHERE ID = @user_id;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));

            try
            {
                return DBHealper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        /// <summary> 更新會員密碼 </summary>
        public static bool UpdateUserPwd(string user_id, string user_password)
        {
            string connectionString = DBHealper.GetConnectionString();
            string queryString = $@" UPDATE [dbo].[UserInfo]
                                                    SET
                                                               PWD = @user_password
                                                    WHERE
                                                               ID = @user_id;";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));
            list.Add(new SqlParameter("@user_password", user_password));
            try
            {
                return DBHealper.ModifyData(connectionString, queryString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
    }
}
