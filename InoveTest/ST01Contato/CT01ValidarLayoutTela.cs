using System;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using InoveTest.Page_Object;
using OpenQA.Selenium.Support.PageObjects;
using InoveTest;
using OpenQA.Selenium.Remote;

namespace ST01Contato
{
    [TestFixture]
    public class CT01ValidarLayoutTela
    {
        private RemoteWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        [Obsolete]
        public void SetupTest()
        {
            driver = Comandos.GetBrowserRemote(driver, ConfigurationManager.AppSettings["browser"]);//, ConfigurationManager.AppSettings["uri"]);
            //driver = Comandos.GetBrowserMobile(driver, ConfigurationManager.AppSettings["platform"], "Celular", ConfigurationManager.AppSettings["browser"], "http://localhost:4723/wd/hub");
            baseURL = "https://livros.inoveteste.com.br/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheCT01ValidarLayoutTelaTest()
        {
            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");
            // Valida o layout da tela
            Assert.AreEqual("Envie uma mensagem", driver.FindElement(By.CssSelector("h1")).Text);
            /*Assert.IsTrue(IsElementPresent(By.Name("your-name")));
            Assert.IsTrue(IsElementPresent(By.Name("your-email")));
            Assert.IsTrue(IsElementPresent(By.Name("your-subject")));
            Assert.IsTrue(IsElementPresent(By.Name("your-message")));
            Assert.IsTrue(IsElementPresent(By.CssSelector("input.wpcf7-form-control.wpcf7-submit")));*/

            //Page Object
            // Usando PageFactory (Obsoleto) ************
            /*Contato contato = new Contato();
            PageFactory.InitElements(driver, contato);

            Assert.IsTrue(contato.name.Enabled);
            Assert.IsTrue(contato.email.Enabled);
            Assert.IsTrue(contato.subject.Enabled);
            Assert.IsTrue(contato.message.Enabled);
            Assert.IsTrue(contato.enviar.Enabled);
            */

            // Sem usar PageFactory (Obsoleto) ************
            Contato contato = new Contato(driver);
            contato.VerificarExistenciaCampos();

        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
