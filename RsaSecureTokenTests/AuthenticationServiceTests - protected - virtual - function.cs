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
            var target = new StubAuthenticationService();
            var actual = target.IsValid("joey", "91000000");

            //always failed
            Assert.IsTrue(actual);
        }
    }

    public class StubAuthenticationService : AuthenticationService
    {
        protected override ProfileDao GetProfileDao()
        {
            return new StubProfileDao();
        }
        protected override RsaTokenDao GetRsaToken()
        {
            return new StubRsaTokenDao();
        }
    }

    public class StubProfileDao : AuthenticationService.ProfileDao
    {
        public override string GetPassword(string account)
        {
            return "91";
        }
    }
    public class StubRsaTokenDao : AuthenticationService.RsaTokenDao
    {
        public override string GetRandom(string account)
        {
            return "000000";
        }
    }
}