using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoEDA
{
    public class Node
    {
        #region Atributos e Propriesdades

        private List<Arco> arcos;
        private object info;
        private bool visit;
        private string nome, caminho;
        private int nivel;

        public int Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }

        public string Caminho
        {
            get { return caminho; }
            set { caminho = value; }
        }

        public List<Arco> Arcos
        {
            get { return arcos; }
        }

        public object Info
        {
            get { return info; }
            set { info = value; }
        }

        public bool Visit
        {
            get { return visit; }
            set { visit = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        #endregion

        #region Construtores
        public Node(string nome, object info)
        {
            arcos = new List<Arco>();
            this.nome = nome;
            this.info = info;
            this.visit = false;
            caminho = string.Empty;
            nivel = 0;
        }

        public Node(string nome) : this(nome, null) { }

        #endregion

        #region Metodos Publicos
        public void AdicionarArco(Arco a)
        {
            arcos.Add(a);
        }
        #endregion
    }
}
