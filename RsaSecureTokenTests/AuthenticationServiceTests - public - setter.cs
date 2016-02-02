using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsaSecureToken;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RsaSecureToken.Tests
{
    [TestClass()]
    public class AuthenticationServiceTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            var target = new AuthenticationService();
            target.profile = new StubProfileDao();
            target.rsaToken = new StubRsaTokenDao();
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