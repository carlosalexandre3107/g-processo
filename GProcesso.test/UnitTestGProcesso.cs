using System;
using System.Collections.Generic;
using System.Linq;
using GProcessos.Dominio;
using GProcessos.Serializado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GProcesso.Teste
{
    [TestClass]
    public class UnitTestGProcesso
    {
        //1) Calcular a soma dos processos ativos. A aplicação deve retornar R$ 1.087.000,00
        [TestMethod]
        public void TestProcessosAtivos()
        {
            Arquivo _arquivo = new Arquivo();
            List<Processo> _processos = _arquivo.DeserializarProcessos();
            Decimal _total = (from x in _processos where x.Ativo select x.Valor).ToList().Sum();
            Assert.AreEqual(_total, 1087000);
        }
        
        //2) Calcular a a média do valor dos processos no Rio de Janeiro para o Cliente "Empresa A". 
        //A aplicação deve retornar R$ 110.000,00.
        [TestMethod]
        public void TestMediaValorProcesso()
        {
            Arquivo _arquivo = new Arquivo();
            List<Processo> _processos = _arquivo.DeserializarProcessos();
            Decimal _media = (from x in _processos where x.Estado == "Rio de Janeiro" && x.Empresa == "Empresa A" select x).ToList().Average(c => c.Valor);
            Assert.AreEqual(_media, 110000);
        }

        //3) Calcular o Número de processos com valor acima de R$ 100.000,00. A aplicação deve
        //retornar 2.
        [TestMethod]
        public void TestNumerosProcessoPorValor()
        {
            Arquivo _arquivo = new Arquivo();
            List<Processo> _processos = _arquivo.DeserializarProcessos();
            Decimal _numProcessos = _processos.Count(c => c.Valor > 100000);
            Assert.AreEqual(_numProcessos, 2);
        }

        //4) Obter a lista de Processos de Setembro de 2007. A aplicação deve retornar uma lista
        //com somente o Processo “00010TRABAM”.
        [TestMethod]
        public void TestListarProcessosPorDataInicio()
        {
            Arquivo _arquivo = new Arquivo();
            List<Processo> _processosDados = _arquivo.DeserializarProcessos();
            Processo _processo = (from x in _processosDados where x.DataInicio.Month == 9 && x.DataInicio.Year == 2007 select x).ToList().First();
            Assert.AreEqual(_processo.Numero, "00010TRABAM");
        }

        //5)Obter a lista de processos no mesmo estado do cliente, para cada um dos clientes. 
        //A aplicação deve retornar uma lista com os processos de número
        //“00001CIVELRJ”,”00004CIVELRJ” para o Cliente "Empresa A" e
        //“00008CIVELSP”,”00009CIVELSP” para o o Cliente "Empresa B".
        [TestMethod]
        public void TestListarProcessosEstadoCliente()
        {
            Arquivo _arquivo = new Arquivo();
            List<Processo> _processos = _arquivo.DeserializarProcessos();
            List<String> _numeroProcesso = (from x in _processos where x.Empresa == "Empresa A" && x.Estado == "Rio de Janeiro" 
                                                 || x.Empresa == "Empresa B" && x.Estado == "São Paulo" select x.Numero).ToList();

            List<String> _numerosProcessoTest = new List<String>();
            _numerosProcessoTest.Add("00001CIVELRJ");
            _numerosProcessoTest.Add("00004CIVELRJ");
            _numerosProcessoTest.Add("00008CIVELSP");
            _numerosProcessoTest.Add("00009CIVELSP");

            CollectionAssert.AreEqual(_numeroProcesso, _numerosProcessoTest);

        }

        //6) Obter a lista de processos que contenham a sigla “TRAB”. A aplicação deve retornar uma
        //lista com os processos “00003TRABMG” e “00010TRABAM”.
        [TestMethod]
        public void TestListarProcessosSiglas()
        {
            Arquivo _arquivo = new Arquivo();
            List<Processo> _processos = _arquivo.DeserializarProcessos();
            List<String> _numeroProcesso = (from x in _processos where x.Numero.Contains("TRAB") select x.Numero).ToList();
            List<String> _numerosProcessoTest = new List<String>();
            _numerosProcessoTest.Add("00003TRABMG");
            _numerosProcessoTest.Add("00010TRABAM");
            CollectionAssert.AreEqual(_numeroProcesso, _numerosProcessoTest);
        }
    }
}
