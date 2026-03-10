using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.UseCases.Clients.Register;

public class RegisterClientUseCase
{
    public ResponseClientJson Execute(RequestClientJson request)
    {
        var validator = new RegisterClientValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            throw new DivideByZeroException("Erro nos dados recebidos.");
        }
        
        return new ResponseClientJson();
    }
}