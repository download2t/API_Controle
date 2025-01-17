using System.ComponentModel.DataAnnotations;

public class MenuOpcao
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public byte Nivel { get; set; }
}
