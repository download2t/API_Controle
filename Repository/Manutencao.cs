using API_Loja.Repository;

public class Manutencao : Pai
{
    private Patrimonios _patrimonio;
    private DateTime? _dataReparo;
    private decimal _valorConserto;
    private string _descricao;
    private string _profissional;
    private string _telefone;
    private Patrimonios _idPatrimonio;// exclusivo para ID no relatório.

    public Patrimonios Patrimonio
    {
        get { return _patrimonio; }
        set { _patrimonio = value; }
    }
    public Patrimonios IdPatrimonio
    {
        get { return _idPatrimonio; }
        set { _idPatrimonio = value; }
    }

    public DateTime? DataReparo
    {
        get { return _dataReparo; }
        set { _dataReparo = value; }
    }

    public decimal ValorConserto
    {
        get { return _valorConserto; }
        set { _valorConserto = value; }
    }

    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }

    public string Profissional
    {
        get { return _profissional; }
        set { _profissional = value; }
    }

    public string Telefone
    {
        get { return _telefone; }
        set { _telefone = value; }
    }

    public Manutencao() : base()
    {
        _patrimonio = new Patrimonios();
        _dataReparo = null;
        _valorConserto = 0;
        _descricao = "";
        _profissional = "";
        _telefone = "";
        _idPatrimonio = new Patrimonios();
    }

    public Manutencao(int id, Patrimonios patrimonio, DateTime? dataReparo, decimal valorConserto, string descricao, string profissional, string telefone) : base(id)
    {
        _patrimonio = patrimonio;
        _dataReparo = dataReparo;
        _valorConserto = valorConserto;
        _descricao = descricao;
        _profissional = profissional;
        _telefone = telefone;
    }
    public Manutencao(int id, Patrimonios patrimonio, DateTime? dataReparo, decimal valorConserto, string descricao, string profissional, string telefone, Patrimonios idPatrimonio) : base(id)
    {
        Id = id;
        _patrimonio = patrimonio;
        _dataReparo = dataReparo;
        _valorConserto = valorConserto;
        _descricao = descricao;
        _profissional = profissional;
        _telefone = telefone;
        _idPatrimonio = idPatrimonio;
    }
}