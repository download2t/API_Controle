using API_Loja.Repository;

public class Senhas : Pai
{
    private string _login;
    private string _senha;
    private string _descricao;
    private string _link;
    private Categoria aCategoria;
    public Senhas() : base()
    {
        Id = 0;
        Login = "";
        Senha = "";
        Descricao = "";
        Link = "";
        Categoria = new Categoria();
    }
    public Senhas(int id, string login, string senha, string descricao, string link, Categoria categoria) : base(id)
    {
        Id = id;
        Login = login;
        Senha = senha;
        Descricao = descricao;
        Link = link;
        Categoria = categoria;
    }
    public string Login
    {
        get { return _login; }
        set { _login = value; }
    }
    public string Senha
    {
        get { return _senha; }
        set { _senha = value; }
    }
    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    public string Link
    {
        get { return _link; }
        set { _link = value; }
    }

    public Categoria Categoria
    {
        get { return aCategoria; }
        set { aCategoria = value; }
    }


}