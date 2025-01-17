

using API_Loja.Repository;

public class Patrimonios : Pai
{
    private string _patrimonio;
    private string _descricao;
    private decimal _valor;
    private Setores oSetor;
    private Subcategoria aSubcategoria;
    private byte[] _foto;
    private string _caminhoFoto;
    private string _baixa;
    private Categoria aCategoria; // Exclusivo para ID no Relatório.

    public Patrimonios() : base()
    {
        Patrimonio = "";
        Descricao = "";
        Valor = 0;
        Setor = new Setores();
        Subcategoria = new Subcategoria();
        CaminhoFoto = "";
        Foto = null; //default nulo
        Baixa = " ";
        aCategoria = new Categoria();
    }

    public Patrimonios(int id, string patrimonio, string descricao, decimal valor, Setores setor, Subcategoria subcategoria, byte[] foto, string caminhoFoto, string baixa) : base(id)
    {
        Patrimonio = patrimonio;
        Descricao = descricao;
        Valor = valor;
        Setor = setor;
        Subcategoria = subcategoria;
        Foto = foto;
        CaminhoFoto = caminhoFoto;
        Baixa = baixa;
    }
    public Patrimonios(int id, string patrimonio, string descricao, decimal valor, Setores setor, Subcategoria subcategoria, byte[] foto, string caminhoFoto, string baixa, Categoria categoria) : base(id)
    {
        Patrimonio = patrimonio;
        Descricao = descricao;
        Valor = valor;
        Setor = setor;
        Subcategoria = subcategoria;
        Foto = foto;
        CaminhoFoto = caminhoFoto;
        Baixa = baixa;
        aCategoria = categoria;
    }

    public string Patrimonio
    {
        get { return _patrimonio; }
        set { _patrimonio = value; }
    }

    public string Baixa
    {
        get { return _baixa; }
        set { _baixa = value; }
    }

    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }

    public decimal Valor
    {
        get { return _valor; }
        set { _valor = value; }
    }

    public Setores Setor
    {
        get { return oSetor; }
        set { oSetor = value; }
    }

    public Subcategoria Subcategoria
    {
        get { return aSubcategoria; }
        set { aSubcategoria = value; }
    }
    public Categoria Categoria
    {
        get { return aCategoria; }
        set { aCategoria = value; }
    }

    public byte[] Foto
    {
        get { return _foto; }
        set { _foto = value; }
    }

    public string CaminhoFoto
    {
        get { return _caminhoFoto; }
        set { _caminhoFoto = value; }
    }
}