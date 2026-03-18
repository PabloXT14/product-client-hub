using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.GetAll;

public class GetAllClientsUseCase
{
    public ResponseAllClientsJson Execute()
    {
        var dbContext = new ProductClientHubDbContext();
        
        var clients = dbContext.Clients.ToList();

        var clientsFormatted = clients.Select(client => new ResponseShortClientJson
        {
            Id = client.Id,
            Name = client.Name
        }).ToList();

        return new ResponseAllClientsJson
        {
            Clients = clientsFormatted
        };
    }
}