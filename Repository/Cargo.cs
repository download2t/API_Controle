
    public class Cargo
    {
        private int _id;
        private string _funcao;
        private int _pontos;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Funcao
        {
            get { return _funcao; }
            set { _funcao = value; }
        }

        public int Pontos
        {
            get { return _pontos; }
            set { _pontos = value; }
        }

        public Cargo()
        {
            _id = 0;
            _funcao = "";
            _pontos = 0;
        }

        public Cargo(int id, string funcao, int pontos)
        {
            _id = id;
            _funcao = funcao;
            _pontos = pontos;
        }
    }
