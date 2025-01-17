
public class Cofres
{
    public int CodCofres { get; set; }
    public int CodChave { get; set; }
    public int Quarto { get; set; }

    public Cofres()
    {
        CodCofres = 0;
        CodChave = 0;
        Quarto = 0;
    }

    public Cofres(int codCofres, int codChave, int quarto)
    {
        CodCofres = codCofres;
        CodChave = codChave;
        Quarto = quarto;
    }
}

