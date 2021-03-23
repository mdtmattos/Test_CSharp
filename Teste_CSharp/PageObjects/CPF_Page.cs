using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

namespace Teste_CSharp.PageObjects
{
    class CPF_Page
    {
        private RemoteWebDriver _driver;
     
        public CPF_Page(RemoteWebDriver driver) => _driver = driver;
        IWebElement BtnValidadorCpf => _driver.FindElementByXPath("//body/div[1]/div[1]/div[2]/div[5]/div[1]/div[6]/a[1]");
        IWebElement BtnValidarCPF => _driver.FindElementById("gerar");
        IWebElement InserirCpf => _driver.FindElementById("cpf");
        IWebElement MsgCPFValido => _driver.FindElementById("resposta1");
        IWebElement MsgCPFInvalido => _driver.FindElementById("resposta2");

        public void ClicarBtnValidarCPF()
        {
            BtnValidarCPF.Click();
        }

        public void ClicarBtnValidadorCPF()
        {
            BtnValidadorCpf.Click();
        }
        public void ValidarTituloPaginaCPF()
        {
            Assert.AreEqual("Validar CPF", _driver.FindElement(By.XPath("//h3[contains(text(),'Validar CPF')]")).Text);
        }

        public void InserirCPFInvalido()
        {
            InserirCpf.SendKeys(ConfigurationManager.AppSettings["cpfInvalido"]);
        }
        public void InserirCPFValido()
        {
            InserirCpf.SendKeys(ConfigurationManager.AppSettings["cpfValido"]);
        }
        public void ValidarMensagemCPFinvalido()
        {
            Assert.AreEqual("O CPF é inválido", _driver.FindElement(By.Id("resposta2")).Text);
        }
        public void ValidarMensagemCPFValido()
        {
            Assert.IsTrue(MsgCPFValido.Displayed);
        }
    }
}
