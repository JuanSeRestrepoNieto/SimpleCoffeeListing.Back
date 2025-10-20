namespace SimpleCoffee.Domain.Entities;

public class Coffee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public double? Rating { get; set; }
    public int Votes { get; set; }
    public bool Popular { get; set; }
    public bool Available { get; set; }
}
