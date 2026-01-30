using ECI.Test.Shared.DTOs;

namespace ECI.Test.BL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool ValidateLogin(LoginDto loginDto);
    }
}
