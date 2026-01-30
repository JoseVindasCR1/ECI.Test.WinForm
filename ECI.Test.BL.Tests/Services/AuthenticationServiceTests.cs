using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ECI.Test.BL.Services;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.BL.Validation;
using ECI.Test.Shared.DTOs;

namespace ECI.Test.BL.Tests.Services
{
    [TestClass]
    public class AuthenticationServiceTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<ILoginDtoValidator> _mockLoginValidator;
        private AuthenticationService _authenticationService;

        [TestInitialize]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockLoginValidator = new Mock<ILoginDtoValidator>();
            _authenticationService = new AuthenticationService(_mockUserService.Object, _mockLoginValidator.Object);
        }

        [TestMethod]
        public void ValidateLogin_WithValidCredentials_ReturnsTrue()
        {
            var loginDto = new LoginDto { Username = "admin", Password = "admin123" };
            var validationResult = new ValidationResult();
            
            _mockLoginValidator.Setup(x => x.Validate(loginDto)).Returns(validationResult);
            _mockUserService.Setup(x => x.ValidateCredentials("admin", "admin123")).Returns(true);

            var result = _authenticationService.ValidateLogin(loginDto);

            Assert.IsTrue(result);
            _mockLoginValidator.Verify(x => x.Validate(loginDto), Times.Once);
            _mockUserService.Verify(x => x.ValidateCredentials("admin", "admin123"), Times.Once);
        }

        [TestMethod]
        public void ValidateLogin_WithInvalidCredentials_ReturnsFalse()
        {
            var loginDto = new LoginDto { Username = "admin", Password = "wrongpassword" };
            var validationResult = new ValidationResult();
            
            _mockLoginValidator.Setup(x => x.Validate(loginDto)).Returns(validationResult);
            _mockUserService.Setup(x => x.ValidateCredentials("admin", "wrongpassword")).Returns(false);
   
            var result = _authenticationService.ValidateLogin(loginDto);

            Assert.IsFalse(result);
            _mockLoginValidator.Verify(x => x.Validate(loginDto), Times.Once);
            _mockUserService.Verify(x => x.ValidateCredentials("admin", "wrongpassword"), Times.Once);
        }

        [TestMethod]
        public void ValidateLogin_WithInvalidValidationResult_ReturnsFalse()
        {
            var loginDto = new LoginDto { Username = "", Password = "" };
            var validationResult = new ValidationResult();
            validationResult.AddError("Username is required.");
            
            _mockLoginValidator.Setup(x => x.Validate(loginDto)).Returns(validationResult);

            var result = _authenticationService.ValidateLogin(loginDto);

            Assert.IsFalse(result);
            _mockLoginValidator.Verify(x => x.Validate(loginDto), Times.Once);
            _mockUserService.Verify(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ValidateLogin_WithNullUsername_ReturnsFalse()
        {
            var loginDto = new LoginDto { Username = null, Password = "password" };
            var validationResult = new ValidationResult();
            validationResult.AddError("Username is required.");
            
            _mockLoginValidator.Setup(x => x.Validate(loginDto)).Returns(validationResult);
     
            var result = _authenticationService.ValidateLogin(loginDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateLogin_WithEmptyPassword_ReturnsFalse()
        {
            var loginDto = new LoginDto { Username = "admin", Password = "" };
            var validationResult = new ValidationResult();
            validationResult.AddError("Password is required.");
            
            _mockLoginValidator.Setup(x => x.Validate(loginDto)).Returns(validationResult);

            var result = _authenticationService.ValidateLogin(loginDto);

            Assert.IsFalse(result);
        }
    }
}