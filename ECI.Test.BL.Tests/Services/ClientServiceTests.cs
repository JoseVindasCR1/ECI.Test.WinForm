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
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _mockClientRepository;
        private Mock<IClientValidator> _mockClientValidator;
        private ClientService _clientService;

        [TestInitialize]
        public void Setup()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockClientValidator = new Mock<IClientValidator>();
            _clientService = new ClientService(_mockClientRepository.Object, _mockClientValidator.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsAllClients()
        {
            var clients = new List<Client>
            {
                new Client { Id = 1, Name = "John Smith", Phone = "555-0101" },
                new Client { Id = 2, Name = "Jane Doe", Phone = "555-0102" }
            };
            _mockClientRepository.Setup(x => x.GetAll()).Returns(clients);
     
            var result = _clientService.GetAll();

            Assert.AreEqual(2, ((List<Client>)result).Count);
            _mockClientRepository.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetById_WithValidId_ReturnsClient()
        {
            var client = new Client { Id = 1, Name = "John Smith", Phone = "555-0101" };
            _mockClientRepository.Setup(x => x.GetById(1)).Returns(client);

            var result = _clientService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John Smith", result.Name);
            Assert.AreEqual("555-0101", result.Phone);
            _mockClientRepository.Verify(x => x.GetById(1), Times.Once);
        }

        [TestMethod]
        public void Add_WithValidClient_CallsRepositoryAdd()
        {
            var client = new Client { Name = "New Client", Phone = "555-0103" };
            var validationResult = new ValidationResult();
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);
            
            _clientService.Add(client);

            _mockClientRepository.Verify(x => x.Add(client), Times.Once);
            _mockClientValidator.Verify(x => x.Validate(client), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_WithInvalidClient_ThrowsArgumentException()
        {
            var client = new Client { Name = "", Phone = "555-0103" };
            var validationResult = new ValidationResult();
            validationResult.AddError("Client name is required.");
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);
          
            _clientService.Add(client);
        }

        [TestMethod]
        public void Add_WithClientNameTooLong_ThrowsArgumentException()
        {
            var client = new Client { Name = new string('A', 150), Phone = "555-0103" }; // 150 characters
            var validationResult = new ValidationResult();
            validationResult.AddError("Client name cannot exceed 100 characters.");
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);

            Assert.ThrowsException<ArgumentException>(() => _clientService.Add(client));
        }

        [TestMethod]
        public void Update_WithValidClient_CallsRepositoryUpdate()
        {
            var client = new Client { Id = 1, Name = "Updated Client", Phone = "555-0104" };
            var validationResult = new ValidationResult();
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);
          
            _clientService.Update(client);

            _mockClientRepository.Verify(x => x.Update(client), Times.Once);
            _mockClientValidator.Verify(x => x.Validate(client), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Update_WithInvalidClient_ThrowsArgumentException()
        {
            var client = new Client { Id = 1, Name = "", Phone = "555-0104" };
            var validationResult = new ValidationResult();
            validationResult.AddError("Client name is required.");
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);            
            _clientService.Update(client);
        }

        [TestMethod]
        public void Delete_WithValidId_CallsRepositoryDelete()
        {
            var clientId = 1;         
            _clientService.Delete(clientId);
            _mockClientRepository.Verify(x => x.Delete(clientId), Times.Once);
        }

        [TestMethod]
        public void Validate_WithValidClient_ReturnsNull()
        {
            var client = new Client { Name = "Valid Client", Phone = "555-0105" };
            var validationResult = new ValidationResult();
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);

            var result = _clientService.Validate(client);

            Assert.IsNull(result);
            _mockClientValidator.Verify(x => x.Validate(client), Times.Once);
        }

        [TestMethod]
        public void Validate_WithInvalidClient_ReturnsErrorMessage()
        {
            var client = new Client { Name = "", Phone = "555-0105" };
            var validationResult = new ValidationResult();
            validationResult.AddError("Client name is required.");
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);
  
            var result = _clientService.Validate(client);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("Client name is required."));
            _mockClientValidator.Verify(x => x.Validate(client), Times.Once);
        }

        [TestMethod]
        public void Add_WithValidClientNameAndEmptyPhone_Succeeds()
        {
            var client = new Client { Name = "Client Without Phone", Phone = "" };
            var validationResult = new ValidationResult();
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);
            _clientService.Add(client);

            _mockClientRepository.Verify(x => x.Add(client), Times.Once);
        }

        [TestMethod]
        public void Add_WithValidClientNameAndNullPhone_Succeeds()
        {
            var client = new Client { Name = "Client Without Phone", Phone = null };
            var validationResult = new ValidationResult();
            
            _mockClientValidator.Setup(x => x.Validate(client)).Returns(validationResult);

            _clientService.Add(client);

            _mockClientRepository.Verify(x => x.Add(client), Times.Once);
        }
    }
}