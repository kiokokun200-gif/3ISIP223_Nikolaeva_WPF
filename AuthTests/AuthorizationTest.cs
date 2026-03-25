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
            string err;
            Assert.IsTrue(PageLogin.LogIn("niconii@icloud.cpm", "qwerty123", out err));
        }

        [TestMethod]
        public void AuthTestFail() {
            string err;
            Assert.IsFalse(PageLogin.LogIn("Adam@gmai.com", "7SP9CV223", out err));
            Assert.IsFalse(PageLogin.LogIn("", "qwerty123", out err));
            Assert.IsFalse(PageLogin.LogIn("niconii@icloud.cpm", "", out err));
            Assert.IsFalse(PageLogin.LogIn("", "", out err));
            Assert.IsFalse(PageLogin.LogIn("niconiiicloud.cpm", "qwerty123", out err));
            Assert.IsFalse(PageLogin.LogIn("niconii@icloudcpm", "qwerty123", out err));      
            Assert.IsFalse(PageLogin.LogIn("niconii@icloud.cpm", "qwer", out err));
            Assert.IsFalse(PageLogin.LogIn("niconii@icloud.cpm", "qwertyui", out err));
        }

    }
}
