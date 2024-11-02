using Coffee_Break.Domain.Produto;

namespace API_Coffee.Domain.Produto;

public class Product : Entity
{
    public string name { get; set; }
    public Category category { get; set; }
    public string descricao { get; set; }
    public bool hasStock { get; set; }
    public bool active { get; set; } = true;
}