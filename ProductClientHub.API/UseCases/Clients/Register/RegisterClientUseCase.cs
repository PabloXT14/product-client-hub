using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Register;

public class RegisterClientUseCase
{
    public ResponseShortClientJson Execute(RequestClientJson request)
    {
        Validate(request);
        
        // Add client to database
        var dbContext = new ProductClientHubDbContext();

        var entity = new Client
        {
            Name = request.Name,
            Email = request.Email
        };
        
        dbContext.Clients.Add(entity); // Prepare query to add the client to the database
        
        dbContext.SaveChanges(); // Save changes to database
        
        return new ResponseShortClientJson
        {
            Id = entity.Id,
            Name =  entity.Name,
        };
    }
    
    private void Validate(RequestClientJson request)
    {
        var validator = new RegisterClientValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errors);
        }
    }
}