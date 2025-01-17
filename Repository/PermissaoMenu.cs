using API_Loja.Repository;
public class PermissaoMenu : Pai
{

    public Usuarios usuario;
    public Opcoes opcao;
    public bool _podeAdicionar;
    public bool _podeAlterar;
    public bool _podeExcluir;
    public bool _podeConsultar;

    public Usuarios Usuario
    {
        get { return usuario; }
        set { usuario = value; }
    }
    public Opcoes Opcao
    {
        get { return opcao; }
        set { opcao = value; }
    }
    public bool PodeAdicionar
    {
        get { return _podeAdicionar; }
        set { _podeAdicionar = value; }
    }
    public bool PodeAlterar
    {
        get { return _podeAlterar; }
        set { _podeAlterar = value; }
    }
    public bool PodeExcluir
    {
        get { return _podeExcluir; }
        set { _podeExcluir = value; }
    }
    public bool PodeConsultar
    {
        get { return _podeConsultar; }
        set { _podeConsultar = value; }
    }


    public PermissaoMenu() : base()
    {
        usuario = new Usuarios();
        opcao = new Opcoes();
        _podeAdicionar = false;
        _podeAlterar = false;
        _podeExcluir = false;
        _podeConsultar = false;
    }
    public PermissaoMenu(Usuarios ousuario, Opcoes aopcao, bool adicionar, bool alterar, bool excluir, bool consultar)
    {
        usuario = ousuario;
        opcao = aopcao;
        _podeAdicionar = adicionar;
        _podeAlterar = alterar;
        _podeConsultar = consultar;
        _podeExcluir = excluir;

    }
}