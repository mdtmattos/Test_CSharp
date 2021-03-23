using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Teste_CSharp.PageObjects
{
    class CEP_Page
    {
        private RemoteWebDriver _driver;
        public CEP_Page(RemoteWebDriver driver) => _driver = driver;
        //Elementos
        IWebElement Uf => _driver.FindElementById("uf");
        IWebElement localidade => _driver.FindElementById("localidade");
        IWebElement tipo => _driver.FindElementById("tipologradouro");
        IWebElement logradouro => _driver.FindElementById("logradouro");
        IWebElement numero => _driver.FindElementById("numeroLogradouro");
        IWebElement btnBuscar => _driver.FindElementById("btn_pesquisar");
        IWebElement tituloPagina => _driver.FindElementById("titulo_tela");
        IWebElement tabela => _driver.FindElementById("resultado-DNEC");
        IWebElement endereco => _driver.FindElementById("endereco");
        IWebElement menuCEP => _driver.FindElementById("menu");
        IWebElement campoCEP => _driver.FindElementByName("tipoCEP");
        IWebElement mensagemresultado => _driver.FindElementById("mensagem-resultado");

        //Metodos
        public void SelecionarUF()
        {
            new SelectElement(_driver.FindElement(By.Id("uf"))).SelectByText("PR");
        }
        public void DigitarLocalidade()
        {
            localidade.SendKeys("Curitiba");
        }
        public void DigitarLogradouro()
        {
            logradouro.SendKeys("Parintins");
        }
        public void DigitarCEP()
        {
            endereco.SendKeys("80320-110");
        }
        public void DigitarCEPInvalido()
        {
            endereco.SendKeys("8888888888");
        }
        public void ValidarResultadoCEP()
        {
            Assert.IsTrue(tabela.Displayed);
        }
        public void ValidarCampos()
        {
            Assert.IsTrue(tituloPagina.Displayed);
            Assert.IsTrue(Uf.Displayed);
            Assert.IsTrue(localidade.Displayed);
            Assert.IsTrue(tipo.Displayed);
            Assert.IsTrue(logradouro.Displayed);
            Assert.IsTrue(numero.Displayed);
            Assert.IsTrue(btnBuscar.Displayed);
        }
        public void ValidarCamposCEP()
        {
            Assert.IsTrue(tituloPagina.Displayed);
            Assert.IsTrue(endereco.Displayed);
            Assert.IsTrue(campoCEP.Displayed);
            Assert.IsTrue(btnBuscar.Displayed);
        }
        public void ValidarCEP()
        {
            var table = _driver.FindElement(By.Id("resultado-DNEC"));
            var rows = table.FindElements(By.TagName("tr"));
            foreach (var row in rows)
            {
                if (row.Text.Contains("Rua Professor Veríssimo Antônio de Souza"))
                {
                    Assert.AreEqual("Rua Professor Veríssimo Antônio de Souza", _driver.FindElement(By.XPath("//td[contains(text(),'Rua Professor Veríssimo Antônio de Souza')]")).Text);
                }
            }
        }
        public void ValidarMensagemResultado()
        {
            Assert.AreEqual("Não há dados a serem exibidos", mensagemresultado.Text);
        }

        public void ClicarBtnBuscar()
        {
            btnBuscar.Click();
        }
        public void ClicarMenuCEP()
        {
            menuCEP.Click();
        }

    }
}
