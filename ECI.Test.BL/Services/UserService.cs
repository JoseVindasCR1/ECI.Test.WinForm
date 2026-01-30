using System;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;
using ECI.Test.Shared.Utilities;

namespace ECI.Test.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;

        public UserService(IUserRepository userRepository, IUserValidator userValidator)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
        }

        public bool ValidateCredentials(string userName, string password)
        {
            var validationResult = _userValidator.Validate(new User()
            {
                UserName = userName,
                Password = password
            });

            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            var user = _userRepository.GetByUserName(userName);
            if (user == null)
                return false;

            return PasswordHasher.VerifyPassword(password, user.Password);
        }
    }
}