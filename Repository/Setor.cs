using API_Loja.Repository;

public class Setores : Pai
{
    private string _setor;

    public string Setor
    {
        get { return _setor; }
        set { _setor = value; }
    }

    public Setores() : base()
    {
        Id = 0;
        _setor = "";
    }

    public Setores(int id, string setor) : base(id)
    {
        _setor = setor;
    }
}

