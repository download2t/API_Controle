using API_Loja.Repository;

public class Subcategoria : Pai
{
    private string _nome;
    private string _descricao;
    private Categoria _categoria;

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }

    public Categoria Categoria
    {
        get { return _categoria; }
        set { _categoria = value; }
    }

    public Subcategoria() : base()
    {
        _nome = "";
        _descricao = "";
        _categoria = new Categoria();
    }

    public Subcategoria(int id, string nome, string descricao, Categoria categoria) : base(id)
    {
        _nome = nome;
        _descricao = descricao;
        _categoria = categoria;
    }
}