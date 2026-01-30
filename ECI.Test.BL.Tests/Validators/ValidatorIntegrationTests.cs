using Microsoft.VisualStudio.TestTools.UnitTesting;
using ECI.Test.BL.Validators;
using ECI.Test.Shared.Models;
using ECI.Test.Shared.DTOs;
using ECI.Test.BL.Tests.TestUtilities;
using System;

namespace ECI.Test.BL.Tests.Validators
{
    [TestClass]
    public class ValidatorIntegrationTests
    {
        [TestClass]
        public class ClientValidatorTests
        {
            private ClientValidator _validator;

            [TestInitialize]
            public void Setup()
            {
                _validator = new ClientValidator();
            }

            [TestMethod]
            public void Validate_WithValidClient_ReturnsValid()
            {
                // Arrange
                var client = new Client { Name = TestHelper.TestData.ValidClientName, Phone = TestHelper.TestData.ValidPhone };

                
                var result = _validator.Validate(client);

                // Assert
                Assert.IsTrue(result.IsValid);
                Assert.AreEqual(0, result.Errors.Count);
            }

            [TestMethod]
            public void Validate_WithEmptyName_ReturnsInvalid()
            {
                // Arrange
                var client = new Client { Name = "", Phone = TestHelper.TestData.ValidPhone };

                
                var result = _validator.Validate(client);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("Client name is required"));
            }

            [TestMethod]
            public void Validate_WithNullName_ReturnsInvalid()
            {
                // Arrange
                var client = new Client { Name = null, Phone = TestHelper.TestData.ValidPhone };

                
                var result = _validator.Validate(client);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("Client name is required"));
            }

            [TestMethod]
            public void Validate_WithTooLongName_ReturnsInvalid()
            {
                // Arrange
                var client = new Client { Name = TestHelper.TestData.TooLongClientName, Phone = TestHelper.TestData.ValidPhone };

                
                var result = _validator.Validate(client);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("cannot exceed 100 characters"));
            }

            [TestMethod]
            public void Validate_WithNullPhone_ReturnsValid()
            {
                // Arrange
                var client = new Client { Name = TestHelper.TestData.ValidClientName, Phone = null };

                
                var result = _validator.Validate(client);

                // Assert
                Assert.IsTrue(result.IsValid);
            }

            [TestMethod]
            public void Validate_WithEmptyPhone_ReturnsValid()
            {
                // Arrange
                var client = new Client { Name = TestHelper.TestData.ValidClientName, Phone = "" };

                
                var result = _validator.Validate(client);

                // Assert
                Assert.IsTrue(result.IsValid);
            }
        }

        [TestClass]
        public class DogValidatorTests
        {
            private DogValidator _validator;

            [TestInitialize]
            public void Setup()
            {
                _validator = new DogValidator();
            }

            [TestMethod]
            public void Validate_WithValidDog_ReturnsValid()
            {
                // Arrange
                var dog = new Dog 
                { 
                    Name = TestHelper.TestData.ValidDogName, 
                    Breed = TestHelper.TestData.ValidBreed, 
                    Age = TestHelper.TestData.ValidAge 
                };

                
                var result = _validator.Validate(dog);

                // Assert
                Assert.IsTrue(result.IsValid);
            }

            [TestMethod]
            public void Validate_WithEmptyName_ReturnsInvalid()
            {
                // Arrange
                var dog = new Dog { Name = "", Breed = TestHelper.TestData.ValidBreed, Age = TestHelper.TestData.ValidAge };

                
                var result = _validator.Validate(dog);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("Dog name is required"));
            }

            [TestMethod]
            public void Validate_WithNegativeAge_ReturnsInvalid()
            {
                // Arrange
                var dog = new Dog { Name = TestHelper.TestData.ValidDogName, Breed = TestHelper.TestData.ValidBreed, Age = -1 };

                
                var result = _validator.Validate(dog);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("positive number"));
            }

            [TestMethod]
            public void Validate_WithZeroAge_ReturnsValid()
            {
                // Arrange
                var dog = new Dog { Name = TestHelper.TestData.ValidDogName, Breed = TestHelper.TestData.ValidBreed, Age = 0 };

                
                var result = _validator.Validate(dog);

                // Assert
                Assert.IsTrue(result.IsValid);
            }

            [TestMethod]
            public void Validate_WithEmptyBreed_ReturnsValid()
            {
                // Arrange
                var dog = new Dog { Name = TestHelper.TestData.ValidDogName, Breed = "", Age = TestHelper.TestData.ValidAge };

                
                var result = _validator.Validate(dog);

                // Assert
                Assert.IsTrue(result.IsValid);
            }
        }

        [TestClass]
        public class WalkValidatorTests
        {
            private WalkValidator _validator;

            [TestInitialize]
            public void Setup()
            {
                _validator = new WalkValidator();
            }

            [TestMethod]
            public void Validate_WithValidWalk_ReturnsValid()
            {
                // Arrange
                var walk = new Walk 
                { 
                    ClientId = TestHelper.TestData.ValidClientId,
                    DogId = TestHelper.TestData.ValidDogId,
                    Date = TestHelper.DateTimeHelper.Today,
                    Duration = TestHelper.TestData.ValidDuration
                };

                
                var result = _validator.Validate(walk);

                // Assert
                Assert.IsTrue(result.IsValid);
            }

            [TestMethod]
            public void Validate_WithZeroClientId_ReturnsInvalid()
            {
                // Arrange
                var walk = new Walk 
                { 
                    ClientId = 0,
                    DogId = TestHelper.TestData.ValidDogId,
                    Date = TestHelper.DateTimeHelper.Today,
                    Duration = TestHelper.TestData.ValidDuration
                };

                
                var result = _validator.Validate(walk);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("Client ID is required"));
            }

            [TestMethod]
            public void Validate_WithZeroDuration_ReturnsInvalid()
            {
                // Arrange
                var walk = new Walk 
                { 
                    ClientId = TestHelper.TestData.ValidClientId,
                    DogId = TestHelper.TestData.ValidDogId,
                    Date = TestHelper.DateTimeHelper.Today,
                    Duration = 0
                };

                
                var result = _validator.Validate(walk);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("at least 1 minute"));
            }
        }

        [TestClass]
        public class LoginDtoValidatorTests
        {
            private LoginDtoValidator _validator;

            [TestInitialize]
            public void Setup()
            {
                _validator = new LoginDtoValidator();
            }

            [TestMethod]
            public void Validate_WithValidLogin_ReturnsValid()
            {
                // Arrange
                var loginDto = new LoginDto 
                { 
                    Username = TestHelper.TestData.ValidUsername, 
                    Password = TestHelper.TestData.ValidPassword 
                };

                
                var result = _validator.Validate(loginDto);

                // Assert
                Assert.IsTrue(result.IsValid);
            }

            [TestMethod]
            public void Validate_WithEmptyUsername_ReturnsInvalid()
            {
                // Arrange
                var loginDto = new LoginDto { Username = "", Password = TestHelper.TestData.ValidPassword };

                
                var result = _validator.Validate(loginDto);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("Username is required"));
            }

            [TestMethod]
            public void Validate_WithEmptyPassword_ReturnsInvalid()
            {
                // Arrange
                var loginDto = new LoginDto { Username = TestHelper.TestData.ValidUsername, Password = "" };

                
                var result = _validator.Validate(loginDto);

                // Assert
                Assert.IsFalse(result.IsValid);
                Assert.IsTrue(result.ToString().Contains("Password is required"));
            }
        }
    }
}