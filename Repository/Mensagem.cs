
using API_Loja.Repository;

public class Mensagens : Pai
{
    public Contatos oContato;
    public DateTime? _dataEnvio;
    public string _mensagem;
    public char _status;
    public string _telefone;

    public Mensagens()
    {
        Id = 0;
        oContato = new Contatos();
        _dataEnvio = null;
        _mensagem = "";
        _status = ' ';
        _telefone = "";
    }
    public Mensagens(int id, Contatos oContato, DateTime? dataEnvio, string mensagem, char status, string telefone) : base(id)
    {
        Id = id;
        this.oContato = oContato;
        _dataEnvio = dataEnvio;
        _mensagem = mensagem;
        _status = status;
        _telefone = telefone;
    }
    public Contatos OContato
    {
        get => oContato;
        set => oContato = value;
    }
    public DateTime? DataEnvio
    {
        get => _dataEnvio;
        set => _dataEnvio = value;
    }
    public string Mensagem
    {
        get => _mensagem;
        set => _mensagem = value;
    }
    public char Status
    {
        get => _status;
        set => _status = value;
    }
    public string Telefone
    {
        get => _telefone;
        set => _telefone = value;
    }
}