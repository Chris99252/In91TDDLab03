using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        public IProfileDao profile { private get; set; }
        public IRsaToken rsaToken { private get; set; }

        public bool IsValid(string account, string password)
        {
            if (this.profile == null)
            {
                throw new ArgumentNullException();
            }
            if (this.rsaToken == null)
            {
                throw new ArgumentNullException();
            }

            // 根據 account 取得自訂密碼
            var profileDao = new ProfileDao();
            //var passwordFromDao = profileDao.GetPassword(account);
            var passwordFromDao = this.profile.GetPassword(account);

            // 根據 account 取得 RSA token 目前的亂數
            var rsaToken = new RsaTokenDao();
            //var randomCode = rsaToken.GetRandom(account);
            var randomCode = this.rsaToken.GetRandom(account);

            // 驗證傳入的 password 是否等於自訂密碼 + RSA token亂數
            var validPassword = passwordFromDao + randomCode;
            var isValid = password == validPassword;
            return isValid;
        }
    }

    public class ProfileDao
    {
        public string GetPassword(string account)
        {
            var result = Context.GetPassword(account);
            return result;
        }
    }

    public static class Context
    {
        public static Dictionary<string, string> profiles;

        static Context()
        {
            profiles = new Dictionary<string, string>();
            profiles.Add("joey", "91");
            profiles.Add("mei", "99");
        }

        public static string GetPassword(string key)
        {
            return profiles[key];
        }
    }

    public class RsaTokenDao
    {
        public string GetRandom(string account)
        {
            var seed = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            var result = seed.Next(0, 999999);
            return result.ToString("000000");
        }
    }
}