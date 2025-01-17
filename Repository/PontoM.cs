public class PontosM
{
    private string _codPontoM;
    private decimal _taxaServico;
    private decimal _valorRecisao;
    private decimal _totalDistribuicao;
    private string _mes;
    private int _ano;

    public string CodPontoM
    {
        get { return _codPontoM; }
        set { _codPontoM = value; }
    }

    public decimal TaxaServico
    {
        get { return _taxaServico; }
        set { _taxaServico = value; }
    }

    public decimal ValorRecisao
    {
        get { return _valorRecisao; }
        set { _valorRecisao = value; }
    }

    public decimal TotalDistribuicao
    {
        get { return _totalDistribuicao; }
        set { _totalDistribuicao = value; }
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

    public PontosM() { }

    public PontosM(string codPontoM, decimal taxaServico, decimal valorRecisao, decimal totalDistribuicao, string mes, int ano)
    {
        _codPontoM = codPontoM;
        _taxaServico = taxaServico;
        _valorRecisao = valorRecisao;
        _totalDistribuicao = totalDistribuicao;
        _mes = mes;
        _ano = ano;
    }
}