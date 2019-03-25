using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoEDA
{
    public class Grafo
    {
        #region Atributos e Propriesdades
        private List<Node> nos;
        private List<Arco> arcos;
        private string passeio;
        private int regioes;
        private bool direcionado;
        private int caminhos;

        public List<Arco> Arcos
        {
            get { return arcos; }
        }

        public List<Node> Nos
        {
            get { return nos; }
        }
        #endregion

        #region Construtores
        public Grafo() : this(true) { }

        public Grafo(bool direcionado)
        {
            this.direcionado = direcionado;
            nos = new List<Node>();
            arcos = new List<Arco>();
            passeio = string.Empty;
            regioes = 1;
            caminhos = 0;
        }
        #endregion

        #region Métodos Privados
        private Node BuscaEmLargura(Node n, string n_destino)
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(n);
            NodeAcess(n, null);

            if (n_destino != null) { n.Caminho += n.Nome; }
            
            while (q.Count > 0)
            {
                n = ((Node)(q.Dequeue()));

                foreach (Arco a in n.Arcos)
                {
                    if (!a.Destino.Visit)
                    {
                        NodeAcess(a.Destino, n);
                        q.Enqueue(a.Destino);
                    }
                    else { regioes++; }
                    if (a.Destino.Nome == n_destino)
                    {
                        return a.Destino;
                    }
                }
            }
            return null;
        }
        
        private Node BuscaEmProfundidade(string nome, Node n)
        {
            if (n == null || n.Nome == nome) { return n; }
            n.Visit = true;
            foreach (Arco a in n.Arcos)
            {
                if (!a.Destino.Visit)
                {
                    Node aux = BuscaEmProfundidade(nome, a.Destino);
                    if (aux != null) { return aux; }
                }
            }
            return null;
        }

        private void NodeAcess(Node n, Node nParent)
        {
            n.Visit = true;
            passeio += n.Nome + ",";
            if (nParent != null)
            {
                n.Nivel += nParent.Nivel + 1;
                n.Caminho += nParent.Caminho + "," + n.Nome;
            }
        }
        
        private bool FullAccess(Node node)
        {
            string s = PasseioEmLargura(node.Nome);
            foreach (Node n in nos)
            {
                if (!s.Contains(n.Nome)) { return false; }
            }
            return true;
        }       

        private void LimpaGrafo()
        {
            LimpaVisit();
            LimpaCaminho();
            LimpaNivel();
            LimpaPasseio();
            LimpaRegiao();
            
        }

        private void LimpaVisit()
        {
            foreach (Node n in nos) { n.Visit = false; }
        }

        private void LimpaCaminho()
        {
            foreach (Node n in nos) { n.Caminho = string.Empty; }
            caminhos = 0;
        }

        private void LimpaNivel()
        {
            foreach (Node n in nos) { n.Nivel = 0; }
        }

        private void LimpaPasseio() { passeio = string.Empty; }

        private void LimpaRegiao() { regioes = 1; }

        private Node BuscaNo(string nome)
        {
            foreach (Node n in nos)
            {
                if (n.Nome == nome) { return n; }
            }
            return null;
        }

        private Arco BuscaArco(string n_origem, string n_destino)
        {
            Node origem = BuscaNo(n_origem);
            Node destino = BuscaNo(n_destino);
            foreach(Arco a in origem.Arcos)
            {
                if (a.Destino == destino) { return a; }
            }
            return null;
        }

        private void Caminhos(Node n, string n_destino)
        {
            if (n.Nome == n_destino) { caminhos++; }

            n.Visit = true;
            foreach (Arco a in n.Arcos)
            {
                if (!a.Destino.Visit)
                {
                    Caminhos(a.Destino, n_destino);
                    a.Destino.Visit = false;
                }
            }
        }

        private bool NoCiclico(Node n)
        {
            foreach (Arco a in n.Arcos)
            {
                if (n == BuscaEmProfundidade(n.Nome, a.Destino)) { return true; }
            }
            return false;
        }

        // Verifica se o Grafo é Euleriano
        private bool EhEuleriano()
        {
            if (arcos.Count == 0) return false;
            return (RaizEuler() != null);
        }

        private Node RaizEuler()
        {
            int impares = 0;
            Node raiz = null;
            foreach (Node n in nos)
            {
                int grau = 0;
                grau += n.Arcos.Count;
                // Verifica o grau do nó 
                if (grau % 2 == 1)
                {
                    impares++;
                    raiz = n;
                }
            }
            if (impares == 0) { raiz = nos[0]; }
            if (impares <= 2) { return raiz; }
            return null;
        }

        // Verifica se já existe esse arco na árvore de busca
        private bool ArcoExiste(Node n, string arco)
        {
            // Raiz da árvore
            if (n.Arcos.Count == 0) { return false; }

            do
            {
                if ((string)n.Arcos[0].Info == arco) { return true; }
                n = n.Arcos[0].Destino;
            } while (n.Arcos.Count > 0);

            return false;
        }

        // Monta o caminho final euleriano
        private string CaminhoEuleriano(Node n)
        {
            string caminho = string.Empty;
            do
            {
                caminho = n.Nome + "," + caminho;
                n = n.Arcos[0].Destino;
            } while (n.Arcos.Count > 0);
            caminho = n.Nome + "," + caminho + ",";
            caminho = caminho.Replace(",,", "");

            return caminho;
        }

        #endregion
        
        #region Métodos Publicos
        public void AdicionarNo(string nome, object info) 
        {
            if (BuscaNo(nome) == null) { nos.Add(new Node(nome, info)); }
            else { throw new Exception("Nó " + nome + " já existente !"); }
        }

        public void AdicionarNo(string nome) { AdicionarNo(nome, null); }

        public void RemoverNo(string nome)
        {
            Node n = BuscaNo(nome);
            if (n != null)
            {
                List<Arco> l = new List<Arco>();
                foreach (Arco a in arcos)
                {
                    if (a.Destino == n || a.Origem == n) { l.Add(a); }
                }
                foreach (Arco a in l) { arcos.Remove(a); }
                nos.Remove(n);
            }
            else { throw new Exception("Nó " + nome + " não existente !"); }
        }

        public void AdicionarArco(string origem, string destino, object info)
        {      
            Node or = BuscaNo(origem);
            Node de = BuscaNo(destino);
            if (info == null) { info = origem + destino; }

            if (or != null && de != null)
            {
                Arco a = new Arco(or, de, info);
                arcos.Add(a);
                or.AdicionarArco(a);
                if(!direcionado)
                {
                    a = new Arco(de, or, info);
                    arcos.Add(a);
                    de.AdicionarArco(a);
                }
            }
            else { throw new Exception("Um dos Nós não existem: " + origem + ", " + destino); }
        }

        public void AdicionarArco(string n_origem, string n_destino) { AdicionarArco(n_origem, n_destino, null); }
        
        public void RemoverArco(string n_origem, string n_destino)
        {
            Node origem = BuscaNo(n_origem);
            Node destino = BuscaNo(n_destino);

            if (destino != null && origem != null)
            {
                Arco aux = BuscaArco(n_origem, n_destino);
                if (aux != null)
                {
                    origem.Arcos.Remove(aux);
                    arcos.Remove(aux);

                    if (!direcionado) { RemoverArco(n_destino, n_origem); }
                }
            }
            else { throw new Exception("Um dos Nós não existem: " + origem + ", " + destino); }
        }

        public void ConverteNaoDirigido()
        {
            if (EhDirigido())
            {
                int count = arcos.Count;
                for (int i = 0; i < count; i++)
                {
                    Arco a = arcos[i];
                    bool add = true;
                    foreach (Arco a1 in arcos)
                    {
                        if (a.Origem == a1.Destino && a.Destino == a1.Origem)
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add) { AdicionarArco(a.Destino.Nome, a.Origem.Nome, a.Info); }
                }
            }
        }

        public bool EhVizinho(string n_destino, string n_origem)
        {
            Node n = BuscaNo(n_origem);
            if (n != null)
            {
                foreach (Arco a in n.Arcos)
                {
                    if (a.Destino.Nome == n_destino) { return true; }
                }
            }
            return false;
        }       

        public bool EhCaminho(string[] nos)
        {
            bool r = false;
            for (int i = 0; i < nos.Length - 1; i++ )
            {
                Node node = BuscaNo(nos[i]);
                foreach (Arco a in node.Arcos)
                {
                    if (a.Destino.Nome == nos[i + 1])
                    {
                        r = true;
                        break;
                    }
                    else { r = false; }
                }
            }
            return r;
        }

        public string PasseioEmLargura(string nome)
        {
            BuscaEmLargura(BuscaNo(nome), null);
            passeio += ",";
            passeio = passeio.Replace(",,", "");
            string p = passeio;
            LimpaGrafo();
            return p;
        }                

        public bool ExisteCaminho(string n_origem, string n_destino)
        {
            Node n = BuscaEmProfundidade(n_destino, BuscaNo(n_origem));
            LimpaGrafo();
            if (n == null) { return false; }
            return true;
        }

        public bool ExisteCiclo()
        {
            bool r = false;
            foreach(Node n in nos)
            {
                r = NoCiclico(n);
                if (r) { break; }
            }
            LimpaGrafo();
            return r;
        }

        public bool Conexo()
        {   
            foreach(Node n in nos)
            {
                if (!FullAccess(n)) { return false; }
            }
            return true;
        }           

        public string MenorCaminho(string n_origem, string n_destino)
        {
            Node n = BuscaEmLargura(BuscaNo(n_origem), n_destino);
            string r = string.Empty;
            if (n != null)
                r = n.Caminho;
            LimpaGrafo();
            return r;
        }

        public bool EhDirigido()
        {
            foreach (Arco a in arcos)
            {
                bool r = true;
                foreach (Arco a1 in arcos)
                {
                    if (a.Destino.Nome == a1.Origem.Nome && a.Origem.Nome == a1.Destino.Nome)
                    {
                        r = false;
                        break;
                    }
                }
                if (r) { return true; }
            }
            return false;
        }

        public int Nivel(string n_origem, string n_destino)
        {
            int nivel = BuscaEmLargura(BuscaNo(n_origem), n_destino).Nivel;
            LimpaGrafo();
            return nivel;
        }

        public string NodesNivel(int nivel, string nome)
        {
            string r = string.Empty;
            BuscaEmLargura(BuscaNo(nome), null);
            foreach (Node n in nos)
            {
                if (n.Nivel == nivel) { r += n.Nome + ","; }
            }
            LimpaGrafo();
            r += ",";
            r = r.Replace(",,", "");
            return r;
        }

        public int CalcRegiao()
        {
            bool fullaccess = false;
            foreach (Node n in nos)
            {
                if (FullAccess(n))
                {
                    BuscaEmLargura(n, null);
                    fullaccess = true;
                    break;
                }
            }
            if (!fullaccess)
            {
                foreach (Node n in nos)
                {
                    if (!n.Visit) { BuscaEmLargura(n, null); }
                }
            }
            int r = regioes;
            LimpaGrafo();
            return r;
        }
            
        public string RegioesConexas()
        {
            string r = string.Empty;
            foreach (Node n in nos)
            {
                if (!passeio.Contains(n.Nome)) {
                    if (NoCiclico(n))
                    {
                        r += "{";
                        LimpaVisit();
                        BuscaEmLargura(n, null);
                        r += passeio + "}";
                        LimpaPasseio();
                    }
                }
            }
            r += ",";
            r = r.Replace(",}", "}");
            r = r.Replace("},", "}");
            LimpaGrafo();
            return r;
        }

        public bool VerificaConsistencia()
        {
            int soma = 0;
            int dobro = 0;
            foreach (Node n in nos)
            {
                soma += n.Arcos.Count;
            }
            dobro = arcos.Count;
            if (soma == dobro)
                return true;
            
            return false;
        }

        public Node RetornaNo(string n)
        {
            return BuscaNo(n);
        }

        public bool GrafoCompleto()
        {
            for(int i = 1; i < nos.Count; i++)
            {
                if(!(nos[i].Arcos.Count == nos[i - 1].Arcos.Count))
                {
                    return false;
                }
            }
            return true ;
        }

        public int CaminhosPossiveis(string n_origem, string n_destino)
        {
            int r = 0;
            Caminhos(BuscaNo(n_origem), n_destino);
            r = caminhos;
            LimpaGrafo();
            return r;
        }

        public string Passeio(string n)
        {
            return PasseioEmLargura(n);
        }
        
        public object EncontraEuleriano()
        {
            throw new NotImplementedException();
        }

        public object EncontraEulerianoRecursividade()
        {
            return EncontraEulerianoLargura();
        }

        public string EncontraEulerianoLargura()
        {
            if (!EhEuleriano()) { return string.Empty; }

            Queue<Node> q = new Queue<Node>();
            Node raiz = new Node(RaizEuler().Nome, 0);
            q.Enqueue(raiz);

            while (q.Count > 0)
            {
                // Retirando o nó da arvore da fila e buscando seu original no grafo
                Node no_arvore = q.Dequeue();
                Node no_grafo = BuscaNo(no_arvore.Nome);

                // Condição de parada
                if ((int)no_arvore.Info == arcos.Count / 2) { return CaminhoEuleriano(no_arvore); }

                // Adicionar os vizinhos na fila e na arvore
                foreach (Arco a in no_grafo.Arcos)
                {
                    if (!ArcoExiste(no_arvore, (string)a.Info))
                    {
                        Node filho = new Node(a.Destino.Nome, (int)no_arvore.Info + 1);
                        filho.Arcos.Add(new Arco(null, no_arvore, a.Info));
                        q.Enqueue(filho);
                    }
                }
            }
            return string.Empty;
        }

        #endregion


        public string CaminhoMinimo(string n_origem, string n_destino)
        {
            Grafo solucao = new Grafo();
            solucao.AdicionarNo(n_origem, 0);
            for(int i = 0; i < solucao.Nos.Count; i++)
            {
                int iaux = i;
                Node ns = solucao.Nos[i];
                Node no = this.BuscaNo(ns.Nome);
                Arco aux = null;
                foreach(Arco a in no.Arcos)
                {
                    Node destino = solucao.BuscaNo(a.Destino.Nome);
                    int arcoInfo = (int)a.Info + (int)ns.Info;
                        
                    if (aux != null)
                    {                            
                        int auxInfo = (int)aux.Info + (int)ns.Info;
                        if (destino == null)
                        {
                            if (arcoInfo == auxInfo) { i = iaux - 1; }
                            if (arcoInfo <= auxInfo) { aux = a; }
                        } 
                        else if (destino != null && auxInfo < (int)destino.Info)
                        {
                            if (arcoInfo == auxInfo) { i = iaux - 1; }
                            if (arcoInfo <= auxInfo) { aux = a; }
                            aux = a;
                            destino.Arcos.Clear();
                            destino.Info = arcoInfo;
                            solucao.AdicionarArco(aux.Destino.Nome, ns.Nome, aux.Info);
                        }
                    }
                    else
                    {
                        aux = a;
                        int auxInfo = (int)aux.Info + (int)ns.Info;
                        if (destino != null && auxInfo < (int)destino.Info)
                        {
                            destino.Arcos.Clear();
                            destino.Info = auxInfo;
                            solucao.AdicionarArco(aux.Destino.Nome, ns.Nome, aux.Info);
                        }

                    }                        
                }
                if (solucao.BuscaNo(aux.Destino.Nome) == null)
                {
                    solucao.AdicionarNo(aux.Destino.Nome, (int)aux.Info + (int)ns.Info);
                    solucao.AdicionarArco(aux.Destino.Nome, ns.Nome, aux.Info);
                }
            }
            if(solucao.BuscaNo(n_destino) != null)
                return MontaCaminho(solucao, n_origem, n_destino);
            return "";
        }

        private string MontaCaminho(Grafo g, string o, string d)
        {
            string[] s = g.MenorCaminho(d, o).Split(',');
            Stack<string> stack = new Stack<string>();
            foreach(string ss in s)
            {
                stack.Push(ss);
            }
            string r = string.Empty;
            while(stack.Count > 0)
            {
                r += stack.Pop() + ",";
            }
            r += ",";
            r = r.Replace(",,", ",");
            return r;
        }

        public string PasseioProfundidade(string raiz)
        {
            Node no = BuscaNo(raiz);
            LimpaVisit();
            if (no == null) return "";

            string resultado = "";
            Stack<Node> P = new Stack<Node>();
            no.Visit = true;
            P.Push(no);

            while (P.Count > 0)
            {
                Node n = P.Pop();
                resultado += n.Nome + ",";
                foreach (Arco a in n.Arcos)
                {
                    if (!a.Destino.Visit)//! = não(não Visit)
                    {
                        a.Destino.Visit = true;
                        P.Push(a.Destino);

                    }
                }
            }
            return resultado;
        }
    }
}
