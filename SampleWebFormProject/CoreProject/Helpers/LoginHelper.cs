using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoreProject.Helpers
{
    public class LoginHelper
    {
        private const string _sessionKey = "IsLogined";
        private const string _sessionKey_Account = "Account";

        /// <summary> 檢查是否有登入 </summary>
        /// <returns></returns>
        public static bool HasLogined()
        {
            bool? val = HttpContext.Current.Session[_sessionKey] as bool?;

            if (val.HasValue && val.Value)
                return true;
            else
                return false;
        }

        /// <summary> 嘗試登入 </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        public static bool TryLogin(string account, string pwd)
        {
            return true;
            //if (LoginHelper.HasLogined())
            //    return true;

            //// Get user account from DB
            //DataTable dt = DBAccountManager.GetUserAccount(account);

            //if (dt == null || dt.Rows.Count == 0)
            //    return false;

            ////bool isAccountRight = string.Compare("admin", account, true) == 0;
            //string dbPwd = dt.Rows[0].Field<string>("Pwd");
            //string dbName = dt.Rows[0].Field<string>("Name");
            //bool isPasswordRight = string.Compare(dbPwd, pwd) == 0;

            ////if (isAccountRight && isPasswordRight)
            //if (isPasswordRight)
            //{
            //    HttpContext.Current.Session[_sessionKey_Account] = dbName;
            //    HttpContext.Current.Session[_sessionKey] = true;

            //    return true;
            //}
            //else
            //    return false;
        }

        /// <summary> 登出目前使用者，如果還沒登入就不執行 </summary>
        public static void Logout()
        {
            if (!LoginHelper.HasLogined())
                return;

            HttpContext.Current.Session.Remove(_sessionKey);
            HttpContext.Current.Session.Remove(_sessionKey_Account);
        }

        /// <summary> 取得已登入者的資訊，如果還沒登入回傳空字串 </summary>
        /// <returns></returns>
        public static string GetCurrentUserInfo()
        {
            if (!LoginHelper.HasLogined())
                return string.Empty;

            return HttpContext.Current.Session[_sessionKey_Account] as string;
        }
    }
}
