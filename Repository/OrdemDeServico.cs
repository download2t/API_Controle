using API_Loja.Repository;

public class OrdemDeServico : Pai
{
    private Usuarios _usuario;
    private string _titulo;
    private string _ticket;
    private string _status;
    private string _descricao;
    private DateTime _dataAbertura;
    private DateTime? _dataFechamento;
    private DateTime? _dataReabertura;
    private string _prioridade;

    public Usuarios Usuario
    {
        get { return _usuario; }
        set { _usuario = value; }
    }

    public string Titulo
    {
        get { return _titulo; }
        set { _titulo = value; }
    }
    public string Ticket
    {
        get { return _ticket; }
        set { _ticket = value; }
    }
    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }

    public DateTime DataAbertura
    {
        get { return _dataAbertura; }
        set { _dataAbertura = value; }
    }

    public DateTime? DataFechamento
    {
        get { return _dataFechamento; }
        set { _dataFechamento = value; }
    }

    public DateTime? DataReabertura
    {
        get { return _dataReabertura; }
        set { _dataReabertura = value; }
    }

    public string Prioridade
    {
        get { return _prioridade; }
        set { _prioridade = value; }
    }

    public OrdemDeServico() : base()
    {
        _usuario = new Usuarios();
        _titulo = "";
        _descricao = "";
        _dataAbertura = DateTime.Now;
        _dataFechamento = null;
        _dataReabertura = null;
        _prioridade = "";
        _ticket = "";
        _status = "";
    }

    public OrdemDeServico(int id, Usuarios usuario, string titulo, string descricao, DateTime dataAbertura, DateTime? dataFechamento, DateTime? dataReabertura, string prioridade, string ticket, string status) : base(id)
    {
        _usuario = usuario;
        _titulo = titulo;
        _descricao = descricao;
        _dataAbertura = dataAbertura;
        _dataFechamento = dataFechamento;
        _dataReabertura = dataReabertura;
        _prioridade = prioridade;
        _ticket = ticket;
        _status = status;
    }
}
