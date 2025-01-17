

using API_Loja.Repository;

public class Fotos : Pai
{
    private OrdemDeServico _ordemDeServico;
    private byte[] _foto;
    private string _descricao;
    public Fotos() : base()
    {
        Os = new OrdemDeServico();
        Foto = null; //default nulo
        Descricao = "";
    }
    public Fotos(int id, OrdemDeServico ordemDeServico, string descricao) : base(id)
    {
        id = Id;
        Os = ordemDeServico;
        Descricao = descricao;
    }
    public OrdemDeServico Os
    {
        get { return _ordemDeServico; }
        set { _ordemDeServico = value; }
    }
    public byte[] Foto
    {
        get { return _foto; }
        set { _foto = value; }
    }
    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
}