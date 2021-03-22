using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Threading;
using Teste_CSharp.PageObjects;
using Teste_CSharp.Util;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Configuration;
using OpenQA.Selenium;
using Teste_CSharp;

namespace Teste_CSharp
{
    [TestFixture]
    public class ValidarCPF
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = Biblioteca.GetBrowserRemote(driver, ConfigurationManager.AppSettings["browser"]);//, ConfigurationManager.AppSettings["uri"]);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            baseURL = "https://clevert.com.br/t/";
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
            NUnit.Framework.Assert.AreEqual("", verificationErrors.ToString());
        }
        [Test]
        [Obsolete]
        public void Cpf_Valido()
        {
            driver.Navigate().GoToUrl(baseURL);
            CPF_Page cpf_page = new CPF_Page(driver);

            cpf_page.ClicarBtnValidadorCPF();
            cpf_page.ValidarTituloPaginaCPF();
            cpf_page.InserirCPFValido();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("resposta1")));
            cpf_page.ValidarMensagemCPFValido();

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

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
