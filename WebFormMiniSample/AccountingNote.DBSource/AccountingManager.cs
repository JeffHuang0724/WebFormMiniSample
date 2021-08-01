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
    public class AccountingManager
    {
        public static void CreateAccountingList(string user_id, string caption, int amount, int act_type, string description)
        {
            // <<<<< check input >>>>>
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000.");
            }
            if (act_type < 0 || act_type > 1)
            {
                throw new ArgumentException("act_type must between 0 and 1.");
            }
            // <<<<< check input >>>>>

            string connectionString = DBHealper.GetConnectionString();
            string queryString = @"INSERT INTO [dbo].[accounting]
                                                               (user_id
                                                               ,caption
                                                               ,amount
                                                               ,act_type
                                                               ,create_date
                                                               ,description)
                                                 VALUES
                                                                (@user_id
                                                               ,@caption
                                                               ,@amount
                                                               ,@act_type
                                                               ,@create_date
                                                               ,@description) ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@user_id", user_id));
            list.Add(new SqlParameter("@caption", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@act_type", act_type));
            list.Add(new SqlParameter("@create_date", DateTime.Now));
            list.Add(new SqlParameter("@description", description));
            try
            {
                DBHealper.CreateData(connectionString, queryString, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static bool UpdateAccountingList(int list_id, string user_id, string caption, int amount, int act_type, string description)
        {
            // <<<<< check input >>>>>
            if (amount < 0 || amount > 1000000)
            {
                throw new ArgumentException("Amount must between 0 and 1,000,000.");
            }
            if (act_type < 0 || act_type > 1)
            {
                throw new ArgumentException("act_type must between 0 and 1.");
            }
            // <<<<< check input >>>>>

            string connectionString = DBHealper.GetConnectionString();
            string queryString = @" UPDATE [dbo].[accounting]
                                                  SET
                                                               user_id = @user_id
                                                               ,caption = @caption
                                                               ,amount = @amount
                                                               ,act_type = @act_type
                                                               ,create_date = @create_date
                                                               ,description = @description
                                                 WHERE
                                                               list_id = @list_id ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@list_id", list_id));
            list.Add(new SqlParameter("@user_id", user_id));
            list.Add(new SqlParameter("@caption", caption));
            list.Add(new SqlParameter("@amount", amount));
            list.Add(new SqlParameter("@act_type", act_type));
            list.Add(new SqlParameter("@create_date", DateTime.Now));
            list.Add(new SqlParameter("@description", description));
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
        public static bool DeleteAccountingByListId(string list_id)
        {
            string connectionString = DBHealper.GetConnectionString();
            string queryString = @"DELETE 
                                                 FROM accounting 
                                                 WHERE list_id = @list_id ";
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@list_id", list_id));

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
        public static DataTable GetAccountingList(string userId)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString =
                $@" SELECT 
                            list_id
                          ,caption
                          ,amount
                          ,act_type
                          ,create_date
                       FROM accounting
                       WHERE user_id = @userId
                        ORDER BY create_date DESC
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userId", userId));

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
        public static DataRow GetAccountingByListId(int list_id, string user_id)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString = @"SELECT 
                                                                  caption
                                                                 ,amount
                                                                  ,act_type
                                                                  ,create_date
                                                                  ,description
                                                            FROM accounting
                                                            WHERE list_id = @list_id AND user_id = @user_id";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@list_id", list_id));
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
        public static DataRow GetAccountingDefaultInfo()
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString =
                @"SELECT 
                        COUNT(list_id) AS accounting_count, MAX(create_date) AS newest_date, MIN(create_date) AS  oldest_date
                     FROM 
                         accounting";

            List<SqlParameter> list = new List<SqlParameter>();
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
        public static DataRow GetAccountingAddAmount(string userId)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString =
                $@" SELECT 
                       SUM(amount) AS 'accounting_add_amount'
                       FROM accounting
                       WHERE act_type = '1' AND user_id = @userId
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userId", userId));

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
        public static DataRow GetAccountingMinusAmount(string userId)
        {
            string connectionString = DBHealper.GetConnectionString();
            string dbCommandString =
                $@" SELECT 
                       SUM(amount) AS 'accounting_minus_amount'
                       FROM accounting
                       WHERE act_type = '0' AND user_id = @userId
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userId", userId));

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
    }
}
