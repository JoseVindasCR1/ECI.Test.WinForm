using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using ECI.Test.BL.Services;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.BL.Validation;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Tests.Services
{
    [TestClass]
    public class DogServiceTests
    {
        private Mock<IDogRepository> _mockDogRepository;
        private Mock<IClientDogRepository> _mockClientDogRepository;
        private Mock<IDogValidator> _mockDogValidator;
        private DogService _dogService;

        [TestInitialize]
        public void Setup()
        {
            _mockDogRepository = new Mock<IDogRepository>();
            _mockClientDogRepository = new Mock<IClientDogRepository>();
            _mockDogValidator = new Mock<IDogValidator>();
            _dogService = new DogService(_mockDogRepository.Object, _mockClientDogRepository.Object, _mockDogValidator.Object);
        }

        [TestMethod]
        public void GetByClientId_WithValidClientId_ReturnsDogs()
        {
            var dogs = new List<Dog>
            {
                new Dog { Id = 1, Name = "Max", Breed = "Golden Retriever", Age = 3 },
                new Dog { Id = 2, Name = "Bella", Breed = "Labrador", Age = 5 }
            };
            _mockClientDogRepository.Setup(x => x.GetDogsByClientId(1)).Returns(dogs);
           
            var result = _dogService.GetByClientId(1);

            Assert.AreEqual(2, ((List<Dog>)result).Count);
            _mockClientDogRepository.Verify(x => x.GetDogsByClientId(1), Times.Once);
        }

        [TestMethod]
        public void Add_WithValidDog_CallsRepositoryAdd()
        {
            var dog = new Dog { Name = "Charlie", Breed = "Beagle", Age = 2 };
            var validationResult = new ValidationResult();
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);
           
            _dogService.Add(dog);

            _mockDogRepository.Verify(x => x.Add(dog), Times.Once);
            _mockDogValidator.Verify(x => x.Validate(dog), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_WithInvalidDog_ThrowsArgumentException()
        {
            var dog = new Dog { Name = "", Breed = "Beagle", Age = 2 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Dog name is required.");
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);
            
            _dogService.Add(dog);
        }

        [TestMethod]
        public void Add_WithNegativeAge_ThrowsArgumentException()
        {
            var dog = new Dog { Name = "Charlie", Breed = "Beagle", Age = -1 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Dog age must be a positive number.");
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);

            Assert.ThrowsException<ArgumentException>(() => _dogService.Add(dog));
        }

        [TestMethod]
        public void Update_WithValidDog_CallsRepositoryUpdate()
        {
            var dog = new Dog { Id = 1, Name = "Max Updated", Breed = "Golden Retriever", Age = 4 };
            var validationResult = new ValidationResult();
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);
            
            _dogService.Update(dog);

            _mockDogRepository.Verify(x => x.Update(dog), Times.Once);
            _mockDogValidator.Verify(x => x.Validate(dog), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_WithInvalidDog_ThrowsArgumentException()
        {
            var dog = new Dog { Id = 1, Name = "", Breed = "Golden Retriever", Age = 4 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Dog name is required.");
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);
           
            _dogService.Update(dog);
        }

        [TestMethod]
        public void Delete_WithValidId_CallsRepositoryDelete()
        {
            var dogId = 1;
            _dogService.Delete(dogId);
            _mockDogRepository.Verify(x => x.Delete(dogId), Times.Once);
        }

        [TestMethod]
        public void AssignToClient_WithValidIds_CallsRepositoryAssign()
        {
            var clientId = 1;
            var dogId = 1;
            
            _dogService.AssignToClient(clientId, dogId);

            _mockClientDogRepository.Verify(x => x.AssignDogToClient(clientId, dogId), Times.Once);
        }

        [TestMethod]
        public void Add_WithEmptyBreed_Succeeds()
        {
            var dog = new Dog { Name = "Dog Without Breed", Breed = "", Age = 2 };
            var validationResult = new ValidationResult();
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);
            
            _dogService.Add(dog);

            _mockDogRepository.Verify(x => x.Add(dog), Times.Once);
        }

        [TestMethod]
        public void Add_WithZeroAge_Succeeds()
        {
            var dog = new Dog { Name = "Puppy", Breed = "Poodle", Age = 0 };
            var validationResult = new ValidationResult();
            
            _mockDogValidator.Setup(x => x.Validate(dog)).Returns(validationResult);

            _dogService.Add(dog);

            _mockDogRepository.Verify(x => x.Add(dog), Times.Once);
        }
    }
}