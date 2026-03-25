using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using _3ISIP223_Nikolaeva_WPF.Pages;


namespace RegistrationTests
{
    [TestClass]
    public class RegisterTests
    {
        public _4RegisterPage page = new _4RegisterPage();
        [TestMethod]
        public void RegTestSuccess()
        {
            string err;
            Assert.IsTrue(page.Registration("Арина", "arina@gmail.com", "arrri123", "arrri123", out err));
            
        }
        [TestMethod]
        public void RegTestFail()
        {
            string err;
            Assert.IsFalse( page.Registration("", "edik2007@gmail.com", "Pog123!@#", "Pog123!@#", out err));
            Assert.IsFalse( page.Registration("Эдик", "", "Pog123!@#", "Pog123!@#", out err));
            Assert.IsFalse( page.Registration("Эдик", "edik2007@gmail.com", "", "Pog123!@#", out err));
            Assert.IsFalse( page.Registration("", "", "", "", out err));
            Assert.IsFalse( page.Registration("Эдик", "edik2007@gmail.com", "Pog12!@#", "Pog123!@#", out err));
            Assert.IsFalse(page.Registration("Эдик", "edik2007gmail.com", "Pog123!@#", "Pog123!@#", out err));
            Assert.IsFalse(page.Registration("Эдик", "edik2007@gmailcom", "Pog123!@#", "Pog123!@#", out err));
            Assert.IsFalse(page.Registration("Эдик", "edik2007gmail.com", "Pog12", "Pog12", out err));
            Assert.IsFalse(page.Registration("Эдик", "edik2007gmail.com", "Pogosyan", "Pogosyan", out err));
            Assert.IsFalse(page.Registration("Эдик", "niconii@icloud.cpm", "Pog123!@#", "Pog123!@#", out err));
            
        }
    }
}
