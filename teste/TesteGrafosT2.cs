using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrafoEDA;

namespace Teste
{
    [TestClass]
    public class TesteGrafosT2
    {
        [TestMethod]
        public void TesteArco()
        {
            Arco a = new Arco();
            a.Info = 3;
            a.Origem = new Node("A");
            Assert.AreEqual(3, a.Info);
        }

        [TestMethod]
        public void TesteNoh()
        {
            Node n1 = new Node("A");
            Node n2 = new Node("B");
            Arco a = new Arco() {
                Origem = n1,
                Destino = n2,
                Info = null
            };
            n1.AdicionarArco(a);
            Assert.AreEqual("B", n1.Arcos[0].Destino.Nome);
        }

        [TestMethod]
        public void TesteGrafo()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarNo("E");
            g.AdicionarArco("A","B");
            g.AdicionarArco("A", "E");
            g.AdicionarArco("B", "D");
            g.AdicionarArco("C", "A");
            g.AdicionarArco("C", "B");
            g.AdicionarArco("E", "C");
            g.AdicionarArco("E", "D");
            Assert.AreEqual(true, g.EhVizinho("B", "A"));
            Assert.AreEqual(false, g.EhVizinho("A", "B"));
            Assert.AreEqual(true, g.EhVizinho("A", "C"));
        }

        [TestMethod]
        public void TesteCaminho()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarNo("E");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("A", "E");
            g.AdicionarArco("B", "D");
            g.AdicionarArco("C", "A");
            g.AdicionarArco("C", "B");
            g.AdicionarArco("E", "C");
            g.AdicionarArco("E", "D");
            g.AdicionarArco("D", "C");
            string[] caso1 = { "A", "B", "D", "C"};
            Assert.AreEqual(true, g.EhCaminho(caso1));
            string[] caso2 = { "A", "B", "E"};
            Assert.AreEqual(false, g.EhCaminho(caso2));
            string[] caso3 = { "A", "B", "D", "C", "A" };
            Assert.AreEqual(true, g.EhCaminho(caso3));
        }

        [TestMethod]
        public void TestePasseioLargura()
        {
            Grafo g = new Grafo();
            g.AdicionarNo("A0");
            int cj = 0;
            for (int i = 0; i < 5; i++)
            {
                string nome_i = "B" + i.ToString();
                g.AdicionarNo(nome_i);
                g.AdicionarArco("A0", nome_i);

                for (int j = 0; j < 5; j++)
                {
                    string nome_j = "C" + cj.ToString();
                    g.AdicionarNo(nome_j);
                    g.AdicionarArco(nome_i, nome_j);
                    cj++;
                }
            }

            string final = "A0,";
            for (int i = 0; i < 5; i++)
                final += "B" + i.ToString() + ",";
            for (int i = 0; i < 25; i++)
                final += "C" + i.ToString() + ",";
            final += ",";
            final = final.Replace(",,", "");

            Assert.AreEqual(final, g.Passeio("A0"));
        }

        /// <summary>
        /// Criado para a prova. Cria um grafo usado por algumas questões. 
        /// </summary>
        private Grafo CriaGrafoProva(bool direcionado)
        {
            Grafo g = new Grafo(direcionado);
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarNo("E");
            g.AdicionarNo("F");
            g.AdicionarNo("G");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("B", "F");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "E");
            g.AdicionarArco("E", "A");
            g.AdicionarArco("F", "G");
            g.AdicionarArco("G", "C");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("D", "B");
            return g;
        }

        /// <summary>
        /// Criado para a prova. Cria um grafo usado por algumas questões. 
        /// </summary>
        private Grafo CriaGrafoCompleto(bool direcionado)
        {
            Grafo g = new Grafo(direcionado);
            g.AdicionarNo("A");
            g.AdicionarNo("B");
            g.AdicionarNo("C");
            g.AdicionarNo("D");
            g.AdicionarArco("A", "B");
            g.AdicionarArco("A", "C");
            g.AdicionarArco("A", "D");
            g.AdicionarArco("B", "A");
            g.AdicionarArco("B", "C");
            g.AdicionarArco("B", "D");
            g.AdicionarArco("C", "A");
            g.AdicionarArco("C", "B");
            g.AdicionarArco("C", "D");
            g.AdicionarArco("D", "A");
            g.AdicionarArco("D", "B");
            return g;
        }

        /// <summary>
        /// Teste do construtor especial para criar grafos não direcionados (parâmetro direcionado = false). 
        /// Vale 2 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_2P_TesteNaoDirecionado()
        {
            Grafo g = CriaGrafoProva(false);
            string[] caso1 = { "A", "B", "A", "E", "D" };
            Assert.AreEqual(true, g.EhCaminho(caso1));
            string[] caso2 = { "A", "B", "E", "C" };
            Assert.AreEqual(false, g.EhCaminho(caso2));
        }

        /// <summary>
        /// Em grafos não direcionados a soma dos graus de todos os vértices é igual ao dobro do número de arestas.
        /// A função VerificaConsistencia avalia se o grafo atende esse requisito. 
        /// Vale 2 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_3P_TesteConsistencia()
        {
            Grafo g = CriaGrafoProva(false);
            bool lol = g.VerificaConsistencia();
            Assert.AreEqual(true, g.VerificaConsistencia());
            g.AdicionarNo("Z");
            g.RetornaNo("Z").Arcos.Add(new Arco());
            Assert.AreEqual(false, g.VerificaConsistencia());
        }

        /// <summary>
        /// Teste do método GrafoCompleto. Grafo completo é o grafo simples em que, para cada vértice do grafo, 
        /// existe uma aresta conectando este vértice a cada um dos demais. Ou seja, todos os vértices do grafo 
        /// possuem mesmo grau. 
        /// Vale 2 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_2P_TesteGrafoCompleto()
        {
            Grafo g = CriaGrafoCompleto(true);
            Assert.AreEqual(false, g.GrafoCompleto());
            g.AdicionarArco("D", "C");
            Assert.AreEqual(true, g.GrafoCompleto());
        }

        /// <summary>
        /// Verifica quantos caminhos possíveis existem entre dois nós de um grafo.
        /// Vale 3 pontos. 
        /// </summary>
        [TestMethod]
        public void Prova_3P_CaminhosPossiveis()
        {
            Grafo g = CriaGrafoProva(false);
            int a = g.CaminhosPossiveis("A", "F");
            Assert.AreEqual(6, g.CaminhosPossiveis("A", "F"));
            Assert.AreEqual(5, g.CaminhosPossiveis("A", "B"));
        }
    }
}
