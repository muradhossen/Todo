using Domain.Entities.Users;

namespace Application.ServiceInterfaces.Token;

public interface ITokenService
{
    string CreateToken(User user);
}
