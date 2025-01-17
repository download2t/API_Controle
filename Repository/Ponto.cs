public class Pontos
{
    private string _codPontoM;
    private Funcionario _funcionario;
    private decimal _salarioBase;
    private decimal _vrPontos;
    private int _nPontos;
    private decimal _valorPontoTotal;
    private decimal _salarioTotal;
    private string _mes;
    private int _ano;

    public string CodPontoM
    {
        get { return _codPontoM; }
        set { _codPontoM = value; }
    }

    public Funcionario Funcionarios
    {
        get { return _funcionario; }
        set { _funcionario = value; }
    }

    public decimal SalarioBase
    {
        get { return _salarioBase; }
        set { _salarioBase = value; }
    }

    public decimal VrPontos
    {
        get { return _vrPontos; }
        set { _vrPontos = value; }
    }

    public int NPontos
    {
        get { return _nPontos; }
        set { _nPontos = value; }
    }

    public decimal ValorPontoTotal
    {
        get { return _valorPontoTotal; }
        set { _valorPontoTotal = value; }
    }

    public decimal SalarioTotal
    {
        get { return _salarioTotal; }
        set { _salarioTotal = value; }
    }

    public string Mes
    {
        get { return _mes; }
        set { _mes = value; }
    }

    public int Ano
    {
        get { return _ano; }
        set { _ano = value; }
    }

    public Pontos()
    {
        _codPontoM = "";
        _funcionario = new Funcionario();
        _salarioBase = 0;
        _vrPontos = 0;
        _nPontos = 0;
        _valorPontoTotal = 0;
        _salarioTotal = 0;
        _mes = null;
        _ano = 0;
    }

    public Pontos(string codPontoM, Funcionario codFuncionarios, decimal salarioBase, decimal vrPontos, int nPontos, decimal valorPontoTotal, decimal salarioTotal, string mes, int ano)
    {
        _codPontoM = codPontoM;
        _funcionario = codFuncionarios;
        _salarioBase = salarioBase;
        _vrPontos = vrPontos;
        _nPontos = nPontos;
        _valorPontoTotal = valorPontoTotal;
        _salarioTotal = salarioTotal;
        _mes = mes;
        _ano = ano;
    }
}