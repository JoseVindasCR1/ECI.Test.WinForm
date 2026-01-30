using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using ECI.Test.BL.Services;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Tests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IUserValidator> _mockUserValidator;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserValidator = new Mock<IUserValidator>();
            _userService = new UserService(_mockUserRepository.Object, _mockUserValidator.Object);
        }


        [TestMethod]
        public void ValidateCredentials_WithValidCredentials_ReturnsTrue()
        {
            var user = new User 
            { 
                Id = 1, 
                UserName = "admin", 
                Password = "hashedPassword", 
                CreateDate = DateTime.Now 
            };
            _mockUserRepository.Setup(x => x.GetByUserName("admin")).Returns(user);
            _mockUserValidator.Setup(x => x.Validate(It.IsAny<User>())).Returns(new Validation.ValidationResult());

            var result = _userService.ValidateCredentials("admin", "password");

            _mockUserRepository.Verify(x => x.GetByUserName("admin"), Times.Once);
        }

        [TestMethod]
        public void ValidateCredentials_WithInvalidUsername_ReturnsFalse()
        {
            _mockUserRepository.Setup(x => x.GetByUserName("nonexistent")).Returns((User)null);
            _mockUserValidator.Setup(x => x.Validate(It.IsAny<User>())).Returns(new Validation.ValidationResult());

            var result = _userService.ValidateCredentials("nonexistent", "password");

            Assert.IsFalse(result);
            _mockUserRepository.Verify(x => x.GetByUserName("nonexistent"), Times.Once);
        }
    }
}