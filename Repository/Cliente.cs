using API_Loja.Repository;

public class Clientes : Pai
{

    private string _nome;
    private string _documento;
    private string _telefone;
    private string _email;
    private string _cep;
    private string _logradouro;
    private int _numero;
    private string _bairro;
    private string _cidade;
    private string _uf;

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }

    public string Documento
    {
        get { return _documento; }
        set { _documento = value; }
    }

    public string Telefone
    {
        get { return _telefone; }
        set { _telefone = value; }
    }

    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    public string Cep
    {
        get { return _cep; }
        set { _cep = value; }
    }

    public string Logradouro
    {
        get { return _logradouro; }
        set { _logradouro = value; }
    }

    public int Numero
    {
        get { return _numero; }
        set { _numero = value; }
    }

    public string Bairro
    {
        get { return _bairro; }
        set { _bairro = value; }
    }

    public string Cidade
    {
        get { return _cidade; }
        set { _cidade = value; }
    }

    public string UF
    {
        get { return _uf; }
        set { _uf = value; }
    }

    public Clientes() : base()
    {

        _nome = "";
        _documento = "";
        _telefone = "";
        _email = "";
        _cep = "";
        _logradouro = "";
        _numero = 0;
        _bairro = "";
        _cidade = "";
        _uf = "";
    }

    public Clientes(int id, string nome, string documento, string telefone, string email, string cep, string logradouro, int numero, string bairro, string cidade, string uf) : base(id)
    {
        _nome = nome;
        _documento = documento;
        _telefone = telefone;
        _email = email;
        _cep = cep;
        _logradouro = logradouro;
        _numero = numero;
        _bairro = bairro;
        _cidade = cidade;
        _uf = uf;
    }
}