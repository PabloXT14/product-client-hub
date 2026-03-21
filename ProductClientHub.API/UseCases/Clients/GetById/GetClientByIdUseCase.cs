using Microsoft.EntityFrameworkCore;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.GetById;

public class GetClientByIdUseCase
{
    public ResponseClientJson Execute(Guid id)
    {
        var dbContext = new ProductClientHubDbContext();

        var entity = dbContext
            .Clients
            .Include(client => client.Products) // Inclui os produtos do cliente (pois o EF não faz o JOIN automaticamente)
            .FirstOrDefault(client => client.Id == id);

        if (entity is null)
        {
            throw new NotFoundException("Cliente não encontrado.");
        }
        
        return new ResponseClientJson
        {
            Id = entity.Id,
            Name = entity.Name,
            Email =  entity.Email,
            Products = entity.Products.Select(product => new ResponseShortProductJson
            {
                Id = product.Id,
                Name = product.Name
            }).ToList()
        };
    }
}