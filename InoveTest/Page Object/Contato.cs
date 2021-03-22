using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace InoveTest.Page_Object
{
    class Contato
    {
        /* [FindsBy(How = How.Name, Using = "your-name")]
         public IWebElement name { get; set; }

         [FindsBy(How = How.Name, Using = "your-email")]
         public IWebElement email { get; set; }

         [FindsBy(How = How.Name, Using = "your-subject")]
         public IWebElement subject { get; set; }

         [FindsBy(How = How.Name, Using = "your-message")]
         public IWebElement message { get; set; }

         [FindsBy(How = How.CssSelector, Using = "input.wpcf7-form-control.wpcf7-submit")]
         public IWebElement enviar { get; set; }

         public void ButtonEnviarClick()
         {
             enviar.Click();
         }
        */

        private RemoteWebDriver _driver;

        public Contato(RemoteWebDriver driver) => _driver = driver;
        IWebElement name => _driver.FindElementByName("your-name");
        IWebElement email => _driver.FindElementByName("your-email");
        IWebElement subject => _driver.FindElementByName("your-subject");
        IWebElement message => _driver.FindElementByName("your-message");
        IWebElement enviar => _driver.FindElementByCssSelector("input.wpcf7-form-control.wpcf7-submit");

        public void VerificarExistenciaCampos()
        {
            Assert.IsTrue(name.Enabled);
            Assert.IsTrue(email.Enabled);
            Assert.IsTrue(subject.Enabled);
            Assert.IsTrue(message.Enabled);
            Assert.IsTrue(enviar.Enabled);
        }

        public void ClicarBtnEnviar()
        {
            enviar.Click();
        }
    }
}
