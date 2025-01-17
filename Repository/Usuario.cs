using API_Loja.Repository;

public class Usuarios : Pai
{
    private string _nome;
    private string _sobrenome;
    private string _email;
    private string _usuario;
    private DateTime? _dataCadastro;
    private DateTime? _dataNascimento;
    private string _senha;
    private string _status;
    private string _perfil;
    private List<PermissaoMenu> _permissoes;

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public List<PermissaoMenu> Permissoes
    {
        get { return _permissoes; }
        set { _permissoes = value; }
    }

    public string Sobrenome
    {
        get { return _sobrenome; }
        set { _sobrenome = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Usuario
    {
        get { return _usuario; }
        set { _usuario = value; }
    }

    public DateTime? DataCadastro
    {
        get { return _dataCadastro; }
        set { _dataCadastro = value; }
    }

    public DateTime? DataNascimento
    {
        get { return _dataNascimento; }
        set { _dataNascimento = value; }
    }

    public string Senha
    {
        get { return _senha; }
        set { _senha = value; }
    }

    public string Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public string Perfil
    {
        get { return _perfil; }
        set { _perfil = value; }
    }

    public Usuarios() : base()
    {
        _nome = "";
        _sobrenome = "";
        _email = "";
        _usuario = "";
        _dataCadastro = null;
        _dataNascimento = null;
        _senha = "";
        _status = "";
        _perfil = "";
        _permissoes = new List<PermissaoMenu>();
    }

    public Usuarios(int id, string nome, string sobrenome, string email, string usuario, DateTime datacad, DateTime datanasc, string senha, string status, string perfil)
        : base(id)
    {
        _nome = nome;
        _sobrenome = sobrenome;
        _email = email;
        _usuario = usuario;
        _dataCadastro = datacad;
        _dataNascimento = datanasc;
        _senha = senha;
        _status = status;
        _perfil = perfil;
        _permissoes = new List<PermissaoMenu>();
    }
}