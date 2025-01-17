namespace API_Loja.Repository
{
    public class Pai
    {
        private int _id;
        private DateTime _dataCadastro;
        private DateTime _dataUltimaMovimentacao;
        private int _ultimaAlteracao;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int UltimaAlteracao
        {
            get { return _ultimaAlteracao; }
            set { _ultimaAlteracao = value; }
        }
        public DateTime DataCadastro
        {
            get { return _dataCadastro; }
            set { _dataCadastro = value; }
        }
        public DateTime DataUltimaMovimentacao
        {
            get { return _dataUltimaMovimentacao; }
            set { _dataUltimaMovimentacao = value; }
        }
        public Pai()
        {
            _id = 0;
        }

        public Pai(int id)
        {
            _id = id;
        }

        public Pai(int id, DateTime datacad, DateTime dataultimamovimentacao, int alteracao)
        {
            _id = id;
            _dataCadastro = datacad;
            _dataUltimaMovimentacao = dataultimamovimentacao;
            _ultimaAlteracao = alteracao;

        }
    }
}
