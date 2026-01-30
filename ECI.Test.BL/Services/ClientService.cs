using System;
using System.Collections.Generic;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientValidator _clientValidator;

        public ClientService(IClientRepository clientRepository, IClientValidator clientValidator)
        {
            _clientRepository = clientRepository;
            _clientValidator = clientValidator;
        }

        public IEnumerable<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        public Client GetById(int id)
        {
            return _clientRepository.GetById(id);
        }

        public void Add(Client client)
        {
            var validationResult = _clientValidator.Validate(client);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            _clientRepository.Add(client);
        }

        public void Update(Client client)
        {
            var validationResult = _clientValidator.Validate(client);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            _clientRepository.Update(client);
        }

        public void Delete(int id)
        {
            _clientRepository.Delete(id);
        }

        public string Validate(Client client)
        {
            var validationResult = _clientValidator.Validate(client);
            return validationResult.IsValid ? null : validationResult.ToString();
        }
    }
}
