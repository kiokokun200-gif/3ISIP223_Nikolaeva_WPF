using _3ISIP223_Nikolaeva_WPF;
using _3ISIP223_Nikolaeva_WPF.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;


namespace AuthTests
{
    [TestClass]
    public class AuthorizationTest
    {
        public _3PageLogin PageLogin = new _3PageLogin();

        [TestMethod]
        public void AuthTestSuccess()
        { 
            Assert.IsTrue(PageLogin.LogIn("niconii@icloud.cpm", "qwerty123"));
        }

        [TestMethod]
        public void AuthTestFailData() {
            Assert.IsFalse(PageLogin.LogIn("Adam@gmai.com", "7SP9CV223"));
        }
        [TestMethod]
        public void AuthTestFailEmptyLogin()
        {
            Assert.IsFalse(PageLogin.LogIn("", "qwerty123"));
        }
        [TestMethod]
        public void AuthTestFailEmptyPassword()
        {
            Assert.IsFalse(PageLogin.LogIn("niconii@icloud.cpm", ""));
        }
    }
}
