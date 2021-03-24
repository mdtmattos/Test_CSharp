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
    public class ConsultarCEP
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;
        private StringBuilder verificationErrors;
        private string baseURL;
        private string baseURLCEP;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = TestConfig.GetBrowserRemoteMobile(driver, ConfigurationManager.AppSettings["browser"], ConfigurationManager.AppSettings["uri"]);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            baseURL = "https://buscacepinter.correios.com.br/app/localidade_logradouro/index.php";
            baseURLCEP = "https://buscacepinter.correios.com.br/app/endereco/index.php";
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
        public void ConsultarCEPPorLogradouro()
        {
            driver.Navigate().GoToUrl(baseURL);
            CEP_Page cep_page = new CEP_Page(driver);

            cep_page.ValidarCampos();
            cep_page.SelecionarUF();
            cep_page.DigitarLocalidade();
            cep_page.DigitarLogradouro();
            cep_page.ClicarBtnBuscar();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("resultado-DNEC")));
            cep_page.ValidarResultadoCEP();

        }
        [Test]
        [Obsolete]
        public void ConsultarPorCEP()
        {
            driver.Navigate().GoToUrl(baseURLCEP);
            CEP_Page cep_page = new CEP_Page(driver);

            cep_page.ValidarCamposCEP();
            cep_page.DigitarCEP();
            cep_page.ClicarBtnBuscar();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("resultado-DNEC")));
            cep_page.ValidarCEP();
        }
        [Test]
        [Obsolete]
        public void ConsultaCEPInvalido()
        {
            driver.Navigate().GoToUrl(baseURLCEP);
            CEP_Page cep_page = new CEP_Page(driver);

            cep_page.ValidarCamposCEP();
            cep_page.DigitarCEPInvalido();
            cep_page.ClicarBtnBuscar();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("mensagem-resultado")));
            cep_page.ValidarMensagemResultado();
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
