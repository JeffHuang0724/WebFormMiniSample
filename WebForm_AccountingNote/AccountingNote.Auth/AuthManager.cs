﻿using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    public class AuthManager
    {
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary> 取得已登入的使用者資訊(如果沒登入就回傳null)</summary>
        public static UserInfoModel GetCurentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;

            if (account == null)
                return null;

            DataRow userDr = UserInfoManager.GetUserByAccount(account);

            if (userDr == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }

            UserInfoModel model = new UserInfoModel();
            model.user_id = userDr["ID"].ToString();
            model.user_account = userDr["Account"].ToString();
            model.user_name = userDr["Name"].ToString();
            model.user_email = userDr["Email"].ToString();
            model.user_level = userDr["UserLevel"].ToString();
            model.user_create_date = userDr["CreateDate"].ToString();

            return model;
        }
        /// <summary> 登入 </summary>
        public static bool TryLogin(string user_account, string user_password, out string errMsg)
        {
            // check empty
            if (string.IsNullOrWhiteSpace(user_account) || string.IsNullOrWhiteSpace(user_password))
            {
                errMsg = "Account / Password is required";
                return false;
            }

            var dr = UserInfoManager.GetUserByAccount(user_account);
            // check dr null
            if (dr == null)
            {
                errMsg = "Account doesn't exisit";
                return false;
            }

            // check account / password
            if (string.Compare(dr["Account"].ToString(), user_account, true) == 0 &&
                string.Compare(dr["PWD"].ToString(), user_password, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = dr["Account"].ToString();
                errMsg = string.Empty;
                return true;
            }
            else
            {
                errMsg = "Login fail. Please check Account / Password.";
                return false;
            }
        }
        /// <summary> 登出 </summary>
        public static  void Logout()
        {
            HttpContext.Current.Session["UserLoginInfo"] = null;
        }
    }
}
