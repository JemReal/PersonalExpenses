using Microsoft.AspNetCore.Identity;

namespace PersonalExpenses.API.Repositories
{
    public interface ITokenRepository
    {

        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
