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
            string queryString = @"INSERT INTO [dbo].[user_info]
                                                               (user_id
                                                               ,user_account
                                                               ,user_password
                                                               ,user_name
                                                               ,user_email
                                                               ,user_level
                                                               ,user_create_date)
                                                 VALUES
                                                                (@user_id
                                                               ,@user_account
                                                               ,@user_password
                                                               ,@user_name
                                                               ,@user_email
                                                               ,@user_level
                                                               ,@user_create_date) ";

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
            string queryString = @" UPDATE [dbo].[user_info]
                                                  SET
                                                               user_name = @user_name
                                                               ,user_email = @user_email
                                                 WHERE
                                                               user_id = @user_id ";
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
            string queryString = @"DELETE 
                                                 FROM user_info 
                                                 WHERE user_id = @user_id ";
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
                                                                COUNT(user_id) AS user_count
                                                            FROM user_info";
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
            string dbCommandString = @"SELECT 
                                                                user_id 
                                                                ,user_account
                                                                ,user_password
                                                                ,user_name
                                                                ,user_email
                                                                ,user_level
                                                                ,user_create_date
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

        public static DataRow GetUserInfoByUserId(string user_id)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = @"SELECT 
                                                                  user_account
                                                                 ,user_name
                                                                  ,user_email
                                                                  ,user_level
                                                                  ,user_create_date
                                                            FROM user_info
                                                            WHERE user_id = @user_id";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));

            try
            {
                return DBHealper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static DataTable GetUserList()
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString =
                $@"SELECT
                            user_id
                            ,user_account
                            ,user_name
                            ,user_email
                            ,user_level
                            ,user_create_date
                        FROM user_info
                        ORDER BY user_create_date DESC
                ";

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
            string dbCommandString = @"SELECT 
                                                                user_password
                                                            FROM user_info
                                                            WHERE user_id = @user_id";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));

            try
            {
                return DBHealper.ReadDataRow(connectionString, dbCommandString, list);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool UpdateUserPwd(string user_id, string user_password)
        {
            string connectionString = DBHealper.GetConnectionString();
            string queryString = @" UPDATE [dbo].[user_info]
                                                  SET
                                                               user_password = @user_password
                                                 WHERE
                                                               user_id = @user_id ";
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
