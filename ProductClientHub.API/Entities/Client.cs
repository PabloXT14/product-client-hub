namespace ProductClientHub.API.Entities;

public class Client : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    // EF entende automaticamente a relação entre as tabelas de Clients e Products, ou seja, os produtos com o clientId do client em questão
    public List<Product> Products { get; set; } = [];
}