using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.Shared.DTOs;

namespace ECI.Test.BL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILoginDtoValidator _loginValidator;
        private readonly IUserService _userService;

        public AuthenticationService(IUserService userService, ILoginDtoValidator loginValidator)
        {
            _loginValidator = loginValidator;
            _userService = userService;
        }

        public bool ValidateLogin(LoginDto loginDto)
        {
            var validationResult = _loginValidator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                return false;
            }

            return _userService.ValidateCredentials(loginDto.Username, loginDto.Password);
        }
    }
}
