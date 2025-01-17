
using API_Loja.Repository;

public class Categoria : Pai
{
    private string _nome;
    private string _descricao;
    private bool _senha;

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }
    public bool Senha
    {
        get { return _senha; }
        set { _senha = value; }
    }
    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }

    public Categoria() : base()
    {
        Id = 0;
        _nome = "";
        _descricao = "";
        _senha = false;
    }

    public Categoria(int id, string nome, string descricao, bool senha) : base(id)
    {
        Id = id;
        _nome = nome;
        _descricao = descricao;
        _senha = senha;
    }
}