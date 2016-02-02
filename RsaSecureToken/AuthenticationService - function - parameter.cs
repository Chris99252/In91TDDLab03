using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaSecureToken
{
    public class AuthenticationService
    {
        public bool IsValid(string account, string password, IProfileDao profile, IRsaToken rsaToken)
        {
            // 根據 account 取得自訂密碼
            var passwordFromDao = profile.GetPassword(account);

            // 根據 account 取得 RSA token 目前的亂數
            var randomCode = rsaToken.GetRandom(account);

            // 驗證傳入的 password 是否等於自訂密碼 + RSA token亂數
            var validPassword = passwordFromDao + randomCode;
            var isValid = password == validPassword;
            return isValid;
        }

        public class ProfileDao : IProfileDao
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

        public class RsaTokenDao : IRsaToken
        {
            public string GetRandom(string account)
            {
                var seed = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                var result = seed.Next(0, 999999);
                return result.ToString("000000");
            }
        }
    }
}