

using API_Loja.Repository;

public class Funcionario : Pai
{

    private string _nome;
    private string _cpf;
    private Setores _setor;
    private Cargo _cargo;
    private decimal? _salBruto;
    private char _ativo;
    private string _telefone;

    public Funcionario() : base()
    {

        _nome = "";
        _cpf = "";
        _setor = new Setores();
        _cargo = new Cargo();
        _salBruto = null;
        _ativo = ' ';
        _telefone = "";
    }

    public Funcionario(int id, string nome, string cpf, Setores setor, Cargo cargo, decimal? salBruto, string telefone, char ativo) : base(id)
    {
        _nome = nome;
        _cpf = cpf;
        _setor = setor;
        _cargo = cargo;
        _salBruto = salBruto;
        _telefone = telefone;
        _ativo = ativo;
    }


    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }
    public string Telefone
    {
        get { return _telefone; }
        set { _telefone = value; }
    }

    public string Cpf
    {
        get { return _cpf; }
        set { _cpf = value; }
    }

    public Setores Setor
    {
        get { return _setor; }
        set { _setor = value; }
    }

    public Cargo Cargo
    {
        get { return _cargo; }
        set { _cargo = value; }
    }

    public decimal? SalBruto
    {
        get { return _salBruto; }
        set { _salBruto = value; }
    }

    public char Ativo
    {
        get { return _ativo; }
        set { _ativo = value; }
    }


}