using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using InoveTest.Page_Object;
using InoveTest;
using System.Configuration;
using OpenQA.Selenium.Remote;

namespace ST01Contato
{
    [TestFixture]
    public class CT03EnviarMensagem
    {
        private RemoteWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private IJavaScriptExecutor js;
        CT01ValidarLayoutTela ct01;

        [SetUp]
        public void SetupTest()
        {
            driver = Comandos.GetBrowserRemote(driver, ConfigurationManager.AppSettings["browser"]);//, ConfigurationManager.AppSettings["uri"]);
            driver.Manage().Window.Maximize();
            baseURL = "https://livros.inoveteste.com.br/";
            verificationErrors = new StringBuilder();
            ct01 = new CT01ValidarLayoutTela();
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
        public void TheCT03EnviarMensagemTest()
        {
            //Pré-Requisito: Validar layout da tela
            ct01.SetupTest();
            ct01.TheCT01ValidarLayoutTelaTest();
            ct01.TeardownTest();

            // Acessa o site
            driver.Navigate().GoToUrl(baseURL + "/contato");
            // Preenche todos os campos do formulário ( Dados parametrizados no App.Config )
            driver.FindElement(By.Name("your-name")).Clear();
            driver.FindElement(By.Name("your-name")).SendKeys(ConfigurationManager.AppSettings["nome"]);
            driver.FindElement(By.Name("your-email")).Clear();
            driver.FindElement(By.Name("your-email")).SendKeys(ConfigurationManager.AppSettings["email"]);
            driver.FindElement(By.Name("your-subject")).Clear();
            driver.FindElement(By.Name("your-subject")).SendKeys(ConfigurationManager.AppSettings["assunto"]);
            driver.FindElement(By.Name("your-message")).Clear();
            driver.FindElement(By.Name("your-message")).SendKeys(ConfigurationManager.AppSettings["mensagem"]);
            Thread.Sleep(5000);
            // Clica no botão Enviar após preencher todos os campos obrigatórios
            //Comandos.ExecuteJavaScript(driver, "document.querySelector('input.wpcf7-form-control.wpcf7-submit').click();");

            //Usando PageObject para clicar no botão enviar
            Contato contato = new Contato(driver);
            contato.ClicarBtnEnviar();

            // Valida a mensagem de sucesso do envio da mensagem.
            Thread.Sleep(5000);
            Assert.AreEqual("Agradecemos a sua mensagem. Responderemos em breve.", driver.FindElement(By.XPath("//body/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/form[1]/div[2]")).Text);
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
