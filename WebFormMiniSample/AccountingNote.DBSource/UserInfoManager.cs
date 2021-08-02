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
        public static void CreateUserInfo(string user_account, string user_password, string user_name, string user_email, int user_level)
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
            list.Add(new SqlParameter("@user_password", user_password));
            list.Add(new SqlParameter("@user_name", user_name));
            list.Add(new SqlParameter("@user_email", user_email));
            list.Add(new SqlParameter("@user_level", user_level));
            list.Add(new SqlParameter("@user_create_date", DateTime.Now));
            try
            {
                DBHealper.CreateData(connectionString, queryString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
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
