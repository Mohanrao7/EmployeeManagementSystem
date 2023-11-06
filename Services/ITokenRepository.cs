using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementApi.Services
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
