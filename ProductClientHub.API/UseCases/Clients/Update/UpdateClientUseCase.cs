using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Update;

public class UpdateClientUseCase

{
    public void Execute(Guid clientId, RequestClientJson request)
    {
        Validate(request);
        
        var dbContext = new ProductClientHubDbContext();

        // FirstOrDefault returns null if not found instead of throwing an exception (to us handle the not found case)
        var entity = dbContext.Clients.FirstOrDefault(client => client.Id == clientId);

        if (entity is null)
        {
            throw new NotFoundException("Cliente não encontrado.");
        }
        
        entity.Name = request.Name;
        entity.Email = request.Email;
        
        dbContext.Clients.Update(entity);
        
        dbContext.SaveChanges();
    }

    private void Validate(RequestClientJson request)
    {
        var validator = new RequestClientValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}