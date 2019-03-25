using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrafoEDA;
using System.Collections.Generic;

namespace Teste
{
    [TestClass]
    public class TesteGrafosT1
    {
        
        [TestMethod]
        public void TesteConexo()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "A");

            Assert.AreEqual(true, g.Conexo());

            g.AdicionarArco("A", "D");
            g.RemoverArco("D", "A");
            Assert.AreEqual(false, g.Conexo());
        }
        
        [TestMethod]
        public void TesteMenorCaminho()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "D");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "C");
            g.AdicionarArco("D", "B");

            Assert.AreEqual("A,D,B", g.MenorCaminho("A", "B"));

            g.RemoverArco("A", "D");

            Assert.AreEqual("A,C,D,B", g.MenorCaminho("A", "B"));

            g.RemoverArco("D", "B");
            Assert.AreEqual("", g.MenorCaminho("A", "B"));
        }

        [TestMethod]
        public void TesteNaoDirigido()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("C", "A");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("C", "B");

            Assert.AreEqual(false, g.EhDirigido());

            g.RemoverArco("C", "A");

            Assert.AreEqual(true, g.EhDirigido());
        }

        [TestMethod]
        public void TesteNivel()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "D");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "C");
            g.AdicionarArco("D", "B");

            Assert.AreEqual(2, g.Nivel("A", "B"));

            g.RemoverArco("A", "D");

            Assert.AreEqual(3, g.Nivel("A", "B"));
        }

        [TestMethod]
        public void TesteNivel_2()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarNo("E");
            g.AdicionarNo("F");
            g.AdicionarNo("G");
            g.AdicionarNo("H");
            g.AdicionarArco("A", "D");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("C", "E");
            g.AdicionarArco("D", "F");
            g.AdicionarArco("D", "B");
            g.AdicionarArco("D", "H");
            g.AdicionarArco("C", "A");
            g.AdicionarArco("H", "B");

            Assert.AreEqual("B,E,F,H", g.NodesNivel(2, "A"));
        }

        [TestMethod]
        public void TesteRemoveNo()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "D");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "C");
            g.AdicionarArco("D", "B");
            Assert.AreEqual("D", g.Arcos[0].Destino.Nome);

            g.RemoverNo("D");
            Assert.AreEqual("C", g.Arcos[0].Destino.Nome);
        }

        [TestMethod]
        public void TesteRegioes() //Só funciona se não houver arcos cruzados
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "A");
            g.AdicionarArco("A", "C");
            Assert.AreEqual(3, g.CalcRegiao());
            
            g.AdicionarNo("E");
            g.AdicionarNo("F");
            g.AdicionarNo("G");
            g.AdicionarNo("H");
            g.AdicionarArco("E", "F");
            g.AdicionarArco("F", "G");
            g.AdicionarArco("G", "H");
            g.AdicionarArco("H", "E");
            g.AdicionarArco("E", "G");
            Assert.AreEqual(5, g.CalcRegiao());

            g.AdicionarArco("E", "A");
            g.AdicionarArco("B", "F");
            Assert.AreEqual(6, g.CalcRegiao());

            g.AdicionarArco("E", "B");
            Assert.AreEqual(7, g.CalcRegiao());
        }

        [TestMethod]
        public void TesteNaoDirigido_2()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("B", "A");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "A");
            Assert.AreEqual(true, g.EhDirigido());

            g.ConverteNaoDirigido();
            Assert.AreEqual(false, g.EhDirigido());
        }

        [TestMethod]
        public void TesteRegioesConexas()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarNo("E");
            g.AdicionarNo("F");
            g.AdicionarNo("G");
            g.AdicionarNo("H");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "A");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("E", "F");
            g.AdicionarArco("F", "G");
            g.AdicionarArco("G", "H");
            g.AdicionarArco("H", "E");      
            Assert.AreEqual("{A,B,C,D}{E,F,G,H}", g.RegioesConexas());
            
            g.RemoverArco("H", "E");
            Assert.AreEqual("{A,B,C,D}", g.RegioesConexas());

            g.AdicionarArco("G", "E");
            Assert.AreEqual("{A,B,C,D}{E,F,G,H}", g.RegioesConexas());
        }

        [TestMethod]
        public void TesteEuleriano()
        {
            Grafo g = new Grafo(false);
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "A");
            g.AdicionarArco("A", "C");

            string lll = g.EncontraEulerianoLargura();
            
            Assert.AreEqual("C,B,A,D,C,A", g.EncontraEulerianoLargura());

            g.RemoverArco("A", "C");
            string ashu = g.EncontraEulerianoLargura();
            Assert.AreEqual("A,B,C,D,A", g.EncontraEulerianoLargura());

            g.AdicionarArco("A", "C");
            g.AdicionarArco("B", "D");
            Assert.AreEqual("", g.EncontraEulerianoLargura());
        }

        /// <summary>
        /// Criado para a prova. Cria um grafo usado por todas as questões. 
        /// </summary>
        private Grafo CriaGrafoProva()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarNo("E");
            g.AdicionarNo("F");
            g.AdicionarNo("G");
            g.AdicionarNo("H");
            g.AdicionarNo("I");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("A", "D");
            g.AdicionarArco("A", "H");
            g.AdicionarArco("B", "F");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "E");
            g.AdicionarArco("D", "G");
            g.AdicionarArco("E", "A");
            g.AdicionarArco("E", "C");
            g.AdicionarArco("E", "I");
            g.AdicionarArco("F", "G");
            g.AdicionarArco("G", "A");
            g.AdicionarArco("G", "C");
            g.AdicionarArco("H", "B");
            g.AdicionarArco("H", "I");
            g.AdicionarArco("I", "B");
            return g;
        }

        /// <summary>
        /// Teste do método remover arco, capaz de remover um arco do grafo. 
        /// Vale 2 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_2P_TesteArcos()
        {
            Grafo g = CriaGrafoProva();
            Assert.AreEqual(true, g.EhVizinho("I", "H"));
            g.RemoverArco("H", "I");
            Assert.AreEqual(false, g.EhVizinho("I", "H"));
        }


        /// <summary>
        /// Teste do método MenorCaminho, capaz de retornar o menor caminho em um grafo SEM PESOS NOS ARCOS. 
        /// Vale 4 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_4P_TesteExisteCaminho()
        {
            Grafo g = CriaGrafoProva();
            Assert.AreEqual(true, g.ExisteCaminho("A", "I"));
            Assert.AreEqual(true, g.ExisteCaminho("B", "E"));
            g.RemoverArco("E", "I");
            Assert.AreEqual(false, g.ExisteCaminho("A", "K"));
        }

        /// <summary>
        /// Teste do método ExisteCiclo, capaz de identificar se um grafo possui ou não algum ciclo.
        /// Vale 4 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_4P_TesteCiclico()
        {
            Grafo g = CriaGrafoProva();
            Assert.AreEqual(true, g.ExisteCiclo());
            g.RemoverArco("G", "A");
            g.RemoverArco("E", "A");
            g.RemoverArco("D", "G");
            g.RemoverArco("E", "C");
            g.RemoverArco("E", "I");
            Assert.AreEqual(false, g.ExisteCiclo());
        }


    }
}