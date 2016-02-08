using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsaSecureToken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RsaSecureToken.Tests
{
    //tips: 傳入 stub 物件的多種方式，可參考：http://www.dotblogs.com.tw/hatelove/archive/2012/11/27/learning-tdd-in-30-days-day6-several-ways-to-isolate-object-dependency-and-easy-for-testing.aspx
    // 1. 透過 target 的 constructor 傳入 interface/abstract 的 instance (DI auto-wiring)
    // 2. 透過 target 的 public property 傳入 (DI auto-wiring)
    // 3. target 方法的參數 (常看到傳入的參數型別為 interface的那種情況)
    // 4. target 將欲 stub 的方法擷取成 protected virtual 方法，使用 繼承 + override 的方式來測試原始target class

    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            StubProfileDao stubProfile = new StubProfileDao();
            StubRsaTokenDao stubRsaToken = new StubRsaTokenDao();
            var target = new AuthenticationService(stubProfile, stubRsaToken);
            var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);
        }
    }

    internal class StubProfileDao : IProfileDao
    {
        public string GetPassword(string account)
        {
            return "91";
        }
    }

    internal class StubRsaTokenDao : IRsaToken
    {
        public string GetRandom(string account)
        {
            return "000000";
        }
    }
}
