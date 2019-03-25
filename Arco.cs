using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafoEDA
{
    public class Arco
    {
        #region Atributos e Propriedades
        private Node origem, destino;
        private object info;

        public Node Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        public Node Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        public object Info
        {
            get { return info; }
            set { info = value; }
        }
        #endregion

        #region Construtores
        public Arco(Node origem, Node destino, object info)
        {
            this.origem = origem;
            this.destino = destino;
            this.info = info;
        }
        public Arco(Node origem, Node destino)
            : this(origem, destino, null)
        { }

        public Arco()
            : this(null, null, null)
        { }

        #endregion
    }
}
    