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
    public class WalkServiceTests
    {
        private Mock<IWalkRepository> _mockWalkRepository;
        private Mock<IWalkValidator> _mockWalkValidator;
        private WalkService _walkService;

        [TestInitialize]
        public void Setup()
        {
            _mockWalkRepository = new Mock<IWalkRepository>();
            _mockWalkValidator = new Mock<IWalkValidator>();
            _walkService = new WalkService(_mockWalkRepository.Object, _mockWalkValidator.Object);
        }

        [TestMethod]
        public void GetByClientIdDogId_WithValidIds_ReturnsWalks()
        {
            var walks = new List<Walk>
            {
                new Walk { Id = 1, ClientId = 1, DogId = 1, Date = DateTime.Now, Duration = 30 },
                new Walk { Id = 2, ClientId = 1, DogId = 1, Date = DateTime.Now.AddDays(-1), Duration = 45 }
            };
            _mockWalkRepository.Setup(x => x.GetByClientIdDogId(1, 1)).Returns(walks);
            
            var result = _walkService.GetByClientIdDogId(1, 1);

            Assert.AreEqual(2, ((List<Walk>)result).Count);
            _mockWalkRepository.Verify(x => x.GetByClientIdDogId(1, 1), Times.Once);
        }

        [TestMethod]
        public void Add_WithValidWalk_CallsRepositoryAdd()
        {
            var walk = new Walk { ClientId = 1, DogId = 1, Date = DateTime.Now, Duration = 30 };
            var validationResult = new ValidationResult();
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
     
            _walkService.Add(walk);

            _mockWalkRepository.Verify(x => x.Add(walk), Times.Once);
            _mockWalkValidator.Verify(x => x.Validate(walk), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_WithInvalidWalk_ThrowsArgumentException()
        {
            var walk = new Walk { ClientId = 0, DogId = 1, Date = DateTime.Now, Duration = 30 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Client ID is required.");
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
          
            _walkService.Add(walk);
        }

        [TestMethod]
        public void Add_WithZeroDuration_ThrowsArgumentException()
        {
            var walk = new Walk { ClientId = 1, DogId = 1, Date = DateTime.Now, Duration = 0 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Walk duration must be at least 1 minute.");
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);

            Assert.ThrowsException<ArgumentException>(() => _walkService.Add(walk));
        }

        [TestMethod]
        public void Add_WithNegativeDuration_ThrowsArgumentException()
        {
            var walk = new Walk { ClientId = 1, DogId = 1, Date = DateTime.Now, Duration = -10 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Walk duration must be at least 1 minute.");
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);

            Assert.ThrowsException<ArgumentException>(() => _walkService.Add(walk));
        }

        [TestMethod]
        public void Update_WithValidWalk_CallsRepositoryUpdate()
        {
            var walk = new Walk { Id = 1, ClientId = 1, DogId = 1, Date = DateTime.Now, Duration = 45 };
            var validationResult = new ValidationResult();
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
       
            _walkService.Update(walk);

            _mockWalkRepository.Verify(x => x.Update(walk), Times.Once);
            _mockWalkValidator.Verify(x => x.Validate(walk), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_WithInvalidWalk_ThrowsArgumentException()
        {
            var walk = new Walk { Id = 1, ClientId = 0, DogId = 1, Date = DateTime.Now, Duration = 45 };
            var validationResult = new ValidationResult();
            validationResult.AddError("Client ID is required.");
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
          
            _walkService.Update(walk);
        }

        [TestMethod]
        public void Delete_WithValidId_CallsRepositoryDelete()
        {
            var walkId = 1;
            _walkService.Delete(walkId);
            _mockWalkRepository.Verify(x => x.Delete(walkId), Times.Once);
        }

        [TestMethod]
        public void Add_WithFutureDate_Succeeds()
        {
            var walk = new Walk { ClientId = 1, DogId = 1, Date = DateTime.Now.AddDays(1), Duration = 30 };
            var validationResult = new ValidationResult();
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
    
            _walkService.Add(walk);

            _mockWalkRepository.Verify(x => x.Add(walk), Times.Once);
        }

        [TestMethod]
        public void Add_WithPastDate_Succeeds()
        {
            var walk = new Walk { ClientId = 1, DogId = 1, Date = DateTime.Now.AddDays(-1), Duration = 30 };
            var validationResult = new ValidationResult();
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
        
            _walkService.Add(walk);

            _mockWalkRepository.Verify(x => x.Add(walk), Times.Once);
        }

        [TestMethod]
        public void Add_WithLongDuration_Succeeds()
        {
            var walk = new Walk { ClientId = 1, DogId = 1, Date = DateTime.Now, Duration = 120 }; // 2 hours
            var validationResult = new ValidationResult();
            
            _mockWalkValidator.Setup(x => x.Validate(walk)).Returns(validationResult);
     
            _walkService.Add(walk);

            _mockWalkRepository.Verify(x => x.Add(walk), Times.Once);
        }
    }
}