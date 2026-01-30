using System;
using System.Collections.Generic;
using ECI.Test.BL.Services.Interfaces;
using ECI.Test.BL.Validators.Interfaces;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Services
{
    public class DogService : IDogService
    {
        private readonly IDogRepository _dogRepository;
        private readonly IClientDogRepository _clientDogRepository;
        private readonly IDogValidator _dogValidator;

        public DogService(IDogRepository dogRepository, IClientDogRepository clientDogRepository, IDogValidator dogValidator)
        {
            _dogRepository = dogRepository;
            _clientDogRepository = clientDogRepository;
            _dogValidator = dogValidator;
        }

        public IEnumerable<Dog> GetByClientId(int clientId)
        {
            return _clientDogRepository.GetDogsByClientId(clientId);
        }

        public void Add(Dog dog)
        {
            var validationResult = _dogValidator.Validate(dog);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            _dogRepository.Add(dog);
        }

        public void Update(Dog dog)
        {
            var validationResult = _dogValidator.Validate(dog);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.ToString());
            }

            _dogRepository.Update(dog);
        }

        public void Delete(int id)
        {
            _dogRepository.Delete(id);
        }

        public void AssignToClient(int clientId, int dogId)
        {
            _clientDogRepository.AssignDogToClient(clientId, dogId);
        }
    }
}
